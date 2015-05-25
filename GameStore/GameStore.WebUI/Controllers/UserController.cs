using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Security;
using GameStore.WebUI.Security;

namespace GameStore.WebUI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService,
            IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        [ActionName("Get")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Get()
        {
            IEnumerable<UserModel> userModels = _userService.GetAll();

            var userViewModels = Mapper.Map<IEnumerable<UserViewModel>>(userModels);

            return View(userViewModels);
        }

        [HttpGet]
        [ActionName("Update")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Update(int userId)
        {
            UserModel userModel = _userService.GetModelById(userId);

            if (userModel == null || userModel.IsReadonly)
            {
                return RedirectToAction("Get", "User");
            }

            var userViewModel = Mapper.Map<UserViewModel>(userModel);

            userViewModel.AllRoles = _roleService.GetAll();

            userViewModel.SelectedRoles = userViewModel.AllRoles
                .Where(r => userModel.Roles.Any(ur => ur.RoleId == r.RoleId))
                .Select(r => r.RoleId).ToList();

            return View(userViewModel);
        }

        [HttpPost]
        [ActionName("Update")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Update(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var userModel = Mapper.Map<UserModel>(userViewModel);

                _userService.UpdateUser(userModel);

                MessageSuccess("The user has been updated successfully. ");

                return RedirectToAction("Get", "User");
            }
            
            return View(userViewModel);
        }
        
        [HttpGet]
        [ActionName("Disable")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Disable(int userId)
        {
            UserModel userModel = _userService.GetModelById(userId);

            if (userModel == null || userModel.IsReadonly || userModel.IsDisabled)
            {
                return RedirectToAction("Get", "User");
            }

            userModel.IsDisabled = true;
            _userService.UpdateUser(userModel);

            MessageSuccess("The user has been disabled. ");

            return RedirectToAction("Get", "User");
        }

        [HttpGet]
        [ActionName("Enable")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Enable(int userId)
        {
            UserModel userModel = _userService.GetModelById(userId);

            if (userModel == null || userModel.IsReadonly || !userModel.IsDisabled)
            {
                return RedirectToAction("Get", "User");
            }

            userModel.IsDisabled = false;
            _userService.UpdateUser(userModel);

            MessageSuccess("The user has been enabled. ");

            return RedirectToAction("Get", "User");
        }
    }
}
