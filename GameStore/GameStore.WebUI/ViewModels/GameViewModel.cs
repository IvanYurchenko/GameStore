using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.BLL.Models;

namespace GameStore.WebUI.ViewModels
{
    public class GameViewModel
    {
        private const string RequiredMessage = "Field '{0}' can not be empty";
        private const string RangeMessage = "Field '{0}' must be in range from {1} to {2}";

        [HiddenInput]
        public int GameId { get; set; }

        [Display(Name = "Key")]
        [Required(ErrorMessage = RequiredMessage)]
        public string Key { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = RequiredMessage)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = RequiredMessage)]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = RequiredMessage)]
        [DataType(DataType.Currency)]
        [Range(0.0, double.MaxValue, ErrorMessage = RangeMessage)]
        public decimal Price { get; set; }

        [Display(Name = "Units in stock")]
        [Required(ErrorMessage = RequiredMessage)]
        [Range(0, short.MaxValue, ErrorMessage = RangeMessage)]
        public short UnitsInStock { get; set; }

        [Display(Name = "Is discontinued")]
        public bool Discontinued { get; set; }

        [Display(Name = "Genres")]
        [Required(ErrorMessage = RequiredMessage)]
        public List<int> SelectedGenres { get; set; }

        public IEnumerable<GenreModel> Genres { get; set; }

        [Display(Name = "Platforms")]
        [Required(ErrorMessage = RequiredMessage)]
        public List<int> SelectedPlatformTypes { get; set; }

        public IEnumerable<PlatformTypeModel> PlatformTypes { get; set; }
    }
}