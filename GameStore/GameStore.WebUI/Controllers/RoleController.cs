using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Security;
using GameStore.WebUI.Security;

namespace GameStore.WebUI.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [ActionName("Get")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Get()
        {
            IEnumerable<RoleModel> roleModels = _roleService.GetAll();

            var roleViewModels = Mapper.Map<IEnumerable<RoleViewModel>>(roleModels);

            return View(roleViewModels);
        }
        [HttpGet]
        [ActionName("New")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AddRole()
        {
            return View(new RoleViewModel());
        }

        [HttpPost]
        [ActionName("New")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AddRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var roleModel = Mapper.Map<RoleModel>(roleViewModel);

                _roleService.Add(roleModel);

                MessageSuccess("The role has been added successfully. ");

                return RedirectToAction("Get", "Role");
            }

            ModelState.AddModelError("", "The information provided is incorrect. ");

            return View(roleViewModel);
        }

        [HttpGet]
        [ActionName("Update")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Update(int roleId)
        {
            RoleModel roleModel = _roleService.GetModelById(roleId);

            if (roleModel == null || roleModel.IsReadonly)
            {
                return RedirectToAction("Get", "Role");
            }

            var roleViewModel = Mapper.Map<RoleViewModel>(roleModel);

            return View(roleViewModel);
        }

        [HttpPost]
        [ActionName("Update")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Update(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var roleModel = Mapper.Map<RoleModel>(roleViewModel);

                _roleService.Update(roleModel);

                MessageSuccess("The role has been updated successfully. ");

                return RedirectToAction("Get", "Role");
            }

            ModelState.AddModelError("", "The information provided is incorrect. ");

            return View(roleViewModel);
        }

        [HttpGet]
        [ActionName("Remove")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Remove(int roleId)
        {
            RoleModel roleModel = _roleService.GetModelById(roleId);

            if (roleModel != null)
            {
                _roleService.Remove(roleId);
            }

            MessageSuccess("The role has been removed successfully. ");

            return RedirectToAction("Get", "Role");
        }
    }
}
