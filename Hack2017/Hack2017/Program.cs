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
        private static readonly PredictionService _predictionService = new PredictionService();

        private static void Main(string[] args) {
            
            Aggregate();

            var aggregates = _jsonService.Read<SaleAggregate>(AggregatePath).ToList();

            var keepGoing = true;

            while (keepGoing) {
                
                Console.Write("Product SKU: ");
                var productSku = Console.ReadLine();

                var numberOfEvents = 0;
                Console.Write("Number of Events to Factor: ");
                var eventCountInput = Console.ReadLine();
                int.TryParse(eventCountInput, out numberOfEvents);


                decimal? averageTempForPeriod = null;
                decimal outAverageTempForPeriod;
                Console.Write("Predicted average temperature for period: ");
                var averageTempInput = Console.ReadLine();
                if (decimal.TryParse(averageTempInput, out outAverageTempForPeriod)){
                    averageTempForPeriod = outAverageTempForPeriod;
                }

                Console.WriteLine();

                var skuAggregates = aggregates.Where(a => a.POSCode == productSku);
                if (skuAggregates == null || !skuAggregates.Any())
                {
                    Console.WriteLine($"No sales history for sku {productSku}");
                }
                else
                {
                    Forecast(productSku, skuAggregates, numberOfEvents, averageTempForPeriod);
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
                    DayOfWeek = (int)date.DayOfWeek,
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

        private static void Forecast(string productSku, IEnumerable<SaleAggregate> skuAggregates, int numberOfEvents, decimal? averageTemp) 
        {
            Console.WriteLine("Thinking...");
            Thread.Sleep(1000);
            Console.WriteLine("Asking Siri for the answer...");
            Thread.Sleep(1000);
            Console.WriteLine("Asking again because...well Siri");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine();



            var prediction = _predictionService.Predict(skuAggregates, numberOfEvents, averageTemp);

            Console.WriteLine("*******************************************************************");
            Console.WriteLine($"Previous 10 Sale Quantities for {productSku}");
            Console.WriteLine("*******************************************************************");
            var lastAggregates = skuAggregates.OrderByDescending(s => s.Date).Take(10);
            foreach (var la in lastAggregates)
            {
                var d = la.Date.ToString("yyyy-MM-dd");
                Console.WriteLine($"{d} - Quantity: {la.Quantity}");
            }

            Console.WriteLine("*******************************************************************");
            Console.WriteLine($"10 Day ({numberOfEvents} Events) Projected Sale Quantities for {productSku}");
            Console.WriteLine("*******************************************************************");
            foreach (var p in prediction)
            {
                
                Console.WriteLine($"{p.XLabel} - Quantity: {System.Math.Round(p.Y,2)}");
            }

            Console.WriteLine($"Total - Quantity: {prediction.Sum(p => p.Y)}");
        }

    }

}