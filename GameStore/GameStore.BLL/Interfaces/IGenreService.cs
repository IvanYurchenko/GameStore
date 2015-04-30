using System.Collections.Generic;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        IEnumerable<GenreModel> GetAll();
    }
}