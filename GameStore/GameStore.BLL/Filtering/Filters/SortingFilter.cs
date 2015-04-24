using GameStore.BLL.Enums;

namespace GameStore.BLL.Filtering.Filters
{
    public class SortingFilter : Filter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            switch (container.Model.SortCondition)
            {
                case (SortCondition.MostCommented):
                {
                    container.SortCondition = x => x.Comments.Count*-1;
                    break;
                }

                case (SortCondition.Newest):
                {
                    container.SortCondition = x => x.AddedDate.Ticks*-1;
                    break;
                }

                case (SortCondition.PriceAscending):
                {
                    container.SortCondition = x => x.Price;
                    break;
                }

                case (SortCondition.PriceDescending):
                {
                    container.SortCondition = x => x.Price*-1;
                    break;
                }
            }

            base.Execute(container);
        }
    }
}