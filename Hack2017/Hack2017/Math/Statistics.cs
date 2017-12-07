using Hack2017.Models;
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

        public static decimal Sigma(IEnumerable<decimal> values)
        {
            return values.Sum();
        }

        /// <summary>
        /// Slope function to plot a graph
        /// </summary>
        /// <param name="x">Horizontal Axis Value</param>
        /// <param name="a">ratio</param>
        /// <param name="b">adjustment</param>
        /// <returns>Vertical Axis value</returns>
        public static decimal Slope(decimal x, decimal a, decimal b)
        {
            var y = a + (b * x);
            return y;
        }

        public static IEnumerable<PlotData> LinearRegression<T>(this IEnumerable<T> series, Func<T, decimal> xSelector, Func<T, decimal> YSelector)
        {
            var n = series.Count();
            var sigmaX = series.Sigma(xSelector);
            throw new NotImplementedException();
        }

        public static decimal LinearRegressionPointA(int n, decimal sigmaX, decimal sigmaY, decimal sigmaXY, decimal sigmaXsqr, decimal sigmaYsqr)
        {
            throw new NotImplementedException();
        }

        public static decimal LinearRegressionPointB(int n, decimal sigmaX, decimal sigmaY, decimal sigmaXY, decimal sigmaXsqr, decimal sigmaYsqr)
        {
            throw new NotImplementedException();
        }
    }

}
