using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.WebUI.Filters;

namespace GameStore.WebUI.Controllers
{
    [ExceptionLoggingFilter]
    [PerformanceLoggingFilter]
    public class BasketController : BootstrapBaseController
    {
        private readonly IBasketService _basketService;
        private readonly IGameService _gameService;
        private readonly IBasketItemService _basketItemService;

        public BasketController(
            IBasketService basketService,
            IGameService gameService,
            IBasketItemService basketItemService)
        {
            _basketService = basketService;
            _gameService = gameService;
            _basketItemService = basketItemService;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Basket
        [HttpPost]
        [ActionName("AddGameToBusket")]
        public JsonResult AddGameToBusket(string gameKey, int quantity = 1)
        {
            var gameModel = _gameService.GetGameModelByKey(gameKey);
            BasketItem basketItem = _basketItemService.CreateBasketItem(gameModel, quantity);
            _basketService.Add(basketItem);
            MessageSuccess("The game has been added to your basket.");
            return Json(true);
        }
        
        [HttpPost]
        [ActionName("RemoveGameFromBasket")]
        public ActionResult RemoveGameFromBasket(string gameKey)
        {
            var gameModel = _gameService.GetGameModelByKey(gameKey);

            MessageSuccess("The game has been removed from your basket.");
            return Json(true);
        }
        #endregion
    }
}