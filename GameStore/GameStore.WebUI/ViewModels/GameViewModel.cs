using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.BLL.Models;

namespace GameStore.WebUI.ViewModels
{
    public class GameViewModel : EntitySyncViewModel
    {
        public GameViewModel()
        {
            PublicationDate = new DateTime();
        }

        private const string RequiredMessage = "Field '{0}' can not be empty";
        private const string RangeMessage = "Field '{0}' must be in range from {1} to {2}";

        [HiddenInput]
        [Key]
        public int GameId { get; set; }

        [Display(Name = "Key")]
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(30, MinimumLength = 2)]
        [Remote("IsGameKeyAvailable", "Validation", AdditionalFields = "GameId")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        [Editable(true)]
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
        [Range(0, int.MaxValue, ErrorMessage = RangeMessage)]
        public int UnitsInStock { get; set; }

        [Display(Name = "Is discontinued")]
        public bool Discontinued { get; set; }

        [Display(Name = "Date added")]
        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }

        [Display(Name = "Publication date")]
        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }

        [Display(Name = "Genres")]
        [Required(ErrorMessage = RequiredMessage)]
        public List<int> SelectedGenresIds { get; set; }

        public IEnumerable<GenreModel> Genres { get; set; }

        [Display(Name = "Platforms")]
        [Required(ErrorMessage = RequiredMessage)]
        public List<int> SelectedPlatformTypesIds { get; set; }

        public IEnumerable<PlatformTypeModel> PlatformTypes { get; set; }

        [Display(Name = "Publisher")]
        [Required(ErrorMessage = RequiredMessage)]
        public int SelectedPublisherId { get; set; }

        public PublisherModel Publisher { get; set; }

        public IEnumerable<PublisherModel> Publishers { get; set; }
    }
}