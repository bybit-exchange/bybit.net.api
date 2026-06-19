using Newtonsoft.Json;

namespace bybit.net.api.Models.Lending
{
    public class GetInsLoanInfoResult
    {
        [JsonProperty("marginProductInfo")]
        public List<InsLoanProductInfo>? MarginProductInfo { get; set; }
    }

    public class InsLoanProductInfo
    {
        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("leverage")]
        public string? Leverage { get; set; }

        [JsonProperty("supportSpot")]
        public int? SupportSpot { get; set; }

        [JsonProperty("supportContract")]
        public int? SupportContract { get; set; }

        [JsonProperty("supportMarginTrading")]
        public int? SupportMarginTrading { get; set; }

        [JsonProperty("deferredLiquidationLine")]
        public string? DeferredLiquidationLine { get; set; }

        [JsonProperty("deferredLiquidationTime")]
        public string? DeferredLiquidationTime { get; set; }

        [JsonProperty("withdrawLine")]
        public string? WithdrawLine { get; set; }

        [JsonProperty("transferLine")]
        public string? TransferLine { get; set; }

        [JsonProperty("spotBuyLine")]
        public string? SpotBuyLine { get; set; }

        [JsonProperty("spotSellLine")]
        public string? SpotSellLine { get; set; }

        [JsonProperty("contractOpenLine")]
        public string? ContractOpenLine { get; set; }

        [JsonProperty("liquidationLine")]
        public string? LiquidationLine { get; set; }

        [JsonProperty("stopLiquidationLine")]
        public string? StopLiquidationLine { get; set; }

        [JsonProperty("contractLeverage")]
        public string? ContractLeverage { get; set; }

        [JsonProperty("transferRatio")]
        public string? TransferRatio { get; set; }

        [JsonProperty("spotSymbols")]
        public List<string>? SpotSymbols { get; set; }

        [JsonProperty("contractSymbols")]
        public List<string>? ContractSymbols { get; set; }

        [JsonProperty("supportUSDCContract")]
        public int? SupportUsdcContract { get; set; }

        [JsonProperty("supportUSDCOptions")]
        public int? SupportUsdcOptions { get; set; }

        [JsonProperty("USDTPerpetualOpenLine")]
        public string? UsdtPerpetualOpenLine { get; set; }

        [JsonProperty("USDCContractOpenLine")]
        public string? UsdcContractOpenLine { get; set; }

        [JsonProperty("USDCOptionsOpenLine")]
        public string? UsdcOptionsOpenLine { get; set; }

        [JsonProperty("USDTPerpetualCloseLine")]
        public string? UsdtPerpetualCloseLine { get; set; }

        [JsonProperty("USDCContractCloseLine")]
        public string? UsdcContractCloseLine { get; set; }

        [JsonProperty("USDCOptionsCloseLine")]
        public string? UsdcOptionsCloseLine { get; set; }

        [JsonProperty("USDCContractSymbols")]
        public List<string>? UsdcContractSymbols { get; set; }

        [JsonProperty("USDCOptionsSymbols")]
        public List<string>? UsdcOptionsSymbols { get; set; }

        [JsonProperty("marginLeverage")]
        public string? MarginLeverage { get; set; }

        [JsonProperty("USDTPerpetualLeverage")]
        public List<InsLoanSymbolLeverage>? UsdtPerpetualLeverage { get; set; }

