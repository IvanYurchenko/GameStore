using System;
using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Enums;

namespace GameStore.WebUI.ViewModels.GamesFiltersViewModels
{
    public class PaginationViewModel
    {
        public PaginationViewModel()
        {
            PageCapacity = PageCapacity.Five;
            CurrentPage = 1;
        }

        [Display(Name = "Games per page")]
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