using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.BLL.Models
{
    public class PublisherModel
    {
        [Key]
        public int PublisherId { get; set; }

        [MaxLength(40)]
        [Required]
        [Remote("IsCompanyNameAvailable", "Validation")]
        public string CompanyName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Url]
        public string HomePage { get; set; }
    }
}