using System;
using System.Collections.Generic;
using GameStore.BLL.Enums;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filtering.Filters
{
    public class SortingFilter : BaseFilter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            var sortConditions = new Dictionary<SortCondition, Func<Game, object>>
            {
                {SortCondition.MostCommented, x => x.Comments.Count*-1},
                {SortCondition.Newest,x => x.AddedDate.Ticks*-1},
                {SortCondition.PriceAscending,x => x.Price},
                {SortCondition.PriceDescending,x => x.Price*-1},
            };

            if (sortConditions.ContainsKey(container.Model.SortCondition))
            {
                container.SortCondition = sortConditions[container.Model.SortCondition];
            }

            base.Execute(container);
        }
    }
}