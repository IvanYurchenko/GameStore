using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }

        public int GameId { get; set; }
        public int? ParentCommentId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Body")]
        public string Body { get; set; }

        public bool IsRemoved { get; set; }
    }
}