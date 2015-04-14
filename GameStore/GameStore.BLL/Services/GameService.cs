using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(GameModel gameModel)
        {
            var game = Mapper.Map<Game>(gameModel);
            _unitOfWork.GameRepository.Insert(game);
            _unitOfWork.Save();
        }

        public void Remove(GameModel gameModel)
        {
            var game = _unitOfWork.GameRepository.GetGameByKey(gameModel.Key);
            _unitOfWork.GameRepository.Delete(game);
            _unitOfWork.Save();
        }

        public void Update(GameModel gameModel)
        {
            var game = _unitOfWork.GameRepository.GetGameByKey(gameModel.Key);
            Mapper.Map(gameModel, game);
            _unitOfWork.GameRepository.Update(game);
            _unitOfWork.Save();
        }

        public int GetGamesCount()
        {
            return _unitOfWork.GameRepository.GetCount();
        }

        public GameModel GetGameModelByKey(String key)
        {
            var game = _unitOfWork.GameRepository.GetGameByKey(key);
            var gameModel = Mapper.Map<GameModel>(game);
            return gameModel;
        }

        public GameModel GetGameModelById(int id)
        {
            var game = _unitOfWork.GameRepository.GetById(id);
            var gameModel = Mapper.Map<GameModel>(game);
            return gameModel;
        }

        public bool GameExists(string key)
        {

            try
            {
                var game = _unitOfWork.GameRepository.GetGameByKey(key);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<GameModel> GetAllGames()
        {
            var games = _unitOfWork.GameRepository.GetAll();
            var gameModels = Mapper.Map<IEnumerable<GameModel>>(games);
            return gameModels;
        }

        public IEnumerable<GameModel> GetGamesByGenre(GenreModel genreModel)
        {
            var genre = _unitOfWork.GenreRepository.GetById(genreModel.GenreId);
            var games = _unitOfWork.GameRepository.GetAll().Where(g => g.Genres.Contains(genre));
            var gameModels = Mapper.Map<IEnumerable<GameModel>>(games);
            return gameModels;
        }

        public IEnumerable<GameModel> GetGamesByPlatformType(PlatformTypeModel platformTypeModel)
        {
            var platformType = _unitOfWork.PlatformTypeRepository.GetById(platformTypeModel.PlatformTypeId);
            var games = _unitOfWork.GameRepository.GetAll().Where(g => g.PlatformTypes.Contains(platformType));
            var gameModels = Mapper.Map<IEnumerable<GameModel>>(games);
            return gameModels;
        }

    }
}
