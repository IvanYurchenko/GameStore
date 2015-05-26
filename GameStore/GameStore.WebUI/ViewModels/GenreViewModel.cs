using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.WebUI.ViewModels
{
    public class GenreViewModel : EntitySyncViewModel
    {
        [Key]
        [HiddenInput]
        public int GenreId { get; set; }

        [Display(Name = "Parent genre")]
        public int? ParentGenreId { get; set; }

        [Required]
        [Display(Name = "Genre Name")]
        public string Name { get; set; }

        public IEnumerable<GenreViewModel> AllGenres { get; set; }
    }
}