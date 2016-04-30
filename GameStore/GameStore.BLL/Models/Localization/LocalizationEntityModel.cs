using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.BLL.Models.Localization
{
    public abstract class LocalizationEntityModel
    {
        public int LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public LanguageModel Language { get; set; }
    }
}
