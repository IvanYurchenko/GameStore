using System.Collections.Generic;
using GameStore.BLL.Models.Security;

namespace GameStore.BLL.Interfaces
{
    public interface IRoleService
    {
        void AddRole(RoleModel roleModel);

        void UpdateRole(RoleModel roleModel);
        
        void Remove(int roleId);

        RoleModel GetModelById(int roleId);

        IEnumerable<RoleModel> GetAll();

        bool RoleExists(string roleName, int roleId);
    }
}
