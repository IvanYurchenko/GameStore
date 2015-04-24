using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Models
{
    public class BasketItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BasketItemId { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Discount will be like 0.1, 0.2, 0.25
        public decimal Discount { get; set; }

        public int BasketId { get; set; }
        public BasketModel BasketModel { get; set; }
    }
}