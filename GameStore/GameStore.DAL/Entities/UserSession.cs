using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class UserSession : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserSessionId { get; set; }

        public string SessionKey { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}