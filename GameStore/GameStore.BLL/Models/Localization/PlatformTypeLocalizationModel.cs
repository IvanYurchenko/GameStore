using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models.Localization
{
    public class PlatformTypeLocalizationModel : LocalizationEntityModel
    {
        public int PlatformTypeLocalizationId { get; set; }

        [MaxLength(450)]
        public string Type { get; set; }
    }
}
