using System;
using System.Collections.Generic;
using Calctic.Model.UnitConverter;
using Newtonsoft.Json;

namespace Calctic.Model.CurrencyConverter
{
    public class ExchangeRate
    {
        [JsonProperty("success")]
        public string Result { get; set; }

        [JsonProperty("time_last_update_unix")]
        public long TimestampUnix { get; set; }

        [JsonProperty("time_last_update_utc")]
        public string Timestamp { get; set; }

        [JsonProperty("base_code")]
        public string Base { get; set; }

        [JsonProperty("conversion_rates")]
        public Dictionary<string, double> Rates { get; set; }

        public static ExchangeRate FromJson(string json) =>
            JsonConvert.DeserializeObject<ExchangeRate>(json, JsonDataConverter.Settings);
    }
}