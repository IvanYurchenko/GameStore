using System.ComponentModel;
using GameStore.Core.CustomAttributes;
using GameStore.Resources;

namespace GameStore.BLL.Enums
{
    public enum SortCondition
    {
        [LocalizedDescription("Default", typeof(GlobalRes))]
        Default,

        [LocalizedDescription("MostCommented", typeof(GlobalRes))]
        MostCommented,

        [LocalizedDescription("PriceAscending", typeof(GlobalRes))]
        PriceAscending,

        [LocalizedDescription("PriceDescending", typeof(GlobalRes))]
        PriceDescending,

        [LocalizedDescription("Newest", typeof(GlobalRes))]
        Newest
    }
}