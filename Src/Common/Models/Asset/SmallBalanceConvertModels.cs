using Newtonsoft.Json;

namespace bybit.net.api.Models.Asset
{
    public class GetSmallBalanceCoinsResult
    {
        [JsonProperty("smallAssetCoins")]
        public List<SmallBalanceCoinEntry>? SmallAssetCoins { get; set; }

        [JsonProperty("supportToCoins")]
        public List<string>? SupportToCoins { get; set; }
    }

    public class SmallBalanceCoinEntry
    {
        [JsonProperty("fromCoin")]
        public string? FromCoin { get; set; }

        [JsonProperty("supportConvert")]
        public int? SupportConvert { get; set; }

        [JsonProperty("availableBalance")]
        public string? AvailableBalance { get; set; }

        [JsonProperty("baseValue")]
        public string? BaseValue { get; set; }

        [JsonProperty("toCoin")]
        public string? ToCoin { get; set; }

        [JsonProperty("toAmount")]
        public string? ToAmount { get; set; }

        [JsonProperty("exchangeRate")]
        public string? ExchangeRate { get; set; }

        [JsonProperty("feeInfo")]
        public FeeDetail? FeeInfo { get; set; }

        [JsonProperty("taxFeeInfo")]
        public TaxFeeInfo? TaxFeeInfo { get; set; }
    }

    public class RequestSmallBalanceQuoteResult
    {
        [JsonProperty("quoteId")]
        public string? QuoteId { get; set; }

        [JsonProperty("result")]
        public SmallBalanceQuoteDetail? Result { get; set; }
    }

    public class SmallBalanceQuoteDetail
    {
        [JsonProperty("quoteCreateTime")]
        public string? QuoteCreateTime { get; set; }

        [JsonProperty("quoteExpireTime")]
        public string? QuoteExpireTime { get; set; }

        [JsonProperty("exchangeCoins")]
        public List<SmallBalanceCoinEntry>? ExchangeCoins { get; set; }

        [JsonProperty("totalFeeInfo")]
        public FeeDetail? TotalFeeInfo { get; set; }

        [JsonProperty("totalTaxFeeInfo")]
        public TaxFeeInfo? TotalTaxFeeInfo { get; set; }
    }

    public class ConfirmSmallBalanceQuoteResult
    {
        [JsonProperty("quoteId")]
        public string? QuoteId { get; set; }

        [JsonProperty("exchangeTxId")]
        public string? ExchangeTxId { get; set; }

        [JsonProperty("submitTime")]
        public string? SubmitTime { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("msg")]
        public string? Msg { get; set; }
    }

    public class GetSmallBalanceExchangeHistoryResult
    {
        [JsonProperty("cursor")]
        public string? Cursor { get; set; }

        [JsonProperty("size")]
        public string? Size { get; set; }

        [JsonProperty("lastPage")]
        public string? LastPage { get; set; }

        [JsonProperty("totalCount")]
        public string? TotalCount { get; set; }

        [JsonProperty("records")]
        public List<SmallBalanceExchangeHistoryRecord>? Records { get; set; }
    }

    public class SmallBalanceExchangeHistoryRecord
    {
        [JsonProperty("accountType")]
        public string? AccountType { get; set; }

        [JsonProperty("exchangeTxId")]
        public string? ExchangeTxId { get; set; }

        [JsonProperty("toCoin")]
        public string? ToCoin { get; set; }

        [JsonProperty("toAmount")]
        public string? ToAmount { get; set; }

        [JsonProperty("subRecords")]
        public List<SmallBalanceSubRecord>? SubRecords { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("createdAt")]
        public string? CreatedAt { get; set; }

        [JsonProperty("exchangeSource")]
        public string? ExchangeSource { get; set; }

        [JsonProperty("feeCoin")]
        public string? FeeCoin { get; set; }

        [JsonProperty("totalFeeAmount")]
        public string? TotalFeeAmount { get; set; }

        [JsonProperty("totalTaxFeeInfo")]
        public TaxFeeInfo? TotalTaxFeeInfo { get; set; }
    }

    public class SmallBalanceSubRecord
    {
        [JsonProperty("fromCoin")]
        public string? FromCoin { get; set; }

        [JsonProperty("fromAmount")]
        public string? FromAmount { get; set; }

        [JsonProperty("toCoin")]
        public string? ToCoin { get; set; }

        [JsonProperty("toAmount")]
        public string? ToAmount { get; set; }

        [JsonProperty("feeCoin")]
        public string? FeeCoin { get; set; }

        [JsonProperty("feeAmount")]
        public string? FeeAmount { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("taxFeeInfo")]
        public TaxFeeInfo? TaxFeeInfo { get; set; }
    }

    public class FeeDetail
    {
        [JsonProperty("feeCoin")]
        public string? FeeCoin { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("feeRate")]
        public string? FeeRate { get; set; }
    }

    public class TaxFeeInfo
    {
        [JsonProperty("totalAmount")]
        public string? TotalAmount { get; set; }

        [JsonProperty("feeCoin")]
        public string? FeeCoin { get; set; }

        [JsonProperty("taxFeeItems")]
        public List<object>? TaxFeeItems { get; set; }
    }
}
