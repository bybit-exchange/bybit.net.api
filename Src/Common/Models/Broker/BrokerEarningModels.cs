using Newtonsoft.Json;

namespace bybit.net.api.Models.Broker
{
    public class GetBrokerEarningResult
    {
        [JsonProperty("totalEarningCat")]
        public BrokerEarningCategorySummary? TotalEarningCat { get; set; }

        [JsonProperty("details")]
        public List<BrokerEarningDetail>? Details { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class BrokerEarningCategorySummary
    {
        [JsonProperty("spot")]
        public List<BrokerCoinEarning>? Spot { get; set; }

        [JsonProperty("derivatives")]
        public List<BrokerCoinEarning>? Derivatives { get; set; }

        [JsonProperty("options")]
        public List<BrokerCoinEarning>? Options { get; set; }

        [JsonProperty("convert")]
        public List<BrokerCoinEarning>? Convert { get; set; }

        [JsonProperty("total")]
        public List<BrokerCoinEarning>? Total { get; set; }
    }

    public class BrokerCoinEarning
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("earning")]
        public string? Earning { get; set; }
    }

    public class BrokerEarningDetail
    {
        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("bizType")]
        public string? BizType { get; set; }

        [JsonProperty("symbol")]
        public string? Symbol { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("earning")]
        public string? Earning { get; set; }

        [JsonProperty("markupEarning")]
        public string? MarkupEarning { get; set; }

        [JsonProperty("baseFeeEarning")]
        public string? BaseFeeEarning { get; set; }

        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("execId")]
        public string? ExecId { get; set; }

        [JsonProperty("execTime")]
        public string? ExecTime { get; set; }
    }
}
