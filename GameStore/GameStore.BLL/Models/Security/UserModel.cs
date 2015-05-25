using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models.Security
{
    public class UserModel : EntityModel
    {
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<RoleModel> Roles { get; set; }
    }
}
