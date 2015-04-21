using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Services
{
    public class BasketItemService : IBasketItemService
    {
        public BasketItem CreateBasketItem(GameModel gameModel, int quantity)
        {
            var basketItem = new BasketItem
            {
                GameId = gameModel.GameId,
                Price = gameModel.Price,
                Quantity = quantity,
            };

            return basketItem;
        }
    }
}