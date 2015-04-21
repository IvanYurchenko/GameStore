using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.BLL.Models;

namespace GameStore.WebUI.ViewModels
{
    public class GameIndexViewModel
    {
        public IEnumerable<GameModel> Games { get; set; }
        public GamesFilterModel GamesFilterModel { get; set; }
    }
}