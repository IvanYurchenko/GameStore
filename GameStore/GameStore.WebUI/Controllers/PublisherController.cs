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
            return RedirectToAction("New");
        }

        [ActionName("New")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [ActionName("New")]
        [HttpPost]
        public ActionResult Add(PublisherModel model)
        {
            _publisherService.Add(model);
            Success("The publisher has been added successfully. ");
            return View();
        }

        [ActionName("New")]
        public ActionResult Details(string companyName)
        {
            var model = _publisherService.GetModelByCompanyName(companyName);
            return View(model);
        }
    }
}