using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.Util
{
    public static class AssertUtil
    {
        public static void AllEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.Equal(expected.Count(), actual.Count());
            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.Equal(expected.ElementAt(i), actual.ElementAt(i));
            }
        }
    }
}