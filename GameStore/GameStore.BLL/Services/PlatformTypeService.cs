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

        public IEnumerable<PlatformTypeModel> GetAll()
        {
            var platformTypes = _unitOfWork.PlatformTypeRepository.GetAll();
            var platformTypeModels = Mapper.Map<IEnumerable<PlatformTypeModel>>(platformTypes);
            return platformTypeModels;
        }
    }
}