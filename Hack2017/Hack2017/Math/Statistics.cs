using System;
using System.Collections.Generic;
using System.Linq;

namespace Hack2017.Math
{
    public static class Statistics
    {
       
        public static decimal Sigma(decimal maxI)
        {
            decimal sum = 0;
            for (var i = 1; i <= maxI; i++)
                sum += i;
            return sum;
        }

        public static decimal Sigma<T>(this IEnumerable<T> collection, Func<T, decimal> selector)
        {
            return collection.Sum(selector);
        }

        public static decimal Sigma(int[] values)
        {
            decimal sum = 0;
            for (var i = 0; i < values.Length; i++)
                sum += values[i];
            return sum;
        }
    }

}
