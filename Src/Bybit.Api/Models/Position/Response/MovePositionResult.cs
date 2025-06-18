using Newtonsoft.Json;

namespace bybit.net.api.Models.Position.Response
{
    public class MovePositionResult
    {
        [JsonProperty("blockTradeId")]
        public string? blockTradeId { get; set; }

        [JsonProperty("status")]
        public string? status { get; set; }

        [JsonProperty("rejectParty")]
        public string? rejectParty { get; set; }
    }
}
