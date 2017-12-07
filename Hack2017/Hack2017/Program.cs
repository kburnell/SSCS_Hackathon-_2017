using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Hack2017.Models;
using Hack2017.Services;

namespace Hack2017 {

    internal class Program {

        private const string RootPath = "/Users/keithburnell/dev/sscs/data";
        private static readonly string AggregatePath = $"{RootPath}/aggregate.json";

        private static readonly JSONService _jsonService = new JSONService();
        private static readonly ForecastService _forecastService = new ForecastService();

        private static void Main(string[] args) {
            
            //Aggregate();

            var aggregates = _jsonService.Read<SaleAggregate>(AggregatePath).ToList();

            var keepGoing = true;

            while (keepGoing) {
                
                Console.Write("Product SKU: ");
                var productSku = Console.ReadLine();

                Console.Write("Event in Next 10 Days (Y/N): ");
                var isAnEvent = Console.ReadKey().Key == ConsoleKey.Y;

                Console.WriteLine();

                var skuAggregates = aggregates.Where(a => a.POSCode == productSku);
                if (skuAggregates == null || !skuAggregates.Any())
                {
                    Console.WriteLine($"No sales history for sku {productSku}");
                }
                else
                {
                    Forecast(productSku, skuAggregates, isAnEvent);
                }
                Console.WriteLine();
                Console.WriteLine("Hit 'Enter' to process another sku or any other key to exit");
                keepGoing = Console.ReadKey().Key == ConsoleKey.Enter;
            }
        }

        private static void Aggregate() {

            var sales = _jsonService.Read<Sale>($"{RootPath}/sales.json");
            Console.WriteLine($"Processing...{sales.Count()} Sales");
            var temps = _jsonService.Read<Temperature>($"{RootPath}/weather.json");
            Console.WriteLine($"Processing...{temps.Count()} Temperatures");
            var events = _jsonService.Read<Event>($"{RootPath}/events2.json");
            Console.WriteLine($"Processing...{events.Count()} Events");

            var aggregates = new List<SaleAggregate>();
            foreach (var sale in sales) {
                var date = sale.Date;
                var aggregate = new SaleAggregate {
                    Date = date, 
                    POSCode = sale.POSCode, 
                    Description = sale.Description, 
                    Quantity = sale.Quantity, Amount = sale.Amount
                };
                var e = events.FirstOrDefault(x => x.Date == date);
                if (e != null)
                    aggregate.Event = e.Name;
                var t = temps.FirstOrDefault(x => x.Date == date);
                if (t != null) {
                    aggregate.MinTemp = t.MinTemp;
                    aggregate.MaxTemp = t.MaxTemp;
                    aggregate.FeelsLikeTemp = t.FeelsLikeTemp;
                }
                aggregates.Add(aggregate);
            }

            _jsonService.Write(aggregates.OrderByDescending(a => a.Date), AggregatePath);

            Console.WriteLine($"Aggregating...{aggregates.Count()} Records");
        }

        private static void Forecast(string productSku, IEnumerable<SaleAggregate> skuAggregates, bool isAnEvent) 
        {
            Console.WriteLine("Thinking...");
            Thread.Sleep(1000);
            Console.WriteLine("Asking Siri for the answer...");
            Thread.Sleep(1000);
            Console.WriteLine("Asking again because...well Siri");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine();

            var forecast = _forecastService.Forecast(skuAggregates, isAnEvent);

            Console.WriteLine("*******************************************************************");
            var msg = $"10 Day Projected Sale Quantities for {productSku}";
            if (isAnEvent) msg += " With Event During Period";
            Console.WriteLine(msg);
            Console.WriteLine("*******************************************************************");
            foreach (var f in forecast)
            {
                Console.WriteLine($"{f.Date.Date.ToString("yyyy-MM-dd")} - Quantity: {f.Quantity}");
            }
        }

    }

}