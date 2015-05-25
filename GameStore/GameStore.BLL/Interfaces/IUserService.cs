using System.Collections.Generic;
using GameStore.BLL.Models.Security;

namespace GameStore.BLL.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(UserModel userModel);

        UserModel GetModelById(int userId);
        UserModel GetUserModel(LoginModel loginModel);

        void UpdateUser(UserModel userModel);

        IEnumerable<UserModel> GetAll(); 

        bool UserExists(string userName);
    }
}
