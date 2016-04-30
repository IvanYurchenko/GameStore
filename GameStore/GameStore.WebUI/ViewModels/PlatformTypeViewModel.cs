using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels
{
    public class PlatformTypeViewModel
    {
        [Key]
        [HiddenInput]
        public int PlatformTypeId { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "PlatformType")]
        public string Type { get; set; }
    }
}