using System;
using System.Collections.Generic;
using System.Linq;
using Hack2017.Interfaces;
using Hack2017.Math;
using Hack2017.Models;

namespace Hack2017.Services
{
    public class PredictionService : IPredictionEngine
    {
        public List<PlotData> Predict(IEnumerable<SaleAggregate> skuAggregates) => Predict(skuAggregates, 0, null);

        public List<PlotData> Predict(IEnumerable<SaleAggregate> skuAggregates, int numberOfEvents, decimal? averageTemperature)
        {

            var weatherCorrelation = 0m;
            var weatherCorrelationFunc = GetWeatherCorelationFunction(skuAggregates);

            if (averageTemperature.HasValue)
            {
                weatherCorrelation = weatherCorrelationFunc.Invoke(averageTemperature.Value);
                Console.WriteLine($"Weather Correlation {weatherCorrelation}");
            }

            var salesCorrelationFunc = GetSalesPrediction(skuAggregates);
          
            var start = DateTime.Parse("12/1/2017");
            var forecast = new List<PlotData>();

            for (var i = 0; i < 10; i++)
            {
                var date = start.AddDays(i);

                forecast.Add(new PlotData
                {
                    X = date.DayOfYear,
                    XLabel = date.ToString("yyyy-MM-dd"),
                    Y = salesCorrelationFunc(date.DayOfYear)
                });

            }

            return forecast;


        }

        public Func<decimal, decimal> GetWeatherCorelationFunction(IEnumerable<SaleAggregate> skuAggregates)
        {
            var result = skuAggregates.LinearRegression((arg) => ((decimal)arg.FeelsLikeTemp), (arg) => arg.Quantity);
            return result;
        }


        private Func<decimal, decimal> GetSalesPrediction(IEnumerable<SaleAggregate> skuAggregates)
        {
            Func<SaleAggregate, decimal> salesForDay = x => x.Quantity;
            Func<SaleAggregate, decimal> dateAsDeciaml = x => x.Date.DayOfYear;

            return Math.Statistics.LinearRegression(skuAggregates, dateAsDeciaml, salesForDay);
        }

        private IEnumerable<SaleAggregate> AddBlankDays(IEnumerable<SaleAggregate> data)
        {
            var datalist = data.ToList();
            var minDate = datalist.Min(x => x.Date);
            var maxDate = datalist.Min(x => x.Date);
            for (var i = 1; i < 365; i++)
            {
                if (minDate.AddDays(i) >= maxDate) break;
                if (!datalist.Any(x => x.Date == minDate.AddDays(i))) {
                    datalist.Add(new SaleAggregate()
                    {
                        Quantity = 0,
                        Amount = 0,
                        Date = minDate.AddDays(i)
                    });
                }
            }

            return datalist;
        }


    }
}
