using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using BootstrapSupport;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;

namespace GameStore.WebUI.Controllers
{
    public class PublisherController : BootstrapBaseController
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        [ActionName("Get")]
        public ActionResult GetAll()
        {
            var publishers = _publisherService.GetAll();
            return View(publishers);
        }

        [HttpGet]
        [ActionName("New")]
        public ActionResult Add()
        {
            return View(new PublisherModel());
        }

        [ActionName("New")]
        [HttpPost]
        public ActionResult Add(PublisherModel model)
        {
            if (ModelState.IsValid)
            {
                _publisherService.Add(model);
                MessageSuccess("The publisher has been added successfully. ");
                return RedirectToAction("Get");
            }

            TempData.Add(Alerts.ERROR, "Model state is not valid.");
            return View(model);
        }

        [ActionName("Details")]
        public ActionResult Details(string key)
        {
            var model = _publisherService.GetModelByCompanyName(key);
            return View(model);
        }
    }
}