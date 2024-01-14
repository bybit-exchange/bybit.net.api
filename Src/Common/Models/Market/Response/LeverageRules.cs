using Newtonsoft.Json;

namespace bybit.net.api.Models.Market.Response;

public class LeverageRules
{
    [JsonProperty("minLeverage")]
    public decimal MinLeverage { get; set; }

    [JsonProperty("maxLeverage")]
    public decimal MaxLeverage { get; set; }

    [JsonProperty("leverageStep")]
    public decimal LeverageStep { get; set; }
}