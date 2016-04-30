using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Localization;
using GameStore.Core;
using GameStore.Resources;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Security;
using GameStore.WebUI.ViewModels;
using GameStore.WebUI.ViewModels.GamesFilters;
using GameStore.WebUI.ViewModels.Localization;

namespace GameStore.WebUI.Controllers
{
    [ExceptionLoggingFilter]
    [PerformanceLoggingFilter]
    public class GameController : BaseController
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IBasketService _basketService;
        private readonly IPublisherService _publisherService;
        private readonly ILanguageService _languageService;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameController" /> class.
        /// </summary>
        /// <param name="gameService">The game service.</param>
        /// <param name="commentService">The comment service.</param>
        /// <param name="genreService">The genre service.</param>
        /// <param name="platformTypeService">The platform type service.</param>
        /// <param name="basketService">The basket service.</param>
        /// <param name="publisherService">The publisher service.</param>
        /// <param name="languageService">The language service.</param>
        /// <param name="logger">The logger.</param>
        public GameController(
            IGameService gameService,
            ICommentService commentService,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IBasketService basketService,
            IPublisherService publisherService,
            ILanguageService languageService,
            ILogger logger)
        {
            _gameService = gameService;
            _commentService = commentService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _basketService = basketService;
            _publisherService = publisherService;
            _languageService = languageService;
            _logger = logger;
        }

        /// <summary>
        /// Returns list of all games with filter form.
        /// </summary>
        /// <param name="gameIndexViewModel">The game index view model.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Get")]
        public ActionResult GetGames(GameIndexViewModel gameIndexViewModel)
        {
            gameIndexViewModel = FillGameIndexViewModel(gameIndexViewModel);
            return View(gameIndexViewModel);
        }

        /// <summary>
        /// Gets the game details by game key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Details")]
        public ActionResult GetGameDetails(string key)
        {
            GameModel gameModel = _gameService.GetGameModelByKey(key);
            var gameViewModel = Mapper.Map<GameViewModel>(gameModel);

            gameViewModel.PlatformTypes = Mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
            gameViewModel.Genres = Mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            if (gameModel.PublisherId != null)
            {
                gameViewModel.Publisher =
                    Mapper.Map<PublisherViewModel>(_publisherService.GetModelById((int)gameModel.PublisherId));
            }

            return View(gameViewModel);
        }

