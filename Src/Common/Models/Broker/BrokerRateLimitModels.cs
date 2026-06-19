using Newtonsoft.Json;

namespace bybit.net.api.Models.Broker
{
    public class GetBrokerRateLimitAllResult
    {
        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }

        [JsonProperty("list")]
        public List<BrokerRateLimitEntry>? List { get; set; }
    }

    public class BrokerRateLimitEntry
    {
        [JsonProperty("uids")]
        public string? Uids { get; set; }

        [JsonProperty("bizType")]
        public string? BizType { get; set; }

        [JsonProperty("rate")]
        public int? Rate { get; set; }
    }

    public class GetBrokerRateLimitCapResult
    {
        [JsonProperty("list")]
        public List<BrokerRateLimitCapEntry>? List { get; set; }
    }

    public class BrokerRateLimitCapEntry
    {
        [JsonProperty("bizType")]
        public string? BizType { get; set; }

        [JsonProperty("totalRate")]
        public string? TotalRate { get; set; }

        [JsonProperty("ebCap")]
        public string? EbCap { get; set; }

        [JsonProperty("uidCap")]
        public string? UidCap { get; set; }
    }

    public class BrokerRateLimitRequestItem
    {
        [JsonProperty("uids")]
        public string? Uids { get; set; }

        [JsonProperty("bizType")]
        public string? BizType { get; set; }

        [JsonProperty("rate")]
        public int? Rate { get; set; }
    }

    public class SetBrokerRateLimitResult
    {
        [JsonProperty("result")]
        public List<SetBrokerRateLimitResultItem>? Result { get; set; }
    }

    public class SetBrokerRateLimitResultItem
    {
        [JsonProperty("uids")]
        public string? Uids { get; set; }

        [JsonProperty("bizType")]
        public string? BizType { get; set; }

        [JsonProperty("rate")]
        public int? Rate { get; set; }

        [JsonProperty("success")]
        public bool? Success { get; set; }

        [JsonProperty("msg")]
        public string? Msg { get; set; }
    }
}
