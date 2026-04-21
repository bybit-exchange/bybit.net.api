using Newtonsoft.Json;

namespace bybit.net.api.Models.Earn
{
    public class GetStakeRedeemOrderHistoryResult
    {
        [JsonProperty("list")]
        public List<EarnOrderHistoryItem>? List { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class EarnOrderHistoryItem
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("orderValue")]
        public string? OrderValue { get; set; }

        [JsonProperty("orderType")]
        public string? OrderType { get; set; }

        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("orderLinkId")]
        public string? OrderLinkId { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("createdAt")]
        public string? CreatedAt { get; set; }

        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("updatedAt")]
        public string? UpdatedAt { get; set; }

        [JsonProperty("swapOrderValue")]
        public string? SwapOrderValue { get; set; }

        [JsonProperty("estimateRedeemTime")]
        public string? EstimateRedeemTime { get; set; }

        [JsonProperty("estimateStakeTime")]
        public string? EstimateStakeTime { get; set; }
    }
}
