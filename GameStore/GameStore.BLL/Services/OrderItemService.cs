using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Services
{
    public class OrderItemService : IOrderItemService
    {
        public OrderItem CreateOrderItem(GameModel gameModel, int quantity)
        {
            var orderItem = new OrderItem
            {
                ProductId = gameModel.GameId,
                Price = gameModel.Price,
                Quantity = quantity,
            };

            return orderItem;
        }
    }
}