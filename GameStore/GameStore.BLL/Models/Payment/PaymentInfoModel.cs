using GameStore.BLL.Models.Payment.External;

namespace GameStore.BLL.Models.Payment
{
    public class PaymentInfoModel
    {
        public BankInfoModel BankInfo { get; set; }
        public VisaInfoModel VisaInfo { get; set; }
        public TerminalInfoModel TerminalInfo { get; set; }
    }
}