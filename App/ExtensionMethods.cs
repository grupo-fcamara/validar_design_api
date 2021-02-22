using System.Collections.Generic;
using System.Linq;
using App.Entities;

namespace System.Linq
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> Distinct<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> selector)
        {
            return collection.GroupBy(selector).Select(group => group.First());
        }

        public static IEnumerable<T> Distinct<T, TResult>(this IEnumerable<T> collection, params Func<T, TResult>[] selectors)
        {
            foreach (var selector in selectors)
            {
                collection = collection.GroupBy(selector).Select(group => group.First());
            }
            return collection;
        }
    }
}