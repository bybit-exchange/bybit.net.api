using Newtonsoft.Json;

namespace bybit.net.api.Models.Market.Response;

public class LotSizeRules
{
    [JsonProperty("maxOrderQty")]
    public decimal MaxOrderQty { get; set; }

    [JsonProperty("minOrderQty")]
    public decimal MinOrderQty { get; set; }

    [JsonProperty("qtyStep")]
    public decimal QtyStep { get; set; }

    [JsonProperty("postOnlyMaxOrderQty")]
    public decimal PostOnlyMaxOrderQty { get; set; }
}