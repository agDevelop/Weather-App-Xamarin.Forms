using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ServerReqApp.API
{
    public class OpenWeather
    {
        public coord coord { get; set; }

        public weather[] weather { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        public main main { get; set; }

        public double visibility { get; set; }

        public wind wind { get; set; }

        public clouds clouds { get; set; }

        public int dt { get; set; }

        public sys sys { get; set; }

        public int timezone { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public int cod { get; set; }
    }
}
