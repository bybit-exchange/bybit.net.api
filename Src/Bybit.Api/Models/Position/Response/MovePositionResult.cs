using Newtonsoft.Json;

namespace Bybit.Api.Models.Position.Response;

public class MovePositionResult
{
    [JsonProperty("blockTradeId")]
    public string? blockTradeId { get; set; }

    [JsonProperty("status")]
    public string? status { get; set; }

    [JsonProperty("rejectParty")]
    public string? rejectParty { get; set; }
}