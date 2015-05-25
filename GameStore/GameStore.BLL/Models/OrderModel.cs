using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Core.Enums;

namespace GameStore.BLL.Models
{
    public class OrderModel : EntitySyncModel
    {
        [Key]
        public int OrderId { get; set; }

        public string SessionKey { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderItemModel> OrderItems { get; set; }
    }
}