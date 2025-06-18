using bybit.net.api.Models.Lending;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitBrokerService : BybitApiService
    {
        public BybitBrokerService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitBrokerService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string BROKER_EARNING_DATA = "/v5/broker/earning-record";
       
        public async Task<string?> GetBrokerEarning(BizType? bizType = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("bizType", bizType?.Value),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(BROKER_EARNING_DATA, HttpMethod.Get, query: query);
            return result;
        }
    }
}
