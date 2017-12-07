using Hack2017.Models;
using System.Collections.Generic;

namespace Hack2017.Interfaces
{
    public interface IPredictionEngine
    {
        List<PlotData> Predict(IEnumerable<SaleAggregate> skuAggregates);
        List<PlotData> Predict(IEnumerable<SaleAggregate> skuAggregates, int numberOfEvents);
    }
}
