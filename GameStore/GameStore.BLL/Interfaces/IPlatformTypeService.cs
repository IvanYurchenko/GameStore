using System.Collections.Generic;
using GameStore.BLL.Models;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformTypeService
    {
        #region Getting all platform types

        IEnumerable<PlatformTypeModel> GetAll();

        #endregion
    }
}