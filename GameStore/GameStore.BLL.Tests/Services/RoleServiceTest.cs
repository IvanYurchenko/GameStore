using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public class RoleServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        private static RoleModel GetRoleModel()
        {
            var roleModel = new RoleModel
            {
                RoleId = 1,
                RoleName = "role",
                Description = "description",
                IsReadonly = false,
            };

            return roleModel;
        }

        private static Role GetRole()
        {
            var role = new Role
            {
                RoleId = 1,
                RoleName = "role",
                Description = "description",
                IsReadonly = false,
            };

            return role;
        }

        [TestMethod]
        public void Check_That_Role_Service_Adds_Role()
        {
            // A
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.RoleRepository.Insert(It.IsAny<Role>()));

            var roleService = new RoleService(mock.Object);

            RoleModel roleModel = GetRoleModel();

            // A
            roleService.Add(roleModel);

            // A
            mock.Verify(m => m.RoleRepository.Insert(It.IsAny<Role>()));
        }

        [TestMethod]
        public void Check_That_Role_Service_Gets_Model_By_Id()
        {
            // A
            var mock = new Mock<IUnitOfWork>();
            Role role = GetRole();
            mock.Setup(m => m.RoleRepository.Get(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns<Expression<Func<Role, bool>>>(expr => new List<Role> { role });

            var roleService = new RoleService(mock.Object);

            const int roleId = 1;

            // A
            roleService.GetModelById(roleId);

            // A
            mock.Verify(m => m.RoleRepository.Get(It.IsAny<Expression<Func<Role, bool>>>()));
        }

        [TestMethod]
        public void Check_That_Order_Service_Updates_Role()
        {
            // Arrange
            Role role = GetRole();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.RoleRepository.Get(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns<Expression<Func<Role, bool>>>(expr => new List<Role> { role });
            mock.Setup(m => m.RoleRepository.Update(It.IsAny<Role>())).Verifiable();

            var roleService = new RoleService(mock.Object);

            RoleModel roleModel = GetRoleModel();

            // Act
            roleService.Update(roleModel);

            // Assert
            mock.Verify(m => m.RoleRepository.Update(It.IsAny<Role>()));
        }

        [TestMethod]
        public void Check_That_Order_Service_Gets_All_Roles()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.RoleRepository.GetAll()).Verifiable();

            var roleService = new RoleService(mock.Object);

            // Act
            roleService.GetAll();

            // Assert
            mock.Verify(m => m.RoleRepository.GetAll());
        }

        [TestMethod]
        public void Check_That_Order_Service_Checks_If_Role_Exists()
        {
            // Arrange
            Role role = GetRole();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.RoleRepository.Get(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns<Expression<Func<Role, bool>>>(expr => new List<Role> { role });

            var roleService = new RoleService(mock.Object);

            const string roleName = "RoleName";
            const int currentRoleId = 1;

            // Act
            bool result = roleService.RoleExists(roleName, currentRoleId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_That_Order_Service_Checks_If_Role_Does_Not_Exists()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.RoleRepository.Get(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns<Expression<Func<Role, bool>>>(expr => new List<Role>());

            var roleService = new RoleService(mock.Object);

            const string roleName = "RoleName";
            const int currentRoleId = 1;

            // Act
            bool result = roleService.RoleExists(roleName, currentRoleId);

            // Assert
            Assert.IsTrue(!result);
        }
    }
}
