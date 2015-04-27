using System.ComponentModel;

namespace GameStore.BLL.Enums
{
    public enum SortCondition
    {
        [Description("Default")] Default,
        [Description("Most commented")] MostCommented,
        [Description("Price ascending")] PriceAscending,
        [Description("Price descending")] PriceDescending,
        [Description("Newest")] Newest
    }
}