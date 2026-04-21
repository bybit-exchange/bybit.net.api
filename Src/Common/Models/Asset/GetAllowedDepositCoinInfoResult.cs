using Newtonsoft.Json;

namespace bybit.net.api.Models.Asset
{
    public class GetAllowedDepositCoinInfoResult
    {
        [JsonProperty("rows")]
        public List<AllowedDepositCoinInfo>? Rows { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class AllowedDepositCoinInfo
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("chain")]
        public string? Chain { get; set; }

        [JsonProperty("chainType")]
        public string? ChainType { get; set; }

        [JsonProperty("confirmation")]
        public string? Confirmation { get; set; }

        [JsonProperty("safeConfirmNumber")]
        public string? SafeConfirmNumber { get; set; }
    }
}
