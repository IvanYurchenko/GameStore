using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Models.Security;
using GameStore.BLL.Services;
using GameStore.DAL.Entities.Security;
using GameStore.DAL.Interfaces;
using GameStore.WebUI.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.BLL.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        private static UserModel GetUserModel()
        {
            var userModel = new UserModel
            {
                UserId = 1,
                IsReadonly = false,
                CreateDate = DateTime.UtcNow,
                UserName = "UserName",
                Password = "Password",
                LastName = "LastName",
                Email = "abc@my.com",
                FirstName = "FirstName",
                IsDisabled = false,
                Roles = new List<RoleModel>
                {
                    new RoleModel()
                }
            };

            return userModel;
        }

        private static User GetUser()
        {
            var userModel = new User
            {
                UserId = 1,
                IsReadonly = false,
                CreateDate = DateTime.UtcNow,
                UserName = "UserName",
                Password = "Password",
                LastName = "LastName",
                Email = "abc@my.com",
                FirstName = "FirstName",
                IsDisabled = false,
                Roles = new List<Role>
                {
                    new Role()
                }
            };

            return userModel;
        }

        private LoginModel GetLoginModel()
        {
            var loginModel = new LoginModel
            {
                UserName = "UserName",
                Password = "Password",
            };

            return loginModel;
        }

        [TestMethod]
        public void Check_That_User_Service_Registers_User()
        {
            // A
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.RoleRepository.Get(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns<Expression<Func<Role, bool>>>(expr => new List<Role> { new Role() });
            mock.Setup(m => m.UserRepository.Insert(It.IsAny<User>()));

            var userService = new UserService(mock.Object);

            var userModel = GetUserModel();

            // A
            userService.RegisterUser(userModel);

            // A
            mock.Verify(m => m.RoleRepository.Get(It.IsAny<Expression<Func<Role, bool>>>()));
            mock.Verify(m => m.UserRepository.Insert(It.IsAny<User>()));
        }

        [TestMethod]
        public void Check_That_User_Service_Gets_Model_By_Id()
        {
            // A
            var mock = new Mock<IUnitOfWork>();
            var user = GetUser();
            mock.Setup(m => m.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<Expression<Func<User, bool>>>(expr => new List<User> {user});

            var userService = new UserService(mock.Object);

            const int userId = 1;

            // A
            userService.GetModelById(userId);

            // A
            mock.Verify(m => m.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>()));
        }

        [TestMethod]
        public void Check_That_User_Service_Gets_User_Model()
        {
            // A
            var mock = new Mock<IUnitOfWork>();
            var user = GetUser();
            mock.Setup(m => m.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<Expression<Func<User, bool>>>(expr => new List<User> { user });

            var userService = new UserService(mock.Object);
            
            LoginModel loginModel = GetLoginModel();

            // A
            userService.GetUserModel(loginModel);

            // A
            mock.Verify(m => m.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>()));
        }

        [TestMethod]
        public void Check_That_Order_Service_Updates_User()
        {
            // Arrange
            User user = GetUser();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.UserRepository.Update(It.IsAny<User>())).Verifiable();
            mock.Setup(m => m.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<Expression<Func<User, bool>>>(expr => new List<User> { user });

            var userService = new UserService(mock.Object);

            var userModel = new UserModel
            {
                UserId = 1,
            };

            // Act
            userService.UpdateUser(userModel);

            // Assert
            mock.Verify(m => m.UserRepository.Update(It.IsAny<User>()));
        }

        [TestMethod]
        public void Check_That_Order_Service_Gets_All_Users()
        {
            // Arrange
            User user = GetUser();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.UserRepository.GetAll()).Verifiable();

            var userService = new UserService(mock.Object);
            
            // Act
            userService.GetAll();

            // Assert
            mock.Verify(m => m.UserRepository.GetAll());
        }

        [TestMethod]
        public void Check_That_Order_Service_Checks_If_User_Exists()
        {
            // Arrange
            User user = GetUser();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<Expression<Func<User, bool>>>(expr => new List<User> { user });

            var userService = new UserService(mock.Object);

            const string userName = "userName";

            // Act
            bool result = userService.UserExists(userName);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_That_Order_Service_Checks_If_User_Does_Not_Exists()
        {
            // Arrange
            User user = GetUser();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<Expression<Func<User, bool>>>(expr => new List<User>());

            var userService = new UserService(mock.Object);

            const string userName = "userName";

            // Act
            bool result = userService.UserExists(userName);

            // Assert
            Assert.IsTrue(!result);
        }
    }
}
