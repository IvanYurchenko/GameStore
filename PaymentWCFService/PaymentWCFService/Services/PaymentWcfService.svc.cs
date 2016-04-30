using System;
using System.Linq;
using PaymentWCFService.DataContracts;
using PaymentWCFService.DataStoring;
using PaymentWCFService.Email;
using PaymentWCFService.Enums;
using PaymentWCFService.Models;
using PaymentWCFService.ServiceContracts;

namespace PaymentWCFService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PaymentWcfService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PaymentWcfService.svc or PaymentWcfService.svc.cs at the Solution Explorer and start debugging.
    public class PaymentWcfService : IPaymentWcfService
    {
        private readonly IEmailSender _emailSender;

        public PaymentWcfService()
        {
            _emailSender = new EmailSender();
        }

        public Guid GetToken()
        {
            Guid guid = Guid.NewGuid();
            DataStore.Tokens.Add(guid);
            return guid;
        }

        public PaymentResult MakePayment(VisaPaymentInfo paymentInfo)
        {
            if (!DataStore.Tokens.Any(t => t.Equals(paymentInfo.Token)))
            {
                return PaymentResult.WrongToken;
            }

            Card card = DataStore.Cards.FirstOrDefault(c => c.Number == paymentInfo.CardNumber
                                                      && c.Cvv == paymentInfo.Cvv
                                                      && c.ExpirationYear == paymentInfo.ExpirationYear
                                                      && c.ExpirationMonth == paymentInfo.ExpirationMonth);

            if (card == null)
            {
                return PaymentResult.CardDoesNotExist;
            }

            if (card.Balance < paymentInfo.PaymentAmount)
            {
                return PaymentResult.NotEnoughMoney;
            }

            Client payee = DataStore.Clients.FirstOrDefault(c =>
                    String.Equals(c.Name, paymentInfo.Payee, StringComparison.CurrentCultureIgnoreCase));

            if (payee == null)
            {
                return PaymentResult.PayeeDoesNotExist;
            }

            var payment = new Payment
            {
                Date = DateTime.UtcNow,
                Amount = paymentInfo.PaymentAmount,
                PayerName = paymentInfo.FullName,
                Payee = payee,
            };

            DataStore.Payments.Add(payment);

            _emailSender.SendEmailToRecipient(payment, payee.Email);

            if (paymentInfo.UserEmail != null)
            {
                _emailSender.SendEmailToSender(payment, paymentInfo.UserEmail);
            }

            card.Balance -= paymentInfo.PaymentAmount;

            return PaymentResult.Success;
        }
    }
}
