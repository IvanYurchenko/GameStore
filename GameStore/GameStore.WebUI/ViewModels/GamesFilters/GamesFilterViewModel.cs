using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Enums;

namespace GameStore.WebUI.ViewModels.GamesFilters
{
    public class GamesFilterViewModel
    {
        [Display(Name = "Price from")]
        [Range(0, int.MaxValue, ErrorMessage = "Price can not be negative.")]
        public decimal? PriceFrom { get; set; }

        [Display(Name = "Price to")]
        [Range(0, int.MaxValue, ErrorMessage = "Price can not be negative.")]
        public decimal? PriceTo { get; set; }

        [Display(Name = "Availability")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Sorting by")]
        public SortCondition SortCondition { get; set; }

        [Display(Name = "Date period")]
        public DatePeriod DatePeriod { get; set; }

        [Display(Name = "Game title")]
        public string GameNamePart { get; set; }

        [Display(Name = "Genres")]
        public List<int> Genres { get; set; }

        public IEnumerable<GenreFilterViewModel> AvailableGenres { get; set; }
        public IEnumerable<GenreFilterViewModel> SelectedGenres { get; set; }

        [Display(Name = "Publishers")]
        public List<int> Publishers { get; set; }

        public IEnumerable<PublisherFilterViewModel> AvailablePublishers { get; set; }
        public IEnumerable<PublisherFilterViewModel> SelectedPublishers { get; set; }

        [Display(Name = "Platform types")]
        public List<int> PlatformTypes { get; set; }

        public IEnumerable<PlatformTypeFilterViewModel> AvailablePlatformTypes { get; set; }
        public IEnumerable<PlatformTypeFilterViewModel> SelectedPlatformTypes { get; set; }
    }
}