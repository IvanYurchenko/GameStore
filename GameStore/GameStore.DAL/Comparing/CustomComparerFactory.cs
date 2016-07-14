using System;
using System.Collections.Generic;
using GameStore.DAL.Comparing.Concrete;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Comparing
{
    public class CustomComparerFactory : ICustomComparerFactory
    {
        private readonly Dictionary<Type, Func<ICustomComparer>> _customComparers;

        public CustomComparerFactory()
        {
            _customComparers = new Dictionary<Type, Func<ICustomComparer>>
            {
                {typeof (Game), () => new GameComparer()},
                {typeof (Genre), () => new GenreComparer()},
                {typeof (Publisher), () => new PublisherComparer()},
                {typeof (Order), () => new OrderComparer()},
                {typeof (OrderItem), () => new OrderItemComparer()},
            };
        }

        public ICustomComparer GetComparer(Type type)
        {
            ICustomComparer result = _customComparers.ContainsKey(type) ? _customComparers[type]() : null;
            return result;
        }
    }
}