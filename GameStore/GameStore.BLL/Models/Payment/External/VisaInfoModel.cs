using System;

namespace GameStore.BLL.Models.Payment.External
{
    public class VisaInfoModel
    {
        public string FullName { get; set; }

        public string CardNumber { get; set; }
        public string Cvv2 { get; set; }
        public string Cvc2 { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}