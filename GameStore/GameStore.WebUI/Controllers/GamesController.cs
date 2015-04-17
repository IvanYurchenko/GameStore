using System.Collections.Generic;
using System.Net.Mime;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebUI.Filters;

namespace GameStore.WebUI.Controllers
{
    [ExceptionLoggingFilter]
    [PerformanceLoggingFilter]
    public class GamesController : BootstrapBaseController
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IOrderItemService _orderItemService;
        private readonly IBasketService _basketService;
        private readonly ILogger _logger;

        public GamesController(
            IGameService gameService,
            ICommentService commentService,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IOrderItemService orderItemService,
            IBasketService basketService,
            ILogger logger)
        {
            _gameService = gameService;
            _commentService = commentService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _orderItemService = orderItemService;
            _basketService = basketService;
            _logger = logger;
        }

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

        public ActionResult Index()
        {
            return RedirectToAction("Games");
        }

        #region Get games lists

        [HttpGet]
        [ActionName("Games")]
        //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult GetGames()
        {
            IEnumerable<GameModel> games = _gameService.GetAllGames();
            return View(games);
        }

        [HttpGet]
        public ActionResult GetGamesByGenre()
        {
            IEnumerable<GenreModel> genres = _genreService.GetAllGenres();

            return View(genres);
        }

        [HttpPost]
        public ActionResult GetGamesByGenre(GenreModel genreModel)
        {
            IEnumerable<GameModel> games = _gameService.GetGamesByGenre(genreModel);
            return Json(games, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetGamesByPlatformType()
        {
            IEnumerable<PlatformTypeModel> platformTypes = _platformTypeService.GetAllPlatformTypes();

            return View(platformTypes);
        }

        [HttpPost]
        public ActionResult GetGamesByPlatformType(PlatformTypeModel platformTypeModel)
        {
            IEnumerable<GameModel> games = _gameService.GetGamesByPlatformType(platformTypeModel);
            return Json(games, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Working with a single game

        [HttpGet]
        [ActionName("Details")]
        //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult GetGameDetails(int gameId)
        {
            GameModel model = _gameService.GetGameModelById(gameId);
            return View(model);
        }

        [HttpGet]
        [ActionName("New")]
        public ActionResult AddGame()
        {
            return View(new GameModel());
        }

        [HttpPost]
        [ActionName("New")]
        public ActionResult AddGame(GameModel gameModel)
        {
            if (ModelState.IsValid)
            {
                _gameService.Add(gameModel);

                Success("The game has been added successfully!");
                return RedirectToAction("Index");
            }

            return View(gameModel);
        }

        [HttpGet]
        [ActionName("Update")]
        public ActionResult UpdateGame(int gameId)
        {
            GameModel game = _gameService.GetGameModelById(gameId);
            return View(game);
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult UpdateGame(GameModel gameModel)
        {
            _gameService.Update(gameModel);
            Success("The game has been updated successfully!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Remove")]
        public ActionResult RemoveGame(int gameId)
        {
            GameModel model = _gameService.GetGameModelById(gameId);
            return View(model);
        }

        [HttpPost]
        [ActionName("Remove")]
        public ActionResult RemoveGame(GameModel gameModel)
        {
            _gameService.Remove(gameModel);
            Success("The game has been removed successfully!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Download")]
        //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult DownloadGame()
        {
            byte[] fileBytes = {1, 2, 3};
            var fileName = "myfile.bin";
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }

        #endregion

        #region Comments

        [HttpGet]
        [ActionName("NewComment")]
        public ActionResult AddComment()
        {
            return View(new CommentModel());
        }

        [HttpPost]
        [ActionName("NewComment")]
        public ActionResult AddComment(string key, CommentModel commentModel)
        {
            _commentService.Add(commentModel, key);
            Success("The comment has been added successfully!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Comments")]
        public ActionResult GetComments(string key)
        {
            GameModel game = _gameService.GetGameModelByKey(key);
            ICollection<CommentModel> comments = game.Comments;
            return View(comments);
        }

        #endregion
    }
}