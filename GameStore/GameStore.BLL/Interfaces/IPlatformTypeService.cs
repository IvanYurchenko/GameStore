using System.Collections.Generic;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformTypeService
    {
        #region Getting all platform types

        /// <summary>
        /// Gets all platform types.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PlatformTypeModel> GetAll();

        #endregion
    }
}