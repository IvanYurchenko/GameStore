using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformTypeService
    {
        // Converting
        PlatformTypeModel ConvertToModel(PlatformType platformType);
        PlatformType ConvertToPlatformType(PlatformTypeModel platformTypeModel);

        // Getting all platform types
        IEnumerable<PlatformTypeModel> GetAllPlatformTypes();
    }
}