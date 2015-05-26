using System.Collections.Generic;
using GameStore.BLL.Models.Security;

namespace GameStore.BLL.Interfaces
{
    public interface IUserService
    {
        void Register(UserModel userModel);

        UserModel GetModelById(int userId);
        UserModel GetUserModel(LoginModel loginModel);

        void Update(UserModel userModel);

        IEnumerable<UserModel> GetAll(); 

        bool UserExists(string userName);
    }
}
