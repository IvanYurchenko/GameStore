using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Core.Enums;

namespace GameStore.WebUI.ViewModels
{
    public class OrderViewModel : EntitySyncViewModel
    {
        [Key]
        [HiddenInput]
        public int OrderId { get; set; }

        [HiddenInput]
        public string SessionKey { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}