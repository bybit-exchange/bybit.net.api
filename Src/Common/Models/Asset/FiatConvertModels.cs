using Newtonsoft.Json;

namespace bybit.net.api.Models.Asset
{
    public class GetFiatBalanceResult
    {
        [JsonProperty("totalBalance")]
        public string? TotalBalance { get; set; }

        [JsonProperty("balance")]
        public string? Balance { get; set; }

        [JsonProperty("frozenBalance")]
        public string? FrozenBalance { get; set; }

        [JsonProperty("currency")]
        public string? Currency { get; set; }
    }

    public class GetFiatTradingPairListResult
    {
        [JsonProperty("fiats")]
        public List<FiatCoinEntry>? Fiats { get; set; }

        [JsonProperty("cryptos")]
        public List<FiatCoinEntry>? Cryptos { get; set; }
    }

    public class FiatCoinEntry
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("fullName")]
        public string? FullName { get; set; }

        [JsonProperty("icon")]
        public string? Icon { get; set; }

        [JsonProperty("iconNight")]
        public string? IconNight { get; set; }

        [JsonProperty("precision")]
        public int? Precision { get; set; }

        [JsonProperty("disable")]
        public bool? Disable { get; set; }

        [JsonProperty("singleFromMinLimit")]
        public string? SingleFromMinLimit { get; set; }

        [JsonProperty("singleFromMaxLimit")]
        public string? SingleFromMaxLimit { get; set; }
    }

    public class RequestFiatQuoteResult
    {
        [JsonProperty("quoteTaxId")]
        public string? QuoteTxId { get; set; }

        [JsonProperty("quoteTxId")]
        private string? QuoteTxIdAlias { set => QuoteTxId ??= value; }

        [JsonProperty("exchangeRate")]
        public string? ExchangeRate { get; set; }

        [JsonProperty("fromCoin")]
        public string? FromCoin { get; set; }

        [JsonProperty("fromCoinType")]
        public string? FromCoinType { get; set; }

        [JsonProperty("toCoin")]
        public string? ToCoin { get; set; }

        [JsonProperty("toCoinType")]
        public string? ToCoinType { get; set; }

        [JsonProperty("fromAmount")]
        public string? FromAmount { get; set; }

        [JsonProperty("toAmount")]
        public string? ToAmount { get; set; }

        [JsonProperty("expireTime")]
        public string? ExpireTime { get; set; }

        [JsonProperty("expiredTime")]
        private string? ExpireTimeAlias { set => ExpireTime ??= value; }
    }

    public class ConfirmFiatQuoteResult
    {
        [JsonProperty("tradeNo")]
        public string? TradeNo { get; set; }

        [JsonProperty("merchantRequestId")]
        public string? MerchantRequestId { get; set; }
    }

    public class GetFiatConvertStatusResult
    {
        [JsonProperty("tradeNo")]
        public string? TradeNo { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("quoteTaxId")]
        public string? QuoteTxId { get; set; }

        [JsonProperty("quoteTxId")]
        private string? QuoteTxIdAlias { set => QuoteTxId ??= value; }

        [JsonProperty("exchangeRate")]
        public string? ExchangeRate { get; set; }

        [JsonProperty("fromCoin")]
        public string? FromCoin { get; set; }

        [JsonProperty("fromCoinType")]
        public string? FromCoinType { get; set; }

        [JsonProperty("toCoin")]
        public string? ToCoin { get; set; }

        [JsonProperty("toCoinType")]
        public string? ToCoinType { get; set; }

        [JsonProperty("fromAmount")]
        public string? FromAmount { get; set; }

        [JsonProperty("toAmount")]
        public string? ToAmount { get; set; }

        [JsonProperty("createdAt")]
        public string? CreatedAt { get; set; }

        [JsonProperty("subUserId")]
        public string? SubUserId { get; set; }
    }

    public class GetFiatReferencePriceResult
    {
        [JsonProperty("symbol")]
        public string? Symbol { get; set; }

        [JsonProperty("fiat")]
        public string? Fiat { get; set; }

        [JsonProperty("crypto")]
        public string? Crypto { get; set; }

        [JsonProperty("timestamp")]
        public string? Timestamp { get; set; }

        [JsonProperty("buys")]
        public List<ReferencePriceQuote>? Buys { get; set; }

        [JsonProperty("sells")]
        public List<ReferencePriceQuote>? Sells { get; set; }
    }

    public class ReferencePriceQuote
    {
        [JsonProperty("unitPrice")]
        public string? UnitPrice { get; set; }

        [JsonProperty("paymentMethod")]
        public string? PaymentMethod { get; set; }
    }
}
