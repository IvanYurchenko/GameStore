using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models
{
    public class PublisherModel
    {
        [Key]
        public int PublisherId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }
    }
}