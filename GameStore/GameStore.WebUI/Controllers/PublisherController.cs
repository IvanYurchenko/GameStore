using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Localization;
using GameStore.WebUI.Security;
using GameStore.WebUI.ViewModels;
using GameStore.WebUI.ViewModels.Localization;

namespace GameStore.WebUI.Controllers
{
    public class PublisherController : BaseController
    {
        private readonly IPublisherService _publisherService;
        private readonly ILanguageService _languageService;

        public PublisherController(
            IPublisherService publisherService,
            ILanguageService languageService)
        {
            _publisherService = publisherService;
            _languageService = languageService;
        }

        /// <summary>
        /// Returns all publishers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Get")]
        public ActionResult GetAll()
        {
            IEnumerable<PublisherModel> publishers = _publisherService.GetAll();
            var viewModels = Mapper.Map<IEnumerable<PublisherViewModel>>(publishers);
            return View(viewModels);
        }
        
        [HttpGet]
        [ActionName("New")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Add()
        {
            var publisherAddUpdateViewModel = new PublisherAddUpdateViewModel();

            AdjustLocalizations(publisherAddUpdateViewModel);

            return View(publisherAddUpdateViewModel);
        }

        /// <summary>
        /// Adds the publisher.
        /// </summary>
        /// <param name="publisherAddUpdateViewModel">The publisher view model.</param>
        /// <returns></returns>
        [ActionName("New")]
        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Add(PublisherAddUpdateViewModel publisherAddUpdateViewModel)
        {
            PublisherLocalizationViewModel englishLocalization = GetLocalization(publisherAddUpdateViewModel, "en");

            if (IsLocalizationEmpty(englishLocalization))
            {
                ModelState.AddModelError("localizationError", "English localization should exist. ");
            }

            if (ModelState.IsValid)
            {
                CleanEmptyLocalizations(publisherAddUpdateViewModel);

                var model = Mapper.Map<PublisherModel>(publisherAddUpdateViewModel);
                _publisherService.Add(model);
                MessageSuccess("The publisher has been added successfully. ");
                return RedirectToAction("Get");
            }

            return View(publisherAddUpdateViewModel);
        }

        /// <summary>
        /// Returns the details of a current publisher specified by the key.
        /// </summary>
        /// <param name="key">Publisher's company name.</param>
        /// <returns></returns>
        [ActionName("Details")]
        public ActionResult GetDetails(string key)
        {
            PublisherModel model = _publisherService.GetModelByCompanyName(key);
            var viewModel = Mapper.Map<PublisherViewModel>(model);
            return View(viewModel);
        }

        private void AdjustLocalizations(PublisherAddUpdateViewModel publisherAddUpdateViewModel)
        {
            IEnumerable<LanguageModel> languages = _languageService.GetAll();

            publisherAddUpdateViewModel.PublisherLocalizations = publisherAddUpdateViewModel.PublisherLocalizations
                ?? new List<PublisherLocalizationViewModel>();

            foreach (LanguageModel languageModel in languages)
            {
                if (GetLocalization(publisherAddUpdateViewModel, languageModel.Code) == null)
                {
                    var publisherLocalization = new PublisherLocalizationViewModel
                    {
                        LanguageId = languageModel.LanguageId,
                        Language = Mapper.Map<LanguageViewModel>(languageModel),
                    };

                    publisherAddUpdateViewModel.PublisherLocalizations.Add(publisherLocalization);
                }
            }
        }

        private static PublisherLocalizationViewModel GetLocalization(
            PublisherAddUpdateViewModel publisherAddUpdateViewModel,
            string languageCode)
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
            bool result = publisherLocalizationViewModel == null ||
                         String.IsNullOrEmpty(publisherLocalizationViewModel.CompanyName) ||
                         String.IsNullOrEmpty(publisherLocalizationViewModel.Description);

            return result;
        }

        private void CleanEmptyLocalizations(PublisherAddUpdateViewModel publisherAddUpdateViewModel)
        {
            List<PublisherLocalizationViewModel> emptyLocalizations =
                publisherAddUpdateViewModel.PublisherLocalizations.Where(IsLocalizationEmpty).ToList();

            foreach (PublisherLocalizationViewModel publisherLocalizationViewModel in emptyLocalizations)
            {
                publisherAddUpdateViewModel.PublisherLocalizations.Remove(publisherLocalizationViewModel);
            }
        }
    }
}