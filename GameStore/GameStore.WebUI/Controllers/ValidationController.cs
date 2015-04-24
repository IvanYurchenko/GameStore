using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.UI;
using GameStore.BLL.Interfaces;

namespace GameStore.WebUI.Controllers
{
    [OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
    public class ValidationController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IPublisherService _publisherService;

        public ValidationController(
            IGameService gameService,
            IPublisherService publisherService)
        {
            _gameService = gameService;
            _publisherService = publisherService;
        }

        public JsonResult IsGameKeyAvailable(string Key, int GameId)
        {
            JsonResult result = !_gameService.GameExists(Key, GameId)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(String.Format(CultureInfo.InvariantCulture, "The game with key '{0}' already exists.", Key),
                    JsonRequestBehavior.AllowGet);

            return result;
        }

        public JsonResult IsCompanyNameAvailable(string companyName)
        {
            JsonResult result = !_publisherService.PublisherExists(companyName)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(
                    String.Format(CultureInfo.InvariantCulture, "The company with name '{0}' already exists.",
                        companyName),
                    JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}