using AutoMapper;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;

namespace GameStore.BLL.Services
{
    public class PublisherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private void Add(PublisherModel model)
        {
            var publisher = Mapper.Map<Publisher>(model);
            _unitOfWork.PublisherRepository.Insert(publisher);
        }

        private PublisherModel GetModelByCompanyName(string companyName)
        {
            var publisher = _unitOfWork.PublisherRepository.Get(p => p.CompanyName == companyName);
            var model = Mapper.Map<PublisherModel>(publisher);
            return model;
        }
    }
}