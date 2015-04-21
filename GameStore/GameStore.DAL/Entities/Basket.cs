using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class Basket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BasketId { get; set; }

        public int UserSessionId { get; set; }
        [ForeignKey("UserSessionId")]
        public virtual UserSession UserSession { get; set; }

        public IEnumerable<BasketItem> BasketItems { get; set; }
    }
}