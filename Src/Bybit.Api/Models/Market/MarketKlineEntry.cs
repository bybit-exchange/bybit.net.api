using Newtonsoft.Json;

namespace bybit.net.api.Models.Market
{
    public class MarketKlineEntry
    {
        [JsonProperty(Order = 1)]
        public string? StartTime { get; set; }

        [JsonProperty(Order = 2)]
        public string? OpenPrice { get; set; }

        [JsonProperty(Order = 3)]
        public string? HighPrice { get; set; }

        [JsonProperty(Order = 4)]
        public string? LowPrice { get; set; }

        [JsonProperty(Order = 5)]
        public string? ClosePrice { get; set; }

        [JsonProperty(Order = 6)]
        public string? Volume { get; set; }

        [JsonProperty(Order = 7)]
        public string? Turnover { get; set; }
    }
}