using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.Core.Enums;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

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
                foreach (OrderItem orderItem in order.OrderItems)
                {
                    orderItem.Game = _unitOfWork.GameRepository.GetById((int) orderItem.GameId);
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
            var orderDetailModels = Mapper.Map<IEnumerable<OrderItemModel>>(basketItems);
            var orderDetailsToAdd = Mapper.Map<IEnumerable<OrderItem>>(orderDetailModels);

            Order order = _unitOfWork.OrderRepository.Get(x => x.SessionKey == sessionKey).First();

            foreach (OrderItem orderDetail in orderDetailsToAdd)
            {
                orderDetail.OrderId = order.OrderId;
                orderDetail.Game = _unitOfWork.GameRepository.GetById((int) orderDetail.GameId);
                _unitOfWork.OrderItemRepository.Insert(orderDetail);
            }

            _unitOfWork.SaveChanges();
        }

        public void CleanOrderForUser(string sessionKey)
        {
            Order order = _unitOfWork.OrderRepository.Get(o => o.SessionKey == sessionKey).FirstOrDefault();

            if (order == null) return;

            List<OrderItem> orderItemsToRemove = order.OrderItems.ToList();

            foreach (OrderItem orderItem in orderItemsToRemove)
            {
                _unitOfWork.OrderItemRepository.Delete(orderItem);
            }

            _unitOfWork.OrderRepository.Delete(order);
            _unitOfWork.SaveChanges();
        }

        public void MakeOrderPayed(string sessionKey)
        {
            Order order = _unitOfWork.OrderRepository.Get(o => o.SessionKey == sessionKey).FirstOrDefault();

            if (order == null)
            {
                throw new ArgumentException("The order with current session key doesn't exist. ");
            }

            order.OrderStatus = OrderStatus.Payed;

            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<OrderModel> GetOrdersByDate(DateTime dateFrom, DateTime dateTo)
        {
            Func<Order, bool> filter = (order => order.OrderDate > dateFrom && order.OrderDate < dateTo);
            Func<Order, object> sortCondition = (order => order.OrderDate.Ticks * -1);

            IEnumerable<Order> orders = _unitOfWork.OrderRepository.GetMany(filter, int.MaxValue, 1, sortCondition);

            var orderModels = Mapper.Map<IEnumerable<OrderModel>>(orders);

            return orderModels;
        }

        public OrderModel GetModelById(int orderId)
        {
            Order order = _unitOfWork.OrderRepository.GetById(orderId);

            if (order == null)
            {
                return null;
            }

            var orderModel = Mapper.Map<OrderModel>(order);
            return orderModel;
        }

        public void Update(OrderModel orderModel)
        {
            Order order = _unitOfWork.OrderRepository.GetById(orderModel.OrderId);

            Mapper.Map(orderModel, order);

            if (orderModel.OrderItems != null && orderModel.OrderItems.Count > 0)
            {
                order.OrderItems =
                    orderModel.OrderItems.Select(
                        oi => _unitOfWork.OrderItemRepository.Get(x => x.OrderItemId == oi.OrderItemId).FirstOrDefault())
                        .ToList();
            }

            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.SaveChanges();
        }
    }
}