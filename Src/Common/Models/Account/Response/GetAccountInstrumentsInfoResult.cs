using Newtonsoft.Json;

namespace bybit.net.api.Models.Account.Response
{
    public class GetAccountInstrumentsInfoResult
    {
        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }

        [JsonProperty("list")]
        public List<AccountInstrumentInfo>? List { get; set; }
    }

    public class AccountInstrumentInfo
    {
        [JsonProperty("symbol")]
        public string? Symbol { get; set; }

        [JsonProperty("contractType")]
        public string? ContractType { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("baseCoin")]
        public string? BaseCoin { get; set; }

        [JsonProperty("quoteCoin")]
        public string? QuoteCoin { get; set; }

        [JsonProperty("symbolType")]
        public string? SymbolType { get; set; }

        [JsonProperty("launchTime")]
        public string? LaunchTime { get; set; }

        [JsonProperty("deliveryTime")]
        public string? DeliveryTime { get; set; }

        [JsonProperty("deliveryFeeRate")]
        public string? DeliveryFeeRate { get; set; }

        [JsonProperty("priceScale")]
        public string? PriceScale { get; set; }

        [JsonProperty("leverageFilter")]
        public AccountInstrumentLeverageFilter? LeverageFilter { get; set; }

        [JsonProperty("priceFilter")]
        public AccountInstrumentPriceFilter? PriceFilter { get; set; }

        [JsonProperty("lotSizeFilter")]
        public AccountInstrumentLotSizeFilter? LotSizeFilter { get; set; }

        [JsonProperty("unifiedMarginTrade")]
        public bool? UnifiedMarginTrade { get; set; }

        [JsonProperty("fundingInterval")]
        public int? FundingInterval { get; set; }

        [JsonProperty("settleCoin")]
        public string? SettleCoin { get; set; }

        [JsonProperty("copyTrading")]
        public string? CopyTrading { get; set; }

        [JsonProperty("upperFundingRate")]
        public string? UpperFundingRate { get; set; }

        [JsonProperty("lowerFundingRate")]
        public string? LowerFundingRate { get; set; }

        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }

        [JsonProperty("riskParameters")]
        public AccountInstrumentRiskParameters? RiskParameters { get; set; }

        [JsonProperty("isPreListing")]
        public bool? IsPreListing { get; set; }

        [JsonProperty("preListingInfo")]
        public AccountInstrumentPreListingInfo? PreListingInfo { get; set; }

        [JsonProperty("isPublicRpi")]
        public bool? IsPublicRpi { get; set; }

        [JsonProperty("myRpiPermission")]
        public bool? MyRpiPermission { get; set; }

        [JsonProperty("innovation")]
        public string? Innovation { get; set; }

        [JsonProperty("xstockMultiplier")]
        public string? XstockMultiplier { get; set; }

        [JsonProperty("marginTrading")]
        public string? MarginTrading { get; set; }

        [JsonProperty("stTag")]
        public string? StTag { get; set; }
    }

    public class AccountInstrumentLeverageFilter
    {
        [JsonProperty("minLeverage")]
        public string? MinLeverage { get; set; }

        [JsonProperty("maxLeverage")]
        public string? MaxLeverage { get; set; }

        [JsonProperty("leverageStep")]
        public string? LeverageStep { get; set; }
    }

    public class AccountInstrumentPriceFilter
    {
        [JsonProperty("minPrice")]
        public string? MinPrice { get; set; }

        [JsonProperty("maxPrice")]
        public string? MaxPrice { get; set; }

        [JsonProperty("tickSize")]
        public string? TickSize { get; set; }
    }

    public class AccountInstrumentLotSizeFilter
    {
        [JsonProperty("basePrecision")]
        public string? BasePrecision { get; set; }

        [JsonProperty("quotePrecision")]
        public string? QuotePrecision { get; set; }

        [JsonProperty("minOrderQty")]
        public string? MinOrderQty { get; set; }

        [JsonProperty("maxOrderQty")]
        public string? MaxOrderQty { get; set; }

        [JsonProperty("minOrderAmt")]
        public string? MinOrderAmt { get; set; }

        [JsonProperty("maxOrderAmt")]
        public string? MaxOrderAmt { get; set; }

        [JsonProperty("maxLimitOrderQty")]
        public string? MaxLimitOrderQty { get; set; }

        [JsonProperty("maxMarketOrderQty")]
        public string? MaxMarketOrderQty { get; set; }

        [JsonProperty("postOnlyMaxLimitOrderSize")]
        public string? PostOnlyMaxLimitOrderSize { get; set; }

        [JsonProperty("maxMktOrderQty")]
        public string? MaxMktOrderQty { get; set; }

        [JsonProperty("minNotionalValue")]
        public string? MinNotionalValue { get; set; }

        [JsonProperty("qtyStep")]
        public string? QtyStep { get; set; }

        [JsonProperty("postOnlyMaxOrderQty")]
        public string? PostOnlyMaxOrderQty { get; set; }
    }

    public class AccountInstrumentRiskParameters
    {
        [JsonProperty("priceLimitRatioX")]
        public string? PriceLimitRatioX { get; set; }

        [JsonProperty("priceLimitRatioY")]
        public string? PriceLimitRatioY { get; set; }
    }

    public class AccountInstrumentPreListingInfo
    {
        [JsonProperty("curAuctionPhase")]
        public string? CurAuctionPhase { get; set; }

        [JsonProperty("phases")]
        public List<AccountInstrumentPhase>? Phases { get; set; }

        [JsonProperty("auctionFeeInfo")]
        public AccountInstrumentAuctionFeeInfo? AuctionFeeInfo { get; set; }

        [JsonProperty("skipCallAuction")]
        public bool? SkipCallAuction { get; set; }
    }

    public class AccountInstrumentPhase
    {
        [JsonProperty("phase")]
        public string? Phase { get; set; }

        [JsonProperty("startTime")]
        public string? StartTime { get; set; }

        [JsonProperty("endTime")]
        public string? EndTime { get; set; }
    }

    public class AccountInstrumentAuctionFeeInfo
    {
        [JsonProperty("auctionFeeRate")]
        public string? AuctionFeeRate { get; set; }

        [JsonProperty("takerFeeRate")]
        public string? TakerFeeRate { get; set; }

        [JsonProperty("makerFeeRate")]
        public string? MakerFeeRate { get; set; }
    }
}
