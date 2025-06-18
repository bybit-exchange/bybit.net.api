using Newtonsoft.Json;

namespace Bybit.Api.Models.Account.Response;

public class BatchSetCollateralCoinResult
{
    [JsonProperty("list")]
    public List<CollateralCoinEntry>? collateralCoinEntries { get; set; }
}