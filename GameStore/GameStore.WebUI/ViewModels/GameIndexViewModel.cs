using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.WebUI.ViewModels.GamesFiltersViewModels;

namespace GameStore.WebUI.ViewModels
{
    public class GameIndexViewModel
    {
        public IEnumerable<GameModel> Games { get; set; }
        public GamesFilterViewModel Filter { get; set; }
    }
}