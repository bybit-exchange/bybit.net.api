using Newtonsoft.Json;

namespace bybit.net.api.Models.Asset
{
    public class GetWithdrawalAddressListResult
    {
        [JsonProperty("rows")]
        public List<WithdrawalAddressEntry>? Rows { get; set; }

        [JsonProperty("nextPageCursor")]
        public string? NextPageCursor { get; set; }
    }

    public class WithdrawalAddressEntry
    {
        [JsonProperty("coin")]
        public string? Coin { get; set; }

        [JsonProperty("chain")]
        public string? Chain { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("tag")]
        public string? Tag { get; set; }

        [JsonProperty("remark")]
        public string? Remark { get; set; }

        [JsonProperty("status")]
        public int? Status { get; set; }

        [JsonProperty("addressType")]
        public int? AddressType { get; set; }

        [JsonProperty("verified")]
        public int? Verified { get; set; }

        [JsonProperty("createdAt")]
        public string? CreatedAt { get; set; }
    }
}
