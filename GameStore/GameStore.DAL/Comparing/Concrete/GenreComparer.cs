using System;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Comparing.Concrete
{
    public class GenreComparer : ICustomComparer
    {
        public bool AreEqual(Object object1, Object object2)
        {
            if (object1.GetType() != typeof (Genre) || object2.GetType() != typeof (Genre))
            {
                throw new ArgumentException(String.Format("An object is not an instance of a {0} class. ",
                    typeof (Genre)));
            }

            var genre1 = (Genre) object1;
            var genre2 = (Genre) object2;

            var result = genre1.Name == genre2.Name
                         && genre1.IsReadonly == genre2.IsReadonly
                         && genre1.NorthwindId == genre2.NorthwindId;

            return result;
        }
    }
}