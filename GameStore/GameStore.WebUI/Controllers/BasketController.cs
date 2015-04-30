using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="BasketController"/> class.
        /// </summary>
        /// <param name="basketService">The basket service.</param>
        /// <param name="gameService">The game service.</param>
        public BasketController(
            IBasketService basketService,
            IGameService gameService)
        {
            _basketService = basketService;
            _gameService = gameService;
        }

        /// <summary>
        /// Returns all basket items.
        /// </summary>
        /// <returns></returns>
        [ActionName("Get")]
        public ActionResult Get()
        {
            var sessionKey = Session.SessionID;
            var basketModel = _basketService.GetBasketModelForUser(sessionKey);
            IEnumerable<BasketItemModel> basketItems = basketModel.BasketItems;
            return View(basketItems);
        }

        #region Basket

        /// <summary>
        /// Adds a game with the specified key to the busket.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Add")]
        public ActionResult AddGameToBusket(string key, int quantity = 1)
        {
            var gameModel = _gameService.GetGameModelByKey(key);
            var sessionKey = Session.SessionID;
            var basketModel = _basketService.GetBasketModelForUser(sessionKey);

            var basketItemModel = new BasketItemModel
            {
                BasketId = basketModel.BasketId,
                Quantity = quantity,
                Price = gameModel.Price,
                Discount = 0,
                GameId = gameModel.GameId,
                Game = Mapper.Map<Game>(gameModel)
            };

            _basketService.AddBasketItem(basketItemModel);

            MessageSuccess("The game has been added to your basket.");
            return RedirectToAction("Get", "Basket");
        }

        #endregion
    }
}