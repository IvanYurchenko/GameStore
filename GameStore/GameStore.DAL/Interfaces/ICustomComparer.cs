using System;

namespace GameStore.DAL.Interfaces
{
    public interface ICustomComparer
    {
        bool AreEqual(Object object1, Object object2);
    }
}