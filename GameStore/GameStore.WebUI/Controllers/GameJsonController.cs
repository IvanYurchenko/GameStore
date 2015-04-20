using System.Collections.Generic;
using System.Net.Mime;
using System.Web.Mvc;
using System.Web.UI;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebUI.Filters;

namespace GameStore.WebUI.Controllers
{
    [ExceptionLoggingFilter]
    [PerformanceLoggingFilter]
    public class GameJsonController : Controller
    {
        public GameJsonController(
            IGameService gameService,
            ICommentService commentService,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            ILogger logger)
        {
            _gameService = gameService;
            _commentService = commentService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _logger = logger;
        }

        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly ILogger _logger;

        private const string _success = "Success.";
        private const string _failure = "Failure.";

        // Tasks
        //User can create game (POST URL: /games/new).
        //User can edit game (POST URL: /games/update).
        //User can get game details by key (GET URL: /game/{key}).
        //User can get all games (GET URL: /games).
        //User can delete game (POST URL: /games/remove).
        //User can leave comment for game (POST URL: /game/{gamekey}/newcomment).
        //User can leave comment for another comment (POST URL: /game/{gamekey}/newcomment)
        //User can get all comments by game key (POST URL: /game/{gamekey}/comments).
        //User can download game (jut return any binary file as response) (GET URL: /game/{gamekey}/download)

        #region Get games lists

        [HttpGet]
        [ActionName("Games")]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult GetGames()
        {
            IEnumerable<GameModel> games = _gameService.GetAllGames();
            return Json(games, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGamesByGenre(GenreModel genreModel)
        {
            IEnumerable<GameModel> games = _gameService.GetGamesByGenre(genreModel);
            return Json(games, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGamesByPlatformType(PlatformTypeModel platformTypeModel)
        {
            IEnumerable<GameModel> games = _gameService.GetGamesByPlatformType(platformTypeModel);
            return Json(games, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Working with a single game

        [HttpGet]
        [ActionName("Details")]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public JsonResult GetGameByKey(string key)
        {
            GameModel game = _gameService.GetGameModelByKey(key);
            return Json(game, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("New")]
        public JsonResult AddGame(GameModel gameModel)
        {
            _gameService.Add(gameModel);

            return Json(_success);
        }

        [HttpPost]
        [ActionName("Update")]
        public JsonResult UpdateGame(GameModel gameModel)
        {
            _gameService.Update(gameModel);

            return Json(_success);
        }

        [HttpPost]
        [ActionName("Remove")]
        public JsonResult RemoveGame(GameModel gameModel)
        {
            _gameService.Remove(gameModel);

            return Json(_success);
        }

        [HttpGet]
        [ActionName("Download")]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult DownloadGame()
        {
            byte[] fileBytes = {1, 2, 3};
            var fileName = "myfile.bin";
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }

        #endregion

        #region Comments

        [HttpPost]
        [ActionName("NewComment")]
        public JsonResult AddComment(string key, CommentModel commentModel)
        {
            _commentService.Add(commentModel, key);

            return Json(_success);
        }

        [HttpPost]
        [ActionName("Comments")]
        public JsonResult GetComments(string key)
        {
            GameModel game = _gameService.GetGameModelByKey(key);
            ICollection<CommentModel> comments = game.Comments;
            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}