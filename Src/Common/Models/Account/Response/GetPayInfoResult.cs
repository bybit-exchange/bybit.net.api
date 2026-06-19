using Newtonsoft.Json;

namespace bybit.net.api.Models.Account.Response
{
    public class GetPayInfoResult
    {
        [JsonProperty("collateralInfo")]
        public PayCollateralInfo? CollateralInfo { get; set; }

        [JsonProperty("borrowInfo")]
        public PayBorrowInfo? BorrowInfo { get; set; }
    }

    public class PayCollateralInfo
    {
        [JsonProperty("collateralList")]
        public List<PayCollateralEntry>? CollateralList { get; set; }
    }

    public class PayCollateralEntry
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("availableSize")]
        public string? AvailableSize { get; set; }

        [JsonProperty("availableValue")]
        public string? AvailableValue { get; set; }

        [JsonProperty("coinScale")]
        public int? CoinScale { get; set; }

        [JsonProperty("borrowSize")]
        public string? BorrowSize { get; set; }

        [JsonProperty("spotHedgeAmount")]
        public string? SpotHedgeAmount { get; set; }

        [JsonProperty("assetFrozen")]
        public string? AssetFrozen { get; set; }
    }

    public class PayBorrowInfo
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("borrowSize")]
        public string? BorrowSize { get; set; }

        [JsonProperty("borrowValue")]
        public string? BorrowValue { get; set; }

        [JsonProperty("assetFrozen")]
        public string? AssetFrozen { get; set; }

        [JsonProperty("availableBalance")]
        public string? AvailableBalance { get; set; }
    }
}
