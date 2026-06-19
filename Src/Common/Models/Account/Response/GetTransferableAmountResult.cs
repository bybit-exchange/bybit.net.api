using Newtonsoft.Json;

namespace bybit.net.api.Models.Account.Response
{
    public class GetTransferableAmountResult
    {
        [JsonProperty("availableWithdrawal")]
        public string? AvailableWithdrawal { get; set; }

        [JsonProperty("availableWithdrawalMap")]
        public Dictionary<string, string>? AvailableWithdrawalMap { get; set; }
    }
}
