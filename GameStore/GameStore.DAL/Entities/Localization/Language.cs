
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities.Localization
{
    public class Language
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LanguageId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public virtual ICollection<GameLocalization> GameLocalizations { get; set; }
        public virtual ICollection<GenreLocalization> GenreLocalizations { get; set; }
        public virtual ICollection<PlatformTypeLocalization> PlatformTypeLocalizations { get; set; }
        public virtual ICollection<PublisherLocalization> PublisherLocalizations { get; set; }
    }
}
