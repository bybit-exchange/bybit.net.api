using Newtonsoft.Json;

namespace bybit.net.api.Models.Earn
{
    public class GetFixedTermProductInfoResult
    {
        [JsonProperty("list")]
        public List<FixedTermProduct>? List { get; set; }
    }

    public class FixedTermProduct
    {
        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("duration")]
        public string? Duration { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("tieredApyList")]
        public List<FixedTermTieredApy>? TieredApyList { get; set; }

        [JsonProperty("minStakeAmount")]
        public string? MinStakeAmount { get; set; }

        [JsonProperty("maxStakeAmount")]
        public string? MaxStakeAmount { get; set; }

        [JsonProperty("precision")]
        public int? Precision { get; set; }

        [JsonProperty("subscribeStartAt")]
        public string? SubscribeStartAt { get; set; }

        [JsonProperty("subscribeEndAt")]
        public string? SubscribeEndAt { get; set; }

        [JsonProperty("allowEarlyRedemption")]
        public bool? AllowEarlyRedemption { get; set; }

        [JsonProperty("earlyRedemptionApy")]
        public string? EarlyRedemptionApy { get; set; }

        [JsonProperty("redemptionLimitDuration")]
        public string? RedemptionLimitDuration { get; set; }

        [JsonProperty("allowAutoReinvest")]
        public bool? AllowAutoReinvest { get; set; }

        [JsonProperty("interestCoinApyList")]
        public List<FixedTermInterestCoinApy>? InterestCoinApyList { get; set; }

        [JsonProperty("isVip")]
        public bool? IsVip { get; set; }

        [JsonProperty("creditTime")]
        public string? CreditTime { get; set; }

        [JsonProperty("specialUserGroupRequired")]
        public bool? SpecialUserGroupRequired { get; set; }

        [JsonProperty("specialUserGroupInfo")]
        public string? SpecialUserGroupInfo { get; set; }
    }

    public class FixedTermTieredApy
    {
        [JsonProperty("min")]
        public string? Min { get; set; }

        [JsonProperty("max")]
        public string? Max { get; set; }

        [JsonProperty("apy")]
        public string? Apy { get; set; }
    }

    public class FixedTermInterestCoinApy
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("apy")]
        public string? Apy { get; set; }

        [JsonProperty("expectUnitEarning")]
        public string? ExpectUnitEarning { get; set; }

        [JsonProperty("currentPrice")]
        public string? CurrentPrice { get; set; }
    }

    public class PlaceFixedTermOrderResult
    {
        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("orderLinkId")]
        public string? OrderLinkId { get; set; }
    }

    public class GetFixedTermOrderListResult
    {
        [JsonProperty("list")]
        public List<FixedTermOrderItem>? List { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class FixedTermOrderItem
    {
        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("orderLinkId")]
        public string? OrderLinkId { get; set; }

        [JsonProperty("orderType")]
        public string? OrderType { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("duration")]
        public string? Duration { get; set; }

        [JsonProperty("accountType")]
        public string? AccountType { get; set; }

        [JsonProperty("settlementTime")]
        public string? SettlementTime { get; set; }

        [JsonProperty("createdAt")]
        public string? CreatedAt { get; set; }

        [JsonProperty("yieldInfoList")]
        public List<FixedTermYieldInfo>? YieldInfoList { get; set; }
    }

    public class FixedTermYieldInfo
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("createdAt")]
        public string? CreatedAt { get; set; }

        [JsonProperty("apy")]
        public string? Apy { get; set; }
    }

    public class GetFixedTermPositionResult
    {
        [JsonProperty("list")]
        public List<FixedTermPositionItem>? List { get; set; }
    }

    public class FixedTermPositionItem
    {
        [JsonProperty("positionId")]
        public string? PositionId { get; set; }

        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("effectiveAmount")]
        public string? EffectiveAmount { get; set; }

        [JsonProperty("duration")]
        public string? Duration { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("settlementTime")]
        public string? SettlementTime { get; set; }

        [JsonProperty("createdAt")]
        public string? CreatedAt { get; set; }

        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("earlyRedeemInfo")]
        public FixedTermEarlyRedeemInfo? EarlyRedeemInfo { get; set; }

        [JsonProperty("allowAutoReinvest")]
        public bool? AllowAutoReinvest { get; set; }

        [JsonProperty("autoReinvest")]
        public string? AutoReinvest { get; set; }

        [JsonProperty("interestCoinApyList")]
        public List<FixedTermPositionInterestCoinApy>? InterestCoinApyList { get; set; }
    }

    public class FixedTermEarlyRedeemInfo
    {
        [JsonProperty("allowEarlyRedeem")]
        public bool? AllowEarlyRedeem { get; set; }

        [JsonProperty("earlyRedeemEarning")]
        public string? EarlyRedeemEarning { get; set; }

        [JsonProperty("returnCoin")]
        public string? ReturnCoin { get; set; }

        [JsonProperty("redemptionLimitDuration")]
        public string? RedemptionLimitDuration { get; set; }
    }

    public class FixedTermPositionInterestCoinApy
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("apy")]
        public string? Apy { get; set; }

        [JsonProperty("expectReturnEarning")]
        public string? ExpectReturnEarning { get; set; }

        [JsonProperty("price")]
        public string? Price { get; set; }
    }

    public class RedeemFixedTermResult
    {
        [JsonProperty("redeemAmount")]
        public string? RedeemAmount { get; set; }

        [JsonProperty("estEarnings")]
        public string? EstEarnings { get; set; }
    }

    public class SetFixedTermAutoInvestResult
    {
    }
}
