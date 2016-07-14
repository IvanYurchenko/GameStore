using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        /// <summary>
        /// Adds the comment for game with specified key to the database.
        /// </summary>
        /// <param name="commentModel">The comment model.</param>
        /// <param name="gameKey">The game key.</param>
        void Add(CommentModel commentModel, string gameKey);

        /// <summary>
        /// Removes the specified comment from the database.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        void Remove(int commentId);

        /// <summary>
        /// Updates the specified comment.
        /// </summary>
        /// <param name="commentModel">The comment model.</param>
        void Update(CommentModel commentModel);
    }
}