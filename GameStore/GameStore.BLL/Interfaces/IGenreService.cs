using System.Collections.Generic;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        IEnumerable<GenreModel> GetAll();

        bool GenreExists(string genreName, int currentGenreId);

        void Add(GenreModel genreModel);

        void Update(GenreModel genreModel);

        void Remove(int genreId);

        GenreModel GetModelById(int genreId);
    }
}