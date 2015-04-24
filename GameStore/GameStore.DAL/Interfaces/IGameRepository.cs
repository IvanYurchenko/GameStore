using System;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Game GetGameByKey(String key);
    }
}