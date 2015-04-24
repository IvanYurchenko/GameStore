using System;
using System.Collections.Generic;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filtering.Filters
{
    public class PriceFilter : Filter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            var conditions = new List<Func<Game, bool>>();
            if (container.Model.PriceFrom != null)
            {
                var minPrice = container.Model.PriceFrom;

                conditions.Add(game => game.Price >= minPrice);
            }

            if (container.Model.PriceTo != null)
            {
                var maxPrice = container.Model.PriceTo;

                container.Conditions.Add(game => game.Price <= maxPrice);
            }

            if (conditions.Count != 0)
            {
                container.Conditions.Add(CombinePredicate<Game>.CombineWithOr(conditions));
            }

            base.Execute(container);
        }
    }
}