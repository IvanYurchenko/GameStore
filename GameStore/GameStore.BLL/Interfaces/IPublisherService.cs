using System.Collections.Generic;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService
    {
        /// <summary>
        /// Adds a publisher to the database.
        /// </summary>
        /// <param name="model">The publisher model.</param>
        void Add(PublisherModel model);

        /// <summary>
        /// Gets the publisher model by a company name.
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <returns></returns>
        PublisherModel GetModelByCompanyName(string companyName);

        /// <summary>
        /// Gets the publisher model by identifier.
        /// </summary>
        /// <param name="publisherId">The publisher identifier.</param>
        /// <returns></returns>
        PublisherModel GetModelById(int publisherId);

        /// <summary>
        /// Gets all publishers.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PublisherModel> GetAll();

        /// <summary>
        /// Determines if a publisher with target company name already exists exclude publisher with specified identifier.
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="currentPublisherId">The current publisher identifier.</param>
        /// <returns></returns>
        bool PublisherExists(string companyName, int currentPublisherId);
    }
}