using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Models.Localization;

namespace GameStore.BLL.Models
{
    public class GameModel : EntitySyncModel
    {
        [Key]
        public int GameId { get; set; }

        public string Key { get; set; }

        public decimal Price { get; set; }

        public int UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? PublicationDate { get; set; }

        public int? PublisherId { get; set; }

        public PublisherModel Publisher { get; set; }

        public ICollection<PlatformTypeModel> PlatformTypes { get; set; }
        public ICollection<GenreModel> Genres { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
        public ICollection<BasketItemModel> BasketItems { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }

        public ICollection<GameLocalizationModel> GameLocalizations { get; set; } 
    }
}