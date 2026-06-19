using Newtonsoft.Json;

namespace bybit.net.api.Models.Asset
{
    public class GetUsdcSettlementResult
    {
        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("list")]
        public List<UsdcSettlementEntry>? List { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class UsdcSettlementEntry
    {
        [JsonProperty("symbol")]
        public string? Symbol { get; set; }

        [JsonProperty("side")]
        public string? Side { get; set; }

        [JsonProperty("size")]
        public string? Size { get; set; }

        [JsonProperty("sessionAvgPrice")]
        public string? SessionAvgPrice { get; set; }

        [JsonProperty("markPrice")]
        public string? MarkPrice { get; set; }

        [JsonProperty("realisedPnl")]
        public string? RealisedPnl { get; set; }

        [JsonProperty("createdTime")]
        public string? CreatedTime { get; set; }
    }
}
