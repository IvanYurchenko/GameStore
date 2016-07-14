using GameStore.Core.CustomAttributes;
using GameStore.Resources;

namespace GameStore.Core.Enums
{
    public enum OrderStatus
    {
        [LocalizedDescription("OrderStatusNew", typeof(GlobalRes))]
        New = 1,

        [LocalizedDescription("OrderStatusPayed", typeof(GlobalRes))]
        Payed = 2,

        [LocalizedDescription("OrderStatusShipped", typeof(GlobalRes))]
        Shipped = 3,
    }
}
