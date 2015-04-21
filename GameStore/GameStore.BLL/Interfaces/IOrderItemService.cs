using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IBasketItemService
    {
        BasketItem CreateBasketItem(GameModel gameModel, int quantity);
    }
}