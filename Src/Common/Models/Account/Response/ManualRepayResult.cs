using Newtonsoft.Json;

namespace bybit.net.api.Models.Account.Response
{
    public class ManualRepayResult
    {
        [JsonProperty("resultStatus")]
        public string? ResultStatus { get; set; }
    }
}
