using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    [Table("PlatformTypes")]
    public class PlatformType
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlatformTypeId { get; set; }

        [MaxLength(450)]
        public string Type { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
