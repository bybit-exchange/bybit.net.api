using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitAlphaService : BybitApiService
    {
        public BybitAlphaService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitAlphaService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string GET_ALPHA_ASSET_LIST = "/v5/alpha/trade/asset-list";
        private const string GET_ALPHA_ASSET_DETAIL = "/v5/alpha/trade/asset-detail";
        private const string GET_ALPHA_BIZ_TOKEN_LIST = "/v5/alpha/trade/biz-token-list";
        private const string GET_ALPHA_BIZ_TOKEN_DETAILS = "/v5/alpha/trade/biz-token-details";
        private const string GET_ALPHA_BIZ_TOKEN_PRICE_LIST = "/v5/alpha/trade/biz-token-price-list";
        private const string GET_ALPHA_PAY_TOKEN_LIST = "/v5/alpha/trade/pay-token-list";
        private const string GET_ALPHA_QUOTE = "/v5/alpha/trade/quote";
        private const string PURCHASE_ALPHA_TOKEN = "/v5/alpha/trade/purchase";
        private const string REDEEM_ALPHA_TOKEN = "/v5/alpha/trade/redeem";
        private const string GET_ALPHA_ORDER_LIST = "/v5/alpha/trade/order-list";

        public async Task<string?> GetAlphaAssetList()
        {
            var body = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<string>(GET_ALPHA_ASSET_LIST, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<string?> GetAlphaAssetDetail(string chainCode, string tokenAddress)
        {
            var body = new Dictionary<string, object>
            {
                { "chainCode", chainCode },
                { "tokenAddress", tokenAddress }
            };

            var result = await this.SendSignedAsync<string>(GET_ALPHA_ASSET_DETAIL, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<string?> GetAlphaBizTokenList(int? tokenTag = null)
        {
            var body = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(body,
                ("tokenTag", tokenTag)
            );

            var result = await this.SendSignedAsync<string>(GET_ALPHA_BIZ_TOKEN_LIST, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<string?> GetAlphaBizTokenDetails(string chainCode, string tokenAddress)
        {
            var body = new Dictionary<string, object>
            {
                { "chainCode", chainCode },
                { "tokenAddress", tokenAddress }
            };

            var result = await this.SendSignedAsync<string>(GET_ALPHA_BIZ_TOKEN_DETAILS, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<string?> GetAlphaBizTokenPriceList(IEnumerable<object> tokenAddressInfo)
        {
            var body = new Dictionary<string, object>
            {
                { "tokenAddressInfo", tokenAddressInfo.ToArray() }
            };

            var result = await this.SendSignedAsync<string>(GET_ALPHA_BIZ_TOKEN_PRICE_LIST, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<string?> GetAlphaPayTokenList(string chainCode, string tokenAddress)
        {
            var body = new Dictionary<string, object>
            {
                { "chainCode", chainCode },
                { "tokenAddress", tokenAddress }
            };

            var result = await this.SendSignedAsync<string>(GET_ALPHA_PAY_TOKEN_LIST, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<string?> GetAlphaQuote(int tradeType, string fromTokenCode, string fromTokenAmount, string toTokenCode, int? quoteMode = null)
        {
            var body = new Dictionary<string, object>
            {
                { "tradeType", tradeType },
                { "fromTokenCode", fromTokenCode },
                { "fromTokenAmount", fromTokenAmount },
                { "toTokenCode", toTokenCode }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("quoteMode", quoteMode)
            );

            var result = await this.SendSignedAsync<string>(GET_ALPHA_QUOTE, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<string?> PurchaseAlphaToken(string fromTokenCode, string fromTokenAmount, string toTokenCode, string slippage, string quoteData, string gas, int quoteMode, string correctingCode, string? tenant = null)
        {
            var body = BuildAlphaTradeBody(fromTokenCode, fromTokenAmount, toTokenCode, slippage, quoteData, gas, quoteMode, correctingCode, tenant);
            var result = await this.SendSignedAsync<string>(PURCHASE_ALPHA_TOKEN, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<string?> RedeemAlphaToken(string fromTokenCode, string fromTokenAmount, string toTokenCode, string slippage, string quoteData, string gas, int quoteMode, string correctingCode, string? tenant = null)
        {
            var body = BuildAlphaTradeBody(fromTokenCode, fromTokenAmount, toTokenCode, slippage, quoteData, gas, quoteMode, correctingCode, tenant);
            var result = await this.SendSignedAsync<string>(REDEEM_ALPHA_TOKEN, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<string?> GetAlphaOrderList(int limit, int pageIndex, int? tradeType = null, string? tokenCode = null, IEnumerable<int>? orderStatus = null, int? days = null, string? direction = null)
        {
            var body = new Dictionary<string, object>
            {
                { "limit", limit },
                { "pageIndex", pageIndex }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("tradeType", tradeType),
                ("tokenCode", tokenCode),
                ("orderStatus", orderStatus?.ToArray()),
                ("days", days),
                ("direction", direction)
            );

            var result = await this.SendSignedAsync<string>(GET_ALPHA_ORDER_LIST, HttpMethod.Post, query: body);
            return result;
        }

        private static Dictionary<string, object> BuildAlphaTradeBody(string fromTokenCode, string fromTokenAmount, string toTokenCode, string slippage, string quoteData, string gas, int quoteMode, string correctingCode, string? tenant)
        {
            var body = new Dictionary<string, object>
            {
                { "fromTokenCode", fromTokenCode },
                { "fromTokenAmount", fromTokenAmount },
                { "toTokenCode", toTokenCode },
                { "slippage", slippage },
                { "quoteData", quoteData },
                { "gas", gas },
                { "quoteMode", quoteMode },
                { "correctingCode", correctingCode }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("tenant", tenant)
            );

            return body;
        }

    private const string GET_LPORDER_LIST = "/v5/alpha/lp/order-list";
    /// <summary>
    /// Get LP order history with status and execution details
    /// </summary>
    public async Task<string?> GetLPOrderList(long? orderType = null, string? tokenCode = null, List<object>? orderStatus = null, long? days = null, long? limit = null, long? pageIndex = null, string? poolAddress = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("orderType", orderType),
            ("tokenCode", tokenCode),
            ("orderStatus", orderStatus),
            ("days", days),
            ("limit", limit),
            ("pageIndex", pageIndex),
            ("poolAddress", poolAddress)
        );
        var result = await this.SendSignedAsync<string>(GET_LPORDER_LIST, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_LPPAY_TOKEN_LIST = "/v5/alpha/lp/pay-token-list";
    /// <summary>
    /// Get list of supported payment tokens
    /// </summary>
    public async Task<string?> GetLPPayTokenList(string? chainCode = null, string? tokenAddress = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("chainCode", chainCode),
            ("tokenAddress", tokenAddress)
        );
        var result = await this.SendSignedAsync<string>(GET_LPPAY_TOKEN_LIST, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_LPPAY_TOKEN_PRICE = "/v5/alpha/lp/pay-token-price";
    /// <summary>
    /// Get current prices for payment tokens
    /// </summary>
    public async Task<string?> GetLPPayTokenPrice(List<object> tokenCode, string? chainCode = null)
    {
        var query = new Dictionary<string, object>
        {
            { "tokenCode", tokenCode }
        };
        BybitParametersUtils.AddOptionalParameters(query,
            ("chainCode", chainCode)
        );
        var result = await this.SendSignedAsync<string>(GET_LPPAY_TOKEN_PRICE, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_LPPOOL_INFO = "/v5/alpha/lp/pool-info";
    /// <summary>
    /// Get detailed information for a specific LP pool
    /// </summary>
    public async Task<string?> GetLPPoolInfo(string poolAddress)
    {
        var query = new Dictionary<string, object>
        {
            { "poolAddress", poolAddress }
        };
        var result = await this.SendSignedAsync<string>(GET_LPPOOL_INFO, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_LPPOOL_LIST = "/v5/alpha/lp/pool-list";
    /// <summary>
    /// Get LP pool list with optional filters
    /// </summary>
    public async Task<string?> GetLPPoolList(string? tokenSymbol = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("tokenSymbol", tokenSymbol)
        );
        var result = await this.SendSignedAsync<string>(GET_LPPOOL_LIST, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_LPPOSITION_LIST = "/v5/alpha/lp/position-list";
    /// <summary>
    /// Get user&#x27;s LP positions with rewards and current value
    /// </summary>
    public async Task<string?> GetLPPositionList()
    {
        var result = await this.SendSignedAsync<string>(GET_LPPOSITION_LIST, HttpMethod.Post);
        return result;
    }
    private const string EXECUTE_LPREDEEM = "/v5/alpha/lp/redeem";
    /// <summary>
    /// Redeem (withdraw) liquidity from a position
    /// </summary>
    public async Task<string?> ExecuteLPRedeem(long positionId, string poolAddress, string dercRatio, string? receiveTokenCode = null)
    {
        var query = new Dictionary<string, object>
        {
            { "positionId", positionId },
            { "poolAddress", poolAddress },
            { "dercRatio", dercRatio }
        };
        BybitParametersUtils.AddOptionalParameters(query,
            ("receiveTokenCode", receiveTokenCode)
        );
        var result = await this.SendSignedAsync<string>(EXECUTE_LPREDEEM, HttpMethod.Post, query: query);
        return result;
    }
    private const string EXECUTE_LPSTAKE = "/v5/alpha/lp/stake";
    /// <summary>
    /// Stake tokens into an LP pool
    /// </summary>
    public async Task<string?> ExecuteLPStake(long positionId, string poolAddress, string payTokenAmount, string payTokenCode, string? rangeUpper = null, string? rangeLower = null, string? priceUpper = null, string? priceLower = null)
    {
        var query = new Dictionary<string, object>
        {
            { "positionId", positionId },
            { "poolAddress", poolAddress },
            { "payTokenAmount", payTokenAmount },
            { "payTokenCode", payTokenCode }
        };
        BybitParametersUtils.AddOptionalParameters(query,
            ("rangeUpper", rangeUpper),
            ("rangeLower", rangeLower),
            ("priceUpper", priceUpper),
            ("priceLower", priceLower)
        );
        var result = await this.SendSignedAsync<string>(EXECUTE_LPSTAKE, HttpMethod.Post, query: query);
        return result;
    }
    private const string EXECUTE_PREDICTION_BUY = "/v5/alpha/prediction/buy";
    /// <summary>
    /// Buy prediction outcome tokens with USDC
    /// </summary>
    public async Task<string?> ExecutePredictionBuy(string tokenId, string amount, string payTokenCode, int orderType, string slippage, string eventId)
    {
        var query = new Dictionary<string, object>
        {
            { "tokenId", tokenId },
            { "amount", amount },
            { "payTokenCode", payTokenCode },
            { "orderType", orderType },
            { "slippage", slippage },
            { "eventId", eventId }
        };
        var result = await this.SendSignedAsync<string>(EXECUTE_PREDICTION_BUY, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_ENGINE_STATUS = "/v5/alpha/prediction/engine-status";
    /// <summary>
    /// Get prediction market matching engine availability status
    /// </summary>
    public async Task<string?> GetPredictionEngineStatus()
    {
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_ENGINE_STATUS, HttpMethod.Get);
        return result;
    }
    private const string GET_PREDICTION_EVENT_DETAIL = "/v5/alpha/prediction/event-detail";
    /// <summary>
    /// Get prediction event detail with all markets and outcomes
    /// </summary>
    public async Task<string?> GetPredictionEventDetail(string? eventId = null, string? slug = null, bool? hasMoreMarkets = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("eventId", eventId),
            ("slug", slug),
            ("hasMoreMarkets", hasMoreMarkets)
        );
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_EVENT_DETAIL, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_ORDER_BOOK = "/v5/alpha/prediction/order-book";
    /// <summary>
    /// Get full order book snapshot for prediction tokens
    /// </summary>
    public async Task<string?> GetPredictionOrderBook(List<object> tokenIds)
    {
        var query = new Dictionary<string, object>
        {
            { "tokenIds", tokenIds }
        };
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_ORDER_BOOK, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_ORDER_ESTIMATE = "/v5/alpha/prediction/order-estimate";
    /// <summary>
    /// Estimate buy or sell order fill amount and fees before execution
    /// </summary>
    public async Task<string?> GetPredictionOrderEstimate(string tokenId, int side, string eventId, string amount, int orderType, string? payTokenCode = null)
    {
        var query = new Dictionary<string, object>
        {
            { "tokenId", tokenId },
            { "side", side },
            { "eventId", eventId },
            { "amount", amount },
            { "orderType", orderType }
        };
        BybitParametersUtils.AddOptionalParameters(query,
            ("payTokenCode", payTokenCode)
        );
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_ORDER_ESTIMATE, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_ORDER_LIST = "/v5/alpha/prediction/order-list";
    /// <summary>
    /// Get user&#x27;s prediction market order history
    /// </summary>
    public async Task<string?> GetPredictionOrderList(int? status = null, string? tokenId = null, string? eventId = null, int? side = null, int? days = null, int? limit = null, int? pageIndex = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("status", status),
            ("tokenId", tokenId),
            ("eventId", eventId),
            ("side", side),
            ("days", days),
            ("limit", limit),
            ("pageIndex", pageIndex)
        );
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_ORDER_LIST, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_PAY_TOKEN_LIST = "/v5/alpha/prediction/pay-token-list";
    /// <summary>
    /// Get available payment tokens (USDC) for prediction market
    /// </summary>
    public async Task<string?> GetPredictionPayTokenList()
    {
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_PAY_TOKEN_LIST, HttpMethod.Get);
        return result;
    }
    private const string GET_PREDICTION_PORTFOLIO_SUMMARY = "/v5/alpha/prediction/portfolio-summary";
    /// <summary>
    /// Get user&#x27;s prediction market portfolio overview
    /// </summary>
    public async Task<string?> GetPredictionPortfolioSummary(int? eventType = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("eventType", eventType)
        );
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_PORTFOLIO_SUMMARY, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_POSITION_HISTORY = "/v5/alpha/prediction/position-history";
    /// <summary>
    /// Get user&#x27;s historical closed prediction market positions
    /// </summary>
    public async Task<string?> GetPredictionPositionHistory(string? tokenId = null, string? eventId = null, int? result = null, int? days = null, int? limit = null, int? pageIndex = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("tokenId", tokenId),
            ("eventId", eventId),
            ("result", result),
            ("days", days),
            ("limit", limit),
            ("pageIndex", pageIndex)
        );
        var response = await this.SendSignedAsync<string>(GET_PREDICTION_POSITION_HISTORY, HttpMethod.Post, query: query);
        return response;
    }
    private const string GET_PREDICTION_POSITION_LIST = "/v5/alpha/prediction/position-list";
    /// <summary>
    /// Get user&#x27;s current open prediction market positions
    /// </summary>
    public async Task<string?> GetPredictionPositionList(string? tokenId = null, string? eventId = null, int? limit = null, int? pageIndex = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("tokenId", tokenId),
            ("eventId", eventId),
            ("limit", limit),
            ("pageIndex", pageIndex)
        );
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_POSITION_LIST, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_PRICE_HISTORY = "/v5/alpha/prediction/price-history";
    /// <summary>
    /// Get historical price chart data for prediction tokens
    /// </summary>
    public async Task<string?> GetPredictionPriceHistory(string tokenId, string interval)
    {
        var query = new Dictionary<string, object>
        {
            { "tokenId", tokenId },
            { "interval", interval }
        };
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_PRICE_HISTORY, HttpMethod.Post, query: query);
        return result;
    }
    private const string EXECUTE_PREDICTION_SELL = "/v5/alpha/prediction/sell";
    /// <summary>
    /// Sell prediction outcome token shares for USDC
    /// </summary>
    public async Task<string?> ExecutePredictionSell(string tokenId, string size, int orderType, string slippage, string eventId, string? toTokenCode = null)
    {
        var query = new Dictionary<string, object>
        {
            { "tokenId", tokenId },
            { "size", size },
            { "orderType", orderType },
            { "slippage", slippage },
            { "eventId", eventId }
        };
        BybitParametersUtils.AddOptionalParameters(query,
            ("toTokenCode", toTokenCode)
        );
        var result = await this.SendSignedAsync<string>(EXECUTE_PREDICTION_SELL, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_SIDE_MARKET_LIST = "/v5/alpha/prediction/side-market-list";
    /// <summary>
    /// Get related side markets for a sports event
    /// </summary>
    public async Task<string?> GetPredictionSideMarketList(string eventId, string? sortBy = null, int? marketType = null, int? limit = null, int? pageIndex = null)
    {
        var query = new Dictionary<string, object>
        {
            { "eventId", eventId }
        };
        BybitParametersUtils.AddOptionalParameters(query,
            ("sortBy", sortBy),
            ("marketType", marketType),
            ("limit", limit),
            ("pageIndex", pageIndex)
        );
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_SIDE_MARKET_LIST, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_GROUP_STAGE_DETAIL = "/v5/alpha/prediction/sports/group-stage-detail";
    /// <summary>
    /// Get group standings and results for a tournament stage
    /// </summary>
    public async Task<string?> GetPredictionGroupStageDetail(int eventType, string? groupName = null)
    {
        var query = new Dictionary<string, object>
        {
            { "eventType", eventType }
        };
        BybitParametersUtils.AddOptionalParameters(query,
            ("groupName", groupName)
        );
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_GROUP_STAGE_DETAIL, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_MATCH_LIST = "/v5/alpha/prediction/sports/match-list";
    /// <summary>
    /// Get match list for a sports prediction event
    /// </summary>
    public async Task<string?> GetPredictionMatchList(int eventType, int? status = null, long? startTime = null, long? endTime = null, string? stageCode = null, int? limit = null, int? pageIndex = null)
    {
        var query = new Dictionary<string, object>
        {
            { "eventType", eventType }
        };
        BybitParametersUtils.AddOptionalParameters(query,
            ("status", status),
            ("startTime", startTime),
            ("endTime", endTime),
            ("stageCode", stageCode),
            ("limit", limit),
            ("pageIndex", pageIndex)
        );
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_MATCH_LIST, HttpMethod.Post, query: query);
        return result;
    }
    private const string GET_PREDICTION_TIMELINE_STAGES = "/v5/alpha/prediction/sports/timeline-stages";
    /// <summary>
    /// Get tournament timeline stages for a sports event
    /// </summary>
    public async Task<string?> GetPredictionTimelineStages(int eventType)
    {
        var query = new Dictionary<string, object>
        {
            { "eventType", eventType }
        };
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_TIMELINE_STAGES, HttpMethod.Get, query: query);
        return result;
    }
    private const string GET_PREDICTION_TOKEN_PRICE = "/v5/alpha/prediction/token-price";
    /// <summary>
    /// Get real-time prices for prediction outcome tokens
    /// </summary>
    public async Task<string?> GetPredictionTokenPrice(List<object> tokenIds)
    {
        var query = new Dictionary<string, object>
        {
            { "tokenIds", tokenIds }
        };
        var result = await this.SendSignedAsync<string>(GET_PREDICTION_TOKEN_PRICE, HttpMethod.Post, query: query);
        return result;
    }
    }
}
