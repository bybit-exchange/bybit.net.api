using Newtonsoft.Json;

namespace bybit.net.api.Models.Lending
{
    public class GetC2CLendingCoinInfoResult
    {
        [JsonProperty("list")]
        public List<C2CLendingCoinInfo>? List { get; set; }
    }

    public class C2CLendingCoinInfo
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("maxRedeemQty")]
        public string? MaxRedeemQty { get; set; }

        [JsonProperty("minPurchaseQty")]
        public string? MinPurchaseQty { get; set; }

        [JsonProperty("precision")]
        public string? Precision { get; set; }

        [JsonProperty("rate")]
        public string? Rate { get; set; }

        [JsonProperty("loanToPoolRatio")]
        public string? LoanToPoolRatio { get; set; }

        [JsonProperty("actualApy")]
        public string? ActualApy { get; set; }
    }

    public class C2CLendingOrderResult
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("createdTime")]
        public string? CreatedTime { get; set; }

        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("quantity")]
        public string? Quantity { get; set; }

        [JsonProperty("serialNo")]
        public string? SerialNo { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("updatedTime")]
        public string? UpdatedTime { get; set; }
    }

    public class C2CRedeemOrderResult
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("createdTime")]
        public string? CreatedTime { get; set; }

        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("principalQty")]
        public string? PrincipalQty { get; set; }

        [JsonProperty("serialNo")]
        public string? SerialNo { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("updatedTime")]
        public string? UpdatedTime { get; set; }
    }

    public class C2CCancelRedeemResult
    {
        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("serialNo")]
        public string? SerialNo { get; set; }

        [JsonProperty("updatedTime")]
        public string? UpdatedTime { get; set; }
    }

    public class GetC2CLendingOrdersResult
    {
        [JsonProperty("list")]
        public List<C2CLendingHistoryOrder>? List { get; set; }
    }

    public class C2CLendingHistoryOrder
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("createdTime")]
        public string? CreatedTime { get; set; }

        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("orderType")]
        public string? OrderType { get; set; }

        [JsonProperty("quantity")]
        public string? Quantity { get; set; }

        [JsonProperty("serialNo")]
        public string? SerialNo { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("updatedTime")]
        public string? UpdatedTime { get; set; }
    }

    public class GetC2CLendingAccountInfoResult
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("principalInterest")]
        public string? PrincipalInterest { get; set; }

        [JsonProperty("principalQty")]
        public string? PrincipalQty { get; set; }

        [JsonProperty("principalTotal")]
        public string? PrincipalTotal { get; set; }

        [JsonProperty("quantity")]
        public string? Quantity { get; set; }
    }
}
