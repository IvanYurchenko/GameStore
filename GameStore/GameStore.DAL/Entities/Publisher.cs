using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Entities.Localization;

namespace GameStore.DAL.Entities
{
    public class Publisher : SyncEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublisherId { get; set; }
        
        public string HomePage { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public virtual ICollection<PublisherLocalization> PublisherLocalizations { get; set; }
    }
}