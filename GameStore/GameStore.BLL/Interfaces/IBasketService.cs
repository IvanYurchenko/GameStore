using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IBasketService
    {
        void Add(OrderItem orderItem);
        void Remove(int orderId);
    }
}