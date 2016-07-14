using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models
{
    public class CommentModel : EntityModel
    {
        [Key]
        public int CommentId { get; set; }

        public int GameId { get; set; }

        public int? ParentCommentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }
    }
}