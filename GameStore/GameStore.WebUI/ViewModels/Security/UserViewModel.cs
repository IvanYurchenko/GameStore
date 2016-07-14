using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.BLL.Models.Security;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.Security
{
    public class UserViewModel : EntityViewModel
    {
        [Key]
        [HiddenInput]
        public int UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "UserName")]
        [Remote("IsUserNameAvailable", "Validation")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "Password")]
        [DataType(DataType.Password)]
        public string Password{ get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "PasswordsMustBeEqual")]
        [Display(ResourceType = typeof(GlobalRes), Name = "PasswordConfirm")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "LastName")]
        public string LastName { get; set; }

        [HiddenInput]
        [Display(ResourceType = typeof(GlobalRes), Name = "UserIsDisabled")]
        public bool IsDisabled { get; set; }

        public DateTime CreateDate { get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "Roles")]
        public List<int> SelectedRoles { get; set; }

        public IEnumerable<RoleModel> AllRoles { get; set; }
    }
}