using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models.Security
{
    public class RoleModel : EntityModel
    {
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public string Description { get; set; }
    }
}
