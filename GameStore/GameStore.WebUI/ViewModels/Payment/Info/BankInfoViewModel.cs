using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.ViewModels.Payment.Info
{
    public class BankInfoViewModel
    {
        private const string Required = "This field is required.";

        [Required(ErrorMessage = Required)]
        public string FullName { get; set; }

        [Required(ErrorMessage = Required)]
        public string Credentials { get; set; }
    }
}