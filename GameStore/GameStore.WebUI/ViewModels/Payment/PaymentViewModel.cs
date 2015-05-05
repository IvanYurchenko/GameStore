using GameStore.BLL.Enums;

namespace GameStore.WebUI.ViewModels.Payment
{
    public class PaymentViewModel
    {
        public PaymentInfoViewModel PaymentInfoViewModel { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}