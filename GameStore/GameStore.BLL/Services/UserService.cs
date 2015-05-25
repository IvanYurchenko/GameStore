using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Security;
using GameStore.Core;
using GameStore.DAL.Entities.Security;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void RegisterUser(UserModel userModel)
        {
            Role roleUser = _unitOfWork.RoleRepository
                .Get(x => x.RoleName.ToLower() == Constants.UserRoleName.ToLower()).First();

            var user = Mapper.Map<User>(userModel);
            
            user.Roles = new List<Role>
            {
                roleUser
            };

            user.CreateDate = DateTime.UtcNow;

            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.SaveChanges();
        }

        public UserModel GetModelById(int userId)
        {
            User user = _unitOfWork.UserRepository.Get(u => u.UserId == userId).FirstOrDefault();
            var userModel = Mapper.Map<UserModel>(user);
            return userModel;
        }

        public UserModel GetUserModel(LoginModel loginModel)
        {
            UserModel userModel = null;

            User user =
                _unitOfWork.UserRepository.Get(
                    u => u.UserName == loginModel.Username && u.Password == loginModel.Password).FirstOrDefault();

            if (user != null)
            {
                userModel = Mapper.Map<UserModel>(user);
            }

            return userModel;
        }

        public void UpdateUser(UserModel userModel)
        {
            User user = _unitOfWork.UserRepository.Get(u => u.UserId == userModel.UserId).First();

            Mapper.Map(userModel, user);

            if (userModel.Roles != null)
            {
                user.Roles = userModel.Roles.Select(r => _unitOfWork.RoleRepository.Get(x => x.RoleId == r.RoleId).First()).ToList();
            }

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<UserModel> GetAll()
        {
            IEnumerable<User> users = _unitOfWork.UserRepository.GetAll();
            var userModels = Mapper.Map<IEnumerable<UserModel>>(users);
            return userModels;
        }

        public bool UserExists(string userName)
        {
            User user = _unitOfWork.UserRepository.Get(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
            bool result = user != null;
            return result;
        }
    }
}