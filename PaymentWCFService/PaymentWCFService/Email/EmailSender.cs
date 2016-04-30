using System;
using System.Net.Mail;
using System.Text;
using PaymentWCFService.Models;

namespace PaymentWCFService.Email
{
    public class EmailSender : IEmailSender
    {
        public void SendEmailToRecipient(Payment payment, string email)
        {
            string subject = String.Format("Payment to {0}", payment.Payee.Name);

            string body = String.Format(
@"Dear {0}!

Your payment to {1} dated from {2} with total amount of ${3} has been completed successfully.

Thank you for using our service!", payment.PayerName, payment.Payee.Name, payment.Date, payment.Amount);

            SendEmail(email, subject, body);
        }

        public void SendEmailToSender(Payment payment, string email)
        {
            string subject = String.Format("Payment from {0}", payment.PayerName);

            string body = String.Format(
@"Dear {0}!

You have recieved a payment from {1} dated from {2} with total amount of ${3}.

Thank you for using our service!", payment.Payee.Name, payment.PayerName, payment.Date, payment.Amount);

            SendEmail(email, subject, body);
        }

        private void SendEmail(string email, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("epamfinaltask@gmail.com", "EpamFinalTask123")
                };

                var mailMessage = new MailMessage("epamfinaltask@gmail.com", email, subject, body)
                {
                    BodyEncoding = UTF8Encoding.UTF8,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                };

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
            }
        }
    }
}