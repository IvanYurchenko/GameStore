using System.ComponentModel.DataAnnotations;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.Localization
{
    public class GameLocalizationViewModel : LocalizationEntityViewModel
    {
        public int GameLocalizationId { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Name")]
        public string Name { get; set; }
        
        [Display(ResourceType = typeof(GlobalRes), Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
