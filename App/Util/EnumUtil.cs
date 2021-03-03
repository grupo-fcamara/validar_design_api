using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Util
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}