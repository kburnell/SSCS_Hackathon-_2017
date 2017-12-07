using Hack2017.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hack2017.Interfaces
{
    public interface IPredictionEngine
    {
        List<PlotData> PredictSku(string sku);
        List<PlotData> PredictSku(string sku, int events);
    }
}
