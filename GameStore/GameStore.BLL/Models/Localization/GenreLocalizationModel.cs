using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Models.Localization
{
    public class GenreLocalizationModel : LocalizationEntityModel
    {
        public int GenreLocalizationId { get; set; }

        [MaxLength(450)]
        public string Name { get; set; }
    }
}
