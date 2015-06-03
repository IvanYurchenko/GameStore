using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Models.Localization;

namespace GameStore.BLL.Models
{
    public class GenreModel : EntitySyncModel
    {
        [Key]
        public int GenreId { get; set; }

        public int? ParentGenreId { get; set; }
        
        public ICollection<GenreLocalizationModel> GenreLocalizations { get; set; } 
    }
}