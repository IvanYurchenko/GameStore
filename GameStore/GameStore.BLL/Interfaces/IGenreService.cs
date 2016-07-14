using System.Collections.Generic;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        /// <summary>
        /// Gets all genres.
        /// </summary>
        /// <returns></returns>
        IEnumerable<GenreModel> GetAll();

        /// <summary>
        /// Determines if a genre with specified name exists and it's NOT a genre with specified ID.
        /// </summary>
        /// <param name="genreName">Name of the genre.</param>
        /// <param name="currentGenreId">The current genre identifier.</param>
        /// <returns></returns>
        bool GenreExists(string genreName, int currentGenreId = 0);

        /// <summary>
        /// Adds the specified genre to the database.
        /// </summary>
        /// <param name="genreModel">The genre model.</param>
        void Add(GenreModel genreModel);

        /// <summary>
        /// Updates the specified genre.
        /// </summary>
        /// <param name="genreModel">The genre model.</param>
        void Update(GenreModel genreModel);

        /// <summary>
        /// Removes the specified genre from the database.
        /// </summary>
        /// <param name="genreId">The genre identifier.</param>
        void Remove(int genreId);

        /// <summary>
        /// Gets the genre model by id.
        /// </summary>
        /// <param name="genreId">The genre identifier.</param>
        /// <returns></returns>
        GenreModel GetModelById(int genreId);
    }
}