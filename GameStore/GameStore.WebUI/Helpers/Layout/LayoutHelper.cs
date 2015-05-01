using System.Web.Mvc;
using GameStore.BLL.Interfaces;

namespace GameStore.WebUI.Helpers.Layout
{
    public static class LayoutHelper
    {
        private static readonly IGameService _gameService;

        static LayoutHelper()
        {
            _gameService = DependencyResolver.Current.GetService<IGameService>();
        }

        /// <summary>
        /// Gets the count of games.
        /// </summary>
        /// <returns></returns>
        public static int GetGamesCount()
        {
            return _gameService.GetGamesCount();
        }
    }
}