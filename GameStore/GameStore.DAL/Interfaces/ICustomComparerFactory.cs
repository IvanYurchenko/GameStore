using System;

namespace GameStore.DAL.Interfaces
{
    public interface ICustomComparerFactory
    {
        ICustomComparer GetComparer(Type type);
    }
}