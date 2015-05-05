using System;
using System.Collections.Generic;
using System.Linq;
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
            var orderModel = Mapper.Map<OrderModel>(basketModel);
            orderModel.OrderDate = DateTime.UtcNow;

            var order = Mapper.Map<Order>(orderModel);

            if (order.OrderItems != null)
            {
                foreach (var orderDetail in order.OrderItems)
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

            if (order == null)
            {
                return null;
            }

            var orderModel = Mapper.Map<OrderModel>(order);
            return orderModel;
        }

        public void AddBasketItemsToOrder(IEnumerable<BasketItemModel> basketItems, string sessionKey)
        {
            IEnumerable<OrderItemModel> orderDetailModels = Mapper.Map<IEnumerable<OrderItemModel>>(basketItems);
            IEnumerable<OrderItem> orderDetailsToAdd = Mapper.Map<IEnumerable<OrderItem>>(orderDetailModels);

            Order order = _unitOfWork.OrderRepository.Get(x => x.SessionKey == sessionKey).First();

            foreach (var orderDetail in orderDetailsToAdd)
            {
                orderDetail.OrderId = order.OrderId;
                orderDetail.Game = _unitOfWork.GameRepository.GetById(orderDetail.GameId);
                _unitOfWork.OrderItemRepository.Insert(orderDetail);
            }

            _unitOfWork.SaveChanges();
        }

        public void CleanOrderForUser(string sessionKey)
        {
            Order order = _unitOfWork.OrderRepository.Get(o => o.SessionKey == sessionKey).FirstOrDefault();

            if (order == null) return;

            List<OrderItem> orderItemsToRemove = order.OrderItems.ToList();

            foreach (var orderItem in orderItemsToRemove)
            {
                _unitOfWork.OrderItemRepository.Delete(orderItem);
            }

            _unitOfWork.OrderRepository.Delete(order);
            _unitOfWork.SaveChanges();
        }
    }
}