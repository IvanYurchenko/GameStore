using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.WebUI.ViewModels.GamesFilters;

namespace GameStore.WebUI.ViewModels
{
    public class GameIndexViewModel
    {
        public IEnumerable<GameViewModel> Games { get; set; }

        public GamesFilterViewModel Filter { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}