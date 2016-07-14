using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Entities.Localization;

namespace GameStore.DAL.Entities
{
    public class PlatformType : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlatformTypeId { get; set; }
        
        public virtual ICollection<Game> Games { get; set; }

        public virtual ICollection<PlatformTypeLocalization> PlatformTypeLocalizations { get; set; }
    }
}