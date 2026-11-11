using Newtonsoft.Json;

namespace bybit.net.api.Models.Account
{
    public sealed class DemoApplyMoneyItem
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("amountStr")]
        public string? AmountStr { get; set; }
    }
}
