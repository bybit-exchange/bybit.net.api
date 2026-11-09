using bybit.net.api.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitPreUpgradeService : BybitApiService
    {
        public BybitPreUpgradeService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitPreUpgradeService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string GET_PRE_UPGRADE_TRANSACTION_LOG = "/v5/pre-upgrade/account/transaction-log";
        /// <summary>
        /// Get Pre-upgrade Transaction Log
        /// </summary>
        public async Task<string?> GetPreUpgradeTransactionLog(string category, string? baseCoin = null, string? type = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("baseCoin", baseCoin),
                ("type", type),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_PRE_UPGRADE_TRANSACTION_LOG, HttpMethod.Get, query: query);
            return result;
        }
    }
}
