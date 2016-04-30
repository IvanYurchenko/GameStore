using System;
using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Enums;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.GamesFilters
{
    public class PaginationViewModel
    {
        public PaginationViewModel()
        {
            PageCapacity = PageCapacity.Five;
            CurrentPage = 1;
        }

        [Display(ResourceType = typeof(GlobalRes), Name = "GamesPerPage")]
        public PageCapacity PageCapacity { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsNumber { get; set; }

        public int PagesNumber
        {
            get { return (int) Math.Ceiling((decimal) ItemsNumber/((int) PageCapacity)); }
        }

        public bool IsFirstPage
        {
            get { return CurrentPage == 1; }
        }

        public bool IsLastPage
        {
            get { return CurrentPage == PagesNumber; }
        }
    }
}