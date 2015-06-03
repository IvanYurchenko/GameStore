using System.ComponentModel;
using GameStore.Core.CustomAttributes;
using GameStore.Resources;

namespace GameStore.BLL.Enums
{
    public enum PageCapacity
    {
        [LocalizedDescription("PageCapacityFive", typeof(GlobalRes))]
        Five = 5,

        [LocalizedDescription("PageCapacityTen", typeof(GlobalRes))]
        Ten = 10,

        [LocalizedDescription("PageCapacityTwenty", typeof(GlobalRes))]
        Twenty = 20,

        [LocalizedDescription("PageCapacityFifty", typeof(GlobalRes))]
        Fifty = 50,

        [LocalizedDescription("PageCapacityOneHundred", typeof(GlobalRes))]
        Hundred = 100,

        [LocalizedDescription("PageCapacityAll", typeof(GlobalRes))]
        All = int.MaxValue,
    }
}