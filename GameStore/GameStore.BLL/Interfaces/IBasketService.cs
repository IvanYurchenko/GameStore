using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IBasketService
    {
        void Add(OrderItem orderItem);
        void Remove(int orderId);
    }
}
