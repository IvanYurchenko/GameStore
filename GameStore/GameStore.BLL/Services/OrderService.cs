using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;

namespace GameStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateOrderFromBasket(BasketModel basketModel)
        {
            OrderModel orderModel = Mapper.Map<OrderModel>(basketModel);
            orderModel.OrderDate = DateTime.UtcNow;

            Order order = Mapper.Map<Order>(orderModel);

            if (order.OrderDetails != null)
            {
                foreach (var orderDetail in order.OrderDetails)
                {
                    orderDetail.Game = _unitOfWork.GameRepository.GetById(orderDetail.GameId);
                }
            }

            _unitOfWork.OrderRepository.Insert(order);
            _unitOfWork.SaveChanges();
        }

        public OrderModel GetOrderModelBySessionKey(string sessionKey)
        {
            Order order = _unitOfWork.OrderRepository.Get(b => b.SessionKey == sessionKey).FirstOrDefault();
            if (order == null) return null;
            OrderModel orderModel = Mapper.Map<OrderModel>(order);
            return orderModel;
        }

        public void AddBasketItemsToOrder(IEnumerable<BasketItemModel> basketItems, string sessionKey)
        {
            IEnumerable<OrderDetailModel> orderDetailModels = Mapper.Map<IEnumerable<OrderDetailModel>>(basketItems);
            IEnumerable<OrderDetail> orderDetailsToAdd = Mapper.Map<IEnumerable<OrderDetail>>(orderDetailModels);

            Order order = _unitOfWork.OrderRepository.Get(x => x.SessionKey == sessionKey).First();

            foreach (var orderDetail in orderDetailsToAdd)
            {
                orderDetail.OrderId = order.OrderId;
                orderDetail.Game = _unitOfWork.GameRepository.GetById(orderDetail.GameId);
                _unitOfWork.OrderDetailRepository.Insert(orderDetail);
            }

            _unitOfWork.SaveChanges();

        }
    }
}
