using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Enums;

namespace GameStore.WebUI.ViewModels.GamesFiltersViewModels
{
    public class PaginationViewModel
    {
        [Display(Name = "Games on page")]
        public PageCapacity PageCapacity { get; set; }
        public int CurrentPage { get; set; }

        public int ItemsNumber { get; set; }
        public int PagesNumber { get; set; }

        public bool IsLastPage { get; set; }
        public bool IsFirstPage { get; set; }
    }
}