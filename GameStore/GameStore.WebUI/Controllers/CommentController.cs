using System.Collections.Generic;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebUI.Filters;

namespace GameStore.WebUI.Controllers
{
    [ExceptionLoggingFilter]
    [PerformanceLoggingFilter]
    public class CommentController : BootstrapBaseController
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        /// <param name="gameService">The game service.</param>
        /// <param name="commentService">The comment service.</param>
        public CommentController(
            IGameService gameService,
            ICommentService commentService)
        {
            _gameService = gameService;
            _commentService = commentService;
        }

        /// <summary>
        /// Gets all comments for a game specified by the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Comments")]
        public ActionResult GetCommentsForGame(string key)
        {
            GameModel gameModel = _gameService.GetGameModelByKey(key);
            ICollection<CommentModel> commentModels = gameModel.Comments;
            return View(commentModels);
        }

        /// <summary>
        /// Adds the comment for game specified by the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="commentModel">The comment model.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("NewComment")]
        public ActionResult AddCommentForGame(string key, CommentModel commentModel)
        {
            _commentService.Add(commentModel, key);
            MessageSuccess("The comment has been added successfully!");
            return RedirectToAction("Comments", new {key});
        }
    }
}