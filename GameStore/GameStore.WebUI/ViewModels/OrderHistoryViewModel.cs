using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GameStore.BLL.Models;

namespace GameStore.WebUI.ViewModels
{
    public class OrderHistoryViewModel
    {
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateTo { get; set; }

        public ICollection<OrderViewModel> Orders { get; set; }
    }
}