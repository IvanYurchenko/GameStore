using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        #region Converting

        GenreModel ConvertToModel(Genre genre);
        Genre ConvertToGenre(GenreModel genreModel);

        #endregion

        IEnumerable<GenreModel> GetAll();
    }
}