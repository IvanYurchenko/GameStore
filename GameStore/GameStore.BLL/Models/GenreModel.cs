using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models
{
    public class GenreModel : EntitySyncModel
    {
        [Key]
        public int GenreId { get; set; }

        public int? ParentGenreId { get; set; }

        public string Name { get; set; }
    }
}