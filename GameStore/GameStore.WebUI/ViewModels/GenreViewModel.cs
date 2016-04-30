using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels
{
    public class GenreViewModel : EntitySyncViewModel
    {
        [Key]
        [HiddenInput]
        public int GenreId { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "ParentGenre")]
        public int? ParentGenreId { get; set; }

        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Display(ResourceType = typeof(GlobalRes), Name = "Name")]
        public string Name { get; set; }

        public IEnumerable<GenreViewModel> AllGenres { get; set; }
    }
}