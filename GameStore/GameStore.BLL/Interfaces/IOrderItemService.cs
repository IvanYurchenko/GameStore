using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IOrderItemService
    {
        OrderItem CreateOrderItem(GameModel gameModel, int quantity);
    }
}