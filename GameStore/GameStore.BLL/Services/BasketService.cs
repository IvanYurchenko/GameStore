using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;

namespace GameStore.BLL.Services
{
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BasketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(BasketItem basketItem)
        {
            _unitOfWork.BasketItemRepository.Insert(basketItem);
        }

        public void Remove(int basketItemId)
        {
            _unitOfWork.BasketItemRepository.Delete(basketItemId);
        }
    }
}