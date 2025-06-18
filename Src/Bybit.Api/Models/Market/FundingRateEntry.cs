using Newtonsoft.Json;

namespace Bybit.Api.Models.Market;

public class FundingRateEntry
{
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    [JsonProperty("fundingRate")]
    public string? FundingRate { get; set; }

    [JsonProperty("fundingRateTimestamp")]
    public long? FundingRateTimestamp { get; set; }
}