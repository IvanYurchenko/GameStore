using System.ComponentModel.DataAnnotations;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.Localization
{
    public class PlatformTypeLocalizationViewModel : LocalizationEntityViewModel
    {
        public int PlatformTypeLocalizationId { get; set; }

        [MaxLength(450)]
        [Display(ResourceType = typeof(GlobalRes), Name = "PlatformType")]
        public string Type { get; set; }
    }
}
