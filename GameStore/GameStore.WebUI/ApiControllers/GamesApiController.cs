using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.Resources;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Security;
using GameStore.WebUI.ViewModels;
using GameStore.WebUI.ViewModels.GamesFilters;
using GameStore.WebUI.ViewModels.Localization;

namespace GameStore.WebUI.ApiControllers
{
    [ApiExceptionLoggingFilter]
    public class GamesApiController : BaseApiController
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IBasketService _basketService;
        private readonly IPublisherService _publisherService;
        private readonly ILanguageService _languageService;
        private readonly ILogger _logger;

        public GamesApiController(
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

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager, User")]
        public HttpResponseMessage Get(GamesFilterViewModel filter, PaginationViewModel pagination)
        {
            var gameIndexViewModel = new GameIndexViewModel
            {
                Filter = filter,
                Pagination = pagination,
            };

            FillGameIndexViewModel(gameIndexViewModel);

            return Request.CreateResponse(HttpStatusCode.OK, gameIndexViewModel.Games);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager, User")]
        public HttpResponseMessage Get(int id)
        {
            GameModel gameModel = _gameService.GetGameModelById(id);

            if (gameModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var gameViewModel = Mapper.Map<GameViewModel>(gameModel);

            gameViewModel.PlatformTypes = Mapper.Map<IEnumerable<PlatformTypeViewModel>>(_platformTypeService.GetAll());
            gameViewModel.Genres = Mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());
            if (gameModel.PublisherId != null)
            {
                gameViewModel.Publisher =
                    Mapper.Map<PublisherViewModel>(_publisherService.GetModelById((int)gameModel.PublisherId));
            }

            return Request.CreateResponse(HttpStatusCode.OK, gameViewModel);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Post(GameAddUpdateViewModel gameAddUpdateViewModel)
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

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Put(GameAddUpdateViewModel gameAddUpdateViewModel)
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

                if (!_gameService.GameExists(gameModel.Key))
                {
                    _gameService.Add(gameModel);
                }
                else
                {
                    _gameService.Update(gameModel);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Delete(int id)
        {
            GameModel gameModel = _gameService.GetGameModelById(id);

            if (gameModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            
            _gameService.Remove(gameModel);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [ActionName("Comments")]
        [CustomApiAuthorize(Roles = "Admin, Moderator")]
        public HttpResponseMessage GetComment(int id, int actionId)
        {
            HttpResponseMessage result;

            GameModel gameModel = _gameService.GetGameModelById(id);

            if (gameModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            IEnumerable<CommentModel> commentModels = gameModel.Comments;
            CommentModel commentModel = commentModels.FirstOrDefault(c => c.CommentId == actionId);

            if (commentModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var commentViewModel = Mapper.Map<CommentViewModel>(commentModel);
            result = Request.CreateResponse(HttpStatusCode.OK, commentViewModel);

            return result;
        }

        [ActionName("Comments")]
        [CustomApiAuthorize(Roles = "Admin, User, Moderator")]
        public HttpResponseMessage AddComment(int id, CommentViewModel commentViewModel)
        {
            GameModel gameModel = _gameService.GetGameModelById(id);

            if (gameModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var commentModel = Mapper.Map<CommentModel>(commentViewModel);
            _commentService.Add(commentModel, gameModel.Key);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [ActionName("Comments")]
        [CustomApiAuthorize(Roles = "Admin, Moderator")]
        public HttpResponseMessage UpdateComment(int id, CommentViewModel commentViewModel)
        {
            GameModel gameModel = _gameService.GetGameModelById(id);
            
            if (gameModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var commentModel = Mapper.Map<CommentModel>(commentViewModel);
            _commentService.Update(commentModel);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [ActionName("Comments")]
        [CustomApiAuthorize(Roles = "Admin, Moderator")]
        public HttpResponseMessage RemoveComment(int id, int actionId)
        {
            GameModel gameModel = _gameService.GetGameModelById(id);

            if (gameModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _commentService.Remove(actionId);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [ActionName("Genres")]
        [CustomApiAuthorize(Roles = "Admin, Moderator, User, Manager")]
        public HttpResponseMessage Genres(int id)
        {
            GameModel gameModel = _gameService.GetGameModelById(id);

            if (gameModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var genreViewModels = Mapper.Map<IEnumerable<GenreViewModel>>(gameModel.Genres);

            return Request.CreateResponse(HttpStatusCode.OK, genreViewModels);
        }

        private void FillGameIndexViewModel(GameIndexViewModel gameIndexViewModel)
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