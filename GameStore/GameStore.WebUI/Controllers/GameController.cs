using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;
using AutoMapper;
using BootstrapMvcSample.Controllers;
using BootstrapSupport;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebUI.Filters;
using GameStore.WebUI.ViewModels;
using GameStore.WebUI.ViewModels.GamesFiltersViewModels;

namespace GameStore.WebUI.Controllers
{
    [ExceptionLoggingFilter]
    [PerformanceLoggingFilter]
    public class GameController : BootstrapBaseController
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IBasketService _basketService;
        private readonly IPublisherService _publisherService;
        private readonly ILogger _logger;

        public GameController(
            IGameService gameService,
            ICommentService commentService,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IBasketService basketService,
            IPublisherService publisherService,
            ILogger logger)
        {
            _gameService = gameService;
            _commentService = commentService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _basketService = basketService;
            _publisherService = publisherService;
            _logger = logger;
        }

        #region Get games lists
        [HttpGet]
        [ActionName("Get")]
        public ActionResult GetGames(GameIndexViewModel gameIndexViewModel)
        {
            gameIndexViewModel = gameIndexViewModel ?? new GameIndexViewModel();
            gameIndexViewModel.Filter = gameIndexViewModel.Filter ?? new GamesFilterViewModel();

            var filterModel = Mapper.Map<GamesFilterModel>(gameIndexViewModel.Filter);

            if (gameIndexViewModel.Pagination == null)
            {
                gameIndexViewModel.Pagination = new PaginationViewModel();
            }

            var paginationModel = Mapper.Map<PaginationModel>(gameIndexViewModel.Pagination);

            GamesTransferModel transferModel = _gameService.GetGamesByFilter(filterModel, paginationModel);
            gameIndexViewModel.Games = transferModel.Games;
            gameIndexViewModel.Pagination = Mapper.Map<PaginationViewModel>(transferModel.PaginationModel);

            gameIndexViewModel.Filter.AvailablePlatformTypes = Mapper.Map<IEnumerable<PlatformTypeFilterViewModel>>(_platformTypeService.GetAll());
            gameIndexViewModel.Filter.AvailableGenres = Mapper.Map<IEnumerable<GenreFilterViewModel>>(_genreService.GetAll());
            gameIndexViewModel.Filter.AvailablePublishers = Mapper.Map<IEnumerable<PublisherFilterViewModel>>(_publisherService.GetAll());

            gameIndexViewModel.Filter.Genres = gameIndexViewModel.Filter.Genres ?? new List<int>();
            gameIndexViewModel.Filter.PlatformTypes = gameIndexViewModel.Filter.PlatformTypes ?? new List<int>();
            gameIndexViewModel.Filter.Publishers = gameIndexViewModel.Filter.Publishers ?? new List<int>();

            gameIndexViewModel.Filter.SelectedGenres = gameIndexViewModel.Filter.AvailableGenres
                .Where(x => gameIndexViewModel.Filter.Genres.Contains(x.GenreId));
            gameIndexViewModel.Filter.SelectedPlatformTypes = gameIndexViewModel.Filter.AvailablePlatformTypes
                .Where(x => gameIndexViewModel.Filter.PlatformTypes.Contains(x.PlatformTypeId));
            gameIndexViewModel.Filter.SelectedPublishers = gameIndexViewModel.Filter.AvailablePublishers
                .Where(x => gameIndexViewModel.Filter.Publishers.Contains(x.PublisherId));

            return View(gameIndexViewModel); 
        }

        #endregion

        #region Working with a single game

        [HttpGet]
        [ActionName("Details")]
        public ActionResult GetGameDetails(string key)
        {
            GameModel gameModel = _gameService.GetGameModelByKey(key);
            var gameViewModel = Mapper.Map<GameViewModel>(gameModel);
            gameViewModel.PlatformTypes = _platformTypeService.GetAll();
            gameViewModel.Genres = _genreService.GetAll();
            gameViewModel.Publisher = _publisherService.GetModelById(gameModel.PublisherId);

            return View(gameViewModel);
        }

        [HttpGet]
        [ActionName("New")]
        public ActionResult AddGame()
        {
            var gameViewModel = new GameViewModel();
            gameViewModel.AddedDate = DateTime.Now;
            gameViewModel.PlatformTypes = _platformTypeService.GetAll();
            gameViewModel.Genres = _genreService.GetAll();
            gameViewModel.Publishers = _publisherService.GetAll();

            return View(gameViewModel);
        }

        [HttpPost]
        [ActionName("New")]
        public ActionResult AddGame(GameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                var gameModel = Mapper.Map<GameModel>(gameViewModel);
                _gameService.Add(gameModel);

                MessageSuccess("The game has been added successfully!");
                return RedirectToAction("Get");
            }
            
            TempData.Add(Alerts.ERROR, "Model state is not valid.");

            gameViewModel.PlatformTypes = _platformTypeService.GetAll();
            gameViewModel.Genres = _genreService.GetAll();
            gameViewModel.Publishers = _publisherService.GetAll();
            return View(gameViewModel);
        }

        [HttpGet]
        [ActionName("Update")]
        public ActionResult UpdateGame(string key)
        {
            GameModel gameModel = _gameService.GetGameModelByKey(key);
            var gameViewModel = Mapper.Map<GameViewModel>(gameModel);
            gameViewModel.SelectedGenresIds.AddRange(gameViewModel.Genres.Select(g => g.GenreId));
            gameViewModel.Genres = _genreService.GetAll();
            gameViewModel.SelectedPlatformTypesIds.AddRange(gameViewModel.PlatformTypes.Select(g => g.PlatformTypeId));
            gameViewModel.PlatformTypes = _platformTypeService.GetAll();
            gameViewModel.SelectedPublisherId = gameModel.PublisherId;
            gameViewModel.Publishers = _publisherService.GetAll();

            return View(gameViewModel);
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult UpdateGame(GameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                var gameModel = Mapper.Map<GameModel>(gameViewModel);
                _gameService.Update(gameModel);
                MessageSuccess("The game has been updated successfully!");
                return RedirectToAction("Get");
            }

            return View(gameViewModel);
        }

        [HttpGet]
        [ActionName("Remove")]
        public ActionResult RemoveGame(string key)
        {
            GameModel gameModel = _gameService.GetGameModelByKey(key);
            GameViewModel gameViewModel = Mapper.Map<GameViewModel>(gameModel);
            gameViewModel.PlatformTypes = _platformTypeService.GetAll();
            gameViewModel.Genres = _genreService.GetAll();
            gameViewModel.Publisher = _publisherService.GetModelById(gameModel.PublisherId);

            return View(gameViewModel);
        }

        [HttpPost]
        [ActionName("Remove")]
        public ActionResult RemoveGame(GameViewModel gameViewModel)
        {
            GameModel gameModel = _gameService.GetGameModelByKey(gameViewModel.Key);
            _gameService.Remove(gameModel);
            MessageSuccess("The game has been removed successfully!");
            return RedirectToAction("Get");
        }

        [HttpGet]
        [ActionName("Download")]
        public ActionResult DownloadGame(string key)
        {
            var gameModel = _gameService.GetGameModelByKey(key);
            var fileBytes = new byte[gameModel.Name.Length * sizeof(char)];
            Buffer.BlockCopy(gameModel.Name.ToCharArray(), 0, fileBytes, 0, fileBytes.Length);
            var fileName = String.Format("{0}.bin", gameModel.Name);
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }

        #endregion
    }
}