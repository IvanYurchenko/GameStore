using System.ComponentModel.DataAnnotations;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.Security
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name  = "UserName")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(GlobalRes), Name  = "Password")]
        public string Password { get; set; }
    }
}