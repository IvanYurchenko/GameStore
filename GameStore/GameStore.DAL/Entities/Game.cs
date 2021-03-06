﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Entities.Localization;

namespace GameStore.DAL.Entities
{
    public class Game : SyncEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }

        [MaxLength(450)]
        public string Key { get; set; }
        
        public decimal Price { get; set; }

        public int UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime? PublicationDate { get; set; }

        public int? PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PlatformType> PlatformTypes { get; set; }
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        
        public virtual ICollection<GameLocalization> GameLocalizations { get; set; }
    }
}