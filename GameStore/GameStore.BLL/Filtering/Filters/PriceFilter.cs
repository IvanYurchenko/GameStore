namespace GameStore.BLL.Filtering.Filters
{
    public class PriceFilter : Filter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            if (container.Model.PriceFrom != null)
            {
                container.Conditions.Add(game => game.Price >= container.Model.PriceFrom);
            }

            if (container.Model.PriceTo != null)
            {
                container.Conditions.Add(game => game.Price <= container.Model.PriceTo);
            }

            base.Execute(container);
        }
    }
}