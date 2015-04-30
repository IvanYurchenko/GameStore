using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Filtering;
using GameStore.BLL.Filtering.Filters;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(GameModel gameModel)
        {
            var game = Mapper.Map<Game>(gameModel);

            if (gameModel.Genres != null)
            {
                game.Genres = gameModel.Genres.Select(x => _unitOfWork.GenreRepository.GetById(x.GenreId)).ToList();
            }

            if (gameModel.PlatformTypes != null)
            {
                game.PlatformTypes = gameModel.PlatformTypes
                    .Select(x => _unitOfWork.PlatformTypeRepository.GetById(x.PlatformTypeId)).ToList();
            }

            if (gameModel.Publisher != null)
            {
                game.Publisher = _unitOfWork.PublisherRepository.GetById(gameModel.PublisherId);
            }

            if (gameModel.Comments != null)
            {
                game.Comments =
                    gameModel.Comments.Select(x => _unitOfWork.CommentRepository.GetById(x.CommentId)).ToList();
            }

            if (gameModel.BasketItems != null)
            {
                game.BasketItems =
                    gameModel.BasketItems.Select(x => _unitOfWork.BasketItemRepository.GetById(x.BasketItemId)).ToList();
            }

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
            var game = _unitOfWork.GameRepository.GetById(gameModel.GameId);
            Mapper.Map(gameModel, game);

            if (gameModel.PlatformTypes != null)
            {
                game.PlatformTypes =
                    game.PlatformTypes.Where(
                        pl => gameModel.PlatformTypes.Any(p => p.PlatformTypeId == pl.PlatformTypeId)).ToList();

                gameModel.PlatformTypes =
                    gameModel.PlatformTypes.Where(
                        pl => gameModel.PlatformTypes.All(p => p.PlatformTypeId != pl.PlatformTypeId)).ToList();

                game.PlatformTypes =
                    game.PlatformTypes.Union(
                        gameModel.PlatformTypes.Select(p => _unitOfWork.PlatformTypeRepository.GetById(p.PlatformTypeId)))
                        .ToList();
            }

            if (gameModel.Genres != null)
            {
                game.Genres =
                    game.Genres.Where(
                        pl => gameModel.Genres.Any(p => p.GenreId == pl.GenreId)).ToList();

                gameModel.Genres =
                    gameModel.Genres.Where(
                        genre => gameModel.Genres.All(g => g.GenreId != genre.GenreId)).ToList();

                game.Genres =
                    game.Genres.Union(
                        gameModel.Genres.Select(g => _unitOfWork.GenreRepository.GetById(g.GenreId)))
                        .ToList();
            }

            if (gameModel.Publisher != null)
            {
                game.PublisherId = gameModel.PublisherId;
            }

            _unitOfWork.GameRepository.Update(game);
            _unitOfWork.Save();
        }

        public GameModel GetGameModelByKey(string key)
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


        public bool GameExists(string key, int currentGameId)
        {
            bool result = _unitOfWork.GameRepository.Get(g => g.Key == key && g.GameId != currentGameId).Any();
            return result;
        }


        public IEnumerable<GameModel> GetAll()
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

        public GamesTransferModel GetGamesByFilter(GamesFilterModel filterModel, PaginationModel paginationModel)
        {
            var container = new GameFilterContainer
            {
                Model = filterModel,
            };

            var pipeLine = GetPipelineWithFilters();
            pipeLine.ExecuteAll(container);
            var resultCondition = CombinePredicate<Game>.CombineWithAnd(container.Conditions);

            IEnumerable<Game> games = _unitOfWork.GameRepository.GetMany(
                resultCondition, (int) paginationModel.PageCapacity, paginationModel.CurrentPage,
                container.SortCondition);
            IEnumerable<GameModel> gameModels = Mapper.Map<IEnumerable<GameModel>>(games);

            paginationModel.ItemsNumber = _unitOfWork.GameRepository.GetCount(resultCondition);

            var transferModel = new GamesTransferModel
            {
                Games = gameModels,
                PaginationModel = paginationModel,
            };

            return transferModel;
        }

        public int GetGamesCount(Func<Game, bool> filterCondition = null)
        {
            return _unitOfWork.GameRepository.GetCount(filterCondition);
        }

        #region Helpers

        private static IPipeline<GameFilterContainer> GetPipelineWithFilters()
        {
            var pipeline = new Pipeline<GameFilterContainer>();

            pipeline.BeginRegister(new PriceFilter())
                .Register(new NameFilter())
                .Register(new PlatformTypeFilter())
                .Register(new PublisherFilter())
                .Register(new GenreFilter())
                .Register(new DateFilter())
                .Register(new SortingFilter());

            return pipeline;
        }

        #endregion
    }
}