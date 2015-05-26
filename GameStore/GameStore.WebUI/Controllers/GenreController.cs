using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebUI.Security;
using GameStore.WebUI.ViewModels;

namespace GameStore.WebUI.Controllers
{
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
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
            var genreViewModel = new GenreViewModel();
            IEnumerable<GenreModel> genreModels = _genreService.GetAll();
            genreViewModel.AllGenres = Mapper.Map<IEnumerable<GenreViewModel>>(genreModels);

            return View(genreViewModel);
        }

        [HttpPost]
        [ActionName("New")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult AddGenre(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                var genreModel = Mapper.Map<GenreModel>(genreViewModel);

                _genreService.Add(genreModel);

                MessageSuccess("The genre has been added successfully. ");

                return RedirectToAction("Get", "Genre");
            }

            IEnumerable<GenreModel> genreModels = _genreService.GetAll();
            genreViewModel.AllGenres = Mapper.Map<IEnumerable<GenreViewModel>>(genreModels);

            ModelState.AddModelError("", "The information provided is incorrect. ");

            return View(genreViewModel);
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

            var genreViewModel = Mapper.Map<GenreViewModel>(genreModel);

            return View(genreViewModel);
        }

        [HttpPost]
        [ActionName("Update")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult Update(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                var genreModel = Mapper.Map<GenreModel>(genreViewModel);

                _genreService.Update(genreModel);

                MessageSuccess("The genre has been updated successfully. ");

                return RedirectToAction("Get", "Genre");
            }
            
            ModelState.AddModelError("", "The information provided is incorrect. ");

            return View(genreViewModel);
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
    }
}
