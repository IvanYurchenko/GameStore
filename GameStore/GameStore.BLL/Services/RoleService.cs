using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Security;
using GameStore.DAL.Entities.Security;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddRole(RoleModel roleModel)
        {
            var role = Mapper.Map<Role>(roleModel);

            _unitOfWork.RoleRepository.Insert(role);
            _unitOfWork.SaveChanges();
        }

        public void Remove(int roleId)
        {
            _unitOfWork.RoleRepository.Delete(roleId);
            _unitOfWork.SaveChanges();
        }

        public RoleModel GetModelById(int roleId)
        {
            Role role = _unitOfWork.RoleRepository.Get(r => r.RoleId == roleId).FirstOrDefault();
            var roleModel = Mapper.Map<RoleModel>(role);
            return roleModel;
        }

        public void UpdateRole(RoleModel roleModel)
        {
            Role role = _unitOfWork.RoleRepository.Get(r => r.RoleId == roleModel.RoleId).First();

            Mapper.Map(roleModel, role);

            _unitOfWork.RoleRepository.Update(role);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<RoleModel> GetAll()
        {
            IEnumerable<Role> roles = _unitOfWork.RoleRepository.GetAll();
            var roleModels = Mapper.Map<IEnumerable<RoleModel>>(roles);
            return roleModels;
        }

        public bool RoleExists(string roleName, int roleId)
        {
            Role role = _unitOfWork.RoleRepository
                .Get(r => r.RoleName.ToLower() == roleName.ToLower() && r.RoleId != roleId)
                .FirstOrDefault();

            bool result = role != null;
            return result;
        }
    }
}