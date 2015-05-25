using System;
using System.Collections.Generic;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Creates the order from basket.
        /// </summary>
        /// <param name="basketModel">The basket model.</param>
        /// <returns></returns>
        void CreateOrderFromBasket(BasketModel basketModel);

        /// <summary>
        /// Gets the order model by user's session key.
        /// </summary>
        /// <param name="sessionKey">The session key.</param>
        /// <returns></returns>
        OrderModel GetOrderModelBySessionKey(string sessionKey);

        /// <summary>
        /// Adds the basket items to order.
        /// </summary>
        /// <param name="basketItems">The basket items.</param>
        /// <param name="sessionKey">The session key.</param>
        void AddBasketItemsToOrder(IEnumerable<BasketItemModel> basketItems, string sessionKey);

        /// <summary>
        /// Cleans the order for user.
        /// </summary>
        /// <param name="sessionKey">The user's session key.</param>
        void CleanOrderForUser(string sessionKey);

        /// <summary>
        /// Makes the order payed.
        /// </summary>
        /// <param name="sessionKey">The user's session key.</param>
        void MakeOrderPayed(string sessionKey);

        /// <summary>
        /// Gets orders by the date.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <returns></returns>
        IEnumerable<OrderModel> GetOrdersByDate(DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// Gets the order model by order id.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        OrderModel GetModelById(int orderId);

        /// <summary>
        /// Updates the specified order model.
        /// </summary>
        /// <param name="orderModel">The order model.</param>
        void Update(OrderModel orderModel);
    }
}