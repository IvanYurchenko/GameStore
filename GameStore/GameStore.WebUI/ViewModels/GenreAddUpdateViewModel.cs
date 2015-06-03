using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Resources;
using GameStore.WebUI.ViewModels.Localization;

namespace GameStore.WebUI.ViewModels
{
    public class GenreAddUpdateViewModel : EntitySyncViewModel
    {
        [Key]
        [HiddenInput]
        public int GenreId { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "ParentGenre")]
        public int? ParentGenreId { get; set; }

        public IEnumerable<GenreViewModel> AllGenres { get; set; }

        public ICollection<GenreLocalizationViewModel> GenreLocalizations { get; set; }
    }
}