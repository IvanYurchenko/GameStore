using PaymentWCFService.Models;

namespace PaymentWCFService.Email
{
    public interface IEmailSender
    {
        void SendEmailToRecipient(Payment payment, string email);

        void SendEmailToSender(Payment payment, string email);
    }
}