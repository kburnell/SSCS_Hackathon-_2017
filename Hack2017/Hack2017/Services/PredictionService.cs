using System;
using System.Collections.Generic;
using Hack2017.Interfaces;
using Hack2017.Models;

namespace Hack2017.Services
{
    public class PredictionService : IPredictionEngine
    {
        public List<PlotData> Predict(IEnumerable<SaleAggregate> skuAggregates) => Predict(skuAggregates, 0);

        public List<PlotData> Predict(IEnumerable<SaleAggregate> skuAggregates, int numberOfEvents)
        {
            var start = DateTime.Parse("12/1/2017");
            var forecast = new List<PlotData>
            {
                new PlotData{XLabel = start.ToString("yyyy-MM-dd"), Y = 20},
                new PlotData{XLabel = start.AddDays(1).ToString("yyyy-MM-dd"), Y = 20},
                new PlotData{XLabel = start.AddDays(2).ToString("yyyy-MM-dd"), Y = 21},
                new PlotData{XLabel = start.AddDays(3).ToString("yyyy-MM-dd"), Y = 20},
                new PlotData{XLabel = start.AddDays(4).ToString("yyyy-MM-dd"), Y = 23},
                new PlotData{XLabel = start.AddDays(5).ToString("yyyy-MM-dd"), Y = 15},
                new PlotData{XLabel = start.AddDays(6).ToString("yyyy-MM-dd"), Y = 21},
                new PlotData{XLabel = start.AddDays(7).ToString("yyyy-MM-dd"), Y = 22},
                new PlotData{XLabel = start.AddDays(8).ToString("yyyy-MM-dd"), Y = 20},
                new PlotData{XLabel = start.AddDays(9).ToString("yyyy-MM-dd"), Y = 19}
            };

            return forecast; 


        }
    }
}
