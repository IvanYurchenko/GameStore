using System.Collections.Generic;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;

namespace GameStore.BLL.Services
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlatformTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PlatformTypeModel ConvertToModel(PlatformType platformType)
        {
            PlatformTypeModel result = Mapper.Map<PlatformTypeModel>(platformType);
            return result;
        }

        public PlatformType ConvertToPlatformType(PlatformTypeModel platformTypeModel)
        {
            PlatformType result = Mapper.Map<PlatformType>(platformTypeModel);
            return result;
        }

        public IEnumerable<PlatformTypeModel> GetAll()
        {
            var platformTypes = _unitOfWork.PlatformTypeRepository.GetAll();
            var platformTypeModels = Mapper.Map<IEnumerable<PlatformTypeModel>>(platformTypes);
            return platformTypeModels;
        }
    }
}