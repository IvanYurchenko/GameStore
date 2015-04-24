using System;
using System.Linq;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filtering.Filters
{
    public class Filter : Filter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            if (container.Model.Genres != null && container.Model.Genres.Count() != 0)
            {
                var condition =
                    new Func<Game, bool>(
                        game => game.Genres.Any(genre => container.Model.Genres.Any(id => id == genre.GenreId)));

                container.Conditions.Add(condition);
            }

            base.Execute(container);
        }
    }
}