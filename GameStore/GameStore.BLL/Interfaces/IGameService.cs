using System;
using System.Collections.Generic;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        // CRUD
        void Add(GameModel gameModel);
        void Remove(GameModel gameModel);
        void Update(GameModel gameModel);

        // Count
        int GetGamesCount();

        // Getting game Model
        GameModel GetGameModelByKey(String key);
        GameModel GetGameModelById(int id);

        // Game exists
        bool GameExists(string key);

        // Getting games
        IEnumerable<GameModel> GetAllGames();
        IEnumerable<GameModel> GetGamesByGenre(GenreModel genreModel);
        IEnumerable<GameModel> GetGamesByPlatformType(PlatformTypeModel platformTypeModel);
    }
}
