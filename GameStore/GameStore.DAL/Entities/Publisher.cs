using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublisherId { get; set; }

        [MaxLength(40)]
        public string CompanyName { get; set; }

        public string Description { get; set; }
        public string HomePage { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}