        [HttpGet]
        [ActionName("New")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult AddGame()
        {
            var gameAddUpdateViewModel = new GameAddUpdateViewModel();
            gameAddUpdateViewModel.AddedDate = DateTime.Now;
            
            AdjustCollections(gameAddUpdateViewModel);

            AdjustLocalizations(gameAddUpdateViewModel);

            return View(gameAddUpdateViewModel);
        }

        /// <summary>
        /// Adds the game.
        /// </summary>
        /// <param name="gameAddUpdateViewModel">The game view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("New")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult AddGame(GameAddUpdateViewModel gameAddUpdateViewModel)
        {
            GameLocalizationViewModel englishLocalization = GetLocalization(gameAddUpdateViewModel, "en");

            if (IsLocalizationEmpty(englishLocalization))
            {
                ModelState.AddModelError("localizationError", GlobalRes.EnglishLocalizationShouldExist);
            }

            if (ModelState.IsValid)
            {
                CleanEmptyLocalizations(gameAddUpdateViewModel);

                var gameModel = Mapper.Map<GameModel>(gameAddUpdateViewModel);
                _gameService.Add(gameModel);

                MessageSuccess("The game has been added successfully!");
                return RedirectToAction("Get");
            }
            
            AdjustCollections(gameAddUpdateViewModel);

            return View(gameAddUpdateViewModel);
        }

        [HttpGet]
        [ActionName("Update")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult UpdateGame(string key)
        {
            GameModel gameModel = _gameService.GetGameModelByKey(key);

            if (gameModel.IsReadonly)
            {
                return RedirectToAction("Get", "Game");
            }

            var gameAddUpdateViewModel = Mapper.Map<GameAddUpdateViewModel>(gameModel);

            AdjustCollections(gameAddUpdateViewModel);
            
            AdjustLocalizations(gameAddUpdateViewModel);

            return View(gameAddUpdateViewModel);
        }

        /// <summary>
        /// Updates the game.
        /// </summary>
        /// <param name="gameAddUpdateViewModel">The game view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Update")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult UpdateGame(GameAddUpdateViewModel gameAddUpdateViewModel)
        {
            GameLocalizationViewModel englishLocalization = GetLocalization(gameAddUpdateViewModel, "en");

            if (IsLocalizationEmpty(englishLocalization))
            {
                ModelState.AddModelError("LocalizationError", GlobalRes.EnglishLocalizationShouldExist);
            }

            if (ModelState.IsValid)
            {
                CleanEmptyLocalizations(gameAddUpdateViewModel);

                var gameModel = Mapper.Map<GameModel>(gameAddUpdateViewModel);
                _gameService.Update(gameModel);
                MessageSuccess("The game has been updated successfully!");
                return RedirectToAction("Get");
            }

            return View(gameAddUpdateViewModel);
        }

        /// <summary>
        /// Removes the game.
        /// </summary>
        /// <param name="key">The game key.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Remove")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult RemoveGame(string key)
        {
                GameModel gameModel = _gameService.GetGameModelByKey(key);
                _gameService.Remove(gameModel);
                MessageSuccess("The game has been removed successfully!");
                return RedirectToAction("Get");
        }

        /// <summary>
        /// Downloads the game.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Download")]
        [CustomAuthorize(Roles = "User")]
        public ActionResult DownloadGame(string key)
        {
            GameModel gameModel = _gameService.GetGameModelByKey(key);
            string gameName = gameModel.GameLocalizations.First(loc =>
                String.Equals(loc.Language.Code, Constants.EnglishLanguageCode, StringComparison.CurrentCultureIgnoreCase)).Name;

            var fileBytes = new byte[gameName.Length * sizeof(char)];
            Buffer.BlockCopy(gameName.ToCharArray(), 0, fileBytes, 0, fileBytes.Length);
            string fileName = String.Format("{0}.bin", gameName);

            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }

        private GameIndexViewModel FillGameIndexViewModel(GameIndexViewModel gameIndexViewModel)
        {
            gameIndexViewModel = gameIndexViewModel ?? new GameIndexViewModel();
            gameIndexViewModel.Filter = gameIndexViewModel.Filter ?? new GamesFilterViewModel();

            var filterModel = Mapper.Map<GamesFilterModel>(gameIndexViewModel.Filter);

            gameIndexViewModel.Pagination = gameIndexViewModel.Pagination ?? new PaginationViewModel();

            var paginationModel = Mapper.Map<PaginationModel>(gameIndexViewModel.Pagination);

            GamesTransferModel transferModel = _gameService.GetGamesByFilter(filterModel, paginationModel);
            gameIndexViewModel.Games = Mapper.Map<IEnumerable<GameViewModel>>(transferModel.Games);
            gameIndexViewModel.Pagination = Mapper.Map<PaginationViewModel>(transferModel.PaginationModel);

            gameIndexViewModel.Filter.AvailablePlatformTypes =
                Mapper.Map<IEnumerable<PlatformTypeFilterViewModel>>(_platformTypeService.GetAll());
            gameIndexViewModel.Filter.AvailableGenres =
                Mapper.Map<IEnumerable<GenreFilterViewModel>>(_genreService.GetAll());
            gameIndexViewModel.Filter.AvailablePublishers =
                Mapper.Map<IEnumerable<PublisherFilterViewModel>>(_publisherService.GetAll());

            gameIndexViewModel.Filter.Genres = gameIndexViewModel.Filter.Genres ?? new List<int>();
            gameIndexViewModel.Filter.PlatformTypes = gameIndexViewModel.Filter.PlatformTypes ?? new List<int>();
            gameIndexViewModel.Filter.Publishers = gameIndexViewModel.Filter.Publishers ?? new List<int>();

            gameIndexViewModel.Filter.SelectedGenres = gameIndexViewModel.Filter.AvailableGenres
                .Where(x => gameIndexViewModel.Filter.Genres.Contains(x.GenreId));
            gameIndexViewModel.Filter.SelectedPlatformTypes = gameIndexViewModel.Filter.AvailablePlatformTypes
                .Where(x => gameIndexViewModel.Filter.PlatformTypes.Contains(x.PlatformTypeId));
            gameIndexViewModel.Filter.SelectedPublishers = gameIndexViewModel.Filter.AvailablePublishers
                .Where(x => gameIndexViewModel.Filter.Publishers.Contains(x.PublisherId));

            return gameIndexViewModel;
        }

        private void AdjustCollections(GameAddUpdateViewModel gameAddUpdateViewModel)
        {
            gameAddUpdateViewModel.PlatformTypes = Mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
            gameAddUpdateViewModel.Genres = Mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            gameAddUpdateViewModel.Publishers = Mapper.Map<IEnumerable<PublisherViewModel>>(_publisherService.GetAll());
        }

        private void AdjustLocalizations(GameAddUpdateViewModel gameAddUpdateViewModel)
        {
            IEnumerable<LanguageModel> languages = _languageService.GetAll();

            gameAddUpdateViewModel.GameLocalizations = gameAddUpdateViewModel.GameLocalizations
                ?? new List<GameLocalizationViewModel>();

            foreach (LanguageModel languageModel in languages)
            {
                if (GetLocalization(gameAddUpdateViewModel, languageModel.Code) == null)
                {
                    var gameLocalization = new GameLocalizationViewModel
                    {
                        LanguageId = languageModel.LanguageId,
                        Language = Mapper.Map<LanguageViewModel>(languageModel),
                    };

                    gameAddUpdateViewModel.GameLocalizations.Add(gameLocalization);
                }
            }
        }

        private static GameLocalizationViewModel GetLocalization(GameAddUpdateViewModel gameAddUpdateViewModel, string languageCode)
        {
            if (gameAddUpdateViewModel == null || gameAddUpdateViewModel.GameLocalizations == null)
            {
                return null;
            }

            GameLocalizationViewModel result = gameAddUpdateViewModel.GameLocalizations
                .FirstOrDefault(loc => String.Equals(loc.Language.Code, languageCode, StringComparison.CurrentCultureIgnoreCase));

            return result;
        }

        private static bool IsLocalizationEmpty(GameLocalizationViewModel gameLocalizationViewModel)
        {
            bool result = gameLocalizationViewModel == null ||
                         String.IsNullOrEmpty(gameLocalizationViewModel.Name) ||
                         String.IsNullOrEmpty(gameLocalizationViewModel.Description);

            return result;
        }

        private static void CleanEmptyLocalizations(GameAddUpdateViewModel gameAddUpdateViewModel)
        {
            List<GameLocalizationViewModel> emptyLocalizations =
                gameAddUpdateViewModel.GameLocalizations.Where(IsLocalizationEmpty).ToList();

            foreach (GameLocalizationViewModel gameLocalizationViewModel in emptyLocalizations)
            {
                gameAddUpdateViewModel.GameLocalizations.Remove(gameLocalizationViewModel);
            }
        }
    }
}