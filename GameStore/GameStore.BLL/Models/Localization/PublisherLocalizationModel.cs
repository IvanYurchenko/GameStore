using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models.Localization
{
    public class PublisherLocalizationModel : LocalizationEntityModel
    {
        public int PublisherLocalizationId { get; set; }

        [MaxLength(450)]
        public string CompanyName { get; set; }

        public string Description { get; set; }
    }
}
