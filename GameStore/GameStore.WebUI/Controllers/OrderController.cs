using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;

namespace GameStore.WebUI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService,
            IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        [ActionName("Get")]
        public ActionResult Get()
        {
            string sessionKey = Session.SessionID;
            OrderModel orderModel = _orderService.GetOrderModelBySessionKey(sessionKey);
            return View(orderModel);
        }

        [ActionName("New")]
        public ActionResult CreateOrder()
        {
            string sessionKey = Session.SessionID;
            OrderModel orderModel = _orderService.GetOrderModelBySessionKey(sessionKey);
            BasketModel basketModel = _basketService.GetBasketModelForUser(sessionKey);

            if (basketModel.BasketItems.Count == 0)
            {
                MessageAttention(
                    "You don't have any items in your basket to create an order. Initially, add some games to your basket.");
                return RedirectToAction("Get", "Game");
            }

            if (orderModel == null)
            {
                _orderService.CreateOrderFromBasket(basketModel);
            }
            else
            {
                _orderService.AddBasketItemsToOrder(basketModel.BasketItems, sessionKey);
            }

            _basketService.CleanBasketForUser(sessionKey);
            MessageSuccess("New items have been successfully added to the order. ");
            return RedirectToAction("Get");
        }
    }
}