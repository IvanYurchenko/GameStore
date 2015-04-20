using System;
using System.Collections.Generic;

namespace GameStore.BLL.Models
{
    public class GamesFilterModel
    {
        public string GameNamePart { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

        public PublisherModel Publisher { get; set; }

        public List<GenreModel> Genres { get; set; }
        public List<PlatformTypeModel> PlatformTypes { get; set; }
    }
}
