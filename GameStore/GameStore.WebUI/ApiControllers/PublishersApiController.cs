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
    public class PublishersApiController : BaseApiController
    {
        private readonly IGameService _gameService;
        private readonly IPublisherService _publisherService;
        private readonly ILanguageService _languageService;

        public PublishersApiController(
            IGameService gameService,
            IPublisherService publisherService,
            ILanguageService languageService)
        {
            _gameService = gameService;
            _publisherService = publisherService;
            _languageService = languageService;
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager, User")]
        public HttpResponseMessage Get()
        {
            IEnumerable<PublisherModel> publisherModels = _publisherService.GetAll();

            var publisherViewModels = Mapper.Map<IEnumerable<PublisherViewModel>>(publisherModels);

            return Request.CreateResponse(HttpStatusCode.OK, publisherViewModels);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager, User")]
        public HttpResponseMessage Get(int id)
        {
            var publisherModel = _publisherService.GetModelById(id);

            if (publisherModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var publisherViewModel = Mapper.Map<PublisherViewModel>(publisherModel);

            return Request.CreateResponse(HttpStatusCode.OK, publisherViewModel);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Post(PublisherAddUpdateViewModel publisherAddUpdateViewModel)
        {
            var englishLocalization = GetLocalization(publisherAddUpdateViewModel, "en");

            if (IsLocalizationEmpty(englishLocalization))
            {
                ModelState.AddModelError("LocalizationError", "English localization should exist. ");
            }
            else
            {
                CheckAndFixLocalizations(publisherAddUpdateViewModel);
            }

            if (ModelState.IsValid)
            {
                CleanEmptyLocalizations(publisherAddUpdateViewModel);

                var publisherModel = Mapper.Map<PublisherModel>(publisherAddUpdateViewModel);

                _publisherService.Add(publisherModel);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Put(PublisherAddUpdateViewModel publisherAddUpdateViewModel)
        {
            var englishLocalization = GetLocalization(publisherAddUpdateViewModel, "en");

            if (IsLocalizationEmpty(englishLocalization))
            {
                ModelState.AddModelError("localizationError", "English localization should exist. ");
            }

            else
            {
                CheckAndFixLocalizations(publisherAddUpdateViewModel);
            }

            if (ModelState.IsValid)
            {
                CleanEmptyLocalizations(publisherAddUpdateViewModel);

                var publisherModel = Mapper.Map<PublisherModel>(publisherAddUpdateViewModel);

                if (_publisherService.PublisherExists(englishLocalization.CompanyName))
                {
                    _publisherService.Add(publisherModel);
                }

                _publisherService.Update(publisherModel);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [ActionName(DefaultAction)]
        [CustomApiAuthorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Delete(int id)
        {
            PublisherModel publisherModel = _publisherService.GetModelById(id);

            if (publisherModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _publisherService.Remove(id);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [ActionName("Games")]
        [CustomApiAuthorize(Roles = "Admin, Moderator, User, Manager")]
        public HttpResponseMessage Games(int id)
        {
            PublisherModel publisherModel = _publisherService.GetModelById(id);

            if (publisherModel == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            IEnumerable<GameModel> gameModels = _gameService.GetGamesByPublisher(publisherModel);

            var gameViewModels = Mapper.Map<IEnumerable<GameViewModel>>(gameModels);

            return Request.CreateResponse(HttpStatusCode.OK, gameViewModels);
        }

        private void CheckAndFixLocalizations(PublisherAddUpdateViewModel publisherAddUpdateViewModel)
        {
            List<LanguageModel> languages = _languageService.GetAll().ToList();

            foreach (var publisherLocalizationViewModel in publisherAddUpdateViewModel.PublisherLocalizations)
            {
                LanguageModel languageModel =
                    languages.FirstOrDefault(l =>
                        String.Equals(l.Code, publisherLocalizationViewModel.Language.Code, StringComparison.CurrentCultureIgnoreCase));

                if (languageModel == null)
                {
                    ModelState.AddModelError("LocalizationError", "Incorrect localization language. ");
                }
                else
                {
                    publisherLocalizationViewModel.Language = Mapper.Map<LanguageViewModel>(languageModel);
                }
            }
        }

        private static PublisherLocalizationViewModel GetLocalization(PublisherAddUpdateViewModel publisherAddUpdateViewModel, string languageCode)
        {
            if (publisherAddUpdateViewModel == null || publisherAddUpdateViewModel.PublisherLocalizations == null)
            {
                return null;
            }

            PublisherLocalizationViewModel result = publisherAddUpdateViewModel.PublisherLocalizations
                .FirstOrDefault(loc => String.Equals(loc.Language.Code, languageCode, StringComparison.CurrentCultureIgnoreCase));

            return result;
        }

        private static bool IsLocalizationEmpty(PublisherLocalizationViewModel publisherLocalizationViewModel)
        {
            var result = publisherLocalizationViewModel == null ||
                         String.IsNullOrEmpty(publisherLocalizationViewModel.CompanyName);

            return result;
        }

        private static void CleanEmptyLocalizations(PublisherAddUpdateViewModel publisherAddUpdateViewModel)
        {
            List<PublisherLocalizationViewModel> emptyLocalizations = publisherAddUpdateViewModel.PublisherLocalizations.Where(IsLocalizationEmpty).ToList();

            foreach (var publisherLocalizationViewModel in emptyLocalizations)
            {
                publisherAddUpdateViewModel.PublisherLocalizations.Remove(publisherLocalizationViewModel);
            }
        }
    }
}
