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
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;
        private readonly ILanguageService _languageService;

        public GenreController(
            IGenreService genreService,
            ILanguageService languageService)
        {
            _genreService = genreService;
            _languageService = languageService;
        }

        [HttpGet]
        [ActionName("Get")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult Get()
        {
            IEnumerable<GenreModel> genreModels = _genreService.GetAll();

            var genreViewModels = Mapper.Map<IEnumerable<GenreViewModel>>(genreModels);

            return View(genreViewModels);
        }
        [HttpGet]
        [ActionName("New")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult AddGenre()
        {
            var genreAddUpdateViewModel = new GenreAddUpdateViewModel();
            genreAddUpdateViewModel.AllGenres = Mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());

            AdjustLocalizations(genreAddUpdateViewModel);

            return View(genreAddUpdateViewModel);
        }

        [HttpPost]
        [ActionName("New")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult AddGenre(GenreAddUpdateViewModel genreAddUpdateViewModel)
        {
            var englishLocalization = GetLocalization(genreAddUpdateViewModel, "en");

            if (IsLocalizationEmpty(englishLocalization))
            {
                ModelState.AddModelError("localizationError", "English localization should exist. ");
            }

            if (ModelState.IsValid)
            {
                CleanEmptyLocalizations(genreAddUpdateViewModel);

                var genreModel = Mapper.Map<GenreModel>(genreAddUpdateViewModel);

                _genreService.Add(genreModel);

                MessageSuccess("The genre has been added successfully. ");

                return RedirectToAction("Get", "Genre");
            }

            IEnumerable<GenreModel> genreModels = _genreService.GetAll();
            genreAddUpdateViewModel.AllGenres = Mapper.Map<IEnumerable<GenreViewModel>>(genreModels);
            
            return View(genreAddUpdateViewModel);
        }

        [HttpGet]
        [ActionName("Update")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult Update(int genreId)
        {
            GenreModel genreModel = _genreService.GetModelById(genreId);

            if (genreModel == null || genreModel.IsReadonly)
            {
                return RedirectToAction("Get", "Genre");
            }

            var genreAddUpdateViewModel = Mapper.Map<GenreAddUpdateViewModel>(genreModel);

            AdjustLocalizations(genreAddUpdateViewModel);

            genreAddUpdateViewModel.AllGenres = Mapper.Map<IEnumerable<GenreViewModel>>(_genreService.GetAll());


            return View(genreAddUpdateViewModel);
        }

        [HttpPost]
        [ActionName("Update")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult Update(GenreAddUpdateViewModel genreAddUpdateViewModel)
        {
            var englishLocalization = GetLocalization(genreAddUpdateViewModel, "en");

            if (IsLocalizationEmpty(englishLocalization))
            {
                ModelState.AddModelError("localizationError", "English localization should exist. ");
            }

            if (ModelState.IsValid)
            {
                CleanEmptyLocalizations(genreAddUpdateViewModel);

                var genreModel = Mapper.Map<GenreModel>(genreAddUpdateViewModel);

                _genreService.Update(genreModel);

                MessageSuccess("The genre has been updated successfully. ");

                return RedirectToAction("Get", "Genre");
            }

            return View(genreAddUpdateViewModel);
        }

        [HttpGet]
        [ActionName("Remove")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult Remove(int genreId)
        {
            GenreModel genreModel = _genreService.GetModelById(genreId);

            if (genreModel != null)
            {
                _genreService.Remove(genreId);
            }

            MessageSuccess("The genre has been removed successfully. ");

            return RedirectToAction("Get", "Genre");
        }

        private void AdjustLocalizations(GenreAddUpdateViewModel genreAddUpdateViewModel)
        {
            IEnumerable<LanguageModel> languages = _languageService.GetAll();

            genreAddUpdateViewModel.GenreLocalizations = genreAddUpdateViewModel.GenreLocalizations ?? new List<GenreLocalizationViewModel>();

            foreach (var languageModel in languages)
            {
                if (GetLocalization(genreAddUpdateViewModel, languageModel.Code) == null)
                {
                    var genreLocalization = new GenreLocalizationViewModel
                    {
                        LanguageId = languageModel.LanguageId,
                        Language = Mapper.Map<LanguageViewModel>(languageModel),
                    };

                    genreAddUpdateViewModel.GenreLocalizations.Add(genreLocalization);
                }
            }
        }

        private static GenreLocalizationViewModel GetLocalization(GenreAddUpdateViewModel genreAddUpdateViewModel, string languageCode)
        {
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

        private void CleanEmptyLocalizations(GenreAddUpdateViewModel genreAddUpdateViewModel)
        {
            List<GenreLocalizationViewModel> emptyLocalizations = genreAddUpdateViewModel.GenreLocalizations.Where(IsLocalizationEmpty).ToList();

            foreach (var genreLocalizationViewModel in emptyLocalizations)
            {
                genreAddUpdateViewModel.GenreLocalizations.Remove(genreLocalizationViewModel);
            }
        }
    }
}
