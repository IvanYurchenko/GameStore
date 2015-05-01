using System.ComponentModel;

namespace GameStore.BLL.Enums
{
    public enum BanPeriod
    {
        [Description("1 hour")] Hour,
        [Description("1 day")] Day,
        [Description("1 week")] Week,
        [Description("1 month")] Month,
        [Description("1 year")] Year,
        [Description("Permanent")] Permanent,
    }
}