using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IBasketService
    {
        void Add(BasketItem basketItem);
        void Remove(int basketItemId);
    }
}