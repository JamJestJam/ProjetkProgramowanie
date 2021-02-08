using System;
using System.Collections.Generic;
using System.Linq;

namespace DBconnectShop.Addons {
    internal static class Extension {
        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector) {
            var result = source.SelectMany(selector);
            return !result.Any() ? result : result.Union(result.SelectManyRecursive(selector));
        }
    }
}
