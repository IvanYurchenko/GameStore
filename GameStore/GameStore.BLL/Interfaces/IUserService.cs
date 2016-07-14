using System.Collections.Generic;
using GameStore.BLL.Models.Security;

namespace GameStore.BLL.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        void Register(UserModel userModel);

        /// <summary>
        /// Gets the user model by ID.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        UserModel GetModelById(int userId);

        /// <summary>
        /// Gets the user model by login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns></returns>
        UserModel GetUserModel(LoginModel loginModel);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        void Update(UserModel userModel);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserModel> GetAll();

        /// <summary>
        /// Checks if a user with a specified name exists in the database.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        bool UserExists(string userName);
    }
}
