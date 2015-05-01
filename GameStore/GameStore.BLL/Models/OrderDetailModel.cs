using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Models
{
    public class OrderDetailModel
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int GameId { get; set; }
        public virtual GameModel Game { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Discount will be like 0.1, 0.2, 0.25
        public decimal Discount { get; set; }

        public int OrderId { get; set; }
        public OrderModel Order { get; set; }
    }
}
