using System.ComponentModel.DataAnnotations;
using GameStore.Resources;

namespace GameStore.WebUI.ViewModels
{
    public class CommentViewModel : EntityViewModel
    {
        [Key]
        public int CommentId { get; set; }

        public int GameId { get; set; }

        public int? ParentCommentId { get; set; }

        [Required]
        [Display(ResourceType = typeof(GlobalRes), Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(ResourceType = typeof(GlobalRes), Name = "Body")]
        public string Body { get; set; }
    }
}