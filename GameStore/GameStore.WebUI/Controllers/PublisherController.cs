using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
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

        [ActionName("Index")]
        public ActionResult Index()
        {
            return RedirectToAction("Publishers");
        }

        [HttpGet]
        [ActionName("Publishers")]
        public ActionResult GetAll()
        {
            var publishers = _publisherService.GetAll();
            return View(publishers);
        }

        [HttpGet]
        [ActionName("New")]
        public ActionResult Add()
        {
            return View();
        }

        [ActionName("New")]
        [HttpPost]
        public ActionResult Add(PublisherModel model)
        {
            _publisherService.Add(model);
            MessageSuccess("The publisher has been added successfully. ");
            return View();
        }

        [ActionName("Details")]
        public ActionResult Details(string key)
        {
            var model = _publisherService.GetModelByCompanyName(key);
            return View(model);
        }
    }
}