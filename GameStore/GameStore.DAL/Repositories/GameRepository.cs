﻿using System.Data.Entity;
using System.Linq;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(DbContext context) : base(context)
        {
        }

        public Game GetGameByKey(string key)
        {
            var result = Get(game => game.Key == key).First();
            return result;
        }
    }
}