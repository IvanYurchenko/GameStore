using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                CategoryId = gameModel.CategoryId,
                Price = gameModel.Price,
                Quantity = quantity,
            };

            return orderItem;
        }
    }
}
