using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.BLL.Models
{
    public class BasketModel : EntityModel
    {
        [Key]
        public int BasketId { get; set; }

        public string SessionKey { get; set; }

        public ICollection<BasketItemModel> BasketItems { get; set; }
    }
}