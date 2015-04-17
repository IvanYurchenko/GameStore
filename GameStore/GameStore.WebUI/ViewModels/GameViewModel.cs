using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.BLL.Models;

namespace GameStore.WebUI.ViewModels
{
    public class GameViewModel
    {
        private const string FieldCanNotBeEmpty = "Field '{0}' can not be empty";
        private const string FieldIsNotInRange = "Field '{0}' is not in range";

        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Key")]
        [Required(ErrorMessage = FieldCanNotBeEmpty)]
        public string Key { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = FieldCanNotBeEmpty)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = FieldCanNotBeEmpty)]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = FieldCanNotBeEmpty)]
        [DataType(DataType.Currency)]
        [Range(0.0, double.MaxValue, ErrorMessage = FieldIsNotInRange)]
        public decimal Price { get; set; }

        [Display(Name = "Units in stock")]
        [Required(ErrorMessage = FieldCanNotBeEmpty)]
        [Range(0, short.MaxValue, ErrorMessage = FieldIsNotInRange)]
        public short UnitsInStock { get; set; }

        [Display(Name = "Is discontinued")]
        public bool Discontinued { get; set; }

        [Display(Name = "Genres")]
        [Required(ErrorMessage = FieldCanNotBeEmpty)]
        public int[] SelectedGenres { get; set; }

        public IEnumerable<GenreModel> Genres { get; set; }

        [Display(Name = "Platforms")]
        [Required(ErrorMessage = FieldCanNotBeEmpty)]
        public int[] SelectedPlatformTypes { get; set; }

        public IEnumerable<PlatformTypeModel> PlatformTypes { get; set; }
    }
}