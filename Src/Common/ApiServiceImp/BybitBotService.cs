using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitBotService : BybitApiService
    {
        public BybitBotService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitBotService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string VALIDATE_SPOT_GRID_INPUT = "/v5/grid/validate-input";
        private const string CREATE_SPOT_GRID_BOT = "/v5/grid/create-grid";
        private const string CLOSE_SPOT_GRID_BOT = "/v5/grid/close-grid";
        private const string GET_SPOT_GRID_BOT_DETAIL = "/v5/grid/query-grid-detail";

        private const string CREATE_DCA_BOT = "/v5/dca/create-bot";
        private const string CLOSE_DCA_BOT = "/v5/dca/close-bot";

        private const string CREATE_FUTURES_COMBO_BOT = "/v5/fcombobot/create";
        private const string GET_FUTURES_COMBO_BOT_LIMIT = "/v5/fcombobot/getlimit";
        private const string CLOSE_FUTURES_COMBO_BOT = "/v5/fcombobot/close";
        private const string GET_FUTURES_COMBO_BOT_DETAIL = "/v5/fcombobot/detail";

        private const string VALIDATE_FUTURES_GRID_INPUT = "/v5/fgridbot/validate";
        private const string CREATE_FUTURES_GRID_BOT = "/v5/fgridbot/create";
        private const string CLOSE_FUTURES_GRID_BOT = "/v5/fgridbot/close";
        private const string GET_FUTURES_GRID_BOT_DETAIL = "/v5/fgridbot/detail";

        private const string CREATE_FUTURES_MARTINGALE_BOT = "/v5/fmartingalebot/create";
        private const string GET_FUTURES_MARTINGALE_BOT_LIMIT = "/v5/fmartingalebot/getlimit";
        private const string CLOSE_FUTURES_MARTINGALE_BOT = "/v5/fmartingalebot/close";
        private const string GET_FUTURES_MARTINGALE_BOT_DETAIL = "/v5/fmartingalebot/detail";

        public async Task<string?> ValidateSpotGridInput(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(VALIDATE_SPOT_GRID_INPUT, parameters);
        }

        public async Task<string?> CreateSpotGridBot(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(CREATE_SPOT_GRID_BOT, parameters);
        }

        public async Task<string?> CloseSpotGridBot(string gridId, string closeMode)
        {
            var body = new Dictionary<string, object>
            {
                { "grid_id", gridId },
                { "close_mode", closeMode }
            };

            return await SendBotRequest(CLOSE_SPOT_GRID_BOT, body);
        }

        public async Task<string?> GetSpotGridBotDetail(string gridId)
        {
            return await SendBotRequest(GET_SPOT_GRID_BOT_DETAIL, new Dictionary<string, object> { { "grid_id", gridId } });
        }

        public async Task<string?> CreateDcaBot(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(CREATE_DCA_BOT, new Dictionary<string, object> { { "parameters", parameters } });
        }

        public async Task<string?> CloseDcaBot(string botId, string closeMode)
        {
            var body = new Dictionary<string, object>
            {
                { "bot_id", botId },
                { "close_mode", closeMode }
            };

            return await SendBotRequest(CLOSE_DCA_BOT, body);
        }

        public async Task<string?> CreateFuturesComboBot(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(CREATE_FUTURES_COMBO_BOT, parameters);
        }

        public async Task<string?> GetFuturesComboBotLimit(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(GET_FUTURES_COMBO_BOT_LIMIT, parameters);
        }

        public async Task<string?> CloseFuturesComboBot(string botId)
        {
            return await SendBotRequest(CLOSE_FUTURES_COMBO_BOT, new Dictionary<string, object> { { "bot_id", botId } });
        }

        public async Task<string?> GetFuturesComboBotDetail(string botId)
        {
            return await SendBotRequest(GET_FUTURES_COMBO_BOT_DETAIL, new Dictionary<string, object> { { "bot_id", botId } });
        }

        public async Task<string?> ValidateFuturesGridInput(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(VALIDATE_FUTURES_GRID_INPUT, parameters);
        }

        public async Task<string?> CreateFuturesGridBot(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(CREATE_FUTURES_GRID_BOT, parameters);
        }

        public async Task<string?> CloseFuturesGridBot(string botId)
        {
            return await SendBotRequest(CLOSE_FUTURES_GRID_BOT, new Dictionary<string, object> { { "bot_id", botId } });
        }

        public async Task<string?> GetFuturesGridBotDetail(string botId)
        {
            return await SendBotRequest(GET_FUTURES_GRID_BOT_DETAIL, new Dictionary<string, object> { { "bot_id", botId } });
        }

        public async Task<string?> CreateFuturesMartingaleBot(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(CREATE_FUTURES_MARTINGALE_BOT, parameters);
        }

        public async Task<string?> GetFuturesMartingaleBotLimit(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(GET_FUTURES_MARTINGALE_BOT_LIMIT, parameters);
        }

        public async Task<string?> CloseFuturesMartingaleBot(string botId)
        {
            return await SendBotRequest(CLOSE_FUTURES_MARTINGALE_BOT, new Dictionary<string, object> { { "bot_id", botId } });
        }

        public async Task<string?> GetFuturesMartingaleBotDetail(string botId)
        {
            return await SendBotRequest(GET_FUTURES_MARTINGALE_BOT_DETAIL, new Dictionary<string, object> { { "bot_id", botId } });
        }

        private async Task<string?> SendBotRequest(string endpoint, Dictionary<string, object> body)
        {
            var result = await this.SendSignedAsync<string>(endpoint, HttpMethod.Post, query: body);
            return result;
        }

        /// <summary>
        /// Close a running DCA bot with a specified settlement mode
        /// </summary>
        /// <param name="botId">DCA bot ID</param>
        /// <param name="closeMode">Settlement mode for closing the bot</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/dca/close-bot"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> CloseDCABot(long botId, long closeMode)
        {
            var query = new Dictionary<string, object>
            {
                { "bot_id", botId },
                { "close_mode", closeMode }
            };
            var result = await this.SendSignedAsync<string>(CLOSE_DCA_BOT, HttpMethod.Post, query: query);
            return result;
        }

        /// <summary>
        /// Create a new DCA (Dollar-Cost Averaging) bot with custom parameters
        /// </summary>
        /// <param name="parameters">DCA bot configuration parameters</param>
        /// <param name="toolsDiscoveryParameter">Optional tools discovery parameter</param>
        /// <param name="channel">Optional channel identifier</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/dca/create-bot"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> CreateDCABot(Dictionary<string, object> parameters, Dictionary<string, object>? toolsDiscoveryParameter = null, string? channel = null)
        {
            var query = new Dictionary<string, object>
            {
                { "parameters", parameters }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("toolsDiscoveryParameter", toolsDiscoveryParameter),
                ("channel", channel)
            );
            var result = await this.SendSignedAsync<string>(CREATE_DCA_BOT, HttpMethod.Post, query: query);
            return result;
        }

        private const string CLOSE_COMBO_BOT = "/v5/fcombobot/close";
        /// <summary>
        /// Close a running futures combo bot by bot ID
        /// </summary>
        /// <param name="botId">Bot ID</param>
        /// <param name="stopType">Stop type</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fcombobot/close"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> CloseComboBot(long botId, long? stopType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "bot_id", botId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("stop_type", stopType)
            );
            var result = await this.SendSignedAsync<string>(CLOSE_COMBO_BOT, HttpMethod.Post, query: query);
            return result;
        }

        private const string CREATE_COMBO_BOT = "/v5/fcombobot/create";
        /// <summary>
        /// Create a new futures combo bot with multi-symbol portfolio and rebalancing
        /// </summary>
        /// <param name="leverage">Leverage</param>
        /// <param name="initMargin">Initial margin</param>
        /// <param name="adjustPositionMode">Adjust position mode</param>
        /// <param name="symbolSettings">Symbol settings</param>
        /// <param name="adjustPositionPercent">Adjust position percent</param>
        /// <param name="adjustPositionTimeInterval">Adjust position time interval</param>
        /// <param name="slPercent">Stop loss percent</param>
        /// <param name="tpPercent">Take profit percent</param>
        /// <param name="source">Source</param>
        /// <param name="blockSource">Block source</param>
        /// <param name="createType">Create type</param>
        /// <param name="followedBotId">Followed bot ID</param>
        /// <param name="initBonus">Initial bonus</param>
        /// <param name="trailingStopPercent">Trailing stop percent</param>
        /// <param name="channel">Channel</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fcombobot/create"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> CreateComboBot(string leverage, string initMargin, long adjustPositionMode, List<Dictionary<string, object>> symbolSettings, string? adjustPositionPercent = null, long? adjustPositionTimeInterval = null, string? slPercent = null, string? tpPercent = null, long? source = null, long? blockSource = null, long? createType = null, long? followedBotId = null, string? initBonus = null, string? trailingStopPercent = null, string? channel = null)
        {
            var query = new Dictionary<string, object>
            {
                { "leverage", leverage },
                { "init_margin", initMargin },
                { "adjust_position_mode", adjustPositionMode },
                { "symbol_settings", symbolSettings }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("adjust_position_percent", adjustPositionPercent),
                ("adjust_position_time_interval", adjustPositionTimeInterval),
                ("sl_percent", slPercent),
                ("tp_percent", tpPercent),
                ("source", source),
                ("block_source", blockSource),
                ("create_type", createType),
                ("followed_bot_id", followedBotId),
                ("init_bonus", initBonus),
                ("trailing_stop_percent", trailingStopPercent),
                ("channel", channel)
            );
            var result = await this.SendSignedAsync<string>(CREATE_COMBO_BOT, HttpMethod.Post, query: query);
            return result;
        }

        private const string GET_COMBO_DETAIL = "/v5/fcombobot/detail";
        /// <summary>
        /// Get full details of a futures combo bot including PnL, positions, and status
        /// </summary>
        /// <param name="botId">Bot ID</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fcombobot/detail"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetComboDetail(long botId)
        {
            var query = new Dictionary<string, object>
            {
                { "bot_id", botId }
            };
            var result = await this.SendSignedAsync<string>(GET_COMBO_DETAIL, HttpMethod.Post, query: query);
            return result;
        }

        private const string GET_COMBO_LIMIT = "/v5/fcombobot/getlimit";
        /// <summary>
        /// Validate combo bot input parameters and return allowable ranges
        /// </summary>
        /// <param name="leverage">Leverage</param>
        /// <param name="initMargin">Initial margin</param>
        /// <param name="adjustPositionMode">Adjust position mode</param>
        /// <param name="symbolSettings">Symbol settings</param>
        /// <param name="adjustPositionPercent">Adjust position percent</param>
        /// <param name="adjustPositionTimeInterval">Adjust position time interval</param>
        /// <param name="slPercent">Stop loss percent</param>
        /// <param name="tpPercent">Take profit percent</param>
        /// <param name="needToSlippage">Need to slippage</param>
        /// <param name="appName">App name</param>
        /// <param name="trailingStopPercent">Trailing stop percent</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fcombobot/getlimit"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetComboLimit(string leverage, string initMargin, long adjustPositionMode, List<Dictionary<string, object>> symbolSettings, string? adjustPositionPercent = null, long? adjustPositionTimeInterval = null, string? slPercent = null, string? tpPercent = null, bool? needToSlippage = null, string? appName = null, string? trailingStopPercent = null)
        {
            var query = new Dictionary<string, object>
            {
                { "leverage", leverage },
                { "init_margin", initMargin },
                { "adjust_position_mode", adjustPositionMode },
                { "symbol_settings", symbolSettings }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("adjust_position_percent", adjustPositionPercent),
                ("adjust_position_time_interval", adjustPositionTimeInterval),
                ("sl_percent", slPercent),
                ("tp_percent", tpPercent),
                ("need_to_slippage", needToSlippage),
                ("app_name", appName),
                ("trailing_stop_percent", trailingStopPercent)
            );
            var result = await this.SendSignedAsync<string>(GET_COMBO_LIMIT, HttpMethod.Post, query: query);
            return result;
        }

        private const string CLOSE_F_GRID_BOT = "/v5/fgridbot/close";
        /// <summary>
        /// Close a running futures grid bot by bot ID.
        /// </summary>
        /// <param name="botId">Futures grid bot ID to close.</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fgridbot/close"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> CloseFGridBot(long botId)
        {
            var query = new Dictionary<string, object>
            {
                { "bot_id", botId }
            };
            var result = await this.SendSignedAsync<string>(CLOSE_F_GRID_BOT, HttpMethod.Post, query: query);
            return result;
        }

        private const string CREATE_F_GRID_BOT = "/v5/fgridbot/create";
        /// <summary>
        /// Create a new futures grid trading bot with specified parameters.
        /// </summary>
        /// <param name="symbol">Trading symbol.</param>
        /// <param name="gridMode">Grid mode.</param>
        /// <param name="minPrice">Minimum price of the grid range.</param>
        /// <param name="maxPrice">Maximum price of the grid range.</param>
        /// <param name="cellNumber">Number of grid cells.</param>
        /// <param name="leverage">Leverage.</param>
        /// <param name="gridType">Grid type.</param>
        /// <param name="totalInvestment">Total investment amount.</param>
        /// <param name="takeProfitPer">Take profit percentage.</param>
        /// <param name="stopLossPer">Stop loss percentage.</param>
        /// <param name="entryPrice">Entry price.</param>
        /// <param name="source">Source flag.</param>
        /// <param name="followedGridId">Followed grid ID.</param>
        /// <param name="toolsDiscoveryParameter">Tools discovery parameter object.</param>
        /// <param name="stopLossPrice">Stop loss price.</param>
        /// <param name="takeProfitPrice">Take profit price.</param>
        /// <param name="tpSlType">Take profit / stop loss type.</param>
        /// <param name="blockSource">Block source.</param>
        /// <param name="createType">Create type.</param>
        /// <param name="initBonus">Initial bonus amount.</param>
        /// <param name="businessRemark">Business remark.</param>
        /// <param name="trailingStopPer">Trailing stop percentage.</param>
        /// <param name="moveUpPrice">Move up price.</param>
        /// <param name="moveDownPrice">Move down price.</param>
        /// <param name="channel">Channel identifier.</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fgridbot/create"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> CreateFGridBot(string symbol, long gridMode, string minPrice, string maxPrice, long cellNumber, string leverage, long gridType, string totalInvestment, string? takeProfitPer = null, string? stopLossPer = null, string? entryPrice = null, long? source = null, long? followedGridId = null, Dictionary<string, object>? toolsDiscoveryParameter = null, string? stopLossPrice = null, string? takeProfitPrice = null, long? tpSlType = null, long? blockSource = null, long? createType = null, string? initBonus = null, string? businessRemark = null, string? trailingStopPer = null, string? moveUpPrice = null, string? moveDownPrice = null, string? channel = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "grid_mode", gridMode },
                { "min_price", minPrice },
                { "max_price", maxPrice },
                { "cell_number", cellNumber },
                { "leverage", leverage },
                { "grid_type", gridType },
                { "total_investment", totalInvestment }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("take_profit_per", takeProfitPer),
                ("stop_loss_per", stopLossPer),
                ("entry_price", entryPrice),
                ("source", source),
                ("followed_grid_id", followedGridId),
                ("toolsDiscoveryParameter", toolsDiscoveryParameter),
                ("stop_loss_price", stopLossPrice),
                ("take_profit_price", takeProfitPrice),
                ("tp_sl_type", tpSlType),
                ("block_source", blockSource),
                ("create_type", createType),
                ("init_bonus", initBonus),
                ("business_remark", businessRemark),
                ("trailing_stop_per", trailingStopPer),
                ("move_up_price", moveUpPrice),
                ("move_down_price", moveDownPrice),
                ("channel", channel)
            );
            var result = await this.SendSignedAsync<string>(CREATE_F_GRID_BOT, HttpMethod.Post, query: query);
            return result;
        }

        private const string GET_F_GRID_DETAIL = "/v5/fgridbot/detail";
        /// <summary>
        /// Get full details of a futures grid bot including PnL, positions, and status.
        /// </summary>
        /// <param name="botId">Futures grid bot ID.</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fgridbot/detail"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetFGridDetail(long botId)
        {
            var query = new Dictionary<string, object>
            {
                { "bot_id", botId }
            };
            var result = await this.SendSignedAsync<string>(GET_F_GRID_DETAIL, HttpMethod.Post, query: query);
            return result;
        }

        private const string VALIDATE_F_GRID_INPUT = "/v5/fgridbot/validate";
        /// <summary>
        /// Validate futures grid bot input parameters and return allowable ranges.
        /// </summary>
        /// <param name="symbol">Trading symbol.</param>
        /// <param name="cellNumber">Number of grid cells.</param>
        /// <param name="minPrice">Minimum price of the grid range.</param>
        /// <param name="maxPrice">Maximum price of the grid range.</param>
        /// <param name="leverage">Leverage.</param>
        /// <param name="gridType">Grid type.</param>
        /// <param name="gridMode">Grid mode.</param>
        /// <param name="stopLossPrice">Stop loss price.</param>
        /// <param name="takeProfitPrice">Take profit price.</param>
        /// <param name="tpSlType">Take profit / stop loss type.</param>
        /// <param name="entryPrice">Entry price.</param>
        /// <param name="stopLossPer">Stop loss percentage.</param>
        /// <param name="takeProfitPer">Take profit percentage.</param>
        /// <param name="trailingStopPer">Trailing stop percentage.</param>
        /// <param name="initMargin">Initial margin.</param>
        /// <param name="moveUpPrice">Move up price.</param>
        /// <param name="moveDownPrice">Move down price.</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fgridbot/validate"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> ValidateFGridInput(string symbol, long cellNumber, string minPrice, string maxPrice, string leverage, long gridType, long gridMode, string? stopLossPrice = null, string? takeProfitPrice = null, long? tpSlType = null, string? entryPrice = null, string? stopLossPer = null, string? takeProfitPer = null, string? trailingStopPer = null, string? initMargin = null, string? moveUpPrice = null, string? moveDownPrice = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "cell_number", cellNumber },
                { "min_price", minPrice },
                { "max_price", maxPrice },
                { "leverage", leverage },
                { "grid_type", gridType },
                { "grid_mode", gridMode }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("stop_loss_price", stopLossPrice),
                ("take_profit_price", takeProfitPrice),
                ("tp_sl_type", tpSlType),
                ("entry_price", entryPrice),
                ("stop_loss_per", stopLossPer),
                ("take_profit_per", takeProfitPer),
                ("trailing_stop_per", trailingStopPer),
                ("init_margin", initMargin),
                ("move_up_price", moveUpPrice),
                ("move_down_price", moveDownPrice)
            );
            var result = await this.SendSignedAsync<string>(VALIDATE_F_GRID_INPUT, HttpMethod.Post, query: query);
            return result;
        }

        private const string CLOSE_F_MART_BOT = "/v5/fmartingalebot/close";
        /// <summary>
        /// Close a running futures Martingale bot by bot ID
        /// </summary>
        /// <param name="botId">Bot ID of the futures Martingale bot to close</param>
        /// <param name="stopType">Stop type for closing the bot</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fmartingalebot/close"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> CloseFMartBot(long botId, string? stopType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "bot_id", botId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("stop_type", stopType)
            );
            var result = await this.SendSignedAsync<string>(CLOSE_F_MART_BOT, HttpMethod.Post, query: query);
            return result;
        }

        private const string CREATE_F_MART_BOT = "/v5/fmartingalebot/create";
        /// <summary>
        /// Create a new futures Martingale bot with DCA averaging strategy
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="martingaleMode">Martingale mode</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="priceFloatPercent">Price float percent that triggers additional position</param>
        /// <param name="addPositionPercent">Percent increase for each additional position</param>
        /// <param name="addPositionNum">Number of additional positions allowed</param>
        /// <param name="initMargin">Initial margin</param>
        /// <param name="roundTpPercent">Round take-profit percent</param>
        /// <param name="autoCycleToggle">Auto-cycle toggle</param>
        /// <param name="slPercent">Stop-loss percent</param>
        /// <param name="entryPrice">Entry price</param>
        /// <param name="source">Source</param>
        /// <param name="followedBotId">Followed bot ID</param>
        /// <param name="blockSource">Block source</param>
        /// <param name="createType">Create type</param>
        /// <param name="initBonus">Initial bonus</param>
        /// <param name="channel">Channel</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fmartingalebot/create"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> CreateFMartBot(string symbol, string martingaleMode, string leverage, string priceFloatPercent, string addPositionPercent, long addPositionNum, string initMargin, string roundTpPercent, string? autoCycleToggle = null, string? slPercent = null, string? entryPrice = null, string? source = null, long? followedBotId = null, string? blockSource = null, string? createType = null, string? initBonus = null, string? channel = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "martingale_mode", martingaleMode },
                { "leverage", leverage },
                { "price_float_percent", priceFloatPercent },
                { "add_position_percent", addPositionPercent },
                { "add_position_num", addPositionNum },
                { "init_margin", initMargin },
                { "round_tp_percent", roundTpPercent }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("auto_cycle_toggle", autoCycleToggle),
                ("sl_percent", slPercent),
                ("entry_price", entryPrice),
                ("source", source),
                ("followed_bot_id", followedBotId),
                ("block_source", blockSource),
                ("create_type", createType),
                ("init_bonus", initBonus),
                ("channel", channel)
            );
            var result = await this.SendSignedAsync<string>(CREATE_F_MART_BOT, HttpMethod.Post, query: query);
            return result;
        }

        private const string GET_F_MART_DETAIL = "/v5/fmartingalebot/detail";
        /// <summary>
        /// Get full details of a futures Martingale bot including PnL, positions, and round progress
        /// </summary>
        /// <param name="botId">Bot ID of the futures Martingale bot</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fmartingalebot/detail"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetFMartDetail(long botId)
        {
            var query = new Dictionary<string, object>
            {
                { "bot_id", botId }
            };
            var result = await this.SendSignedAsync<string>(GET_F_MART_DETAIL, HttpMethod.Post, query: query);
            return result;
        }

        private const string GET_F_MART_LIMIT = "/v5/fmartingalebot/getlimit";
        /// <summary>
        /// Validate Martingale bot input parameters and return allowable ranges
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="martingaleMode">Martingale mode</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="priceFloatPercent">Price float percent</param>
        /// <param name="addPositionPercent">Add position percent</param>
        /// <param name="addPositionNum">Add position number</param>
        /// <param name="initMargin">Initial margin</param>
        /// <param name="roundTpPercent">Round take-profit percent</param>
        /// <param name="slPercent">Stop-loss percent</param>
        /// <param name="entryPrice">Entry price</param>
        /// <param name="needToSlippage">Whether to include slippage in validation</param>
        /// <param name="appName">App name</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/fmartingalebot/getlimit"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetFMartLimit(string symbol, string martingaleMode, string leverage, string? priceFloatPercent = null, string? addPositionPercent = null, long? addPositionNum = null, string? initMargin = null, string? roundTpPercent = null, string? slPercent = null, string? entryPrice = null, bool? needToSlippage = null, string? appName = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "martingale_mode", martingaleMode },
                { "leverage", leverage }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("price_float_percent", priceFloatPercent),
                ("add_position_percent", addPositionPercent),
                ("add_position_num", addPositionNum),
                ("init_margin", initMargin),
                ("round_tp_percent", roundTpPercent),
                ("sl_percent", slPercent),
                ("entry_price", entryPrice),
                ("need_to_slippage", needToSlippage),
                ("app_name", appName)
            );
            var result = await this.SendSignedAsync<string>(GET_F_MART_LIMIT, HttpMethod.Post, query: query);
            return result;
        }

        private const string CLOSE_GRID_BOT = "/v5/grid/close-grid";
        /// <summary>
        /// Close a running spot grid bot with a specified settlement mode.
        /// </summary>
        /// <param name="gridId">Grid bot identifier to close</param>
        /// <param name="closeMode">Settlement mode for closing the grid bot</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/grid/close-grid"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> CloseGridBot(long gridId, long closeMode)
        {
            var query = new Dictionary<string, object>
            {
                { "grid_id", gridId },
                { "close_mode", closeMode }
            };
            var result = await this.SendSignedAsync<string>(CLOSE_GRID_BOT, HttpMethod.Post, query: query);
            return result;
        }

        private const string CREATE_GRID_BOT = "/v5/grid/create-grid";
        /// <summary>
        /// Create a new spot grid trading bot.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="maxPrice">Upper bound price of the grid range</param>
        /// <param name="minPrice">Lower bound price of the grid range</param>
        /// <param name="totalInvestment">Total investment amount</param>
        /// <param name="cellNumber">Number of grid cells</param>
        /// <param name="followedGridId">ID of a grid to copy configuration from</param>
        /// <param name="source">Creation source identifier</param>
        /// <param name="entryPrice">Initial entry price</param>
        /// <param name="stopLossPrice">Stop-loss trigger price</param>
        /// <param name="takeProfitPrice">Take-profit trigger price</param>
        /// <param name="toolsDiscoveryParameter">Tools discovery parameter object</param>
        /// <param name="baseInvestment">Base asset investment amount</param>
        /// <param name="quoteInvestment">Quote asset investment amount</param>
        /// <param name="investMode">Investment mode</param>
        /// <param name="blockSource">Block source identifier</param>
        /// <param name="createType">Creation type identifier</param>
        /// <param name="tsPercent">Trailing stop percentage</param>
        /// <param name="enableTrailing">Whether trailing behavior is enabled</param>
        /// <param name="limitUpPrice">Upper limit trailing price</param>
        /// <param name="channel">Channel identifier</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/grid/create-grid"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> CreateGridBot(string symbol, string maxPrice, string minPrice, string totalInvestment, long cellNumber, long? followedGridId = null, long? source = null, string? entryPrice = null, string? stopLossPrice = null, string? takeProfitPrice = null, Dictionary<string, object>? toolsDiscoveryParameter = null, string? baseInvestment = null, string? quoteInvestment = null, long? investMode = null, long? blockSource = null, long? createType = null, string? tsPercent = null, bool? enableTrailing = null, string? limitUpPrice = null, string? channel = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "max_price", maxPrice },
                { "min_price", minPrice },
                { "total_investment", totalInvestment },
                { "cell_number", cellNumber }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("followed_grid_id", followedGridId),
                ("source", source),
                ("entry_price", entryPrice),
                ("stop_loss_price", stopLossPrice),
                ("take_profit_price", takeProfitPrice),
                ("toolsDiscoveryParameter", toolsDiscoveryParameter),
                ("base_investment", baseInvestment),
                ("quote_investment", quoteInvestment),
                ("invest_mode", investMode),
                ("block_source", blockSource),
                ("create_type", createType),
                ("ts_percent", tsPercent),
                ("enable_trailing", enableTrailing),
                ("limit_up_price", limitUpPrice),
                ("channel", channel)
            );
            var result = await this.SendSignedAsync<string>(CREATE_GRID_BOT, HttpMethod.Post, query: query);
            return result;
        }

        private const string QUERY_GRID_DETAIL = "/v5/grid/query-grid-detail";
        /// <summary>
        /// Query full details of a specific grid bot by grid_id.
        /// </summary>
        /// <param name="gridId">Grid bot identifier</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/grid/query-grid-detail"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> QueryGridDetail(long gridId)
        {
            var query = new Dictionary<string, object>
            {
                { "grid_id", gridId }
            };
            var result = await this.SendSignedAsync<string>(QUERY_GRID_DETAIL, HttpMethod.Post, query: query);
            return result;
        }

        private const string VALIDATE_GRID_INPUT = "/v5/grid/validate-input";
        /// <summary>
        /// Validate spot grid bot parameters before creation.
        /// </summary>
        /// <param name="symbol">Trading pair symbol</param>
        /// <param name="cellNumber">Number of grid cells</param>
        /// <param name="minPrice">Lower bound price of the grid range</param>
        /// <param name="maxPrice">Upper bound price of the grid range</param>
        /// <param name="totalInvestment">Total investment amount</param>
        /// <param name="stopLoss">Stop-loss price</param>
        /// <param name="takeProfit">Take-profit price</param>
        /// <param name="entryPrice">Initial entry price</param>
        /// <param name="baseInvestment">Base asset investment amount</param>
        /// <param name="quoteInvestment">Quote asset investment amount</param>
        /// <param name="investMode">Investment mode</param>
        /// <param name="tsPercent">Trailing stop percentage</param>
        /// <param name="enableTrailing">Whether trailing behavior is enabled</param>
        /// <param name="limitUpPrice">Upper limit trailing price</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/grid/validate-input"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> ValidateGridInput(string symbol, long cellNumber, string minPrice, string maxPrice, string totalInvestment, string? stopLoss = null, string? takeProfit = null, string? entryPrice = null, string? baseInvestment = null, string? quoteInvestment = null, long? investMode = null, string? tsPercent = null, bool? enableTrailing = null, string? limitUpPrice = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "cell_number", cellNumber },
                { "min_price", minPrice },
                { "max_price", maxPrice },
                { "total_investment", totalInvestment }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("stop_loss", stopLoss),
                ("take_profit", takeProfit),
                ("entry_price", entryPrice),
                ("base_investment", baseInvestment),
                ("quote_investment", quoteInvestment),
                ("invest_mode", investMode),
                ("ts_percent", tsPercent),
                ("enable_trailing", enableTrailing),
                ("limit_up_price", limitUpPrice)
            );
            var result = await this.SendSignedAsync<string>(VALIDATE_GRID_INPUT, HttpMethod.Post, query: query);
            return result;
        }
    }
}
