using GameStore.WebUI.ViewModels.Payment.Info;

namespace GameStore.WebUI.ViewModels.Payment
{
    public class PaymentInfoViewModel
    {
        public BankInfoViewModel BankInfo { get; set; }
        public VisaInfoViewModel VisaInfo { get; set; }
        public TerminalInfoViewModel TerminalInfo { get; set; }
    }
}