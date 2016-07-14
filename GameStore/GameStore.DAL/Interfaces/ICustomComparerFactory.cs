using System;

namespace GameStore.DAL.Interfaces
{
    public interface ICustomComparerFactory
    {
        /// <summary>
        /// Gets the comparer for specified object type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        ICustomComparer GetComparer(Type type);
    }
}