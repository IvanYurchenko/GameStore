using System.ComponentModel;

namespace GameStore.BLL.Enums
{
    public enum SortCondition
    {
        Default,
        [Description("Most commented")] MostCommented,
        [Description("Price asc")] PriceAscending,
        [Description("Price desc")] PriceDescending,
        Newest
    }
}