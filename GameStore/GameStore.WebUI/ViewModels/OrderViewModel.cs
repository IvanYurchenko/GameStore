using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.Models;

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

        public bool IsPayed { get; set; }
    }
}