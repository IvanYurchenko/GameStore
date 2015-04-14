using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CommentModel ConvertToModel(Comment comment)
        {
            CommentModel result = Mapper.Map<CommentModel>(comment);
            return result;
        }

        public Comment ConvertToComment(CommentModel commentModel)
        {
            Comment result = Mapper.Map<Comment>(commentModel);
            return result;
        }

        public void Add(CommentModel commentModel, string gameKey)
        {
            var game = _unitOfWork.GameRepository.GetGameByKey(gameKey);
            commentModel.GameId = game.GameId;

            var comment = ConvertToComment(commentModel);
            _unitOfWork.CommentRepository.Insert(comment);
            _unitOfWork.Save();


        }

        public void Remove(CommentModel commentModel)
        {
            var comment = _unitOfWork.CommentRepository.GetById(commentModel.CommentId);
            _unitOfWork.CommentRepository.Delete(comment);
            _unitOfWork.Save();
        }

        public void Update(CommentModel commentModel)
        {
            var comment = _unitOfWork.CommentRepository.GetById(commentModel.CommentId);
            Mapper.Map(commentModel, comment);
            _unitOfWork.CommentRepository.Update(comment);
            _unitOfWork.Save();
        }

    }
}
