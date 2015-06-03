using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Enums;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels.GamesFilters
{
    public class GamesFilterViewModel
    {
        [Display(ResourceType = typeof(GlobalRes), Name = "PriceFrom")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "PriceCanNotBeNegative")]
        public decimal? PriceFrom { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "PriceTo")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "PriceCanNotBeNegative")]
        public decimal? PriceTo { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Availability")]
        public bool IsAvailable { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "SortingBy")]
        public SortCondition SortCondition { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "DatePeriod")]
        public DatePeriod DatePeriod { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "GameTitle")]
        public string GameNamePart { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Genres")]
        public List<int> Genres { get; set; }

        public IEnumerable<GenreFilterViewModel> AvailableGenres { get; set; }
        public IEnumerable<GenreFilterViewModel> SelectedGenres { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Publishers")]
        public List<int> Publishers { get; set; }

        public IEnumerable<PublisherFilterViewModel> AvailablePublishers { get; set; }
        public IEnumerable<PublisherFilterViewModel> SelectedPublishers { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "PlatformTypes")]
        public List<int> PlatformTypes { get; set; }

        public IEnumerable<PlatformTypeFilterViewModel> AvailablePlatformTypes { get; set; }
        public IEnumerable<PlatformTypeFilterViewModel> SelectedPlatformTypes { get; set; }
    }
}