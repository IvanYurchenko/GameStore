using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Localization;
using GameStore.DAL.Entities.Localization;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LanguageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<LanguageModel> GetAll()
        {
            IEnumerable<Language> languages = _unitOfWork.LanguageRepository.GetAll();

            var languageModels = Mapper.Map<IEnumerable<LanguageModel>>(languages);

            return languageModels;
        }
    }
}
