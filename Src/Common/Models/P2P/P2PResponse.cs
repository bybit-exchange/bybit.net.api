using Newtonsoft.Json;

namespace bybit.net.api.Models.P2P
{
    public class P2PResponse<T>
    {
        [JsonProperty("ret_code")]
        public int? RetCode { get; set; }

        [JsonProperty("ret_msg")]
        public string? RetMsg { get; set; }

        [JsonProperty("result")]
        public T? Result { get; set; }

        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("ext_code")]
        public string? ExtCode { get; set; }

        [JsonProperty("ext_info")]
        public Dictionary<string, object>? ExtInfo { get; set; }

        [JsonProperty("ext_map")]
        public Dictionary<string, object>? ExtMap { get; set; }

        [JsonProperty("time_now")]
        public string? TimeNow { get; set; }
    }
}
