using System;
using Newtonsoft.Json;

namespace Hack2017.Models {

    public class Event {

        public DateTime Date { get; set; }

        [JsonProperty("Event")]
        public string Name { get; set; }

    }

}