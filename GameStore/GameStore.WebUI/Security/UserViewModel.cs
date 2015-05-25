using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.BLL.Models.Security;
using GameStore.WebUI.ViewModels;

namespace GameStore.WebUI.Security
{
    public class UserViewModel : EntityViewModel
    {
        [Key]
        [HiddenInput]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "User name")]
        [Remote("IsUserNameAvailable", "Validation")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password{ get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords must be equal. ")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [HiddenInput]
        public bool IsDisabled { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [Display(Name = "Roles")]
        public List<int> SelectedRoles { get; set; }

        public IEnumerable<RoleModel> AllRoles { get; set; }
    }
}