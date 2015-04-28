using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Models
{
    public class GameModel
    {
        [Key]
        public int GameId { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
        public bool Discontinued { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime? PublicationDate { get; set; }

        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }

        public ICollection<PlatformTypeModel> PlatformTypes { get; set; }
        public ICollection<GenreModel> Genres { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
        public ICollection<BasketItemModel> BasketItems { get; set; }
    }
}