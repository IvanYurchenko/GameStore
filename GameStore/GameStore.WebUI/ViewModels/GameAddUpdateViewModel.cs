using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.BLL.Models;
using GameStore.Resources;
using GameStore.WebUI.ViewModels.Localization;

namespace GameStore.WebUI.ViewModels
{
    public class GameAddUpdateViewModel : EntitySyncViewModel
    {
        public GameAddUpdateViewModel()
        {
            PublicationDate = new DateTime();
        }

        [HiddenInput]
        [Key]
        public int GameId { get; set; }

        [Display(ResourceType = typeof (GlobalRes), Name = "Key")]
        [Required(ErrorMessageResourceType = typeof (GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [StringLength(30, MinimumLength = 2)]
        [Remote("IsGameKeyAvailable", "Validation", AdditionalFields = "GameId")]
        [Editable(true)]
        public string Key { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Price")]
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Range(0.0, double.MaxValue, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RangeValidationMessage")]
        public decimal Price { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "UnitsInStock")]
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RangeValidationMessage")]
        public int UnitsInStock { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "IsDiscontinued")]
        public bool Discontinued { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "AddedDate")]
        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "PublicationDate")]
        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Genres")]
        public List<int> SelectedGenresIds { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "PlatformTypes")]
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "RequiredValidationMessage")]
        public List<int> SelectedPlatformTypesIds { get; set; }

        public IEnumerable<PlatformTypeViewModel> PlatformTypes { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Publisher")]
        public int SelectedPublisherId { get; set; }

        [Display(ResourceType = typeof(GlobalRes), Name = "Publisher")]
        public PublisherModel Publisher { get; set; }

        public IEnumerable<PublisherViewModel> Publishers { get; set; }

        public ICollection<GameLocalizationViewModel> GameLocalizations { get; set; }
    }
}