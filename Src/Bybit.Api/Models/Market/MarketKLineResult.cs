using Newtonsoft.Json;

namespace bybit.net.api.Models.Market
{
    public class MarketKLineResult
    {
        [JsonProperty("category")]
        public String? Category { get; set; }

        [JsonProperty("symbol")]
        public String? Symbol { get; set; }

        [JsonProperty("list")]
        public List<List<string>>? MarketKlineEntries { get; set; }
    }
}
