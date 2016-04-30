using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Core.Enums;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels
{
    public class OrderViewModel : EntitySyncViewModel
    {
        [Key]
        [HiddenInput]
        public int OrderId { get; set; }

        [HiddenInput]
        public string SessionKey { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "OrderDate")]
        public DateTime OrderDate { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "OrderStatus")]
        public OrderStatus OrderStatus { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "OrderItems")]
        public ICollection<OrderItemViewModel> OrderItems { get; set; }
    }
}