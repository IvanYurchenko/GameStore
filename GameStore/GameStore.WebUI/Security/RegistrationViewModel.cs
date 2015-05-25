using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.WebUI.Security
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        [Remote("IsUserNameAvailable", "Validation")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords must be equal. ")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}