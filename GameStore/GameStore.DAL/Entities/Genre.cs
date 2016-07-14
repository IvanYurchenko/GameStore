using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Entities.Localization;

namespace GameStore.DAL.Entities
{
    public class Genre : SyncEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }
        
        public int? ParentGenreId { get; set; }

        [ForeignKey("ParentGenreId")]
        public virtual Genre ParentGenre { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public virtual ICollection<GenreLocalization> GenreLocalizations { get; set; }
    }
}