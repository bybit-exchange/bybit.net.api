using Newtonsoft.Json;

namespace bybit.net.api.Models.Asset
{
    public class GetPortfolioMarginInfoResult
    {
        [JsonProperty("wallet")]
        public PortfolioMarginWallet? Wallet { get; set; }

        [JsonProperty("assetPnlRange")]
        public List<PortfolioMarginAssetPnlRange>? AssetPnlRange { get; set; }
    }

    public class PortfolioMarginWallet
    {
        [JsonProperty("equity")]
        public string? Equity { get; set; }

        [JsonProperty("cashBalance")]
        public string? CashBalance { get; set; }

        [JsonProperty("marginBalance")]
        public string? MarginBalance { get; set; }

        [JsonProperty("availableBalance")]
        public string? AvailableBalance { get; set; }

        [JsonProperty("accountIM")]
        public string? AccountIm { get; set; }

        [JsonProperty("accountMM")]
        public string? AccountMm { get; set; }

        [JsonProperty("accountMMRate")]
        public string? AccountMmRate { get; set; }

        [JsonProperty("accountIMRate")]
        public string? AccountImRate { get; set; }
    }

    public class PortfolioMarginAssetPnlRange
    {
        [JsonProperty("baseCoin")]
        public string? BaseCoin { get; set; }

        [JsonProperty("totalPnlRanges")]
        public Dictionary<string, PortfolioMarginPnlRangeGroup>? TotalPnlRanges { get; set; }

        [JsonProperty("perpPositionPnlRanges")]
        public List<PortfolioMarginPositionPnlRange>? PerpPositionPnlRanges { get; set; }

        [JsonProperty("optionExpiryDatePnlRanges")]
        public List<PortfolioMarginOptionExpiryPnlRange>? OptionExpiryDatePnlRanges { get; set; }

        [JsonProperty("contingency")]
        public PortfolioMarginContingency? Contingency { get; set; }

        [JsonProperty("asset")]
        public PortfolioMarginAssetInfo? Asset { get; set; }

        [JsonProperty("maxLossPriceMove")]
        public string? MaxLossPriceMove { get; set; }

        [JsonProperty("maxLossIvShock")]
        public string? MaxLossIvShock { get; set; }

        [JsonProperty("totalClosePzFee")]
        public string? TotalClosePzFee { get; set; }

        [JsonProperty("spotHedgeInfo")]
        public PortfolioMarginSpotHedgeInfo? SpotHedgeInfo { get; set; }

        [JsonProperty("maxLossIvShockList")]
        public List<string>? MaxLossIvShockList { get; set; }
    }

    public class PortfolioMarginPnlRangeGroup
    {
        [JsonProperty("pnlRanges")]
        public List<PortfolioMarginPnlRange>? PnlRanges { get; set; }
    }

    public class PortfolioMarginPnlRange
    {
        [JsonProperty("priceScale")]
        public string? PriceScale { get; set; }

        [JsonProperty("pnls")]
        public List<string>? Pnls { get; set; }
    }

    public class PortfolioMarginPositionPnlRange
    {
        [JsonProperty("symbolName")]
        public string? SymbolName { get; set; }

        [JsonProperty("position")]
        public string? Position { get; set; }

        [JsonProperty("pnlRanges")]
        public List<PortfolioMarginPnlRange>? PnlRanges { get; set; }

        [JsonProperty("sessionAvgPrice")]
        public string? SessionAvgPrice { get; set; }

        [JsonProperty("markPrice")]
        public string? MarkPrice { get; set; }

        [JsonProperty("orderSize")]
        public string? OrderSize { get; set; }

        [JsonProperty("contractType")]
        public int? ContractType { get; set; }

        [JsonProperty("settleCoin")]
        public string? SettleCoin { get; set; }

        [JsonProperty("symbolAlias")]
        public string? SymbolAlias { get; set; }
    }

    public class PortfolioMarginOptionExpiryPnlRange
    {
        [JsonProperty("expiryDateRepresentation")]
        public string? ExpiryDateRepresentation { get; set; }

        [JsonProperty("pnlRanges")]
        public List<PortfolioMarginPnlRange>? PnlRanges { get; set; }

        [JsonProperty("optionPositionPnlRanges")]
        public List<PortfolioMarginPositionPnlRange>? OptionPositionPnlRanges { get; set; }
    }

    public class PortfolioMarginContingency
    {
        [JsonProperty("optionContingency")]
        public string? OptionContingency { get; set; }

        [JsonProperty("futureDeltaContingency")]
        public string? FutureDeltaContingency { get; set; }

        [JsonProperty("optionVegaContingency")]
        public string? OptionVegaContingency { get; set; }

        [JsonProperty("contingencyComponents")]
        public string? ContingencyComponents { get; set; }

        [JsonProperty("usdtUsdcContingency")]
        public string? UsdtUsdcContingency { get; set; }

        [JsonProperty("futureContingency")]
        public string? FutureContingency { get; set; }
    }

    public class PortfolioMarginAssetInfo
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("assetIM")]
        public string? AssetIm { get; set; }

        [JsonProperty("assetMM")]
        public string? AssetMm { get; set; }
    }

    public class PortfolioMarginSpotHedgeInfo
    {
        [JsonProperty("hedgeSpotSize")]
        public string? HedgeSpotSize { get; set; }

        [JsonProperty("walletBalance")]
        public string? WalletBalance { get; set; }

        [JsonProperty("usdIndexPrice")]
        public string? UsdIndexPrice { get; set; }

        [JsonProperty("pnlRanges")]
        public List<PortfolioMarginPnlRange>? PnlRanges { get; set; }
    }
}
