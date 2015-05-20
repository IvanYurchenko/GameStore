using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using BootstrapMvcSample.Controllers;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebUI.BootstrapSupport;
using GameStore.WebUI.ViewModels;

namespace GameStore.WebUI.Controllers
{
    public class PublisherController : BaseController
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        /// <summary>
        /// Returns all publishers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Get")]
        public ActionResult GetAll()
        {
            var publishers = _publisherService.GetAll();
            var viewModels = Mapper.Map<IEnumerable<PublisherViewModel>>(publishers);
            return View(viewModels);
        }

        [HttpGet]
        [ActionName("New")]
        public ActionResult Add()
        {
            return View(new PublisherViewModel());
        }

        /// <summary>
        /// Adds the publisher.
        /// </summary>
        /// <param name="publisherViewModel">The publisher view model.</param>
        /// <returns></returns>
        [ActionName("New")]
        [HttpPost]
        public ActionResult Add(PublisherViewModel publisherViewModel)
        {
            if (ModelState.IsValid)
            {
                var model = Mapper.Map<PublisherModel>(publisherViewModel);
                _publisherService.Add(model);
                MessageSuccess("The publisher has been added successfully. ");
                return RedirectToAction("Get");
            }

            TempData.Add(Alerts.ERROR, "Model state is not valid.");
            return View(publisherViewModel);
        }

        /// <summary>
        /// Returns the details of a current publisher specified by the key.
        /// </summary>
        /// <param name="key">Publisher's company name.</param>
        /// <returns></returns>
        [ActionName("Details")]
        public ActionResult GetDetails(string key)
        {
            var model = _publisherService.GetModelByCompanyName(key);
            var viewModel = Mapper.Map<PublisherViewModel>(model);
            return View(viewModel);
        }
    }
}