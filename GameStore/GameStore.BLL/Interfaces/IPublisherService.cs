using System.Collections.Generic;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService
    {
        void Add(PublisherModel model);
        PublisherModel GetModelByCompanyName(string companyName);

        IEnumerable<PublisherModel> GetAll();

        bool PublisherExists(string companyName);
    }
}