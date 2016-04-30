using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.Security
{
    public class RoleViewModel : EntityViewModel
    {
        [HiddenInput]
        [Key]
        public int RoleId { get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "Role")]
        [Remote("IsUserNameAvailable", "Validation", AdditionalFields = "RoleId")]
        public string RoleName { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Description")]
        public string Description { get; set; }
    }
}