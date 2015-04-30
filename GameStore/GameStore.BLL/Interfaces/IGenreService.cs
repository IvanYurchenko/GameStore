using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        IEnumerable<GenreModel> GetAll();
    }
}