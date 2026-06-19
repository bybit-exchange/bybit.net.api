using Newtonsoft.Json;

namespace bybit.net.api.Models.Asset
{
    public class GetFundingHistoryResult
    {
        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }

        [JsonProperty("list")]
        public List<FundingHistoryEntry>? List { get; set; }
    }

    public class FundingHistoryEntry
    {
        [JsonProperty("memberId")]
        public string? MemberId { get; set; }

        [JsonProperty("currency")]
        public string? Currency { get; set; }

        [JsonProperty("ioDirection")]
        public string? IoDirection { get; set; }

        [JsonProperty("txnAmt")]
        public string? TxnAmt { get; set; }

        [JsonProperty("afterAmt")]
        public string? AfterAmt { get; set; }

        [JsonProperty("createTime")]
        public string? CreateTime { get; set; }

        [JsonProperty("showBusiType")]
        public string? ShowBusiType { get; set; }

        [JsonProperty("showBusiTypeEn")]
        public string? ShowBusiTypeEn { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("descriptionEn")]
        public string? DescriptionEn { get; set; }
    }

    public class GetTotalMembersAssetsResult
    {
        [JsonProperty("total")]
        public string? Total { get; set; }

        [JsonProperty("quoteTotal")]
        public string? QuoteTotal { get; set; }

        [JsonProperty("stat")]
        public int? Stat { get; set; }

        [JsonProperty("list")]
        public List<MemberAssetEntry>? List { get; set; }
    }

    public class MemberAssetEntry
    {
        [JsonProperty("uid")]
        public long? Uid { get; set; }

        [JsonProperty("isM")]
        public bool? IsMaster { get; set; }

        [JsonProperty("type")]
        public int? Type { get; set; }

        [JsonProperty("stat")]
        public int? Stat { get; set; }

        [JsonProperty("origb")]
        public string? Origb { get; set; }

        [JsonProperty("quoteb")]
        public string? Quoteb { get; set; }

        [JsonProperty("items")]
        public List<MemberAssetItem>? Items { get; set; }
    }

    public class MemberAssetItem
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("origb")]
        public string? Origb { get; set; }

        [JsonProperty("quoteb")]
        public string? Quoteb { get; set; }

        [JsonProperty("stat")]
        public int? Stat { get; set; }
    }

    public class GetAssetOverviewResult
    {
        [JsonProperty("totalEquity")]
        public string? TotalEquity { get; set; }

        [JsonProperty("list")]
        public List<AssetOverviewEntry>? List { get; set; }
    }

    public class AssetOverviewEntry
    {
        [JsonProperty("accountType")]
        public string? AccountType { get; set; }

        [JsonProperty("totalEquity")]
        public string? TotalEquity { get; set; }

        [JsonProperty("valuationCurrency")]
        public string? ValuationCurrency { get; set; }

        [JsonProperty("snapshotTime")]
        public string? SnapshotTime { get; set; }

        [JsonProperty("coinDetail")]
        public List<AssetOverviewCoinDetail>? CoinDetail { get; set; }

        [JsonProperty("categories")]
        public List<AssetOverviewCategory>? Categories { get; set; }
    }

    public class AssetOverviewCategory
    {
        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("equity")]
        public string? Equity { get; set; }

        [JsonProperty("coinDetail")]
        public List<AssetOverviewCoinDetail>? CoinDetail { get; set; }
    }

    public class AssetOverviewCoinDetail
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("equity")]
        public string? Equity { get; set; }

        [JsonProperty("extMap")]
        public Dictionary<string, string>? ExtMap { get; set; }
    }
}
