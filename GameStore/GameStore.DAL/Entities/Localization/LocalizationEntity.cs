using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities.Localization
{
    public abstract class LocalizationEntity
    {
        public int LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
    }
}
