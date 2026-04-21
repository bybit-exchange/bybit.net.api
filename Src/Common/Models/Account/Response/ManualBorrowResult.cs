using Newtonsoft.Json;

namespace bybit.net.api.Models.Account.Response
{
    public class ManualBorrowResult
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }
    }
}
