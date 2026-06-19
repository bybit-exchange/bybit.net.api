using Newtonsoft.Json;

namespace bybit.net.api.Models.Asset
{
    public class GetCoinExchangeRecordsResult
    {
        [JsonProperty("orderBody")]
        public List<CoinExchangeRecord>? OrderBody { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class CoinExchangeRecord
    {
        [JsonProperty("fromCoin")]
        public string? FromCoin { get; set; }

        [JsonProperty("fromAmount")]
        public string? FromAmount { get; set; }

        [JsonProperty("toCoin")]
        public string? ToCoin { get; set; }

        [JsonProperty("toAmount")]
        public string? ToAmount { get; set; }

        [JsonProperty("exchangeRate")]
        public string? ExchangeRate { get; set; }

        [JsonProperty("createdTime")]
        public string? CreatedTime { get; set; }

        [JsonProperty("exchangeTxId")]
        public string? ExchangeTxId { get; set; }
    }
}
