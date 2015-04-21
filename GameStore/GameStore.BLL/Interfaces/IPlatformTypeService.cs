using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformTypeService
    {
        #region Converting
        PlatformTypeModel ConvertToModel(PlatformType platformType);
        PlatformType ConvertToPlatformType(PlatformTypeModel platformTypeModel);
        #endregion

        #region Getting all platform types
        IEnumerable<PlatformTypeModel> GetAll();
        #endregion
    }
}