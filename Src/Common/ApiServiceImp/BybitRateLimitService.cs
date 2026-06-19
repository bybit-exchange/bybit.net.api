using bybit.net.api.Models.RateLimit;
using bybit.net.api.Models;
using bybit.net.api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitRateLimitService : BybitApiService
    {
        public BybitRateLimitService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitRateLimitService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string SET_API_RATE_LIMIT = "/v5/apilimit/set";

        /// <summary>
        /// Set Rate Limit — POST /v5/apilimit/set
        /// </summary>
        public async Task<GeneralResponse<SetApiRateLimitResponse>?> SetApiRateLimit(IEnumerable<ApiRateLimitSetItem> list)
        {
            var body = new Dictionary<string, object>
            {
                { "list", list?.ToArray() ?? Array.Empty<ApiRateLimitSetItem>() }
            };

            var result = await this.SendSignedAsync<GeneralResponse<SetApiRateLimitResponse>>(
                SET_API_RATE_LIMIT,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string GET_API_RATE_LIMIT = "/v5/apilimit/query";

        /// <summary>
        /// Get Rate Limit — GET /v5/apilimit/query
        /// </summary>
        public async Task<GeneralResponse<GetApiRateLimitResponse>?> GetApiRateLimit(string uids)
        {
            var query = new Dictionary<string, object>
            {
                { "uids", uids }
            };

            var result = await this.SendSignedAsync<GeneralResponse<GetApiRateLimitResponse>>(
                GET_API_RATE_LIMIT,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_API_RATE_LIMIT_CAP = "/v5/apilimit/query-cap";

        /// <summary>
        /// Get Rate Limit Cap — GET /v5/apilimit/query-cap
        /// </summary>
        public async Task<GeneralResponse<GetApiRateLimitCapResponse>?> GetApiRateLimitCap()
        {
            var query = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<GeneralResponse<GetApiRateLimitCapResponse>>(
                GET_API_RATE_LIMIT_CAP,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_API_RATE_LIMIT_ALL = "/v5/apilimit/query-all";

        /// <summary>
        /// Get All Rate Limits — GET /v5/apilimit/query-all
        /// </summary>
        public async Task<GeneralResponse<GetAllApiRateLimitsResponse>?> GetAllApiRateLimits(
            string? limit = null,     // [1, 1000], default 1000
            string? cursor = null,
            string? uids = null)      // comma-separated; across different masters
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("limit", limit),
                ("cursor", cursor),
                ("uids", uids)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetAllApiRateLimitsResponse>>(
                GET_API_RATE_LIMIT_ALL,
                HttpMethod.Get,
                query: query);

            return result;
        }
    }
}
