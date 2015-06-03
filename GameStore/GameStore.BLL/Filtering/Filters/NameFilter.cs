using System;
using System.Linq;
using GameStore.Core;

namespace GameStore.BLL.Filtering.Filters
{
    public class NameFilter : BaseFilter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            if (!string.IsNullOrEmpty(container.Model.GameNamePart))
            {
                container.Conditions
                    .Add(x => x.GameLocalizations.First(loc =>
                        String.Equals(loc.Language.Code, Constants.EnglishLanguageCode, StringComparison.CurrentCultureIgnoreCase))
                        .Name
                        .ToLower()
                        .Contains(container.Model.GameNamePart.ToLower()));
            }

            base.Execute(container);
        }
    }
}