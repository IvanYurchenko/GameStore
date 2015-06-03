using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Payment;
using GameStore.Core;
using GameStore.WebUI.ViewModels.Payment;
using GameStore.WebUI.ViewModels.Payment.Info;

namespace GameStore.WebUI.Controllers
{
    public class PaymentController : BaseController
    {
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

        private static ActionResult GetBankFileResult(PaymentModel paymentModel)
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

        #endregion

        [ActionName("Get")]
        [HttpGet]
        public ActionResult MakePayment(PaymentType paymentType)
        {
            PaymentViewModel paymentViewModel = GetEmptyPaymentViewModel(paymentType);
            return View(paymentViewModel);
        }

        [ActionName("Get")]
        [HttpPost]
        public ActionResult MakePayment(PaymentViewModel paymentViewModel)
        {
            string sessionKey = Session.SessionID;
            var paymentInfoModel = Mapper.Map<PaymentInfoModel>(paymentViewModel.PaymentInfoViewModel);

            PaymentModel paymentModel = _paymentService.GetPaymentModel(sessionKey, paymentViewModel.PaymentType,
                paymentInfoModel);

            var actions = new Dictionary<PaymentType, Func<PaymentModel, ActionResult>>
            {
                {PaymentType.Bank, GetBankFileResult},
                {PaymentType.Visa, model => Json(model, JsonRequestBehavior.AllowGet)},
                {PaymentType.Terminal, model => Json(model, JsonRequestBehavior.AllowGet)}
            };

            _orderService.CleanOrderForUser(sessionKey);

            return actions[paymentModel.PaymentType](paymentModel);
        }
    }
}