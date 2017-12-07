using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hack2017.Models;
using Newtonsoft.Json;

namespace Hack2017 {

    internal class Program {

        private const string RootPath = "/Users/keithburnell/dev/sscs/data";
        private static string SalesPath = $"{RootPath}/sales.json";
        private static string EventsPath = $"{RootPath}/events2.json";
        private static string TemperaturesPath = $"{RootPath}/weather.json";
        private static string AggregatePath = $"{RootPath}/aggregate.json";

        private static IList<SaleAggregate> _aggregates = new List<SaleAggregate>();
        private static readonly Reader Reader = new Reader();

        private static string _productSku = "";
        private static bool _continue = true;

        private static void Main(string[] args) {
            //Aggregate();
            //Write(_aggregates, AggregatePath);
            _aggregates = Reader.Read<SaleAggregate>(AggregatePath).ToList();

            while (_continue) {
                Console.Write("Enter Product SKU: ");
                _productSku = Console.ReadLine();

                var skuAggregates = _aggregates.Where(a => a.POSCode == _productSku);

                if (skuAggregates == null || !skuAggregates.Any())
                    Console.WriteLine("No match found");
                else
                    Console.WriteLine(_productSku);

                Console.WriteLine("Hit 'Enter' to enter another sku or any other key to exit");
                _continue = Console.ReadKey().Key == ConsoleKey.Enter;
            }
        }

        private static void Aggregate() {
            var sales = Reader.Read<Sale>(SalesPath);
            Console.WriteLine($"{sales.Count()} Sales");
            var temps = Reader.Read<Temperature>(TemperaturesPath);
            var events = Reader.Read<Event>(EventsPath);

            foreach (var sale in sales) {
                var date = sale.Date;
                var aggregate = new SaleAggregate {Date = date, POSCode = sale.POSCode, Description = sale.Description, Quantity = sale.Quantity, Amount = sale.Amount};
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
        }

        private static void Write(object obj, string filePath) {
            var json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(filePath, json);
        }

    }

}