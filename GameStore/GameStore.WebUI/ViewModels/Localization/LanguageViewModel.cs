using System.ComponentModel.DataAnnotations;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.Localization
{
    public class LanguageViewModel
    {
        public int LanguageId { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "LanguageCode")]
        public string Code { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Name")]
        public string Name { get; set; }
    }
}
