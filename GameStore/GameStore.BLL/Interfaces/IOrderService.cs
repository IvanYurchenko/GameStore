using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
