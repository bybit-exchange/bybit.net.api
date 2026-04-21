using System.Collections.Generic;

namespace bybit.net.api.Models.CryptoLoan
{
    public class CryptoLoanCollateralItem
    {
        public string? currency { get; set; }
        public string? amount { get; set; }
    }

    public class MaxLoanCollateralItem
    {
        public string? ccy { get; set; }
        public string? amount { get; set; }
    }

    public class CryptoLoanOrderIdResult
    {
        public string? orderId { get; set; }
    }

    public class CryptoLoanRepayIdResult
    {
        public string? repayId { get; set; }
    }

    public class GetBorrowableCoinsResult
    {
        public List<BorrowableCoinInfo>? list { get; set; }
    }

    public class BorrowableCoinInfo
    {
        public string? currency { get; set; }
        public bool? fixedBorrowable { get; set; }
        public int? fixedBorrowingAccuracy { get; set; }
        public bool? flexibleBorrowable { get; set; }
        public int? flexibleBorrowingAccuracy { get; set; }
        public string? maxBorrowingAmount { get; set; }
        public string? minFixedBorrowingAmount { get; set; }
        public string? minFlexibleBorrowingAmount { get; set; }
        public string? vipLevel { get; set; }
        public string? flexibleAnnualizedInterestRate { get; set; }
        public string? annualizedInterestRate7D { get; set; }
        public string? annualizedInterestRate14D { get; set; }
        public string? annualizedInterestRate30D { get; set; }
        public string? annualizedInterestRate60D { get; set; }
        public string? annualizedInterestRate90D { get; set; }
        public string? annualizedInterestRate180D { get; set; }
    }

    public class GetCollateralCoinsResult
    {
        public List<CollateralRatioConfig>? collateralRatioConfigList { get; set; }
        public List<CurrencyLiquidationInfo>? currencyLiquidationList { get; set; }
    }

    public class CollateralRatioConfig
    {
        public List<CollateralRatioInfo>? collateralRatioList { get; set; }
        public string? currencies { get; set; }
    }

    public class CollateralRatioInfo
    {
        public string? collateralRatio { get; set; }
        public string? maxValue { get; set; }
        public string? minValue { get; set; }
    }

    public class CurrencyLiquidationInfo
    {
        public string? currency { get; set; }
        public int? liquidationOrder { get; set; }
    }

    public class GetMaxAllowedCollateralReductionAmountResult
    {
        public string? maxCollateralAmount { get; set; }
    }

    public class GetMaxLoanAmountResult
    {
        public string? currency { get; set; }
        public string? maxLoan { get; set; }
        public string? notionalUsd { get; set; }
        public string? remainingQuota { get; set; }
    }

    public class AdjustCollateralAmountResult
    {
        public long? adjustId { get; set; }
    }

    public class GetCollateralAdjustmentHistoryResult
    {
        public List<CollateralAdjustmentHistoryItem>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class CollateralAdjustmentHistoryItem
    {
        public string? collateralCurrency { get; set; }
        public string? amount { get; set; }
        public long? adjustId { get; set; }
        public long? adjustTime { get; set; }
        public string? preLTV { get; set; }
        public string? afterLTV { get; set; }
        public int? direction { get; set; }
        public int? status { get; set; }
    }

    public class GetCryptoLoanPositionResult
    {
        public List<CryptoLoanBorrowPosition>? borrowList { get; set; }
        public List<CryptoLoanCollateralPosition>? collateralList { get; set; }
        public string? ltv { get; set; }
        public List<CryptoLoanSupplyPosition>? supplyList { get; set; }
        public string? totalCollateral { get; set; }
        public string? totalDebt { get; set; }
        public string? totalSupply { get; set; }
    }

    public class CryptoLoanBorrowPosition
    {
        public string? fixedTotalDebt { get; set; }
        public string? fixedTotalDebtUSD { get; set; }
        public string? flexibleHourlyInterestRate { get; set; }
        public string? flexibleTotalDebt { get; set; }
        public string? flexibleTotalDebtUSD { get; set; }
        public string? loanCurrency { get; set; }
    }

    public class CryptoLoanCollateralPosition
    {
        public string? amount { get; set; }
        public string? amountUSD { get; set; }
        public string? currency { get; set; }
    }

    public class CryptoLoanSupplyPosition
    {
        public string? amount { get; set; }
        public string? amountUSD { get; set; }
        public string? currency { get; set; }
    }

    public class GetFlexibleLoansResult
    {
        public List<FlexibleLoanItem>? list { get; set; }
    }

    public class FlexibleLoanItem
    {
        public string? hourlyInterestRate { get; set; }
        public string? loanCurrency { get; set; }
        public string? totalDebt { get; set; }
        public string? unpaidAmount { get; set; }
        public string? unpaidInterest { get; set; }
    }

