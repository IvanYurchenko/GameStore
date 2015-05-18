using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.ViewModels.OrdersFilters
{
    public class OrderDateFilterViewModel
    {
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateTo { get; set; }
    }
}