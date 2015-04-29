using System;
using System.Linq;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filtering.Filters
{
    public class PlatformTypeFilter : BaseFilter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            if (container.Model.PlatformTypes != null && container.Model.PlatformTypes.Count() != 0)
            {
                var condition =
                    new Func<Game, bool>(game => game.PlatformTypes.Any(
                        platform => container.Model.PlatformTypes.Any(
                            id => id == platform.PlatformTypeId)));

                container.Conditions.Add(condition);
            }

            base.Execute(container);
        }
    }
}