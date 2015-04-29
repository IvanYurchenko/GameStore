using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        #region CRUD

        void Add(CommentModel commentModel, string gameKey);
        void Remove(CommentModel commentModel);
        void Update(CommentModel commentModel);

        #endregion
    }
}