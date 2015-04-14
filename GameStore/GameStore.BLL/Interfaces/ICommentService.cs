using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        // Converting
        CommentModel ConvertToModel(Comment comment);
        Comment ConvertToComment(CommentModel commentModel);

        // CRUD
        void Add(CommentModel commentModel, string gameKey);
        void Remove(CommentModel commentModel);
        void Update(CommentModel commentModel);
        
    }
}
