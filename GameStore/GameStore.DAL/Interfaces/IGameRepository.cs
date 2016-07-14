using System;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        /// <summary>
        /// Gets the game by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Game GetGameByKey(String key);
    }
}