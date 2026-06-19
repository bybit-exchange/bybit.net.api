using Newtonsoft.Json;

namespace bybit.net.api.Models.Account.Response
{
    public class GetOptionAssetInfoResult
    {
        [JsonProperty("result")]
        public List<OptionAssetInfoEntry>? Result { get; set; }
    }

    public class OptionAssetInfoEntry
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("totalDelta")]
        public string? TotalDelta { get; set; }

        [JsonProperty("totalRPL")]
        public string? TotalRpl { get; set; }

        [JsonProperty("totalUPL")]
        public string? TotalUpl { get; set; }

        [JsonProperty("assetIM")]
        public string? AssetIm { get; set; }

        [JsonProperty("assetMM")]
        public string? AssetMm { get; set; }

        [JsonProperty("sendTime")]
        public long? SendTime { get; set; }
    }
}
