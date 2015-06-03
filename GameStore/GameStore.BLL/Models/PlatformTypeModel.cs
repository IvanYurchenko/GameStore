using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Models.Localization;

namespace GameStore.BLL.Models
{
    public class PlatformTypeModel : EntityModel
    {
        [Key]
        public int PlatformTypeId { get; set; }
        
        public ICollection<PlatformTypeLocalizationModel> PlatformTypeLocalizations { get; set; } 
    }
}