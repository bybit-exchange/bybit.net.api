using Newtonsoft.Json;

namespace bybit.net.api.Models;

public class CursoredResult
{
    [JsonProperty("nextPageCursor")]
    public string? NextPageCursor { get; set; }
}