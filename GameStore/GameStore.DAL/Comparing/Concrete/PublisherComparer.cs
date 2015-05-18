using System;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Comparing.Concrete
{
    public class PublisherComparer : ICustomComparer
    {
        public bool AreEqual(Object object1, Object object2)
        {
            if (object1.GetType() != typeof (Publisher) || object2.GetType() != typeof (Publisher))
            {
                throw new ArgumentException(String.Format("An object is not an instance of a {0} class. ",
                    typeof (Publisher)));
            }

            var publisher1 = (Publisher) object1;
            var publisher2 = (Publisher) object2;

            var result = publisher1.CompanyName == publisher2.CompanyName
                         && publisher1.Description == publisher2.Description
                         && publisher1.HomePage == publisher2.HomePage
                         && publisher1.NorthwindId == publisher2.NorthwindId;

            return result;
        }
    }
}