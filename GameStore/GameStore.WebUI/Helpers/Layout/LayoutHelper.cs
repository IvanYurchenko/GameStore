using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.Interfaces;
using Ninject;

namespace GameStore.WebUI.Helpers.Layout
{
    public class LayoutHelper
    {
        private readonly IGameService _gameService;

        public LayoutHelper()
        {
            _gameService = DependencyResolver.Current.GetService<IGameService>();
        }

        public int GetGamesCount()
        {

            return _gameService.GetGamesCount();
        }

    }
}