using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;

namespace GameStore.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PublisherModel> GetAll()
        {
            IEnumerable<Publisher> publishers = _unitOfWork.PublisherRepository.GetAll();
            var publisherModels = Mapper.Map<IEnumerable<PublisherModel>>(publishers);
            return publisherModels;
        }

        public void Add(PublisherModel model)
        {
            var publisher = Mapper.Map<Publisher>(model);
            _unitOfWork.PublisherRepository.Insert(publisher);
            _unitOfWork.Save();
        }

        public PublisherModel GetModelByCompanyName(string companyName)
        {
            Publisher publisher = _unitOfWork.PublisherRepository.Get(p => p.CompanyName == companyName).First();
            var model = Mapper.Map<PublisherModel>(publisher);
            return model;
        }

        public PublisherModel GetModelById(int publisherId)
        {
            var publisher = _unitOfWork.PublisherRepository.GetById(publisherId);
            var model = Mapper.Map<PublisherModel>(publisher);
            return model;
        }

        public bool PublisherExists(string companyName)
        {
            bool publisherExists = _unitOfWork.PublisherRepository.Get(p => p.CompanyName == companyName).Any();
            return publisherExists;
        }
    }
}