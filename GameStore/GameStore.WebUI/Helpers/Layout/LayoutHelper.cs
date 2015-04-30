using System.Web.Mvc;
using GameStore.BLL.Interfaces;

namespace GameStore.WebUI.Helpers.Layout
{
    public class LayoutHelper
    {
        private readonly IGameService _gameService;

        public LayoutHelper()
        {
            _gameService = DependencyResolver.Current.GetService<IGameService>();
        }

        /// <summary>
        /// Gets the games count.
        /// </summary>
        /// <returns></returns>
        public int GetGamesCount()
        {
            return _gameService.GetGamesCount();
        }
    }
}