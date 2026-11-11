using Newtonsoft.Json;

namespace bybit.net.api.Models.Tax
{
    public sealed class TaxReportItem
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("number")]
        public string? Number { get; set; }
    }
}
