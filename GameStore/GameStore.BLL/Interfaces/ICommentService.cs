using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        #region CRUD

        /// <summary>
        /// Adds the comment for game with specified key to the database.
        /// </summary>
        /// <param name="commentModel">The comment model.</param>
        /// <param name="gameKey">The game key.</param>
        void Add(CommentModel commentModel, string gameKey);

        /// <summary>
        /// Removes the specified comment from the database.
        /// </summary>
        /// <param name="commentModel">The comment model.</param>
        void Remove(CommentModel commentModel);

        /// <summary>
        /// Updates the specified comment.
        /// </summary>
        /// <param name="commentModel">The comment model.</param>
        void Update(CommentModel commentModel);

        #endregion
    }
}