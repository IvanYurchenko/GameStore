using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities.Localization
{
    public class PlatformTypeLocalization : LocalizationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlatformTypeLocalizationId { get; set; }

        [MaxLength(450)]
        public string Type { get; set; }
        
        public int PlatformTypeId { get; set; }

        [ForeignKey("PlatformTypeId")]
        public virtual PlatformType PlatformType { get; set; }
    }
}
