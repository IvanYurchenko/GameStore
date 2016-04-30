using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models.Localization
{
    public class GenreLocalizationModel : LocalizationEntityModel
    {
        public int GenreLocalizationId { get; set; }

        [MaxLength(450)]
        public string Name { get; set; }
    }
}
