using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IBasketService
    {
        /// <summary>
        /// Adds the basket item to the database.
        /// </summary>
        /// <param name="basketItemModel">The basket item model.</param>
        void AddBasketItem(BasketItemModel basketItemModel);

        /// <summary>
        /// Removes the basket item from the database.
        /// </summary>
        /// <param name="basketItemModelId">The basket item identifier.</param>
        void RemoveBasketItem(int basketItemModelId);

        /// <summary>
        /// Updates the basket item.
        /// </summary>
        /// <param name="basketItemModel">The basket item model.</param>
        void UpdateBasketItem(BasketItemModel basketItemModel);
        
        /// <summary>
        /// Gets the basket model by user session key.
        /// </summary>
        /// <param name="sessionKey">The session key.</param>
        /// <returns></returns>
        BasketModel GetBasketModelForUser(string sessionKey);

        /// <summary>
        /// Cleans the basket for user.
        /// </summary>
        /// <param name="sessionKey">The session key.</param>
        /// <returns></returns>
        void CleanBasketForUser(string sessionKey);
    }
}