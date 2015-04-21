using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.BLL.Models;

namespace GameStore.DAL.Entities
{
    public class BasketItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BasketItemId { get; set; }

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public GameModel GameModel { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Discount will be like 0.1, 0.2, 0.25
        public decimal Discount { get; set; }
    }
}