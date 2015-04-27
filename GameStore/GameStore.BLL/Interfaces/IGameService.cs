using System;
using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        #region CRUD
        void Add(GameModel gameModel);
        void Remove(GameModel gameModel);
        void Update(GameModel gameModel);

        #endregion

        #region Count

        int GetGamesCount(Func<Game, bool> filterCondition = null);

        #endregion

        #region Getting game Model

        GameModel GetGameModelByKey(String key);
        GameModel GetGameModelById(int id);

        #endregion

        #region Game exists

        bool GameExists(string key, int gameId);

        #endregion

        #region Getting games

        IEnumerable<GameModel> GetAll();
        IEnumerable<GameModel> GetGamesByGenre(GenreModel genreModel);
        IEnumerable<GameModel> GetGamesByPlatformType(PlatformTypeModel platformTypeModel);
        GamesTransferModel GetGamesByFilter(GamesFilterModel filterModel, PaginationModel paginationModel);

        #endregion
    }
}