        [JsonProperty("USDCContractLeverage")]
        public List<InsLoanSymbolLeverage>? UsdcContractLeverage { get; set; }
    }

    public class InsLoanSymbolLeverage
    {
        [JsonProperty("symbol")]
        public string? Symbol { get; set; }

        [JsonProperty("leverage")]
        public string? Leverage { get; set; }
    }

    public class GetInsMarginCoinInfoResult
    {
        [JsonProperty("marginToken")]
        public List<InsMarginTokenInfo>? MarginToken { get; set; }
    }

    public class InsMarginTokenInfo
    {
        [JsonProperty("productId")]
        public string? ProductId { get; set; }

        [JsonProperty("tokenInfo")]
        public List<InsMarginTokenDetail>? TokenInfo { get; set; }
    }

    public class InsMarginTokenDetail
    {
        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("convertRatioList")]
        public List<InsMarginConvertRatio>? ConvertRatioList { get; set; }
    }

    public class InsMarginConvertRatio
    {
        [JsonProperty("ladder")]
        public string? Ladder { get; set; }

        [JsonProperty("convertRatio")]
        public string? ConvertRatio { get; set; }
    }

    public class GetInsLoanOrdersResult
    {
        [JsonProperty("loanInfo")]
        public List<InsLoanOrderInfo>? LoanInfo { get; set; }
    }

    public class InsLoanOrderInfo : InsLoanProductInfo
    {
        [JsonProperty("orderId")]
        public string? OrderId { get; set; }

        [JsonProperty("orderProductId")]
        public string? OrderProductId { get; set; }

        [JsonProperty("parentUid")]
        public string? ParentUid { get; set; }

        [JsonProperty("loanTime")]
        public string? LoanTime { get; set; }

        [JsonProperty("loanCoin")]
        public string? LoanCoin { get; set; }

        [JsonProperty("loanAmount")]
        public string? LoanAmount { get; set; }

        [JsonProperty("unpaidAmount")]
        public string? UnpaidAmount { get; set; }

        [JsonProperty("unpaidInterest")]
        public string? UnpaidInterest { get; set; }

        [JsonProperty("repaidAmount")]
        public string? RepaidAmount { get; set; }

        [JsonProperty("repaidInterest")]
        public string? RepaidInterest { get; set; }

        [JsonProperty("interestRate")]
        public string? InterestRate { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("reserveToken")]
        public string? ReserveToken { get; set; }

        [JsonProperty("reserveQuantity")]
        public string? ReserveQuantity { get; set; }
    }

    public class GetInsLoanRepayOrdersResult
    {
        [JsonProperty("repayInfo")]
        public List<InsLoanRepayOrderInfo>? RepayInfo { get; set; }
    }

    public class InsLoanRepayOrderInfo
    {
        [JsonProperty("repayOrderId")]
        public string? RepayOrderId { get; set; }

        [JsonProperty("repaidTime")]
        public string? RepaidTime { get; set; }

        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("quantity")]
        public string? Quantity { get; set; }

        [JsonProperty("interest")]
        public string? Interest { get; set; }

        [JsonProperty("businessType")]
        public string? BusinessType { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }
    }

    public class GetInsLoanToValueResult
    {
        [JsonProperty("ltvInfo")]
        public List<InsLoanToValueInfo>? LtvInfo { get; set; }

        [JsonProperty("liqStatus")]
        public int? LiqStatus { get; set; }
    }

    public class InsLoanToValueInfo
    {
        [JsonProperty("ltv")]
        public string? Ltv { get; set; }

        [JsonProperty("rst")]
        public string? Rst { get; set; }

        [JsonProperty("parentUid")]
        public string? ParentUid { get; set; }

        [JsonProperty("subAccountUids")]
        public List<string>? SubAccountUids { get; set; }

        [JsonProperty("unpaidAmount")]
        public string? UnpaidAmount { get; set; }

        [JsonProperty("unpaidInfo")]
        public List<InsLoanUnpaidInfo>? UnpaidInfo { get; set; }

        [JsonProperty("balance")]
        public string? Balance { get; set; }

        [JsonProperty("balanceInfo")]
        public List<InsLoanBalanceInfo>? BalanceInfo { get; set; }

        [JsonProperty("liqStatus")]
        public int? LiqStatus { get; set; }
    }

    public class InsLoanUnpaidInfo
    {
        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("unpaidQty")]
        public string? UnpaidQty { get; set; }

        [JsonProperty("unpaidInterest")]
        public string? UnpaidInterest { get; set; }
    }

    public class InsLoanBalanceInfo
    {
        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("price")]
        public string? Price { get; set; }

        [JsonProperty("qty")]
        public string? Qty { get; set; }

        [JsonProperty("convertedAmount")]
        public string? ConvertedAmount { get; set; }
    }

    public class UpdateInsLoanUidResult
    {
        [JsonProperty("uid")]
        public string? Uid { get; set; }

        [JsonProperty("operate")]
        public string? Operate { get; set; }
    }

    public class RepayInsLoanResult
    {
        [JsonProperty("repayOrderStatus")]
        public string? RepayOrderStatus { get; set; }
    }
}
