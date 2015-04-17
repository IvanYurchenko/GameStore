using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models
{
    public class PlatformTypeModel
    {
        [Key]
        public int PlatformTypeId { get; set; }

        public string Type { get; set; }
    }
}