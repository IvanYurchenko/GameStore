using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;
using AutoMapper;
using BootstrapMvcSample.Controllers;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.WebUI.Filters;
using GameStore.WebUI.ViewModels;

namespace GameStore.WebUI.Controllers
{
    [ExceptionLoggingFilter]
    [PerformanceLoggingFilter]
    public class GameController : BootstrapBaseController
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IOrderItemService _orderItemService;
        private readonly IBasketService _basketService;
        private readonly ILogger _logger;

        public GameController(
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
        public ActionResult GetGames()
        {
            IEnumerable<GameModel> games = _gameService.GetAllGames();
            return View(games);
        }

        [HttpPost]
        [ActionName("Games")]
        public ActionResult GetGames(GamesFilterModel gamesFilterModel)
        {
            IEnumerable<GameModel> games = _gameService.GetGamesByFilter(gamesFilterModel);
            return View(games);
        }
        #endregion

        #region Working with a single game
        [HttpGet]
        [ActionName("Details")]
        //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult GetGameDetails(int id)
        {
            GameModel gameModel = _gameService.GetGameModelById(id);
            var gameViewModel = Mapper.Map<GameViewModel>(gameModel);
            return View(gameViewModel);
        }

        [HttpGet]
        [ActionName("New")]
        public ActionResult AddGame()
        {
            var gameViewModel = new GameViewModel();
            gameViewModel.PlatformTypes = _platformTypeService.GetAllPlatformTypes();
            gameViewModel.Genres = _genreService.GetAllGenres();

            return View(gameViewModel);
        }

        [HttpPost]
        [ActionName("New")]
        public ActionResult AddGame(GameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                var gameModel = Mapper.Map<GameModel>(gameViewModel);
                _gameService.Add(gameModel);

                MessageSuccess("The game has been added successfully!");
                return RedirectToAction("Index");
            }

            return View(gameViewModel);
        }

        [HttpGet]
        [ActionName("Update")]
        public ActionResult UpdateGame(int gameId)
        {
            GameModel gameModel = _gameService.GetGameModelById(gameId);
            var gameViewModel = Mapper.Map<GameViewModel>(gameModel);
            gameViewModel.SelectedGenres.AddRange(gameViewModel.Genres.Select(g => g.GenreId));
            gameViewModel.Genres = _genreService.GetAllGenres();
            gameViewModel.SelectedPlatformTypes.AddRange(gameViewModel.PlatformTypes.Select(g => g.PlatformTypeId));
            gameViewModel.PlatformTypes = _platformTypeService.GetAllPlatformTypes();

            return View(gameViewModel);
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult UpdateGame(GameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                var gameModel = Mapper.Map<GameModel>(gameViewModel);
                _gameService.Update(gameModel);
                MessageSuccess("The game has been updated successfully!");
                return RedirectToAction("Index");
            }

            return View(gameViewModel);
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
            MessageSuccess("The game has been removed successfully!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Download")]
        //[OutputCache(Duration = 60, Location = OutputCacheLocation.Any)]
        public ActionResult DownloadGame(int gameId)
        {
            var gameModel = _gameService.GetGameModelById(gameId);
            var fileBytes = new byte[gameModel.Name.Length * sizeof(char)];
            Buffer.BlockCopy(gameModel.Name.ToCharArray(), 0, fileBytes, 0, fileBytes.Length);
            var fileName = String.Format("{0}.bin", gameModel.Name);
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }        
        #endregion
    }
}