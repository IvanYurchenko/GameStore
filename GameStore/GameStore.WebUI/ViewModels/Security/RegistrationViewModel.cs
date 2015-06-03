using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.Security
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "UserName")]
        [Remote("IsUserNameAvailable", "Validation")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "PasswordsMustBeEqual")]
        [Display(ResourceType = typeof(GlobalRes), Name = "PasswordConfirm")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "LastName")]
        public string LastName { get; set; }
    }
}