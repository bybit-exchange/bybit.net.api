using bybit.net.api.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitEventService : BybitApiService
    {
        public BybitEventService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitEventService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string GET_EC_ORDER_HISTORY = "/v5/event/order-list";
        /// <summary>
        /// Get Event Contract Order History
        /// </summary>
        public async Task<string?> GetEcOrderHistory(string? symbol = null, string? orderId = null, string? orderLinkId = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_EC_ORDER_HISTORY, HttpMethod.Get, query: query);
            return result;
        }
        private const string GET_EC_ACTIVE_ORDERS = "/v5/event/order-realtime";
        /// <summary>
        /// Get Event Contract Active Orders
        /// </summary>
        public async Task<string?> GetEcActiveOrders(string? symbol = null, string? orderId = null, string? orderLinkId = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_EC_ACTIVE_ORDERS, HttpMethod.Get, query: query);
            return result;
        }
        private const string GET_EC_POSITION_INFO = "/v5/event/positions";
        /// <summary>
        /// Get Event Contract Position Info
        /// </summary>
        public async Task<string?> GetEcPositionInfo(string? symbol = null, string? baseCoin = null, string? settleCoin = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("settleCoin", settleCoin),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_EC_POSITION_INFO, HttpMethod.Get, query: query);
            return result;
        }
        private const string GET_EC_SETTLEMENTS = "/v5/event/settlements";
        /// <summary>
        /// Get Event Contract Settlement Records
        /// </summary>
        public async Task<string?> GetEcSettlements(string symbol, string? orderId = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_EC_SETTLEMENTS, HttpMethod.Get, query: query);
            return result;
        }
        private const string GET_EC_TRADE_HISTORY = "/v5/event/trades";
        /// <summary>
        /// Get Event Contract Trade History
        /// </summary>
        public async Task<string?> GetEcTradeHistory(string symbol, string? orderId = null, string? orderLinkId = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_EC_TRADE_HISTORY, HttpMethod.Get, query: query);
            return result;
        }
    }
}
