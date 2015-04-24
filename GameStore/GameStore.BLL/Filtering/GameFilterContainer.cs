using System;
using System.Collections.Generic;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filtering
{
    public class GameFilterContainer
    {
        public GamesFilterModel Model { get; set; }

        public List<Func<Game, bool>> Conditions { get; set; }

        public Func<Game, object> SortCondition { get; set; }
    }
}