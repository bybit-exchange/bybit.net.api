using Newtonsoft.Json;

namespace bybit.net.api.Models.Broker
{
    public class GetBrokerVoucherSpecResult
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("amountUnit")]
        public string? AmountUnit { get; set; }

        [JsonProperty("productLine")]
        public string? ProductLine { get; set; }

        [JsonProperty("subProductLine")]
        public string? SubProductLine { get; set; }

        [JsonProperty("totalAmount")]
        public string? TotalAmount { get; set; }

        [JsonProperty("usedAmount")]
        public string? UsedAmount { get; set; }
    }

    public class IssueBrokerVoucherResult
    {
    }

    public class GetIssuedBrokerVoucherResult
    {
        [JsonProperty("accountId")]
        public string? AccountId { get; set; }

        [JsonProperty("awardId")]
        public string? AwardId { get; set; }

        [JsonProperty("specCode")]
        public string? SpecCode { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("isClaimed")]
        public bool? IsClaimed { get; set; }

        [JsonProperty("startAt")]
        public string? StartAt { get; set; }

        [JsonProperty("endAt")]
        public string? EndAt { get; set; }

        [JsonProperty("effectiveAt")]
        public string? EffectiveAt { get; set; }

        [JsonProperty("ineffectiveAt")]
        public string? IneffectiveAt { get; set; }

        [JsonProperty("usedAmount")]
        public string? UsedAmount { get; set; }
    }
}
