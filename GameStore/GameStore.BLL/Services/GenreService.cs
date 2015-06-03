using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.Core;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<GenreModel> GetAll()
        {
            IEnumerable<Genre> genres = _unitOfWork.GenreRepository.GetAll();
            var genreModels = Mapper.Map<IEnumerable<GenreModel>>(genres);
            return genreModels;
        }

        public bool GenreExists(string genreName, int currentGenreId)
        {
            Genre genre =
                _unitOfWork.GenreRepository.Get(g =>
                    g.GenreLocalizations.First(loc =>
                        String.Equals(loc.Language.Code, Constants.EnglishLanguageCode, StringComparison.CurrentCultureIgnoreCase))
                        .Name
                        .ToLower()
                        == genreName.ToLower()
                    && g.GenreId != currentGenreId)
                    .FirstOrDefault();

            bool result = genre != null;
            return result;
        }

        public void Add(GenreModel genreModel)
        {
            var genre = Mapper.Map<Genre>(genreModel);

            _unitOfWork.GenreRepository.Insert(genre);
            _unitOfWork.SaveChanges();
        }

        public void Remove(int genreId)
        {
            _unitOfWork.GenreRepository.Delete(genreId);
            _unitOfWork.SaveChanges();
        }

        public GenreModel GetModelById(int genreId)
        {
            Genre genre = _unitOfWork.GenreRepository.Get(r => r.GenreId == genreId).FirstOrDefault();
            var genreModel = Mapper.Map<GenreModel>(genre);
            return genreModel;
        }

        public void Update(GenreModel genreModel)
        {
            Genre genre = _unitOfWork.GenreRepository.Get(r => r.GenreId == genreModel.GenreId).First();

            if (genre.GenreLocalizations != null)
            {
                genre.GenreLocalizations.ToList().ForEach(loc => _unitOfWork.GenreLocalizationRepository.Delete(loc));
                _unitOfWork.SaveChanges();
            }

            Mapper.Map(genreModel, genre);

            _unitOfWork.GenreRepository.Update(genre);
            _unitOfWork.SaveChanges();
        }
    }
}