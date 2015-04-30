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

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationController"/> class.
        /// </summary>
        /// <param name="gameService">The game service.</param>
        /// <param name="publisherService">The publisher service.</param>
        public ValidationController(
            IGameService gameService,
            IPublisherService publisherService)
        {
            _gameService = gameService;
            _publisherService = publisherService;
        }

        /// <summary>
        /// Determines whether [is game key available] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="gameId">The game identifier.</param>
        /// <returns></returns>
        public JsonResult IsGameKeyAvailable(string key, int gameId)
        {
            JsonResult result = !_gameService.GameExists(key, gameId)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(String.Format(CultureInfo.InvariantCulture, "The game with key '{0}' already exists.", key),
                    JsonRequestBehavior.AllowGet);

            return result;
        }

        /// <summary>
        /// Determines whether [is company name available] [the specified company name].
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="publisherId">The publisher identifier.</param>
        /// <returns></returns>
        public JsonResult IsCompanyNameAvailable(string companyName, int publisherId)
        {
            JsonResult result = !_publisherService.PublisherExists(companyName, publisherId)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(
                    String.Format(CultureInfo.InvariantCulture, "The company with name '{0}' already exists.",
                        companyName),
                    JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}