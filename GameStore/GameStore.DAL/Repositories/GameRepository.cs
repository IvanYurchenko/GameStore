using System.Linq;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(GameStoreDbContext context) : base(context)
        {
        }

        public Game GetGameByKey(string key)
        {
            var result = Get(game => game.Key == key).First();
            return result;
        }
    }
}