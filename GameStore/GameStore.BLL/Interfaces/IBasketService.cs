using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IBasketService
    {
        void AddBasketItem(BasketItemModel basketItemModel);
        void RemoveBasketItem(int basketItemModelId);
        void UpdateBasketItem(BasketItemModel basketItemModel);

        BasketModel GetBasketModelForUser(string sessionKey);
    }
}