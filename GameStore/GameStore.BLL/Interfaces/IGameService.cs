using System;
using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        /// <summary>
        /// Adds the specified game to the database.
        /// </summary>
        /// <param name="gameModel">The game model.</param>
        void Add(GameModel gameModel);

        /// <summary>
        /// Removes the specified game from the database.
        /// </summary>
        /// <param name="gameModel">The game model.</param>
        void Remove(GameModel gameModel);

        /// <summary>
        /// Updates the specified game.
        /// </summary>
        /// <param name="gameModel">The game model.</param>
        void Update(GameModel gameModel);
        
        /// <summary>
        /// Gets the count of games suitable for current filter.
        /// </summary>
        /// <param name="filterCondition">The filter condition.</param>
        /// <returns></returns>
        int GetGamesCount(Func<Game, bool> filterCondition = null);

        /// <summary>
        /// Gets the game model by game key.
        /// </summary>
        /// <param name="key">The game key.</param>
        /// <returns></returns>
        GameModel GetGameModelByKey(String key);

        /// <summary>
        /// Gets the game model by identifier.
        /// </summary>
        /// <param name="id">The game identifier.</param>
        /// <returns></returns>
        GameModel GetGameModelById(int id);
        
        /// <summary>
        /// Determines if a game with the same key already exists and it's NOT a game with specified id
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="currentGameId">Current game id to exclude</param>
        /// <returns></returns>
        bool GameExists(string key, int gameId);


        /// <summary>
        /// Gets all games.
        /// </summary>
        /// <returns></returns>
        IEnumerable<GameModel> GetAll();

        /// <summary>
        /// Gets games by the genre.
        /// </summary>
        /// <param name="genreModel">The genre model.</param>
        /// <returns></returns>
        IEnumerable<GameModel> GetGamesByGenre(GenreModel genreModel);

        /// <summary>
        /// Gets games by the platform type.
        /// </summary>
        /// <param name="platformTypeModel">The platform type model.</param>
        /// <returns></returns>
        IEnumerable<GameModel> GetGamesByPlatformType(PlatformTypeModel platformTypeModel);

        /// <summary>
        /// Gets the games by the filter.
        /// </summary>
        /// <param name="filterModel">The filter model.</param>
        /// <param name="paginationModel">The pagination model.</param>
        /// <returns></returns>
        GamesTransferModel GetGamesByFilter(GamesFilterModel filterModel, PaginationModel paginationModel);
    }
}