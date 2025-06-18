using Newtonsoft.Json;

namespace bybit.net.api.Models.Market
{
    public class FundingRateResult
    {
        [JsonProperty("category")]
        public String? Category { get; set; }

        [JsonProperty("list")]
        public List<FundingRateEntry>? FundingRateEntries { get; set; }
    }
}
