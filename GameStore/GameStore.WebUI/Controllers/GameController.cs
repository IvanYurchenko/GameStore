using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;
using AutoMapper;
using BootstrapMvcSample.Controllers;
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

        public ActionResult Index()
        {
            return RedirectToAction("Games");
        }

        #region Get games lists

        [HttpGet]
        [ActionName("Games")]
        public ActionResult GetGames(GamesFilterViewModel filterViewModel)
        {
            var viewModel = new GameIndexViewModel();
            viewModel.Filter = filterViewModel;
            var filterModel = Mapper.Map<GamesFilterModel>(viewModel.Filter);
            viewModel.Games = _gameService.GetGamesByFilter(filterModel);

            viewModel.Filter.AvailablePlatformTypes = viewModel.Filter.AvailablePlatformTypes
                                                      ??
                                                      Mapper.Map<IEnumerable<PlatformTypeFilterViewModel>>(
                                                          _platformTypeService.GetAll());
            viewModel.Filter.AvailableGenres = viewModel.Filter.AvailableGenres
                                               ?? Mapper.Map<IEnumerable<GenreFilterViewModel>>(_genreService.GetAll());
            viewModel.Filter.AvailablePublishers = viewModel.Filter.AvailablePublishers
                                                   ??
                                                   Mapper.Map<IEnumerable<PublisherFilterViewModel>>(
                                                       _publisherService.GetAll());

            viewModel.Filter.Genres = viewModel.Filter.Genres ?? new List<int>();
            viewModel.Filter.PlatformTypes = viewModel.Filter.PlatformTypes ?? new List<int>();
            viewModel.Filter.Publishers = viewModel.Filter.Publishers ?? new List<int>();

            viewModel.Filter.SelectedGenres = viewModel.Filter.AvailableGenres.Select(x => x)
                .Where(x => viewModel.Filter.Genres.Select(id => id).ToList().Contains(x.GenreId));
            viewModel.Filter.SelectedPlatformTypes = viewModel.Filter.AvailablePlatformTypes.Select(x => x)
                .Where(x => viewModel.Filter.PlatformTypes.Select(id => id).ToList().Contains(x.PlatformTypeId));
            viewModel.Filter.SelectedPublishers = viewModel.Filter.AvailablePublishers.Select(x => x)
                .Where(x => viewModel.Filter.Publishers.Select(id => id).ToList().Contains(x.PublisherId));

            viewModel.Filter.Genres = viewModel.Filter.SelectedGenres.Select(g => g.GenreId).ToList();
            viewModel.Filter.PlatformTypes =
                viewModel.Filter.SelectedPlatformTypes.Select(g => g.PlatformTypeId).ToList();
            viewModel.Filter.Publishers = viewModel.Filter.SelectedPublishers.Select(g => g.PublisherId).ToList();

            return View(viewModel);
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
                return RedirectToAction("Index");
            }

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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Download")]
        public ActionResult DownloadGame(string key)
        {
            var gameModel = _gameService.GetGameModelByKey(key);
            var fileBytes = new byte[gameModel.Name.Length*sizeof (char)];
            Buffer.BlockCopy(gameModel.Name.ToCharArray(), 0, fileBytes, 0, fileBytes.Length);
            var fileName = String.Format("{0}.bin", gameModel.Name);
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }

        #endregion
    }
}