using System;
using GameStore.BLL.Enums;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filtering.Filters
{
    public class DateFilter : Filter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            Func<Game, bool> condition = null;
            switch (container.Model.DatePeriod)
            {
                case (DatePeriod.LastWeek):
                {
                    condition = x => x.PublicationDate > DateTime.UtcNow.Date.AddDays(-7);
                    break;
                }

                case (DatePeriod.LastMonth):
                {
                    condition = x => x.PublicationDate > DateTime.UtcNow.Date.AddMonths(-1);
                    break;
                }

                case (DatePeriod.LastYear):
                {
                    condition = x => x.PublicationDate > DateTime.UtcNow.Date.AddYears(-1);
                    break;
                }

                case (DatePeriod.TwoYears):
                {
                    condition = x => x.PublicationDate > DateTime.UtcNow.Date.AddYears(-2);
                    break;
                }

                case (DatePeriod.ThreeYears):
                {
                    condition = x => x.PublicationDate > DateTime.UtcNow.Date.AddYears(-3);
                    break;
                }
            }

            if (condition != null)
            {
                container.Conditions.Add(condition);
            }

            base.Execute(container);
        }
    }
}