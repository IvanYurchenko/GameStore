using System.ComponentModel;

namespace GameStore.Core.Enums
{
    public enum OrderStatus
    {
        [Description("New")]
        New = 1,

        [Description("Payed")]
        Payed = 2,

        [Description("Shipped")]
        Shipped = 3,
    }
}
