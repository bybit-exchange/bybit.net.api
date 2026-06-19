using bybit.net.api.Models;
using bybit.net.api.Models.Account;
using bybit.net.api.Models.Position;
using bybit.net.api.Models.Position.Response;
using bybit.net.api.Models.Trade;
using bybit.net.api.Services;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Numerics;
using System;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitPositionService : BybitApiService
    {
        public BybitPositionService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitPositionService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string POSITION_LIST = "/v5/position/list";
        /// <summary>
        /// Query real-time position data, such as position size, cumulative realizedPNL.
        /// Unified account covers: USDT perpetual / USDC contract / Inverse contract / Options
        /// Classic account covers: USDT perpetual / Inverse contract
        /// Regarding inverse contracts,
        /// you can query all holding positions with "/v5/position/list?category=inverse";
        /// symbol parameter is supported to be passed with multiple symbols up to 10
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="baseCoin"></param>
        /// <param name="settleCoin"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Position Info</returns>
        public async Task<GeneralResponse<GetPositionInfoResult>?> GetPositionInfo(Category category, string? symbol = null, string? baseCoin = null, string? settleCoin = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("settleCoin", settleCoin),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<GeneralResponse<GetPositionInfoResult>>(POSITION_LIST, HttpMethod.Get, query: query);
            return result;
        }

        private const string SET_LEVERAGE = "/v5/position/set-leverage";
        /// <summary>
        /// Set the leverage
        /// Unified account covers: USDT perpetual / USDC contract / Inverse contract
        /// Classic account covers: USDT perpetual / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="buyLeverage"></param>
        /// <param name="sellLeverage"></param>
        /// <returns>None</returns>
        public async Task<GeneralResponse<object>?> SetPositionLeverage(Category category, string symbol, string buyLeverage, string sellLeverage)
        {
            var query = new Dictionary<string, object> { { "category", category.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("buyLeverage", buyLeverage),
                ("sellLeverage", sellLeverage)
            );
            var result = await this.SendSignedAsync<GeneralResponse<object>>(SET_LEVERAGE, HttpMethod.Post, query: query);
            return result;
        }

        private const string SWITCH_POSITION_MODE = "/v5/position/switch-mode";
        /// <summary>
        /// It supports to switch the position mode for USDT perpetual and Inverse futures. If you are in one-way Mode, you can only open one position on Buy or Sell side. If you are in hedge mode, you can open both Buy and Sell side positions simultaneously.
        /// Unified account covers: USDT perpetual / Inverse Futures
        ///Classic account covers: USDT perpetual / Inverse Futures
        /// Priority for configuration to take effect: symbol > coin > system default
        /// System default: one-way mode
        /// If the request is by coin(settleCoin), then all symbols based on this setteCoin that do not have position and open order will be batch switched, and new listed symbol based on this settleCoin will be the same mode you set.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="coin"></param>
        /// <param name="mode"></param>
        /// <returns>None</returns>
        public async Task<GeneralResponse<object>?> SwitchPositionMode(Category category, PositionMode mode, string? symbol = null, string? coin = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                 ("coin", coin),
                ("mode", mode.Value)
            );
            var result = await this.SendSignedAsync<GeneralResponse<object>>(SWITCH_POSITION_MODE, HttpMethod.Post, query: query);
            return result;
        }

        private const string SET_TRADING_STOP = "/v5/position/trading-stop";
        /// <summary>
        /// Set the take profit, stop loss or trailing stop for the position.
        /// Unified account covers: USDT perpetual / USDC contract / Inverse contract
        /// Classic account covers: USDT perpetual / Inverse contract
        /// New version of TP/SL function supports both holding entire position TP/SL orders and holding partial position TP/SL orders.
        /// Full position TP/SL orders: This API can be used to modify the parameters of existing TP/SL orders.
        /// Partial position TP/SL orders: This API can only add partial position TP/SL orders.
        /// Under the new version of Tp/SL function, when calling this API to perform one-sided take profit or stop loss modification on existing TP/SL orders on the holding position, it will cause the paired tp/sl orders to lose binding relationship. This means that when calling the cancel API through the tp/sl order ID, it will only cancel the corresponding one-sided take profit or stop loss order ID.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="positionIdx"></param>
        /// <param name="takeProfit"></param>
        /// <param name="stopLoss"></param>
        /// <param name="tpTriggerBy"></param>
        /// <param name="slTriggerBy"></param>
        /// <param name="activePrice"></param>
        /// <param name="trailingStop"></param>
        /// <param name="tpslMode"></param>
        /// <param name="tpSize"></param>
        /// <param name="slSize"></param>
        /// <param name="tpLimitPrice"></param>
        /// <param name="slLimitPrice"></param>
        /// <param name="tpOrderType"></param>
        /// <param name="slOrderType"></param>
        /// <returns>None</returns>
        public async Task<GeneralResponse<object>?> SetPositionTradingStop(Category category, string symbol, TpslMode tpslMode, PositionIndex positionIdx, string? takeProfit = null,
        string? stopLoss = null, TriggerBy? tpTriggerBy = null, TriggerBy? slTriggerBy = null, string? activePrice = null, string? trailingStop = null,
                                                string? tpSize = null, string? slSize = null, string? tpLimitPrice = null, string? slLimitPrice = null, OrderType? tpOrderType = null, OrderType? slOrderType = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value }, { "symbol", symbol }, { "tpslMode", tpslMode.Value }, { "positionIdx", positionIdx.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("takeProfit", takeProfit),
                ("stopLoss", stopLoss),
                ("tpTriggerBy", tpTriggerBy?.Value),
                ("slTriggerBy", slTriggerBy?.Value),
                ("activePrice", activePrice),
                ("trailingStop", trailingStop),
                 ("tpSize", tpSize),
                ("slSize", slSize),
                ("tpLimitPrice", tpLimitPrice),
                ("slLimitPrice", slLimitPrice),
                ("tpOrderType", tpOrderType?.Value),
                ("slOrderType", slOrderType?.Value)
            );
            var result = await this.SendSignedAsync<GeneralResponse<object>>(SET_TRADING_STOP, HttpMethod.Post, query: query);
            return result;
        }

        private const string SET_AUTO_ADD_MARGIN = "/v5/position/set-auto-add-margin";
        /// <summary>
        /// Turn on/off auto-add-margin for isolated margin position
        /// Unified account covers: USDT perpetual / USDC perpetual / USDC futures / Inverse contract
        /// Classic account covers: USDT perpetual / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="autoAddMargin"></param>
        /// <param name="positionIdx"></param>
        /// <returns>None</returns>
        public async Task<GeneralResponse<object>?> SetPositionAutoAddMargin(Category category, string symbol, AutoAddMargin autoAddMargin, PositionIndex? positionIdx = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value }, { "symbol", symbol }, { "autoAddMargin", autoAddMargin.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("positionIdx", positionIdx?.Value));
            var result = await this.SendSignedAsync<GeneralResponse<object>>(SET_AUTO_ADD_MARGIN, HttpMethod.Post, query: query);
            return result;
        }

        private const string UPDATE_MARGIN = "/v5/position/add-margin";
        /// <summary>
        /// Manually add or reduce margin for isolated margin position
        /// Unified account covers: USDT perpetual / USDC perpetual / USDC futures / Inverse contract
        /// Classic account covers: USDT perpetual / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="margin"></param>
        /// <param name="positionIdx"></param>
        /// <returns>Position Margin</returns>
        public async Task<GeneralResponse<PositionMarginResult>?> SetPositionUpdateMargin(Category category, string symbol, string margin, PositionIndex? positionIdx = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value }, { "symbol", symbol }, { "margin", margin }, };
            BybitParametersUtils.AddOptionalParameters(query,
               ("positionIdx", positionIdx?.Value));
            var result = await this.SendSignedAsync<GeneralResponse<PositionMarginResult>>(UPDATE_MARGIN, HttpMethod.Post, query: query);
            return result;
        }

        private const string CLOSE_PNL = "/v5/position/closed-pnl";
        /// <summary>
        /// Get Closed PnL
        /// Query user's closed profit and loss records. The results are sorted by createdTime in descending order.
        /// Unified account covers: USDT perpetual / USDC contract / Inverse contract
        /// Classic account covers: USDT perpetual / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Close PNL</returns>
        public async Task<GeneralResponse<GetClosedPnlResult>?> GetClosedPnl(Category category, string? symbol = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("startTime", startTime),
                 ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<GeneralResponse<GetClosedPnlResult>>(CLOSE_PNL, HttpMethod.Get, query: query);
            return result;
        }


        private const string CONFIRM_NEW_RISK_LIMIT = "/v5/position/confirm-pending-mmr";
        /// <summary>
        /// Confirm New Risk Limit
        /// It is only applicable when the user is marked as only reducing positions(please see the isReduceOnly field in the Get Position Info interface). After the user actively adjusts the risk level, this interface is called to try to calculate the adjusted risk level, and if it passes(retCode= 0), the system will remove the position reduceOnly mark.You are recommended to call Get Position Info to check isReduceOnly field.
        /// Unified account covers: USDT perpetual / USDC contract / Inverse contract
        /// Classic account covers: USDT perpetual / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <returns>None</returns>
        public async Task<GeneralResponse<object>?> ConfirmPositionRiskLimit(Category category, string symbol)
        {
            var query = new Dictionary<string, object> { { "category", category.Value }, { "symbol", symbol } };
            var result = await this.SendSignedAsync<GeneralResponse<object>>(CONFIRM_NEW_RISK_LIMIT, HttpMethod.Post, query: query);
            return result;
        }

        private const string MOVE_POSITION = "/v5/position/move-positions";
        /// <summary>
        /// You can move positions between sub-master, master-sub, or sub-sub UIDs when necessary
        /// Unified account covers: USDT perpetual / USDC contract / Spot / Option
        /// The endpoint can only be called by master UID api key
        /// UIDs must be the same master-sub account relationship
        /// The trades generated from move-position endpoint will not be displayed in the Recent Trade(Rest API & Websocket)
        /// There is no trading fee
        /// fromUid and toUid both should be Unified trading accounts, and they need to be one-way mode when moving the positions
        /// Please note that once executed, you will get execType = MovePosition entry from Get Trade History, Get Closed Pnl, and stream from Execution.
        /// </summary>
        /// <param name="fromUid"></param>
        /// <param name="toUid"></param>
        /// <returns>Move Position</returns>
        public async Task<GeneralResponse<MovePositionResult>?> MovePosition(string fromUid, string toUid, List<Dictionary<string, object>> list)
        {
            var query = new Dictionary<string, object> { { "fromUid", fromUid }, { "toUid", toUid }, { "list", list } };
            var result = await this.SendSignedAsync<GeneralResponse<MovePositionResult>>(MOVE_POSITION, HttpMethod.Post, query: query);
            return result;
        }

        /// <summary>
        /// You can move positions between sub-master, master-sub, or sub-sub UIDs when necessary
        /// Unified account covers: USDT perpetual / USDC contract / Spot / Option
        /// The endpoint can only be called by master UID api key
        /// UIDs must be the same master-sub account relationship
        /// The trades generated from move-position endpoint will not be displayed in the Recent Trade(Rest API & Websocket)
        /// There is no trading fee
        /// fromUid and toUid both should be Unified trading accounts, and they need to be one-way mode when moving the positions
        /// Please note that once executed, you will get execType = MovePosition entry from Get Trade History, Get Closed Pnl, and stream from Execution.
        /// </summary>
        /// <param name="fromUid"></param>
        /// <param name="toUid"></param>
        /// <returns>Move Position</returns>
        public async Task<GeneralResponse<MovePositionResult>?> MovePosition(string fromUid, string toUid, List<MovePositionRequest> list)
        {
            var query = new Dictionary<string, object> { { "fromUid", fromUid }, { "toUid", toUid }, { "list", list } };
            var result = await this.SendSignedAsync<GeneralResponse<MovePositionResult>>(MOVE_POSITION, HttpMethod.Post, query: query);
            return result;
        }

        private const string MOVE_POSITION_HISTORY = "/v5/position/move-history";
        /// <summary>
        /// You can query moved position data by master UID api key
        /// Unified account covers: USDT perpetual / USDC contract / Spot / Option
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="status"></param>
        /// <param name="blockTradeId"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Move Position History</returns>
        public async Task<GeneralResponse<GetMovePositionHistoryResult>?> GetMovePositionHistory(Category? category = null, string? symbol = null, long? startTime = null, long? endTime = null, string? status = null, string? blockTradeId = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("category", category?.Value),
                ("symbol", symbol),
                ("startTime", startTime),
                ("endTime", endTime),
                ("status", status),
                ("blockTradeId", blockTradeId),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<GeneralResponse<GetMovePositionHistoryResult>>(MOVE_POSITION_HISTORY, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_CLOSED_OPTIONS_POSITIONS = "/v5/position/get-closed-positions";

        /// <summary>
        /// Get Closed Options Positions
        /// Query user's closed options positions, sorted by closeTime in descending order.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public async Task<GeneralResponse<GetClosedOptionsPositionsResult>?> GetClosedOptionsPositions(Category category, string? symbol = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<GeneralResponse<GetClosedOptionsPositionsResult>>(GET_CLOSED_OPTIONS_POSITIONS, HttpMethod.Get, query: query);
            return result;
        }
    }
}
