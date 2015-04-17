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

        public ValidationController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public JsonResult IsKeyAvailable(string key)
        {
            JsonResult result = !_gameService.GameExists(key)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(String.Format(CultureInfo.InvariantCulture, "The key '{0}' is not available.", key),
                    JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}