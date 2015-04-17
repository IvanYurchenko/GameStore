using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        // Converting
        GenreModel ConvertToModel(Genre genre);
        Genre ConvertToGenre(GenreModel genreModel);

        IEnumerable<GenreModel> GetAllGenres();
    }
}