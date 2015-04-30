using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtering
{
    public static class CombinePredicate<T>
    {
        /// <summary>
        /// Combines predicates with OR.
        /// </summary>
        /// <param name="list">The predicates list.</param>
        /// <returns></returns>
        public static Func<T, bool> CombineWithOr(IEnumerable<Func<T, bool>> list)
        {
            return x => list.Any(y => y(x));
        }

        /// <summary>
        /// Combines predicates with AND.
        /// </summary>
        /// <param name="list">The predicates list.</param>
        /// <returns></returns>
        public static Func<T, bool> CombineWithAnd(IEnumerable<Func<T, bool>> list)
        {
            return x => list.All(y => y(x));
        }
    }
}