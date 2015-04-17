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

        [HttpGet]
        [ActionName("AddGameToBusket")]
        public ActionResult AddGameToBusket(int gameId)
        {
            GameModel gameModel = _gameService.GetGameModelById(gameId);
            return View(gameModel);
        }

        [HttpPost]
        [ActionName("AddGameToBusket")]
        public ActionResult AddGameToBusket(GameModel gameModel, int quantity)
        {
            OrderItem orderItem = _orderItemService.CreateOrderItem(gameModel, quantity);
            _basketService.Add(orderItem);
            Success("The game has been added to your basket.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("RemoveGameFromBasket")]
        public ActionResult RemoveGameFromBasket(int gameId)
        {
            GameModel game = _gameService.GetGameModelById(gameId);
            return View(game);
        }

        [HttpPost]
        [ActionName("RemoveFromBasket")]
        public ActionResult RemoveFromBasket(GameModel gameModel)
        {
            _gameService.Update(gameModel);
            Success("The game has been removed from your basket.");
            return RedirectToAction("Index");
        }

        #endregion

        [HttpPost]
        public void Remove(int orderItemId)
        {
            _basketService.Remove(orderItemId);
            Success("An item has been successfully removed from your basket.");
        }
    }
}