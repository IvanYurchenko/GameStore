using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.WebUI.ViewModels.Localization
{
    public abstract class LocalizationEntityViewModel
    {
        public int LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public LanguageViewModel Language { get; set; }
    }
}
