using GameStore.Core.CustomAttributes;
using GameStore.Resources;

namespace GameStore.BLL.Enums
{
    public enum DatePeriod
    {
        [LocalizedDescription("AnyDate", typeof(GlobalRes))]
        All,

        [LocalizedDescription("LastWeek", typeof(GlobalRes))]
        LastWeek,

        [LocalizedDescription("LastMonth", typeof(GlobalRes))]
        LastMonth,

        [LocalizedDescription("LastYear", typeof(GlobalRes))]
        LastYear,

        [LocalizedDescription("LastTwoYears", typeof(GlobalRes))]
        TwoYears,

        [LocalizedDescription("LastThreeYears", typeof(GlobalRes))]
        ThreeYears
    }
}