using Newtonsoft.Json;

namespace bybit.net.api.Models.Earn
{
    public class GetProductInfoResult
    {
        [JsonProperty("list")]
        public List<EarnProduct>? List { get; set; }
    }

    public class EarnProduct
    {
        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("estimateApr")]
        public string? EstimateApr { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("minStakeAmount")]
        public string? MinStakeAmount { get; set; }

        [JsonProperty("maxStakeAmount")]
        public string? MaxStakeAmount { get; set; }

        [JsonProperty("precision")]
        public string? Precision { get; set; }

        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("bonusEvents")]
        public List<BonusEvent>? BonusEvents { get; set; }

        [JsonProperty("minRedeemAmount")]
        public string? MinRedeemAmount { get; set; }

        [JsonProperty("maxRedeemAmount")]
        public string? MaxRedeemAmount { get; set; }

        [JsonProperty("duration")]
        public string? Duration { get; set; }

        [JsonProperty("term")]
        public int? Term { get; set; }

        [JsonProperty("swapCoin")]
        public string? SwapCoin { get; set; }

        [JsonProperty("swapCoinPrecision")]
        public string? SwapCoinPrecision { get; set; }

        [JsonProperty("stakeExchangeRate")]
        public string? StakeExchangeRate { get; set; }

        [JsonProperty("redeemExchangeRate")]
        public string? RedeemExchangeRate { get; set; }

        [JsonProperty("rewardDistributionType")]
        public string? RewardDistributionType { get; set; }

        [JsonProperty("rewardIntervalMinute")]
        public int? RewardIntervalMinute { get; set; }

        [JsonProperty("redeemProcessingMinute")]
        public string? RedeemProcessingMinute { get; set; }

        [JsonProperty("stakeTime")]
        public string? StakeTime { get; set; }

        [JsonProperty("interestCalculationTime")]
        public string? InterestCalculationTime { get; set; }
    }

    public class BonusEvent
    {
        [JsonProperty("apr")]
        public string? Apr { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("announcement")]
        public string? Announcement { get; set; }
    }
}
