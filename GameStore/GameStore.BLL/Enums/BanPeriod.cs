using GameStore.Core.CustomAttributes;
using GameStore.Resources;

namespace GameStore.BLL.Enums
{
    public enum BanPeriod
    {
        [LocalizedDescription("OneHour", typeof(GlobalRes))]
        Hour,

        [LocalizedDescription("OneDay", typeof(GlobalRes))]
        Day,

        [LocalizedDescription("OneWeek", typeof(GlobalRes))]
        Week,

        [LocalizedDescription("OneMonth", typeof(GlobalRes))]
        Month,

        [LocalizedDescription("OneYear", typeof(GlobalRes))]
        Year,

        [LocalizedDescription("BanPermanent", typeof(GlobalRes))]
        Permanent,
    }
}