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

        public void Add(OrderItem orderItem)
        {
            _unitOfWork.OrderItemRepository.Insert(orderItem);
        }

        public void Remove(int orderItemId)
        {
            _unitOfWork.OrderItemRepository.Delete(orderItemId);
        }
    }
}