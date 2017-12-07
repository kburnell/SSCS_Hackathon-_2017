using System;
using System.Collections.Generic;
using Hack2017.Models;

namespace Hack2017.Services
{
    public class ForecastService
    {

        public IEnumerable<SaleAggregate> Forecast(IEnumerable<SaleAggregate> skuAggregates, bool isAnEvent) 
        {
            var start = DateTime.Parse("12/1/2017");
            var forecast = new List<SaleAggregate>{
                new SaleAggregate{Date = start, Quantity = 20},
                new SaleAggregate{Date = start.AddDays(1), Quantity = 20},
                new SaleAggregate{Date = start.AddDays(2), Quantity = 21},
                new SaleAggregate{Date = start.AddDays(3), Quantity = 20},
                new SaleAggregate{Date = start.AddDays(4), Quantity = 23},
                new SaleAggregate{Date = start.AddDays(5), Quantity = 15},
                new SaleAggregate{Date = start.AddDays(6), Quantity = 21},
                new SaleAggregate{Date = start.AddDays(7), Quantity = 22},
                new SaleAggregate{Date = start.AddDays(8), Quantity = 20},
                new SaleAggregate{Date = start.AddDays(9), Quantity = 19}
            };

            return forecast; 

        }
    }
}
