using bybit.net.api.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitStrategyService : BybitApiService
    {
        public BybitStrategyService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitStrategyService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string CREATE_STRATEGY = "/v5/strategy/create";
        /// <summary>
        /// Create a strategy order
        /// </summary>
        public async Task<string?> CreateStrategy(Dictionary<string, object> parameters)
        {
            var result = await this.SendSignedAsync<string>(CREATE_STRATEGY, HttpMethod.Post, query: parameters);
            return result;
        }
        private const string QUERY_STRATEGY_LIST = "/v5/strategy/list";
        /// <summary>
        /// Query trading strategy list with filters
        /// </summary>
        public async Task<string?> QueryStrategyList(string? strategyId = null, string? status = null, string? symbol = null, string? category = null, string? strategyType = null, long? beginTimeE0 = null, long? endTimeE0 = null, long? pageSize = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("strategyId", strategyId),
                ("status", status),
                ("symbol", symbol),
                ("category", category),
                ("strategyType", strategyType),
                ("beginTimeE0", beginTimeE0),
                ("endTimeE0", endTimeE0),
                ("pageSize", pageSize),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(QUERY_STRATEGY_LIST, HttpMethod.Get, query: query);
            return result;
        }
        private const string QUERY_STRATEGY_ORDER_LIST = "/v5/strategy/order-list";
        /// <summary>
        /// Query orders created by a specific strategy
        /// </summary>
        public async Task<string?> QueryStrategyOrderList(string strategyId, string? status = null, string? symbol = null, long? beginTimeE0 = null, long? endTimeE0 = null, long? pageSize = null, string? cursor = null, string? strategyType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "strategyId", strategyId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("status", status),
                ("symbol", symbol),
                ("beginTimeE0", beginTimeE0),
                ("endTimeE0", endTimeE0),
                ("pageSize", pageSize),
                ("cursor", cursor),
                ("strategyType", strategyType)
            );
            var result = await this.SendSignedAsync<string>(QUERY_STRATEGY_ORDER_LIST, HttpMethod.Get, query: query);
            return result;
        }
        private const string STOP_STRATEGY = "/v5/strategy/stop";
        /// <summary>
        /// Stop a running strategy
        /// </summary>
        public async Task<string?> StopStrategy(string strategyId)
        {
            var query = new Dictionary<string, object>
            {
                { "strategyId", strategyId }
            };
            var result = await this.SendSignedAsync<string>(STOP_STRATEGY, HttpMethod.Post, query: query);
            return result;
        }
    }
}
