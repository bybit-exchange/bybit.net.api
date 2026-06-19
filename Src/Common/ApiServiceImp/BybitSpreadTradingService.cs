using bybit.net.api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitSpreadTradingService : BybitApiService
    {
        public BybitSpreadTradingService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitSpreadTradingService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string GET_SPREAD_INSTRUMENT_INFO = "/v5/spread/instrument";

        /// <summary>
        /// Get Instruments Info
        /// Query instrument specification of spread combinations.
        /// HTTP GET /v5/spread/instrument
        /// </summary>
        /// <param name="symbol">Spread combination symbol name</param>
        /// <param name="baseCoin">Base coin (uppercase)</param>
        /// <param name="limit">[1,500], default on server: 200</param>
        /// <param name="cursor">Pagination cursor from response.nextPageCursor</param>
        /// <returns>Raw JSON string</returns>
        public async Task<string?> GetSpreadInstrumentsInfo(
            string? symbol = null,
            string? baseCoin = null,
            int? limit = null,
            string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendPublicAsync<string>(
                GET_SPREAD_INSTRUMENT_INFO,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_SPREAD_ORDERBOOK = "/v5/spread/orderbook";

        /// <summary>
        /// Get Orderbook
        /// Query spread orderbook depth data.
        /// HTTP GET /v5/spread/orderbook
        /// </summary>
        /// <param name="symbol">Spread combination symbol name (required)</param>
        /// <param name="limit">Limit size for each bid/ask [1, 25]. Default: 1</param>
        /// <returns>Raw JSON string</returns>
        public async Task<string?> GetSpreadOrderbook(string symbol, int? limit = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("limit", limit)
            );

            var result = await this.SendPublicAsync<string>(
                GET_SPREAD_ORDERBOOK,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_SPREAD_TICKERS = "/v5/spread/tickers";

        /// <summary>
        /// Get Tickers
        /// Latest price snapshot, best bid/ask, and 24h volume for spread combinations.
        /// HTTP GET /v5/spread/tickers
        /// </summary>
        /// <param name="symbol">Spread combination symbol name (required)</param>
        /// <returns>Raw JSON string</returns>
        public async Task<string?> GetSpreadTickers(string symbol)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            var result = await this.SendPublicAsync<string>(
                GET_SPREAD_TICKERS,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_SPREAD_RECENT_TRADES = "/v5/spread/recent-trade";

        /// <summary>
        /// Get Recent Public Trades
        /// Query recent public spread trading history in Bybit.
        /// HTTP GET /v5/spread/recent-trade
        /// </summary>
        /// <param name="symbol">Spread combination symbol name (required)</param>
        /// <param name="limit">Limit per page [1, 1000], default: 500</param>
        /// <returns>Raw JSON string</returns>
        public async Task<string?> GetSpreadRecentPublicTrades(string symbol, int? limit = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("limit", limit)
            );

            var result = await this.SendPublicAsync<string>(
                GET_SPREAD_RECENT_TRADES,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_SPREAD_MAX_ORDER_QTY = "/v5/spread/max-qty";

        /// <summary>
        /// Get Max Qty
        /// Query the maximum order quantity for the given symbol.
        /// HTTP GET /v5/spread/max-qty
        /// </summary>
        /// <param name="symbol">Spread combination symbol name (required)</param>
        /// <param name="side">Order side: 1 Buy, 2 Sell (required)</param>
        /// <param name="orderPrice">Order price (required)</param>
        /// <returns>Raw JSON string</returns>
        public async Task<string?> GetSpreadMaxOrderQty(string symbol, string side, string orderPrice)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", side },
                { "orderPrice", orderPrice }
            };

            var result = await this.SendSignedAsync<string>(
                GET_SPREAD_MAX_ORDER_QTY,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string CREATE_SPREAD_ORDER = "/v5/spread/order/create";

        /// <summary>
        /// Create Order
        /// HTTP POST /v5/spread/order/create
        /// </summary>
        /// <param name="symbol">Spread combination symbol name (required)</param>
        /// <param name="side">Order side: Buy | Sell (required)</param>
        /// <param name="orderType">Order type: Limit | Market (required)</param>
        /// <param name="qty">Order qty (required)</param>
        /// <param name="price">Order price (optional; required for Limit)</param>
        /// <param name="orderLinkId">
        /// User-customized order ID (<=45 chars). Allowed: 0-9, a-z, A-Z, dash (-), underscore (_).
        /// </param>
        /// <param name="timeInForce">IOC | FOK | GTC | PostOnly</param>
        /// <returns>Raw JSON string</returns>
        public async Task<string?> CreateSpreadOrder(
            string symbol,
            string side,
            string orderType,
            string qty,
            string? price = null,
            string? orderLinkId = null,
            string? timeInForce = null)
        {
            var body = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", side },
                { "orderType", orderType },
                { "qty", qty }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("price", price),
                ("orderLinkId", orderLinkId),
                ("timeInForce", timeInForce)
            );

            var result = await this.SendSignedAsync<string>(
                CREATE_SPREAD_ORDER,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string AMEND_SPREAD_ORDER = "/v5/spread/order/amend";

        /// <summary>
        /// Amend Order
        /// You can only modify unfilled or partially filled orders.
        /// </summary>
        /// <param name="symbol">Spread combination symbol name (required)</param>
        /// <param name="orderId">Spread combination order ID (either orderId or orderLinkId)</param>
        /// <param name="orderLinkId">User customised order ID (either orderId or orderLinkId)</param>
        /// <param name="qty">Order quantity after modification (either qty or price)</param>
        /// <param name="price">
        /// Order price after modification. Use empty string ("") to keep price unchanged, "0" to set price to 0.
        /// </param>
        /// <returns>Raw JSON string</returns>
        public async Task<string?> AmendSpreadOrder(
            string symbol,
            string? orderId = null,
            string? orderLinkId = null,
            string? qty = null,
            string? price = null)
        {
            var body = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("qty", qty),
                ("price", price)
            );

            var result = await this.SendSignedAsync<string>(
                AMEND_SPREAD_ORDER,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string CANCEL_SPREAD_ORDER = "/v5/spread/order/cancel";

        /// <summary>
        /// Cancel Order
        /// HTTP POST /v5/spread/order/cancel
        /// </summary>
        /// <param name="orderId">Spread combination order ID</param>
        /// <param name="orderLinkId">User customised order ID</param>
        /// <returns>Raw JSON string</returns>
        public async Task<string?> CancelSpreadOrder(
            string? orderId = null,
            string? orderLinkId = null)
        {
            var body = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(body,
                ("orderId", orderId),
                ("orderLinkId", orderLinkId)
            );

            var result = await this.SendSignedAsync<string>(
                CANCEL_SPREAD_ORDER,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string CANCEL_ALL_SPREAD_ORDERS = "/v5/spread/order/cancel-all";

        /// <summary>
        /// Cancel All Orders
        /// POST /v5/spread/order/cancel-all
        /// </summary>
        /// <param name="symbol">Spread combination symbol name</param>
        /// <param name="cancelAll">true | false</param>
        public async Task<string?> CancelAllSpreadOrders(string? symbol = null, bool? cancelAll = null)
        {
            var body = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(body,
                ("symbol", symbol),
                ("cancelAll", cancelAll)
            );

            var result = await this.SendSignedAsync<string>(
                CANCEL_ALL_SPREAD_ORDERS,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string GET_SPREAD_OPEN_ORDERS = "/v5/spread/order/realtime";

        /// <summary>
        /// Get Open Orders
        /// GET /v5/spread/order/realtime
        /// </summary>
        public async Task<string?> GetSpreadOpenOrders(
            string? symbol = null,
            string? baseCoin = null,
            string? orderId = null,
            string? orderLinkId = null,
            int? limit = null,
            string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<string>(
                GET_SPREAD_OPEN_ORDERS,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_SPREAD_ORDER_HISTORY = "/v5/spread/order/history";

        /// <summary>
        /// Get Order History
        /// GET /v5/spread/order/history
        /// </summary>
        public async Task<string?> GetSpreadOrderHistory(
            string? symbol = null,
            string? baseCoin = null,
            string? orderId = null,
            string? orderLinkId = null,
            long? startTime = null,
            long? endTime = null,
            int? limit = null,
            string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<string>(
                GET_SPREAD_ORDER_HISTORY,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_SPREAD_TRADE_HISTORY = "/v5/spread/execution/list";

        /// <summary>
        /// Get Trade History
        /// GET /v5/spread/execution/list
        /// </summary>
        public async Task<string?> GetSpreadTradeHistory(
            string? symbol = null,
            string? orderId = null,
            string? orderLinkId = null,
            long? startTime = null,
            long? endTime = null,
            int? limit = null,
            string? cursor = null)
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

            var result = await this.SendSignedAsync<string>(
                GET_SPREAD_TRADE_HISTORY,
                HttpMethod.Get,
                query: query);

            return result;
        }


    }
}
