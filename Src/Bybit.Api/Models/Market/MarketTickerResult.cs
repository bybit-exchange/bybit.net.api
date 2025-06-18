using Newtonsoft.Json;

namespace Bybit.Api.Models.Market;

public class MarketTickerResult
{
    [JsonProperty("category")]
    public string? Category { get; set; }

    [JsonProperty("list")]
    public List<TickerInfoEntry>? MarketTickerInfoEntries { get; set; }
}