using System;

namespace GameStore.DAL.Interfaces
{
    public interface ICustomComparer
    {
        /// <summary>
        /// Determines if specified objects are equal.
        /// </summary>
        /// <param name="object1">The object 1.</param>
        /// <param name="object2">The object 2.</param>
        /// <returns></returns>
        bool AreEqual(Object object1, Object object2);
    }
}