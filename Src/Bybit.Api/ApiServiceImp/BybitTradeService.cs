using bybit.net.api.Models;
using bybit.net.api.Models.Position;
using bybit.net.api.Models.Trade;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitTradeService : BybitApiService
    {
        public BybitTradeService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitTradeService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url : url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string PLACE_ORDER = "/v5/order/create";
        /// <summary>
        /// Send in a new order.
        /// - Supported order types: Limit order (specify order qty and price), Market order (execute at best available market price, with price conversion protections).
        /// - Supported timeInForce strategies: GTC, IOC, FOK, PostOnly (higher quantity limits, consult instruments-info endpoint).
        /// - Conditional orders: Converted when triggerPrice is set; does not occupy margin, may be canceled if margin is insufficient post-trigger.
        /// - Take Profit/Stop Loss: Can be set during order placement and modified for positions.
        /// - Order quantity for perpetual contracts must be positive.
        /// - Limit order price: Must be above liquidation price, consult instruments-info endpoint for minimum changes.
        /// - Custom order IDs (orderLinkId) can be used for linking to system IDs; prioritize orderId over orderLinkId if both provided.
        /// - Order limits: Futures (500 active orders per contract), Spot (500 total, 30 TP/SL, 30 conditional), Option (100 open).
        /// - Rate limits and risk control limits apply. Excessive API requests may trigger restrictions.
        /// TIP: Borrow margin for margin trading on spot in a normal account.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="side"></param>
        /// <param name="orderType"></param>
        /// <param name="qty"></param>
        /// <param name="price"></param>
        /// <param name="timeInForce"></param>
        /// <param name="isLeverage"></param>
        /// <param name="triggerDirection"></param>
        /// <param name="orderFilter"></param>
        /// <param name="triggerPrice"></param>
        /// <param name="triggerBy"></param>
        /// <param name="orderIv"></param>
        /// <param name="positionIdx"></param>
        /// <param name="orderLinkId"></param>
        /// <param name="takeProfit"></param>
        /// <param name="stopLoss"></param>
        /// <param name="tpTriggerBy"></param>
        /// <param name="slTriggerBy"></param>
        /// <param name="reduceOnly"></param>
        /// <param name="closeOnTrigger"></param>
        /// <param name="smpType"></param>
        /// <param name="mmp"></param>
        /// <param name="tpslMode"></param>
        /// <param name="tpLimitPrice"></param>
        /// <param name="slLimitPrice"></param>
        /// <param name="tpOrderType"></param>
        /// <param name="slOrderType"></param>
        /// <returns>Order result</returns>
        public async Task<string?> PlaceOrder(Category category, string symbol, Side side, OrderType orderType, string qty, string? price = null, TimeInForce? timeInForce = null, int? isLeverage = null, int? triggerDirection = null,
                                                string? orderFilter = null, string? triggerPrice = null, TriggerBy? triggerBy = null, string? orderIv = null, int? positionIdx = null, string? orderLinkId = null, string? takeProfit = null,
                                                string? stopLoss = null, TriggerBy? tpTriggerBy = null, TriggerBy? slTriggerBy = null, bool? reduceOnly = null, bool? closeOnTrigger = null, SmpType? smpType = null, bool? mmp = null, TpslMode? tpslMode = null,
                                                string? tpLimitPrice = null, string? slLimitPrice = null, OrderType? tpOrderType = null, OrderType? slOrderType = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                            { "symbol", symbol },
                            { "side", side.Value },
                            { "orderType", orderType.Value },
                            { "qty", qty }
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("price", price),
                ("timeInForce", timeInForce?.Value),
                ("isLeverage", isLeverage),
                ("triggerDirection", triggerDirection),
                ("orderFilter", orderFilter),
                ("triggerPrice", triggerPrice),
                ("triggerBy", triggerBy?.Value),
                ("orderIv", orderIv),
                ("positionIdx", positionIdx),
                ("orderLinkId", orderLinkId),
                ("takeProfit", takeProfit),
                ("stopLoss", stopLoss),
                ("tpTriggerBy", tpTriggerBy?.Value),
                ("slTriggerBy", slTriggerBy?.Value),
                ("reduceOnly", reduceOnly),
                ("closeOnTrigger", closeOnTrigger),
                ("smpType", smpType?.Value),
                ("mmp", mmp),
                ("tpslMode", tpslMode?.Value),
                ("tpLimitPrice", tpLimitPrice),
                ("slLimitPrice", slLimitPrice),
                ("tpOrderType", tpOrderType?.Value),
                ("slOrderType", slOrderType?.Value)
            );
            var result = await this.SendSignedAsync<string>(PLACE_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        private const string BATCH_PLACE_ORDER = "/v5/order/create-batch";
        /// <summary>
        /// Covers: Option (UTA, UTA Pro) / USDT Perpetual, UDSC Perpetual, USDC Futures (UTA Pro)
        /// This endpoint allows you to place more than one order in a single request.
        /// Make sure you have sufficient funds in your account when placing an order.Once an order is placed, according to the funds required by the order, the funds in your account will be frozen by the corresponding amount during the life cycle of the order.
        /// A maximum of 20 orders (option) & 10 orders (linear) can be placed per request. The returned data list is divided into two lists. The first list indicates whether or not the order creation was successful and the second list details the created order information.The structure of the two lists are completely consistent.
        /// Check the rate limit instruction when category=linear here
        /// Risk control limit notice:
        /// Bybit will monitor on your API requests.When the total number of orders of a single user(aggregated the number of orders across main account and sub-accounts) within a day(UTC 0 - UTC 24) exceeds a certain upper limit, the platform will reserve the right to remind, warn, and impose necessary restrictions.Customers who use API default to acceptance of these terms and have the obligation to cooperate with adjustments.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="request"></param>
        /// <returns>List Order Result</returns>
        public async Task<string?> PlaceBatchOrder(Category category, List<Dictionary<string, object>> request)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category.Value },
                { "request", request }
            };
            var result = await this.SendSignedAsync<string>(BATCH_PLACE_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        /// <summary>
        /// Covers: Option (UTA, UTA Pro) / USDT Perpetual, UDSC Perpetual, USDC Futures (UTA Pro)
        /// This endpoint allows you to place more than one order in a single request.
        /// Make sure you have sufficient funds in your account when placing an order.Once an order is placed, according to the funds required by the order, the funds in your account will be frozen by the corresponding amount during the life cycle of the order.
        /// A maximum of 20 orders (option) & 10 orders (linear) can be placed per request. The returned data list is divided into two lists. The first list indicates whether or not the order creation was successful and the second list details the created order information.The structure of the two lists are completely consistent.
        /// Check the rate limit instruction when category=linear here
        /// Risk control limit notice:
        /// Bybit will monitor on your API requests.When the total number of orders of a single user(aggregated the number of orders across main account and sub-accounts) within a day(UTC 0 - UTC 24) exceeds a certain upper limit, the platform will reserve the right to remind, warn, and impose necessary restrictions.Customers who use API default to acceptance of these terms and have the obligation to cooperate with adjustments.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="request"></param>
        /// <returns>List Order Result</returns>
        public async Task<string?> PlaceBatchOrder(Category category, List<OrderRequest> request)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                            { "request", request }
                        };
            var result = await this.SendSignedAsync<string>(BATCH_PLACE_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        private const string AMEND_ORDER = "/v5/order/amend";
        /// <summary>
        /// Amend Order
        ///Unified account covers: USDT perpetual / USDC contract / Inverse contract / Option
        ///Classic account covers: USDT perpetual / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="orderId"></param>
        /// <param name="orderLinkId"></param>
        /// <param name="orderIv"></param>
        /// <param name="triggerPrice"></param>
        /// <param name="qty"></param>
        /// <param name="price"></param>
        /// <param name="takeProfit"></param>
        /// <param name="orderLinkId"></param>
        /// <param name="takeProfit"></param>
        /// <param name="stopLoss"></param>
        /// <param name="tpTriggerBy"></param>
        /// <param name="slTriggerBy"></param>
        /// <param name="triggerBy"></param>
        /// <param name="tpLimitPrice"></param>
        /// <param name="slLimitPrice"></param>
        /// <returns>Order result</returns>
        public async Task<string?> AmendOrder(Category category, string symbol, string? orderId = null, string? orderLinkId = null, string? orderIv = null, string? qty = null, string? price = null,
                                               string? triggerPrice = null, TriggerBy? triggerBy = null, string? takeProfit = null, string? stopLoss = null, TriggerBy? tpTriggerBy = null, TriggerBy? slTriggerBy = null,
                                                string? tpLimitPrice = null, string? slLimitPrice = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                            { "symbol", symbol },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("price", price),
                ("qty", qty),
                ("orderLinkId", orderLinkId),
                ("orderIv", orderIv),
                ("triggerPrice", triggerPrice),
                ("triggerBy", triggerBy?.Value),
                ("takeProfit", takeProfit),
                ("stopLoss", stopLoss),
                ("tpTriggerBy", tpTriggerBy?.Value),
                ("slTriggerBy", slTriggerBy?.Value),
                ("tpLimitPrice", tpLimitPrice),
                ("slLimitPrice", slLimitPrice)
            );
            var result = await this.SendSignedAsync<string>(AMEND_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        private const string BATCH_AMEND_ORDER = "/v5/order/amend-batch";
        /// <summary>
        /// Covers: Option (UTA, UTA Pro) / USDT Perpetual, UDSC Perpetual, USDC Futures (UTA Pro)
        /// This endpoint allows you to amend more than one open order in a single request.
        /// You can modify unfilled or partially filled orders.Conditional orders are not supported.
        /// A maximum of 20 orders (option) and 10 orders (linear) can be amended per request.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="request"></param>
        /// <returns>List Order Result</returns>
        public async Task<string?> AmendBatchOrder(Category category, List<Dictionary<string, object>> request)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category.Value },
                { "request", request }
            };
            var result = await this.SendSignedAsync<string>(BATCH_AMEND_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        /// <summary>
        /// Covers: Option (UTA, UTA Pro) / USDT Perpetual, UDSC Perpetual, USDC Futures (UTA Pro)
        /// This endpoint allows you to amend more than one open order in a single request.
        /// You can modify unfilled or partially filled orders.Conditional orders are not supported.
        /// A maximum of 20 orders (option) & 10 orders (linear) can be amended per request.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="request"></param>
        /// <returns>List Order Result</returns>
        public async Task<string?> AmendBatchOrder(Category category, List<OrderRequest> request)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                            { "request", request }
                        };
            var result = await this.SendSignedAsync<string>(BATCH_AMEND_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        private const string CANCEL_ORDER = "/v5/order/cancel";
        /// <summary>
        /// Amend Order
        ///Unified account covers: USDT perpetual / USDC contract / Inverse contract / Option
        ///Classic account covers: USDT perpetual / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="orderId"></param>
        /// <param name="orderLinkId"></param>
        /// <param name="orderFilter"></param>
        /// <returns>Order result</returns>
        public async Task<string?> CancelOrder(Category category, string symbol, string? orderId = null, string? orderLinkId = null, string? orderFilter = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                            { "symbol", symbol },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("orderFilter", orderFilter)
            );
            var result = await this.SendSignedAsync<string>(CANCEL_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        private const string BATCH_CANCEL_ORDER = "/v5/order/cancel-batch";
        /// <summary>
        /// This endpoint allows you to cancel more than one open order in a single request.
        /// Covers: Option(UTA, UTA Pro) / USDT Perpetual, UDSC Perpetual, USDC Futures(UTA Pro)
        /// You must specify orderId or orderLinkId.
        /// If orderId and orderLinkId is not matched, the system will process orderId first.
        /// You can cancel unfilled or partially filled orders.
        /// A maximum of 20 orders (option) and 10 orders (linear) can be cancelled per request.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="request"></param>
        /// <returns>List Order Result</returns>
        public async Task<string?> CancelBatchOrder(Category category, List<Dictionary<string, object>> request)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category.Value },
                { "request", request }
            };
            var result = await this.SendSignedAsync<string>(BATCH_CANCEL_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        /// <summary>
        /// This endpoint allows you to cancel more than one open order in a single request.
        /// Covers: Option(UTA, UTA Pro) / USDT Perpetual, UDSC Perpetual, USDC Futures(UTA Pro)
        /// You must specify orderId or orderLinkId.
        /// If orderId and orderLinkId is not matched, the system will process orderId first.
        /// You can cancel unfilled or partially filled orders.
        /// A maximum of 20 orders (option) and 10 orders (linear) can be cancelled per request.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="request"></param>
        /// <returns>List Order Result</returns>
        public async Task<string?> CancelBatchOrder(Category category, List<OrderRequest> request)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                            { "request", request }
                        };
            var result = await this.SendSignedAsync<string>(BATCH_CANCEL_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        private const string REALTIME_ORDER = "/v5/order/realtime";
        /// <summary>
        /// Query unfilled or partially filled orders in real-time. To query older order records, please use the order history interface.
        /// Unified account covers: Spot / USDT perpetual / USDC contract / Inverse contract / Options
        /// Classic account covers: Spot / USDT perpetual / Inverse contract
        ///It also supports querying filled, cancelled, and rejected orders which occurred in last 10 minutes(check the openOnly param). At most, 500 orders will be returned.
        /// You can query by symbol, baseCoin, orderId and orderLinkId, and if you pass multiple params, the system will process them according to this priority: orderId > orderLinkId > symbol > baseCoin.
        /// The records are sorted by the createdTime from newest to oldest.
        /// Classic account spot trade can return open orders only
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="baseCoin"></param>
        /// <param name="settleCoin"></param>
        /// <param name="orderId"></param>
        /// <param name="orderLinkId"></param>
        /// <param name="openOnly"></param>
        /// <param name="orderFilter"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Get Open Orders</returns>
        public async Task<string?> GetOpenOrders(Category category, string? symbol = null, string? baseCoin = null, string? settleCoin = null, string? orderId = null, string? orderLinkId = null, int? openOnly = null, string? orderFilter = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("settleCoin", settleCoin),
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("openOnly", openOnly),
                ("orderFilter", orderFilter),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(REALTIME_ORDER, HttpMethod.Get, query: query);
            return result;
        }

        private const string CANCEL_ALL_ORDER = "/v5/order/cancel-all";
        /// <summary>
        /// Cancel all open orders
        /// Unified account covers: Spot / USDT perpetual / USDC contract / Inverse contract / Options
        /// Classic account covers: Spot / USDT perpetual / Inverse contract
        /// Support cancel orders by symbol/baseCoin/settleCoin.If you pass multiple of these params, the system will process one of param, which priority is symbol > baseCoin > settleCoin.
        /// NOTE: category=option, you can cancel all option open orders without passing any of those three params. However, for linear and inverse, you must specify one of those three params.
        /// NOTE: category=spot, you can cancel all spot open orders(normal order by default) without passing other params.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="baseCoin"></param>
        /// <param name="settleCoin"></param>
        /// <param name="orderFilter"></param>
        /// <param name="stopOrderType"></param>
        /// <returns>Orders result</returns>
        public async Task<string?> CancelAllOrder(Category category, string? symbol = null, string? baseCoin = null, string? settleCoin = null, string? orderFilter = null, StopOrderType? stopOrderType = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("settleCoin", settleCoin),
                ("stopOrderType", stopOrderType?.OrderType),
                ("orderFilter", orderFilter)
            );
            var result = await this.SendSignedAsync<string>(CANCEL_ALL_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        private const string ORDER_HISTORY = "/v5/order/history";
        /// <summary>
        /// Query order history. As order creation/cancellation is asynchronous, the data returned from this endpoint may delay. If you want to get real-time order information, you could query this endpoint or rely on the websocket stream (recommended).
        /// Unified account covers: Spot / USDT perpetual / USDC contract / Inverse contract / Options
        /// Classic account covers: Spot / USDT perpetual / Inverse contract
        /// The orders in the last 7 days: supports querying all statuses
        /// The orders beyond 7 days: supports querying filled orders
        /// You can query by symbol, baseCoin, orderId and orderLinkId, and if you pass multiple params, the system will process them according to this priority: orderId > orderLinkId > symbol > baseCoin.
        /// Classic account spot can get final status orders only
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="baseCoin"></param>
        /// <param name="settleCoin"></param>
        /// <param name="orderId"></param>
        /// <param name="orderLinkId"></param>
        /// <param name="orderStatus"></param>
        /// <param name="orderFilter"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Orders History</returns>
        public async Task<string?> GetOrdersHistory(Category category, string? symbol = null, string? baseCoin = null, string? settleCoin = null, string? orderId = null, string? orderLinkId = null, OrderStatus? orderStatus = null, string? orderFilter = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("settleCoin", settleCoin),
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("orderStatus", orderStatus?.Status),
                ("orderFilter", orderFilter),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(ORDER_HISTORY, HttpMethod.Get, query: query);
            return result;
        }

        private const string BORROW_QUOTA = "/v5/order/spot-borrow-check";
        /// <summary>
        /// Query the qty and amount of borrowable coins in spot account.  Covers: Spot(Unified Account)
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="side"></param>
        /// <returns>Spot Borrow Quota</returns>
        public async Task<string?> GetSpotBorrowQuota(Category category, string symbol, Side? side = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                            { "symbol", symbol },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("side", side?.Value)
            );
            var result = await this.SendSignedAsync<string>(BORROW_QUOTA, HttpMethod.Get, query: query);
            return result;
        }

        private const string DISCONNECT_CANCELL_ALL = "/v5/order/disconnected-cancel-all";

        /// <summary>
        /// Set Disconnect Cancel All  Covers: Option(Unified Account)
        /// What is Disconnection Protect (DCP)?
        /// Based on the websocket private connection and heartbeat mechanism, Bybit provides disconnection protection function.The timing starts from the first disconnection. 
        /// If the Bybit server does not receive the reconnection from the client for more than 10 (default) seconds and resumes the heartbeat "ping", 
        /// then the client is in the state of "disconnection protect", all active option orders of the client will be cancelled automatically.
        /// If within 10 seconds, the client reconnects and resumes the heartbeat "ping", the timing will be reset and restarted at the next disconnection.
        /// If you need to turn it on/off, you can contact your client manager for consultation and application. The default time window is 10 seconds.
        /// Effective for options only.
        /// After the request is successfully sent, the system needs a certain time to take effect. It is recommended to query or set again after 10 seconds
        /// </summary>
        /// <param name="timeWindow"></param>
        /// <returns>None</returns>
        public async Task<string?> SetDisconnectCancelAll(int timeWindow)
        {
            var query = new Dictionary<string, object>
            {
                { "timeWindow", timeWindow },
            };

            var result = await this.SendSignedAsync<string>(DISCONNECT_CANCELL_ALL, HttpMethod.Post, query: query);
            return result;
        }

        private const string EXECUTION_LIST = "/v5/execution/list";
        /// <summary>
        /// Get Execution
        /// Query users' execution records, sorted by execTime in descending order. However, for Classic spot, they are sorted by execId in descending order.
        /// Unified account covers: Spot / USDT perpetual / USDC contract / Inverse contract / Options
        /// Classic account covers: Spot / USDT perpetual / Inverse contract
        /// Response items will have sorting issues When 'execTime' is the same.This issue is currently being optimized and will be released at the end of October. If you want to receive real-time execution information, Use the websocket stream (recommended).
        /// You may have multiple executions in a single order.
        /// You can query by symbol, baseCoin, orderId and orderLinkId, and if you pass multiple params, the system will process them according to this priority: orderId > orderLinkId > symbol > baseCoin.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="orderId"></param>
        /// <param name="orderLinkId"></param>
        /// <param name="baseCoin"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="execType"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns> Execution List </returns>
        public async Task<string?> GetTradeHistory(Category category, string? symbol = null, string? orderId = null, string? orderLinkId = null, string? baseCoin = null, long? startTime = null, long? endTime = null, ExecType? execType = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("baseCoin", baseCoin),
                ("startTime", startTime),
                 ("endTime", endTime),
                 ("execType", execType?.Value),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(EXECUTION_LIST, HttpMethod.Get, query: query);
            return result;
        }
    }
}
