using System;

namespace GameStore.BLL.Models.Payment.External
{
    public class VisaInfoModel
    {
            public string CardNumber { get; set; }

            public int Cvv { get; set; }

            public int ExpirationYear { get; set; }

            public int ExpirationMonth { get; set; }

            public string FullName { get; set; }

            public string PaymentPurpose { get; set; }

            public decimal PaymentAmount { get; set; }

            public Guid Token { get; set; }

            public string Payee { get; set; }

            public string UserEmail { get; set; }

            public string UserPhoneNumber { get; set; }
    }
}