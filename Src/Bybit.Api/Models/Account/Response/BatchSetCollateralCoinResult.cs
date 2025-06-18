using Newtonsoft.Json;

namespace bybit.net.api.Models.Account.Response
{
    public class BatchSetCollateralCoinResult
    {
        [JsonProperty("list")]
        public List<CollateralCoinEntry>? collateralCoinEntries { get; set; }
    }
}
