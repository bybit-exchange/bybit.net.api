using Newtonsoft.Json;

namespace bybit.net.api.Models.Asset
{
    public class GetTransferableCoinResult
    {
        [JsonProperty("list")]
        public List<string>? List { get; set; }
    }

    public class CreateTransferResult
    {
        [JsonProperty("transferId")]
        public string? TransferId { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }
    }

    public class GetTransferRecordsResult
    {
        [JsonProperty("list")]
        public List<TransferRecordItem>? List { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class TransferRecordItem
    {
        [JsonProperty("transferId")]
        public string? TransferId { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("fromMemberId")]
        public string? FromMemberId { get; set; }

        [JsonProperty("toMemberId")]
        public string? ToMemberId { get; set; }

        [JsonProperty("fromAccountType")]
        public string? FromAccountType { get; set; }

        [JsonProperty("toAccountType")]
        public string? ToAccountType { get; set; }

        [JsonProperty("timestamp")]
        public string? Timestamp { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }
    }
}
