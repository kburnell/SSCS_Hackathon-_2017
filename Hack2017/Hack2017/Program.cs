using System;
using System.Collections.Generic;
using System.Linq;
using Hack2017.Models;
using Hack2017.Services;

namespace Hack2017 {

    internal class Program {

        private const string RootPath = "/Users/keithburnell/dev/sscs/data";
        private static readonly string AggregatePath = $"{RootPath}/aggregate.json";

        private static IList<SaleAggregate> _aggregates = new List<SaleAggregate>();
        private static readonly JSONService _jsonService = new JSONService();

        private static string _productSku = "";

        private static void Main(string[] args) {
            
            Aggregate();

            _aggregates = _jsonService.Read<SaleAggregate>(AggregatePath).ToList();

            var keepGoing = true;

            while (keepGoing) {
                Console.Write("Enter Product SKU: ");
                _productSku = Console.ReadLine();

                var skuAggregates = _aggregates.Where(a => a.POSCode == _productSku);

                if (skuAggregates == null || !skuAggregates.Any())
                    Console.WriteLine("No match found");
                else
                    Console.WriteLine(_productSku);

                Console.WriteLine("Hit 'Enter' to enter another sku or any other key to exit");
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
                _aggregates.Add(aggregate);
            }

            _jsonService.Write(_aggregates, AggregatePath);

            Console.WriteLine($"Aggregating...{_aggregates.Count()} Records");
        }

    }

}