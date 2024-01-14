using Newtonsoft.Json;

namespace bybit.net.api.Models.Market.Response;

public class InstrumentInfo
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; } = null!;

    [JsonProperty("contractType")]
    public ContractType ContractType { get; set; }

    [JsonProperty("status")]
    public InstrumentStatusType Status { get; set; }

    [JsonProperty("baseCoin")]
    public string BaseCoin { get; set; } = null!;

    [JsonProperty("quoteCoin")]
    public string QuoteCoin { get; set; } = null!;

    [JsonProperty("launchTime")]
    public long LaunchTime { get; set; }

    [JsonProperty("deliveryTime")]
    public long DeliveryTime { get; set; }

    [JsonProperty("deliveryFeeRate")]
    public decimal? DeliveryFeeRate { get; set; }

    [JsonProperty("priceScale")]
    public byte PriceScale { get; set; }

    [JsonProperty("leverageFilter")]
    public LeverageRules LeverageFilter { get; set; } = null!;

    [JsonProperty("priceFilter")]
    public PriceRules PriceFilter { get; set; } = null!;

    [JsonProperty("lotSizeFilter")]
    public LotSizeRules LotSizeFilter { get; set; } = null!;

    [JsonProperty("unifiedMarginTrade")]
    public bool UnifiedMarginTrade { get; set; }

    [JsonProperty("fundingInterval")]
    public long FundingInterval { get; set; }

    [JsonProperty("settleCoin")]
    public string SettleCoin { get; set; } = null!;

    [JsonProperty("copyTrading")]
    public CopyTradingType CopyTrading { get; set; }
}