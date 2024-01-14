using Newtonsoft.Json;

namespace bybit.net.api.Models.Market.Response;

public class InstrumentsResult : CursoredResult
{
    [JsonProperty("category")]
    public ProductType Category { get; set; }

    [JsonProperty("list")]
    public List<InstrumentInfo>? List { get; set; }
}