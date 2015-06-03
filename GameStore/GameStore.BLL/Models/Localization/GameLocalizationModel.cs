using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Models.Localization
{
    public class GameLocalizationModel : LocalizationEntityModel
    {
        public int GameLocalizationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
