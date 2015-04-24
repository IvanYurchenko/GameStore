using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtering
{
    public static class CombinePredicate<T>
    {
        public static Func<T, bool> CombineWithOr(IEnumerable<Func<T, bool>> list)
        {
            return x => list.Any(y => y(x));
        }

        public static Func<T, bool> CombineWithAnd(IEnumerable<Func<T, bool>> list)
        {
            return x => list.All(y => y(x));
        }
    }
}