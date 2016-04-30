using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Resources;
using GameStore.WebUI.ViewModels.Localization;

namespace GameStore.WebUI.ViewModels
{
    public class PublisherAddUpdateViewModel : EntitySyncViewModel
    {
        [HiddenInput]
        [Key]
        public int PublisherId { get; set; }
        
        [Url]
        [Display(ResourceType = typeof(GlobalRes), Name = "HomePage")]
        public string HomePage { get; set; }

        public ICollection<PublisherLocalizationViewModel> PublisherLocalizations { get; set; }
    }
}