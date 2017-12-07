using System;
using Newtonsoft.Json;

namespace Hack2017.Models {

    public class Sale {

        public DateTime Date { get; set; }

        public string POSCode { get; set; }

        public string Description { get; set; }

        [JsonProperty("SalesQuantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("SalesAmount")]
        public decimal Amount { get; set; }

    }

}