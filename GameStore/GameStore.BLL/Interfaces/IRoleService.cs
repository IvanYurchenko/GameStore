using System.Collections.Generic;
using GameStore.BLL.Models.Security;

namespace GameStore.BLL.Interfaces
{
    public interface IRoleService
    {
        void Add(RoleModel roleModel);

        void Update(RoleModel roleModel);
        
        void Remove(int roleId);

        RoleModel GetModelById(int roleId);

        IEnumerable<RoleModel> GetAll();

        bool RoleExists(string roleName, int roleId);
    }
}
