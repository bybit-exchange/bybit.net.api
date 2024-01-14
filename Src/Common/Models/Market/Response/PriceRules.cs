using Newtonsoft.Json;

namespace bybit.net.api.Models.Market.Response;

public class PriceRules
{
    [JsonProperty("minPrice")]
    public decimal MinPrice { get; set; }

    [JsonProperty("maxPrice")]
    public decimal MaxPrice { get; set; }

    [JsonProperty("tickSize")]
    public decimal TickSize { get; set; }
}