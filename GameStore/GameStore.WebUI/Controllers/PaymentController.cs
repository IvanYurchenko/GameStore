using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Payment;
using GameStore.Core;
using GameStore.Resources;
using GameStore.WebUI.PaymentWcfService;
using GameStore.WebUI.ViewModels.Payment;
using GameStore.WebUI.ViewModels.Payment.Info;

namespace GameStore.WebUI.Controllers
{
    public class PaymentController : BaseController
    {
        private const string GameStoreName = "GameStore";

        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;

        public PaymentController(
            IPaymentService paymentService,
            IOrderService orderService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
        }

        #region Helpers

        private static PaymentViewModel GetEmptyPaymentViewModel(PaymentType paymentType)
        {
            var paymentViewModel = new PaymentViewModel
            {
                PaymentType = paymentType,
                PaymentInfoViewModel = new PaymentInfoViewModel
                {
                    BankInfo = new BankInfoViewModel(),
                    TerminalInfo = new TerminalInfoViewModel(),
                    VisaInfo = new VisaInfoViewModel(),
                }
            };

            return paymentViewModel;
        }

        private ActionResult GetBankFileResult(PaymentModel paymentModel)
        {
            var sb = new StringBuilder();

            sb.Append(String.Format("Client full name: {0}.\r\n", paymentModel.PaymentInfo.BankInfo.FullName));
            sb.Append(String.Format("Client credentials: {0}.\r\n", paymentModel.PaymentInfo.BankInfo.Credentials));

            sb.Append("\r\nGame\t\t\tPrice\t\t\tDiscount\r\n");

            foreach (OrderItemModel orderItemModel in paymentModel.OrderItems)
            {
                sb.Append(String.Format("{0}\t\t${1}\t\t-{2}%\r\n",
                    orderItemModel.Game.GameLocalizations.First(loc =>
                        String.Equals(loc.Language.Code, Constants.EnglishLanguageCode, StringComparison.CurrentCultureIgnoreCase))
                        .Name,
                    orderItemModel.Price,
                    orderItemModel.Discount));
            }

            sb.Append(String.Format("Sum: ${0}", paymentModel.Sum));

            string finalString = sb.ToString();

            byte[] bytes = Encoding.UTF8.GetBytes(finalString);
            var result = new FileContentResult(bytes, "text");
            result.FileDownloadName = "BankPayment.txt";
            return result;
        }

        private ActionResult GetVisaResult(PaymentModel paymentModel, PaymentViewModel paymentViewModel)
        {
            var paymentWcfServiceClient = new PaymentWcfServiceClient();

            var visaInfo = Mapper.Map<VisaPaymentInfo>(paymentModel.PaymentInfo.VisaInfo);
            visaInfo.PaymentAmount = paymentModel.Sum;

            var paymentResult = PaymentResult.Failure;

            try
            {
                paymentResult = paymentWcfServiceClient.MakePayment(visaInfo);
            }
            catch (FaultException<ValidationFault> validationEx)
            {
                string errorMessage = string.Empty;
                errorMessage = validationEx.Detail.Details.Aggregate(errorMessage, (current, item) => current + (item.Message + " "));
                ModelState.AddModelError("ServiceError", errorMessage);
            }

            if (paymentResult != PaymentResult.Success)
            {
                ModelState.AddModelError("WrongData", paymentResult.ToString());
                return View("Get", paymentViewModel);
            }

            _orderService.CleanOrderForUser(Session.SessionID);
            MessageSuccess(GlobalRes.OrderMadeSuccessfully);
            return RedirectToAction("Get", "Game");
        }

        #endregion

        [ActionName("Get")]
        [HttpGet]
        public ActionResult MakePayment(PaymentType paymentType)
        {
            var paymentWcfServiceClient = new PaymentWcfServiceClient();
            Guid token = paymentWcfServiceClient.GetToken();

            var visaInfoViewModel = new VisaInfoViewModel
            {
                Token = token,
                Payee = GameStoreName,
            };

            PaymentViewModel paymentViewModel = GetEmptyPaymentViewModel(paymentType);

            paymentViewModel.PaymentInfoViewModel.VisaInfo = visaInfoViewModel;

            return View(paymentViewModel);
        }

        [ActionName("Get")]
        [HttpPost]
        public ActionResult MakePayment(PaymentViewModel paymentViewModel)
        {
            if (ModelState.IsValid)
            {
                string sessionKey = Session.SessionID;
                var paymentInfoModel = Mapper.Map<PaymentInfoModel>(paymentViewModel.PaymentInfoViewModel);

                PaymentModel paymentModel =
                    _paymentService.CreatePaymentModel(sessionKey, paymentViewModel.PaymentType, paymentInfoModel);

                var actions = new Dictionary<PaymentType, Func<PaymentModel, ActionResult>>
                {
                    {PaymentType.Bank, GetBankFileResult},
                    {PaymentType.Visa, model => GetVisaResult(model, paymentViewModel)},
                    {PaymentType.Terminal, model => Json(model, JsonRequestBehavior.AllowGet)}
                };

                return actions[paymentModel.PaymentType](paymentModel);
            }

            return View(paymentViewModel);
        }
    }
}