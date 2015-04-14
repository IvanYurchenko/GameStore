using System.Collections.Generic;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.UnitsOfWork;

namespace GameStore.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GenreModel ConvertToModel(Genre genre)
        {
            GenreModel result = Mapper.Map<GenreModel>(genre);
            return result;
        }

        public Genre ConvertToGenre(GenreModel genreModel)
        {
            Genre result = Mapper.Map<Genre>(genreModel);
            return result;
        }

        public IEnumerable<GenreModel> GetAllGenres()
        {
            var genres = _unitOfWork.GenreRepository.GetAll();
            var genreModels = Mapper.Map<IEnumerable<GenreModel>>(genres);
            return genreModels;
        }
    }
}
