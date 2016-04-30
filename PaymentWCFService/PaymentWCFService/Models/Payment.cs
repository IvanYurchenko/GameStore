using System;

namespace PaymentWCFService.Models
{
    public class Payment
    {
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string PayerName { get; set; }

        public Client Payee { get; set; }
    }
}