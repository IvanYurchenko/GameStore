using System;
using System.Linq;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filtering.Filters
{
    public class PublisherFilter : BaseFilter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            if (container.Model.Publishers != null && container.Model.Publishers.Count() != 0)
            {
                var condition =
                    new Func<Game, bool>(game => container.Model.Publishers.Contains((int) game.PublisherId));

                container.Conditions.Add(condition);
            }

            base.Execute(container);
        }
    }
}