using System.Collections.Generic;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
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

        public IEnumerable<GenreModel> GetAll()
        {
            var genres = _unitOfWork.GenreRepository.GetAll();
            var genreModels = Mapper.Map<IEnumerable<GenreModel>>(genres);
            return genreModels;
        }
    }
}