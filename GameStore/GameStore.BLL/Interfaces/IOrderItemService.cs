using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IOrderItemService
    {
        OrderItem CreateOrderItem(GameModel gameModel, int quantity);
    }
}