    public class GetFlexibleBorrowOrdersHistoryResult
    {
        public List<FlexibleBorrowOrderHistoryItem>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class FlexibleBorrowOrderHistoryItem
    {
        public long? borrowTime { get; set; }
        public string? initialLoanAmount { get; set; }
        public string? loanCurrency { get; set; }
        public string? orderId { get; set; }
        public int? status { get; set; }
    }

    public class GetFlexibleRepaymentOrdersHistoryResult
    {
        public List<FlexibleRepaymentOrderHistoryItem>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class FlexibleRepaymentOrderHistoryItem
    {
        public string? loanCurrency { get; set; }
        public string? repayAmount { get; set; }
        public string? repayId { get; set; }
        public int? repayStatus { get; set; }
        public long? repayTime { get; set; }
        public int? repayType { get; set; }
    }

    public class GetFixedLoanMarketResult
    {
        public List<FixedLoanMarketItem>? list { get; set; }
    }

    public class FixedLoanMarketItem
    {
        public string? orderCurrency { get; set; }
        public int? term { get; set; }
        public string? annualRate { get; set; }
        public string? qty { get; set; }
    }

    public class GetBorrowContractInfoFixedResult
    {
        public List<BorrowContractInfoFixedItem>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class BorrowContractInfoFixedItem
    {
        public string? annualRate { get; set; }
        public string? autoRepay { get; set; }
        public string? borrowCurrency { get; set; }
        public string? borrowTime { get; set; }
        public string? interestPaid { get; set; }
        public string? loanId { get; set; }
        public string? orderId { get; set; }
        public string? repaymentTime { get; set; }
        public string? residualPenaltyInterest { get; set; }
        public string? residualPrincipal { get; set; }
        public int? status { get; set; }
        public string? term { get; set; }
        public string? repayType { get; set; }
    }

    public class GetSupplyContractInfoFixedResult
    {
        public List<SupplyContractInfoFixedItem>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class SupplyContractInfoFixedItem
    {
        public string? annualRate { get; set; }
        public string? supplyCurrency { get; set; }
        public string? supplyTime { get; set; }
        public string? supplyAmount { get; set; }
        public string? interestPaid { get; set; }
        public string? supplyId { get; set; }
        public string? orderId { get; set; }
        public string? redemptionTime { get; set; }
        public string? penaltyInterest { get; set; }
        public string? actualRedemptionTime { get; set; }
        public int? status { get; set; }
        public string? term { get; set; }
    }

    public class GetBorrowOrderInfoFixedResult
    {
        public List<FixedBorrowOrderInfoItem>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class FixedBorrowOrderInfoItem
    {
        public string? annualRate { get; set; }
        public long? orderId { get; set; }
        public string? orderTime { get; set; }
        public string? filledQty { get; set; }
        public string? orderQty { get; set; }
        public string? orderCurrency { get; set; }
        public int? state { get; set; }
        public int? term { get; set; }
        public string? repayType { get; set; }
    }

    public class GetSupplyOrderInfoFixedResult
    {
        public List<FixedSupplyOrderInfoItem>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class FixedSupplyOrderInfoItem
    {
        public string? annualRate { get; set; }
        public long? orderId { get; set; }
        public string? orderTime { get; set; }
        public string? filledQty { get; set; }
        public string? orderQty { get; set; }
        public string? orderCurrency { get; set; }
        public int? state { get; set; }
        public int? term { get; set; }
    }

    public class GetRepaymentHistoryFixedResult
    {
        public List<FixedRepaymentHistoryItem>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class FixedRepaymentHistoryItem
    {
        public List<FixedRepaymentDetail>? details { get; set; }
        public string? loanCurrency { get; set; }
        public long? repayAmount { get; set; }
        public string? repayId { get; set; }
        public int? repayStatus { get; set; }
        public long? repayTime { get; set; }
        public int? repayType { get; set; }
    }

    public class FixedRepaymentDetail
    {
        public string? loanCurrency { get; set; }
        public long? repayAmount { get; set; }
        public string? loanId { get; set; }
    }

    public class GetRenewOrderInfoFixedResult
    {
        public List<RenewOrderInfoFixedItem>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class RenewOrderInfoFixedItem
    {
        public string? borrowCurrency { get; set; }
        public string? amount { get; set; }
        public int? autoRepay { get; set; }
        public string? contractNo { get; set; }
        public string? dueTime { get; set; }
        public int? orderId { get; set; }
        public string? loanId { get; set; }
        public string? renewLoanNo { get; set; }
        public string? time { get; set; }
    }
}
