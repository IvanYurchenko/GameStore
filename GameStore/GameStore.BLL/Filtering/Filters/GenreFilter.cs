using System;
using System.Linq;
using GameStore.Core;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filtering.Filters
{
    public class GenreFilter : BaseFilter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            if (container.Model.Genres != null && container.Model.Genres.Count() != 0)
            {
                Func<Game, bool> condition;
                if (container.Model.Genres.Any(id => id == Constants.OtherGenreId))
                {
                    condition = game => game.Genres.Any(genre => container.Model.Genres.Any(id => id == genre.GenreId))
                        || game.Genres == null || game.Genres.Count == 0;
                }
                else
                {
                    condition = game => game.Genres.Any(genre => container.Model.Genres.Any(id => id == genre.GenreId));
                }

                container.Conditions.Add(condition);
            }

            base.Execute(container);
        }
    }
}