using System.ComponentModel;

namespace GameStore.BLL.Enums
{
    public enum DatePeriod
    {
        [Description("Any date")]
        All,

        [Description("Last week")]
        LastWeek,

        [Description("Last month")]
        LastMonth,

        [Description("Last year")]
        LastYear,

        [Description("Last two years")]
        TwoYears,

        [Description("Last three years")]
        ThreeYears
    }
}