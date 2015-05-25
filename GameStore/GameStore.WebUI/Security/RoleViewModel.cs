using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.WebUI.ViewModels;

namespace GameStore.WebUI.Security
{
    public class RoleViewModel : EntityViewModel
    {
        [HiddenInput]
        [Key]
        public int RoleId { get; set; }

        [Required]
        [Display(Name = "Role")]
        [Remote("IsUserNameAvailable", "Validation", AdditionalFields = "RoleId")]
        public string RoleName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}