using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.DAL.Entities.Security
{
    public class User : Entity
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

        public virtual ICollection<Role> Roles { get; set; }
    }
}
