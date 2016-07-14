using System.Collections.Generic;
using GameStore.BLL.Models.Security;

namespace GameStore.BLL.Interfaces
{
    public interface IRoleService
    {
        /// <summary>
        /// Adds the specified role to the database.
        /// </summary>
        /// <param name="roleModel">The role model.</param>
        void Add(RoleModel roleModel);

        /// <summary>
        /// Updates the specified role.
        /// </summary>
        /// <param name="roleModel">The role model.</param>
        void Update(RoleModel roleModel);

        /// <summary>
        /// Removes the specified role from the database.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        void Remove(int roleId);

        /// <summary>
        /// Gets the model by it's ID.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        RoleModel GetModelById(int roleId);

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns></returns>
        IEnumerable<RoleModel> GetAll();

        /// <summary>
        /// Determines if a role exists and it's NOT a role with the specified ID.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        bool RoleExists(string roleName, int roleId);
    }
}
