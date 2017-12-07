using System;

namespace Hack2017.Models {

    public class SaleAggregate {

        public DateTime Date { get; set; }

        public string POSCode { get; set; }

        public string Description { get; set; }

        public decimal Quantity { get; set; }

        public decimal Amount { get; set; }

        public string Event { get; set; }

        public int? MinTemp { get; set; }

        public int? MaxTemp { get; set; }

        public int? FeelsLikeTemp { get; set; }

    }

}