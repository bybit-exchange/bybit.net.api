using Newtonsoft.Json;

namespace bybit.net.api.Models.Earn
{
    public class GetStakedPositionResult
    {
        [JsonProperty("list")]
        public List<EarnStakedPosition>? List { get; set; }
    }

    public class EarnStakedPosition
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("totalPnl")]
        public string? TotalPnl { get; set; }

        [JsonProperty("claimableYield")]
        public string? ClaimableYield { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("estimateRedeemTime")]
        public string? EstimateRedeemTime { get; set; }

        [JsonProperty("estimateStakeTime")]
        public string? EstimateStakeTime { get; set; }

        [JsonProperty("estimateInterestCalculationTime")]
        public string? EstimateInterestCalculationTime { get; set; }

        [JsonProperty("settlementTime")]
        public string? SettlementTime { get; set; }

        [JsonProperty("autoReinvest")]
        public string? AutoReinvest { get; set; }
    }
}
