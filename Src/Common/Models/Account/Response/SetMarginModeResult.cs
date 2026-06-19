using Newtonsoft.Json;

namespace bybit.net.api.Models.Account.Response
{
    public class SetMarginModeResult
    {
        [JsonProperty("reasons")]
        public List<SetMarginModeReason>? Reasons { get; set; }
    }

    public class SetMarginModeReason
    {
        [JsonProperty("reasonCode")]
        public string? ReasonCode { get; set; }

        [JsonProperty("reasonMsg")]
        public string? ReasonMsg { get; set; }
    }
}
