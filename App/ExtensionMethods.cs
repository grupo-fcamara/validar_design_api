using System.Collections.Generic;

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

        public static bool AllEqual<T>(this IEnumerable<T> collection, IEnumerable<T> other)
        {
            if (collection.Count() != other.Count())
                return false;

            for (int i = 0; i < other.Count(); i++)
            {
                if (!collection.ElementAt(i).Equals(other.ElementAt(i)))
                    return false;
            }

            return true;
        }
    }
}