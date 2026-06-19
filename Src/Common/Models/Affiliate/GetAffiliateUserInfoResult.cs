using Newtonsoft.Json;

namespace bybit.net.api.Models.Affiliate
{
    public class GetAffiliateUserInfoResult
    {
        [JsonProperty("uid")]
        public string? Uid { get; set; }

        [JsonProperty("vipLevel")]
        public string? VipLevel { get; set; }

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

        [JsonProperty("totalWalletBalance")]
        public string? TotalWalletBalance { get; set; }

        [JsonProperty("depositUpdateTime")]
        public string? DepositUpdateTime { get; set; }

        [JsonProperty("volUpdateTime")]
        public string? VolUpdateTime { get; set; }

        [JsonProperty("KycLevel")]
        public int? KycLevel { get; set; }

        [JsonProperty("tradfiTradeVol30Day")]
        public string? TradfiTradeVol30Day { get; set; }

        [JsonProperty("tradfiTradeVol365Day")]
        public string? TradfiTradeVol365Day { get; set; }

        [JsonProperty("commissions30Day")]
        public Dictionary<string, string>? Commissions30Day { get; set; }

        [JsonProperty("commissions365Day")]
        public Dictionary<string, string>? Commissions365Day { get; set; }
    }
}
