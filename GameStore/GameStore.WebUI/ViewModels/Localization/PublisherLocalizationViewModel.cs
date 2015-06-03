using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.Localization
{
    public class PublisherLocalizationViewModel : LocalizationEntityViewModel
    {
        public int PublisherLocalizationId { get; set; }

        [MaxLength(40)]
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Remote("IsCompanyNameAvailable", "Validation", AdditionalFields = "PublisherId")]
        [Display(ResourceType = typeof(GlobalRes), Name = "CompanyName")]
        public string CompanyName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(GlobalRes), Name = "Description")]
        public string Description { get; set; }
    }
}
