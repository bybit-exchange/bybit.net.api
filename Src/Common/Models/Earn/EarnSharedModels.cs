using Newtonsoft.Json;

namespace bybit.net.api.Models.Earn
{
    public class PlaceEarnOrderResult
    {
        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("orderLinkId")]
        public string? OrderLinkId { get; set; }
    }

    public class GetEarnAprHistoryResult
    {
        [JsonProperty("list")]
        public List<EarnAprHistoryItem>? List { get; set; }
    }

    public class EarnAprHistoryItem
    {
        [JsonProperty("timestamp")]
        public string? Timestamp { get; set; }

        [JsonProperty("apr")]
        public string? Apr { get; set; }
    }

    public class GetEarnHourlyYieldResult
    {
        [JsonProperty("list")]
        public List<EarnHourlyYieldItem>? List { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class EarnHourlyYieldItem
    {
        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("effectiveStakingAmount")]
        public string? EffectiveStakingAmount { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("hourlyDate")]
        public string? HourlyDate { get; set; }

        [JsonProperty("createdAt")]
        public string? CreatedAt { get; set; }
    }

    public class GetEarnYieldHistoryResult
    {
        [JsonProperty("yield")]
        public List<EarnYieldHistoryItem>? Yield { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class EarnYieldHistoryItem
    {
        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("yieldType")]
        public string? YieldType { get; set; }

        [JsonProperty("distributionMode")]
        public string? DistributionMode { get; set; }

        [JsonProperty("effectiveStakingAmount")]
        public string? EffectiveStakingAmount { get; set; }

        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("createdAt")]
        public string? CreatedAt { get; set; }
    }

    public class ModifyEarnPositionResult
    {
    }
}
