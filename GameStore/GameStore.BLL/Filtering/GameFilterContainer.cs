using System;
using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filtering
{
    public class GameFilterContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameFilterContainer"/> class.
        /// </summary>
        public GameFilterContainer()
        {
            Conditions = new List<Func<Game, bool>>();
        }

        public GamesFilterModel Model { get; set; }

        public List<Func<Game, bool>> Conditions { get; set; }

        public Func<Game, object> SortCondition { get; set; }
    }
}