using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GameStore.BLL.Models;

namespace GameStore.WebUI.ViewModels
{
    public class BasketViewModel : EntityViewModel
    {
        [Key]
        public int BasketId { get; set; }

        public string SessionKey { get; set; }

        public ICollection<BasketItemViewModel> BasketItems { get; set; }
    }
}