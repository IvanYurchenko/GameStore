using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.ViewModels.Payment.Info
{
    public class VisaInfoViewModel
    {
        private const string Required = "This field is required.";

        [Required(ErrorMessage = Required)]
        public string FullName { get; set; }

        [Required(ErrorMessage = Required)]
        public string CardNumber { get; set; }

        public string Cvv2 { get; set; }
        public string Cvc2 { get; set; }

        [Required(ErrorMessage = Required)]
        public DateTime ExpirationDate { get; set; }
    }
}