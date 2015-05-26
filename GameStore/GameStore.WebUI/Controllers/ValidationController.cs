using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.UI;
using GameStore.BLL.Interfaces;

namespace GameStore.WebUI.Controllers
{
    [OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
    public class ValidationController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IPublisherService _publisherService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IGenreService _genreService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationController" /> class.
        /// </summary>
        /// <param name="gameService">The game service.</param>
        /// <param name="publisherService">The publisher service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="roleService">The role service.</param>
        /// <param name="genreService">The genre service.</param>
        public ValidationController(
            IGameService gameService,
            IPublisherService publisherService,
            IUserService userService,
            IRoleService roleService,
            IGenreService genreService)
        {
            _gameService = gameService;
            _publisherService = publisherService;
            _userService = userService;
            _roleService = roleService;
            _genreService = genreService;
        }

        /// <summary>
        /// Determines whether [is game key available] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="gameId">The game identifier.</param>
        /// <returns></returns>
        public JsonResult IsGameKeyAvailable(string key, int gameId)
        {
            JsonResult result = !_gameService.GameExists(key, gameId)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(String.Format(CultureInfo.InvariantCulture, "The game with key '{0}' already exists.", key),
                    JsonRequestBehavior.AllowGet);

            return result;
        }

        /// <summary>
        /// Determines whether [is company name available] [the specified company name].
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="publisherId">The publisher identifier.</param>
        /// <returns></returns>
        public JsonResult IsCompanyNameAvailable(string companyName, int publisherId)
        {
            JsonResult result = !_publisherService.PublisherExists(companyName, publisherId)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(
                    String.Format(CultureInfo.InvariantCulture, "The company with name '{0}' already exists.",
                        companyName),
                    JsonRequestBehavior.AllowGet);

            return result;
        }

        public JsonResult IsUserNameAvailable(string userName)
        {
            JsonResult result = !_userService.UserExists(userName)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(
                    String.Format(CultureInfo.InvariantCulture, "The user with name '{0}' already exists.", userName),
                    JsonRequestBehavior.AllowGet);

            return result;
        }

        public JsonResult IsRoleNameAvailable(string roleName, int roleId)
        {
            JsonResult result = !_roleService.RoleExists(roleName, roleId)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(
                    String.Format(CultureInfo.InvariantCulture, "The role with name '{0}' already exists.", roleName),
                    JsonRequestBehavior.AllowGet);

            return result;
        }

        public JsonResult IsGenreNameAvailable(string genreName, int genreId)
        {
            JsonResult result = !_genreService.GenreExists(genreName, genreId)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(
                    String.Format(CultureInfo.InvariantCulture, "The genre with name '{0}' already exists.", genreName),
                    JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}