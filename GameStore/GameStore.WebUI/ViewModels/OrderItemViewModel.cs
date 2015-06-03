using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GameStore.BLL.Models;

namespace GameStore.WebUI.ViewModels
{
    public class OrderItemViewModel
    {
        [Key]
        public int OrderItemId { get; set; }

        public int GameId { get; set; }

        public virtual GameViewModel Game { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        // Discount will be like 0.1, 0.2, 0.25
        public decimal Discount { get; set; }

        public int? NorthwindOrderId { get; set; }

        public int? NorthwindProductId { get; set; }
    }
}