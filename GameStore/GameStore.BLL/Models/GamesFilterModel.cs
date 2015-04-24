using System.Collections.Generic;
using GameStore.BLL.Enums;

namespace GameStore.BLL.Models
{
    public class GamesFilterModel
    {
        public string GameNamePart { get; set; }

        public DatePeriod DatePeriod { get; set; }
        public SortCondition SortCondition { get; set; }

        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

        public List<int> Publishers { get; set; }
        public List<int> Genres { get; set; }
        public List<int> PlatformTypes { get; set; }
    }
}