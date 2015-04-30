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
    }
}