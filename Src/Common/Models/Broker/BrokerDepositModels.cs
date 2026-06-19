using Newtonsoft.Json;

namespace bybit.net.api.Models.Broker
{
    public class GetBrokerSubMemberDepositRecordsResult
    {
        [JsonProperty("rows")]
        public List<BrokerSubMemberDepositRecord>? Rows { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class BrokerSubMemberDepositRecord
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("subMemberId")]
        public string? SubMemberId { get; set; }

        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("chain")]
        public string? Chain { get; set; }

        [JsonProperty("amount")]
        public string? Amount { get; set; }

        [JsonProperty("txID")]
        public string? TxId { get; set; }

        [JsonProperty("status")]
        public int? Status { get; set; }

        [JsonProperty("toAddress")]
        public string? ToAddress { get; set; }

        [JsonProperty("tag")]
        public string? Tag { get; set; }

        [JsonProperty("depositFee")]
        public string? DepositFee { get; set; }

        [JsonProperty("successAt")]
        public string? SuccessAt { get; set; }

        [JsonProperty("confirmations")]
        public string? Confirmations { get; set; }

        [JsonProperty("txIndex")]
        public string? TxIndex { get; set; }

        [JsonProperty("blockHash")]
        public string? BlockHash { get; set; }

        [JsonProperty("batchReleaseLimit")]
        public string? BatchReleaseLimit { get; set; }

        [JsonProperty("depositType")]
        public string? DepositType { get; set; }

        [JsonProperty("fromAddress")]
        public string? FromAddress { get; set; }

        [JsonProperty("taxDepositRecordsId")]
        public string? TaxDepositRecordsId { get; set; }

        [JsonProperty("taxStatus")]
        public int? TaxStatus { get; set; }
    }
}
