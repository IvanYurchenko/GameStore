using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Enums
{
    public enum PageCapacity
    {
        [Description("5")]
        Five = 5,
        [Description("10")]
        Ten = 10,
        [Description("20")]
        Twenty = 20,
        [Description("50")]
        Fifty = 50,
        [Description("100")]
        Hundred = 100,
        [Description("All")]
        All = int.MaxValue,
    }
}
