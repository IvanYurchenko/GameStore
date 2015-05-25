using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.Core.Enums;
using GameStore.WebUI.Security;
using GameStore.WebUI.ViewModels;

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
        [CustomAuthorize(Roles = "User")]
        public ActionResult Get()
        {
            string sessionKey = Session.SessionID;
            OrderModel orderModel = _orderService.GetOrderModelBySessionKey(sessionKey);
            return View(orderModel);
        }

        [ActionName("New")]
        [CustomAuthorize(Roles = "User")]
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

        [HttpGet]
        [ActionName("History")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult GetOrdersByDate(OrderHistoryViewModel orderHistoryViewModel)
        {
            orderHistoryViewModel = orderHistoryViewModel ?? new OrderHistoryViewModel();

            var defaultDate = new DateTime(01, 01, 01, 00, 00, 00);

            if (orderHistoryViewModel.DateFrom == defaultDate)
            {
                orderHistoryViewModel.DateFrom = DateTime.UtcNow.AddYears(-100);
            }

            if (orderHistoryViewModel.DateTo == defaultDate)
            {
                orderHistoryViewModel.DateTo = DateTime.UtcNow.AddDays(-30);
            }

            IEnumerable<OrderModel> orderModels =
                _orderService.GetOrdersByDate(orderHistoryViewModel.DateFrom, orderHistoryViewModel.DateTo);

            var orderViewModels = Mapper.Map<IEnumerable<OrderViewModel>>(orderModels);

            orderHistoryViewModel.Orders = orderViewModels.ToList();

            return View("History", orderHistoryViewModel);
        }

        [HttpGet]
        [ActionName("Orders")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult GetOrdersForLastMonth()
        {
            var orderHistoryViewModel = new OrderHistoryViewModel
            {
                DateFrom = DateTime.UtcNow.AddDays(-30),
                DateTo = DateTime.UtcNow,
            };

            return GetOrdersByDate(orderHistoryViewModel);
        }

        [HttpGet]
        [ActionName("MakeShipped")]
        [CustomAuthorize(Roles = "Admin, Manager")]
        public ActionResult MakeShipped(int orderId, DateTime dateFrom, DateTime dateTo)
        {
            var orderHistoryViewModel = new OrderHistoryViewModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
            };

            OrderModel orderModel = _orderService.GetModelById(orderId);

            if (orderModel == null || orderModel.IsReadonly || orderModel.OrderStatus != OrderStatus.Payed)
            {
                return RedirectToAction("History", "Order", orderHistoryViewModel);
            }

            orderModel.OrderStatus = OrderStatus.Shipped;
            _orderService.Update(orderModel);

            MessageSuccess("The order status has been changed to 'Shipped'. ");

            return RedirectToAction("History", "Order", orderHistoryViewModel);
        }
    }
}