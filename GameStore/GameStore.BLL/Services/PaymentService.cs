using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Payment;
using GameStore.BLL.Strategies;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IPaymentStrategy PaymentStrategy { get; set; }

        public PaymentModel CreatePaymentModel(string sessionKey, PaymentType paymentType,
            PaymentInfoModel paymentInfoModel)
        {
            var paymentModel = new PaymentModel
            {
                PaymentInfo = paymentInfoModel,
                PaymentType = paymentType,
            };

            ICollection<OrderItem> orderItems =
                _unitOfWork.OrderRepository.Get(order => order.SessionKey == sessionKey).First().OrderItems;
            paymentModel.OrderItems = Mapper.Map<ICollection<OrderItemModel>>(orderItems);

            var paymentStrategies = new Dictionary<PaymentType, IPaymentStrategy>
            {
                {PaymentType.Bank, new BankPaymentStrategy()},
                {PaymentType.Visa, new VisaPaymentStrategy()},
                {PaymentType.Terminal, new TerminalPaymentStrategy()},
            };

            PaymentStrategy = paymentStrategies[paymentType];

            PaymentModel finalPaymentModel = PaymentStrategy.GetFinalPaymentModel(paymentModel);
            return finalPaymentModel;
        }
    }
}