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
        private readonly IOrderItemService _orderItemService;

        public BasketController(
            IBasketService basketService,
            IGameService gameService,
            IOrderItemService orderItemService)
        {
            _basketService = basketService;
            _gameService = gameService;
            _orderItemService = orderItemService;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Basket
        [HttpPost]
        [ActionName("AddGameToBusket")]
        public JsonResult AddGameToBusket(GameModel gameModel, int quantity = 1)
        {
            OrderItem orderItem = _orderItemService.CreateOrderItem(gameModel, quantity);
            _basketService.Add(orderItem);
            MessageSuccess("The game has been added to your basket.");
            return Json(true);
        }
        
        [HttpPost]
        [ActionName("RemoveGameFromBasket")]
        public ActionResult RemoveGameFromBasket(GameModel gameModel)
        {
            _gameService.Update(gameModel);
            MessageSuccess("The game has been removed from your basket.");
            return Json(true);
        }
        #endregion
    }
}