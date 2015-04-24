using System.Linq;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
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

        public void AddBasketItem(BasketItemModel basketItemModel)
        {
            var basketItem = Mapper.Map<BasketItem>(basketItemModel);

            if (basketItemModel.Game != null)
            {
                basketItem.Game = _unitOfWork.GameRepository.GetById(basketItemModel.GameId);
            }

            _unitOfWork.BasketItemRepository.Insert(basketItem);
            _unitOfWork.Save();
        }

        public void RemoveBasketItem(int basketItemId)
        {
            _unitOfWork.BasketItemRepository.Delete(basketItemId);
            _unitOfWork.Save();
        }

        public void UpdateBasketItem(BasketItemModel basketItemModel)
        {
            var basketItem = _unitOfWork.BasketItemRepository.GetById(basketItemModel.BasketItemId);
            Mapper.Map(basketItemModel, basketItem);

            if (basketItemModel.Game != null)
            {
                basketItem.Game = _unitOfWork.GameRepository.GetById(basketItemModel.GameId);
            }

            _unitOfWork.BasketItemRepository.Update(basketItem);
            _unitOfWork.Save();
        }

        public BasketModel GetBasketModelForUser(string sessionKey)
        {
            Basket basket = _unitOfWork.BasketRepository.Get(b => b.SessionKey == sessionKey).FirstOrDefault();

            if (basket == null)
            {
                basket = new Basket {SessionKey = sessionKey};
                _unitOfWork.BasketRepository.Insert(basket);
                _unitOfWork.Save();
            }

            var basketModel = Mapper.Map<BasketModel>(basket);
            return basketModel;
        }
    }
}