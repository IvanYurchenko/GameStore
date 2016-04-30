using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Filtering;
using GameStore.BLL.Filtering.Filters;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

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

            if (gameModel.PublisherId != null)
            {
                game.Publisher = _unitOfWork.PublisherRepository.GetById((int)gameModel.PublisherId);
            }

            if (gameModel.Comments != null)
            {
                game.Comments =
                    gameModel.Comments.Select(x => _unitOfWork.CommentRepository.GetById(x.CommentId))
                        .ToList();
            }

            if (gameModel.BasketItems != null)
            {
                game.BasketItems =
                    gameModel.BasketItems.Select(x => _unitOfWork.BasketItemRepository.GetById(x.BasketItemId)).ToList();
            }

            _unitOfWork.GameRepository.Insert(game);
            _unitOfWork.SaveChanges();
        }

        public void Remove(GameModel gameModel)
        {
            Game game = _unitOfWork.GameRepository.GetGameByKey(gameModel.Key);
            _unitOfWork.GameRepository.Delete(game);
            _unitOfWork.SaveChanges();
        }

        public void Update(GameModel gameModel)
        {
            Game game = _unitOfWork.GameRepository.GetById(gameModel.GameId);

            if (game.GameLocalizations != null)
            {
                game.GameLocalizations.ToList().ForEach(loc => _unitOfWork.GameLocalizationRepository.Delete(loc));
                _unitOfWork.SaveChanges();
            }

            Mapper.Map(gameModel, game);

            if (gameModel.PlatformTypes != null)
            {
                game.PlatformTypes =
                    gameModel.PlatformTypes.Select(pt => _unitOfWork.PlatformTypeRepository.GetById(pt.PlatformTypeId))
                        .ToList();
            }

            if (gameModel.Genres != null)
            {
                game.Genres =
                    gameModel.Genres.Select(g => _unitOfWork.GenreRepository.GetById(g.GenreId))
                        .ToList();
            }

            if (gameModel.PublisherId != null)
            {
                game.PublisherId = gameModel.PublisherId;
            }
            
            _unitOfWork.GameRepository.Update(game);
            _unitOfWork.SaveChanges();
        }

        public GameModel GetGameModelByKey(string key)
        {
            Game game = _unitOfWork.GameRepository.GetGameByKey(key);
            var gameModel = Mapper.Map<GameModel>(game);
            return gameModel;
        }

        public GameModel GetGameModelById(int id)
        {
            Game game = _unitOfWork.GameRepository.GetById(id);
            var gameModel = Mapper.Map<GameModel>(game);
            return gameModel;
        }

        public bool GameExists(string key, int currentGameId = 0)
        {
            bool result = _unitOfWork.GameRepository.Get(g => g.Key == key && g.GameId != currentGameId).Any();
            return result;
        }

        public IEnumerable<GameModel> GetAll()
        {
            IEnumerable<Game> games = _unitOfWork.GameRepository.GetAll();
            var gameModels = Mapper.Map<IEnumerable<GameModel>>(games);
            return gameModels;
        }

        public IEnumerable<GameModel> GetGamesByGenre(GenreModel genreModel)
        {
            Genre genre = _unitOfWork.GenreRepository.GetById(genreModel.GenreId);
            IEnumerable<Game> games = _unitOfWork.GameRepository.GetAll().Where(g => g.Genres.Contains(genre));
            var gameModels = Mapper.Map<IEnumerable<GameModel>>(games);
            return gameModels;
        }

        public IEnumerable<GameModel> GetGamesByPlatformType(PlatformTypeModel platformTypeModel)
        {
            PlatformType platformType = _unitOfWork.PlatformTypeRepository.GetById(platformTypeModel.PlatformTypeId);
            IEnumerable<Game> games = _unitOfWork.GameRepository.GetAll().Where(g => g.PlatformTypes.Contains(platformType));
            var gameModels = Mapper.Map<IEnumerable<GameModel>>(games);
            return gameModels;
        }

        public IEnumerable<GameModel> GetGamesByPublisher(PublisherModel publisherModel)
        {
            Publisher publisher = _unitOfWork.PublisherRepository.GetById(publisherModel.PublisherId);
            IEnumerable<Game> games = _unitOfWork.GameRepository.GetAll().Where(g => g.Publisher == publisher);
            var gameModels = Mapper.Map<IEnumerable<GameModel>>(games);
            return gameModels;
        }

        public GamesTransferModel GetGamesByFilter(GamesFilterModel filterModel, PaginationModel paginationModel)
        {
            var container = new GameFilterContainer
            {
                Model = filterModel,
            };

            IPipeline<GameFilterContainer> pipeline = GetPipelineWithFilters();
            pipeline.ExecuteAll(container);
            Func<Game, bool> resultCondition = CombinePredicate<Game>.CombineWithAnd(container.Conditions);
            
            IEnumerable<Game> games = _unitOfWork.GameRepository.GetMany(
                resultCondition, (int) paginationModel.PageCapacity, paginationModel.CurrentPage,
                container.SortCondition);
            var gameModels = Mapper.Map<IEnumerable<GameModel>>(games);

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
    }
}