using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Security;
using GameStore.WebUI.Security;
using Newtonsoft.Json;

namespace GameStore.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionName("Login")]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Get", "Game");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Login")]
        public ActionResult Login(LoginViewModel loginViewModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var loginModel = Mapper.Map<LoginModel>(loginViewModel);

                UserModel userModel = _userService.GetUserModel(loginModel);
                if (userModel != null)
                {
                    LoginUser(userModel);

                    return RedirectToAction("Get", "Game");
                }

                ModelState.AddModelError(string.Empty, "Incorrect username and/or password");
            }

            return View(loginViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionName("Register")]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Get", "Game");
            }

            return View(new RegistrationViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Register")]
        public ActionResult Register(RegistrationViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var userModel = Mapper.Map<UserModel>(registerViewModel);
                _userService.RegisterUser(userModel);

                var loginModel = Mapper.Map<LoginModel>(userModel);
                userModel = _userService.GetUserModel(loginModel);

                LoginUser(userModel);

                return RedirectToAction("Get", "Game");
            }

            ModelState.AddModelError(string.Empty, "Information provided is incorrect. ");
            return View(registerViewModel);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Get", "Game");
        }

        private void LoginUser(UserModel userModel)
        {
            var serializeModel = Mapper.Map<CustomPrincipalSerializeModel>(userModel);

            string userData = JsonConvert.SerializeObject(serializeModel);

            var authTicket = new FormsAuthenticationTicket(
                1,
                userModel.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(15),
                false, // pass here true, if you want to implement remember me functionality
                userData);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);
        }
    }
}