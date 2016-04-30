using System;
using System.Collections.Generic;
using GameStore.BLL.Enums;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filtering.Filters
{
    public class DateFilter : BaseFilter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            var conditions = new Dictionary<DatePeriod, Func<Game, bool>>
            {
                {DatePeriod.LastWeek, x => x.PublicationDate > DateTime.UtcNow.Date.AddDays(-7)},
                {DatePeriod.LastMonth, x => x.PublicationDate > DateTime.UtcNow.Date.AddMonths(-1)},
                {DatePeriod.LastYear, x => x.PublicationDate > DateTime.UtcNow.Date.AddYears(-1)},
                {DatePeriod.TwoYears, x => x.PublicationDate > DateTime.UtcNow.Date.AddYears(-2)},
                {DatePeriod.ThreeYears, x => x.PublicationDate > DateTime.UtcNow.Date.AddYears(-3)},
            };

            if (conditions.ContainsKey(container.Model.DatePeriod))
            {
                Func<Game, bool> condition = conditions[container.Model.DatePeriod];
                container.Conditions.Add(condition);
            }

            base.Execute(container);
        }
    }
}