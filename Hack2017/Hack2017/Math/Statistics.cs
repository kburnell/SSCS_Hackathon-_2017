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

        public static decimal Sigma(this IEnumerable<decimal> values)
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

        public static Func<decimal, decimal> LinearRegression<T>(this IEnumerable<T> series, Func<T, decimal> xSelector, Func<T, decimal> ySelector)
        {
            var n = series.Count();

            //get our sigmas
            var sigmaX = series.Sigma(xSelector);
            var sigmaY = series.Sigma(ySelector);
            var sigmaXY = series.Sigma(item => xSelector(item) * ySelector(item));
            var sigmaX2 = series.Sigma(item => (decimal)System.Math.Pow((double)xSelector(item), 2));
            var sigmaY2 = series.Sigma(item => (decimal)System.Math.Pow((double)ySelector(item), 2));

            //get opur A & B values
            var a = LinearRegressionPointA(n, sigmaX, sigmaY, sigmaXY, sigmaX2, sigmaY2);
            var b = LinearRegressionPointB(n, sigmaX, sigmaY, sigmaXY, sigmaX2, sigmaY2);

            //calculate our Y values off each item's X
            var plotItems = new List<PlotData>();

            return x => Slope(x, a, b);
        }

        public static decimal LinearRegressionPointA(int n, decimal sigmaX, decimal sigmaY, decimal sigmaXY, decimal sigmaXsqr, decimal sigmaYsqr)
        {
            var top = sigmaY * sigmaXsqr - sigmaX * sigmaXY;
            var bottom = n * sigmaXsqr - sigmaXsqr;

            return top / bottom;
        }

        public static decimal LinearRegressionPointB(int n, decimal sigmaX, decimal sigmaY, decimal sigmaXY, decimal sigmaXsqr, decimal sigmaYsqr)
        {
            var top = n * sigmaXY - sigmaX * sigmaY;
            var bottom = n * sigmaXsqr - sigmaXsqr;

            return top / bottom;
        }
    }

}
