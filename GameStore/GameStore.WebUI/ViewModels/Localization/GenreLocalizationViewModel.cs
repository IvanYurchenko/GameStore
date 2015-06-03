using System.ComponentModel.DataAnnotations;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.Localization
{
    public class GenreLocalizationViewModel : LocalizationEntityViewModel
    {
        public int GenreLocalizationId { get; set; }

        [MaxLength(450)]
        [Display(ResourceType = typeof(GlobalRes), Name = "Name")]
        public string Name { get; set; }
    }
}
