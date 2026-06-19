using Newtonsoft.Json;

namespace bybit.net.api.Models.Earn
{
    public class GetTokenProductResult
    {
        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("mintFeeRateE8")]
        public string? MintFeeRateE8 { get; set; }

        [JsonProperty("redeemFeeRateE8")]
        public string? RedeemFeeRateE8 { get; set; }

        [JsonProperty("minInvestment")]
        public string? MinInvestment { get; set; }

        [JsonProperty("userHolding")]
        public string? UserHolding { get; set; }

        [JsonProperty("leftQuota")]
        public string? LeftQuota { get; set; }

        [JsonProperty("canMint")]
        public bool? CanMint { get; set; }

        [JsonProperty("savingsBalance")]
        public string? SavingsBalance { get; set; }

        [JsonProperty("aprE8")]
        public string? AprE8 { get; set; }

        [JsonProperty("bonusAprE8")]
        public string? BonusAprE8 { get; set; }

        [JsonProperty("bonusMaxAmount")]
        public string? BonusMaxAmount { get; set; }

        [JsonProperty("baseCoinPrecision")]
        public int? BaseCoinPrecision { get; set; }

        [JsonProperty("tokenPrecision")]
        public int? TokenPrecision { get; set; }
    }

    public class PlaceTokenOrderResult
    {
        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("orderLinkId")]
        public string? OrderLinkId { get; set; }
    }

    public class GetTokenOrderListResult
    {
        [JsonProperty("list")]
        public List<TokenOrderItem>? List { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class TokenOrderItem
    {
        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("orderLinkId")]
        public string? OrderLinkId { get; set; }

        [JsonProperty("orderType")]
        public string? OrderType { get; set; }

        [JsonProperty("fromCoin")]
        public string? FromCoin { get; set; }

        [JsonProperty("toCoin")]
        public string? ToCoin { get; set; }

        [JsonProperty("fromAmount")]
        public string? FromAmount { get; set; }

        [JsonProperty("toAmount")]
        public string? ToAmount { get; set; }

        [JsonProperty("serviceFee")]
        public string? ServiceFee { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("createdTime")]
        public string? CreatedTime { get; set; }
    }

    public class GetTokenPositionResult
    {
        [JsonProperty("totalAmount")]
        public string? TotalAmount { get; set; }

        [JsonProperty("totalYield")]
        public string? TotalYield { get; set; }

        [JsonProperty("yesterdayYield")]
        public string? YesterdayYield { get; set; }

        [JsonProperty("aprE8")]
        public string? AprE8 { get; set; }

        [JsonProperty("bonusAprE8")]
        public string? BonusAprE8 { get; set; }

        [JsonProperty("bonusMaxAmount")]
        public string? BonusMaxAmount { get; set; }

        [JsonProperty("hasQuota")]
        public bool? HasQuota { get; set; }
    }

    public class GetTokenYieldResult
    {
        [JsonProperty("list")]
        public List<TokenYieldItem>? List { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class TokenYieldItem
    {
        [JsonProperty("yield")]
        public string? Yield { get; set; }

        [JsonProperty("bonusYield")]
        public string? BonusYield { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("createdTime")]
        public string? CreatedTime { get; set; }
    }

    public class GetTokenHourlyYieldResult
    {
        [JsonProperty("list")]
        public List<TokenHourlyYieldItem>? List { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class TokenHourlyYieldItem
    {
        [JsonProperty("effectiveAmount")]
        public string? EffectiveAmount { get; set; }

        [JsonProperty("yield")]
        public string? Yield { get; set; }

        [JsonProperty("rewardType")]
        public int? RewardType { get; set; }

        [JsonProperty("aprE8")]
        public string? AprE8 { get; set; }

        [JsonProperty("hourlyDate")]
        public string? HourlyDate { get; set; }

        [JsonProperty("createdTime")]
        public string? CreatedTime { get; set; }
    }

    public class GetTokenHistoryAprResult
    {
        [JsonProperty("list")]
        public List<TokenHistoryAprItem>? List { get; set; }
    }

    public class TokenHistoryAprItem
    {
        [JsonProperty("timestamp")]
        public string? Timestamp { get; set; }

        [JsonProperty("aprE8")]
        public string? AprE8 { get; set; }
    }
}
