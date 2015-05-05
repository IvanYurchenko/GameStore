using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        public string SessionKey { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderItemModel> OrderItems { get; set; }
    }
}