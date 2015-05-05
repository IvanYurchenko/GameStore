using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.ViewModels.Payment.Info
{
    public class TerminalInfoViewModel
    {
        private const string Required = "This field is required.";

        [Required(ErrorMessage = Required)]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = Required)]
        public string InvoiceNumber { get; set; }
    }
}