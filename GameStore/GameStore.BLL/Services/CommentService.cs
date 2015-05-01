using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;

namespace GameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(CommentModel commentModel, string gameKey)
        {
            var game = _unitOfWork.GameRepository.GetGameByKey(gameKey);
            commentModel.GameId = game.GameId;

            var comment = Mapper.Map<Comment>(commentModel);
            _unitOfWork.CommentRepository.Insert(comment);
            _unitOfWork.SaveChanges();
        }

        public void Remove(int commentId)
        {
            var comment = _unitOfWork.CommentRepository.GetById(commentId);
            comment.IsRemoved = true;
            _unitOfWork.CommentRepository.Update(comment);
            _unitOfWork.SaveChanges();
        }

        public void Update(CommentModel commentModel)
        {
            var comment = _unitOfWork.CommentRepository.GetById(commentModel.CommentId);
            Mapper.Map(commentModel, comment);
            _unitOfWork.CommentRepository.Update(comment);
            _unitOfWork.SaveChanges();
        }
    }
}