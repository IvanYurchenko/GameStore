namespace GameStore.BLL.Filtering.Filters
{
    public class NameFilter : Filter<GameFilterContainer>
    {
        public override void Execute(GameFilterContainer container)
        {
            if (!string.IsNullOrEmpty(container.Model.GameNamePart))
            {
                container.Conditions.Add(x => x.Name.ToLower().Contains(container.Model.GameNamePart.ToLower()));
            }

            base.Execute(container);
        }
    }
}