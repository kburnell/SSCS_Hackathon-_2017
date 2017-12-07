using System;
using Newtonsoft.Json;

namespace Hack2017.Models {

    public class Temperature {

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("mintempF")]
        public int MinTemp { get; set; }

        [JsonProperty("maxtempF")]
        public int MaxTemp { get; set; }

        [JsonProperty("FeelsLikeF")]
        public int FeelsLikeTemp { get; set; }

    }

}