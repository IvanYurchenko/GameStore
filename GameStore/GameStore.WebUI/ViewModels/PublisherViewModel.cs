using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.WebUI.ViewModels
{
    public class PublisherViewModel
    {
        [HiddenInput]
        [Key]
        public int PublisherId { get; set; }

        [MaxLength(40)]
        [Required]
        [Remote("IsCompanyNameAvailable", "Validation", AdditionalFields = "PublisherId")]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Url]
        [DisplayName("Home Page")]
        public string HomePage { get; set; }
    }
}