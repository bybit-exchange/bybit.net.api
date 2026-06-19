using Newtonsoft.Json;

namespace bybit.net.api.Models.Affiliate
{
    public class GetAffiliateUserListResult
    {
        [JsonProperty("list")]
        public List<AffiliateUser>? List { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class AffiliateUser
    {
        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("registerTime")]
        public string? RegisterTime { get; set; }

        [JsonProperty("source")]
        public string? Source { get; set; }

        [JsonProperty("remarks")]
        public string? Remarks { get; set; }

        [JsonProperty("isKyc")]
        public bool? IsKyc { get; set; }

        [JsonProperty("takerVol30Day")]
        public string? TakerVol30Day { get; set; }

        [JsonProperty("makerVol30Day")]
        public string? MakerVol30Day { get; set; }

        [JsonProperty("tradeVol30Day")]
        public string? TradeVol30Day { get; set; }

        [JsonProperty("depositAmount30Day")]
        public string? DepositAmount30Day { get; set; }

        [JsonProperty("takerVol365Day")]
        public string? TakerVol365Day { get; set; }

        [JsonProperty("makerVol365Day")]
        public string? MakerVol365Day { get; set; }

        [JsonProperty("tradeVol365Day")]
        public string? TradeVol365Day { get; set; }

        [JsonProperty("depositAmount365Day")]
        public string? DepositAmount365Day { get; set; }

        [JsonProperty("takerVol")]
        public string? TakerVol { get; set; }

        [JsonProperty("makerVol")]
        public string? MakerVol { get; set; }

        [JsonProperty("tradeVol")]
        public string? TradeVol { get; set; }

        [JsonProperty("startDate")]
        public string? StartDate { get; set; }

        [JsonProperty("endDate")]
        public string? EndDate { get; set; }

        [JsonProperty("tradfiTradeVol")]
        public string? TradfiTradeVol { get; set; }

        [JsonProperty("tradfiTradeVol30Day")]
        public string? TradfiTradeVol30Day { get; set; }

        [JsonProperty("tradfiTradeVol365Day")]
        public string? TradfiTradeVol365Day { get; set; }

        [JsonProperty("commissionsVol")]
        public Dictionary<string, string>? CommissionsVol { get; set; }

        [JsonProperty("commissions30Day")]
        public Dictionary<string, string>? Commissions30Day { get; set; }

        [JsonProperty("commissions365Day")]
        public Dictionary<string, string>? Commissions365Day { get; set; }
    }
}
