using System;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Game GetGameByKey(String key);
    }
}