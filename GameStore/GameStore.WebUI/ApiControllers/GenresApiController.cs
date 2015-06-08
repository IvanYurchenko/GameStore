using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Localization;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Security;
using GameStore.WebUI.ViewModels;
using GameStore.WebUI.ViewModels.Localization;

namespace GameStore.WebUI.ApiControllers
{
    [ApiExceptionLoggingFilter]
    public class GenresApiController : BaseApiController
    {
        private readonly IGenreService _genreService;
        private readonly IGameService _gameService;
        private readonly ILanguageService _languageService;

        public GenresApiController(
            IGameService gameService,
            IGenreService genreService,
            ILanguageService languageService)
        {
            _gameService = gameService;
            _genreService = genreService;
            _languageService = languageService;
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Get()
        {
            IEnumerable<GenreModel> genreModels = _genreService.GetAll();

            var genreViewModels = Mapper.Map<IEnumerable<GenreViewModel>>(genreModels);

            return Request.CreateResponse(HttpStatusCode.OK, genreViewModels);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Get(int id)
        {
            var genreModel = _genreService.GetModelById(id);

            if (genreModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var genreViewModel = Mapper.Map<GenreViewModel>(genreModel);

            return Request.CreateResponse(HttpStatusCode.OK, genreViewModel);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Post(GenreAddUpdateViewModel genreAddUpdateViewModel)
        {
            var englishLocalization = GetLocalization(genreAddUpdateViewModel, "en");

            if (IsLocalizationEmpty(englishLocalization))
            {
                ModelState.AddModelError("LocalizationError", "English localization should exist. ");
            }
            else
            {
                CheckAndFixLocalizations(genreAddUpdateViewModel);
            }

            if (ModelState.IsValid)
            {
                CleanEmptyLocalizations(genreAddUpdateViewModel);

                var genreModel = Mapper.Map<GenreModel>(genreAddUpdateViewModel);

                _genreService.Add(genreModel);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Put(GenreAddUpdateViewModel genreAddUpdateViewModel)
        {
            var englishLocalization = GetLocalization(genreAddUpdateViewModel, "en");

            if (IsLocalizationEmpty(englishLocalization))
            {
                ModelState.AddModelError("localizationError", "English localization should exist. ");
            }
            else
            {
                CheckAndFixLocalizations(genreAddUpdateViewModel);
            }

            if (ModelState.IsValid)
            {
                CleanEmptyLocalizations(genreAddUpdateViewModel);

                var genreModel = Mapper.Map<GenreModel>(genreAddUpdateViewModel);

                if (!_genreService.GenreExists(englishLocalization.Name))
                {
                    _genreService.Add(genreModel);
                }
                else
                {

                    _genreService.Update(genreModel);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Delete(int id)
        {
            GenreModel genreModel = _genreService.GetModelById(id);

            if (genreModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _genreService.Remove(id);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [ActionName("Games")]
        [CustomApiAuthorize(Roles = "Admin, Moderator, User, Manager")]
        public HttpResponseMessage Games(int id)
        {
            GenreModel genreModel = _genreService.GetModelById(id);

            if (genreModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            IEnumerable<GameModel> gameModels = _gameService.GetGamesByGenre(genreModel);

            var gameViewModels = Mapper.Map<IEnumerable<GameViewModel>>(gameModels);

            return Request.CreateResponse(HttpStatusCode.OK, gameViewModels);
        }

        private void CheckAndFixLocalizations(GenreAddUpdateViewModel genreAddUpdateViewModel)
        {
            List<LanguageModel> languages = _languageService.GetAll().ToList();

            foreach (var genreLocalizationViewModel in genreAddUpdateViewModel.GenreLocalizations)
            {
                LanguageModel languageModel =
                    languages.FirstOrDefault(l =>
                        String.Equals(l.Code, genreLocalizationViewModel.Language.Code, StringComparison.CurrentCultureIgnoreCase));

                if (languageModel == null)
                {
                    ModelState.AddModelError("LocalizationError", "Incorrect localization language. ");
                }
                else
                {
                    genreLocalizationViewModel.Language = Mapper.Map<LanguageViewModel>(languageModel);
                }
            }
        }

        private static GenreLocalizationViewModel GetLocalization(GenreAddUpdateViewModel genreAddUpdateViewModel, string languageCode)
        {
            if (genreAddUpdateViewModel == null || genreAddUpdateViewModel.GenreLocalizations == null)
            {
                return null;
            }

            GenreLocalizationViewModel result = genreAddUpdateViewModel.GenreLocalizations
                .FirstOrDefault(loc => String.Equals(loc.Language.Code, languageCode, StringComparison.CurrentCultureIgnoreCase));

            return result;
        }

        private static bool IsLocalizationEmpty(GenreLocalizationViewModel genreLocalizationViewModel)
        {
            var result = genreLocalizationViewModel == null ||
                         String.IsNullOrEmpty(genreLocalizationViewModel.Name);

            return result;
        }

        private static void CleanEmptyLocalizations(GenreAddUpdateViewModel genreAddUpdateViewModel)
        {
            List<GenreLocalizationViewModel> emptyLocalizations = genreAddUpdateViewModel.GenreLocalizations.Where(IsLocalizationEmpty).ToList();

            foreach (var genreLocalizationViewModel in emptyLocalizations)
            {
                genreAddUpdateViewModel.GenreLocalizations.Remove(genreLocalizationViewModel);
            }
        }
    }
}
