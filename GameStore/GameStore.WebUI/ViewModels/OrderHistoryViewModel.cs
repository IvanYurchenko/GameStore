using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels
{
    public class OrderHistoryViewModel
    {
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "DateFrom")]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "DateTo")]
        public DateTime DateTo { get; set; }

        public ICollection<OrderViewModel> Orders { get; set; }
    }
}