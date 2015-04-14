using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }
        public int GameId { get; set; }
        public int? ParentCommentId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
    }
}
