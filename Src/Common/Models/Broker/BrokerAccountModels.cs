using Newtonsoft.Json;

namespace bybit.net.api.Models.Broker
{
    public class GetBrokerAccountInfoResult
    {
        [JsonProperty("subAcctQty")]
        public string? SubAcctQty { get; set; }

        [JsonProperty("maxSubAcctQty")]
        public string? MaxSubAcctQty { get; set; }

        [JsonProperty("baseFeeRebateRate")]
        public BrokerRebateRate? BaseFeeRebateRate { get; set; }

        [JsonProperty("markupFeeRebateRate")]
        public BrokerRebateRate? MarkupFeeRebateRate { get; set; }

        [JsonProperty("ts")]
        public string? Ts { get; set; }
    }

    public class BrokerRebateRate
    {
        [JsonProperty("spot")]
        public string? Spot { get; set; }

        [JsonProperty("derivatives")]
        public string? Derivatives { get; set; }

        [JsonProperty("convert")]
        public string? Convert { get; set; }
    }
}
