using bybit.net.api.Models;
using bybit.net.api.Models.Earn;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitEarnService : BybitApiService
    {
        public BybitEarnService(string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitEarnService(HttpClient httpClient, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitEarnService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitEarnService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string GET_PRODUCT_INFO = "/v5/earn/product";
        private const string PLACE_EARN_ORDER = "/v5/earn/place-order";
        private const string GET_EARN_ORDER_HISTORY = "/v5/earn/order";
        private const string GET_STAKED_POSITION = "/v5/earn/position";
        private const string GET_EARN_APR_HISTORY = "/v5/earn/apr-history";
        private const string GET_EARN_HOURLY_YIELD = "/v5/earn/hourly-yield";
        private const string GET_EARN_YIELD_HISTORY = "/v5/earn/yield";
        private const string MODIFY_EARN_POSITION = "/v5/earn/position/modify";

        private const string GET_FIXED_TERM_PRODUCT = "/v5/earn/fixed-term/product";
        private const string PLACE_FIXED_TERM_ORDER = "/v5/earn/fixed-term/place-order";
        private const string GET_FIXED_TERM_ORDER = "/v5/earn/fixed-term/order";
        private const string GET_FIXED_TERM_POSITION = "/v5/earn/fixed-term/position";
        private const string REDEEM_FIXED_TERM = "/v5/earn/fixed-term/redeem";
        private const string SET_FIXED_TERM_AUTO_INVEST = "/v5/earn/fixed-term/position/auto-invest";

        private const string GET_TOKEN_PRODUCT = "/v5/earn/token/product";
        private const string PLACE_TOKEN_ORDER = "/v5/earn/token/place-order";
        private const string GET_TOKEN_ORDER = "/v5/earn/token/order";
        private const string GET_TOKEN_POSITION = "/v5/earn/token/position";
        private const string GET_TOKEN_YIELD = "/v5/earn/token/yield";
        private const string GET_TOKEN_HOURLY_YIELD = "/v5/earn/token/hourly-yield";
        private const string GET_TOKEN_HISTORY_APR = "/v5/earn/token/history-apr";

        public async Task<GeneralResponse<GetProductInfoResult>?> GetProductInfo(string category, string? coin = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };
            BybitParametersUtils.AddOptionalParameters(query, ("coin", coin));

            var result = await this.SendPublicAsync<GeneralResponse<GetProductInfoResult>>(GET_PRODUCT_INFO, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<PlaceEarnOrderResult>?> PlaceEarnOrder(
            string category,
            string orderType,
            string accountType,
            string amount,
            string coin,
            string productId,
            string orderLinkId,
            string? redeemPositionId = null,
            string? toAccountType = null)
        {
            var body = new Dictionary<string, object>
            {
                { "category", category },
                { "orderType", orderType },
                { "accountType", accountType },
                { "amount", amount },
                { "coin", coin },
                { "productId", productId },
                { "orderLinkId", orderLinkId }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("redeemPositionId", redeemPositionId),
                ("toAccountType", toAccountType)
            );

            var result = await this.SendSignedAsync<GeneralResponse<PlaceEarnOrderResult>>(PLACE_EARN_ORDER, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<GeneralResponse<GetStakeRedeemOrderHistoryResult>?> GetEarnOrderHistory(
            string category,
            string? orderId = null,
            string? orderLinkId = null,
            string? productId = null,
            long? startTime = null,
            long? endTime = null,
            int? limit = null,
            string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("productId", productId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetStakeRedeemOrderHistoryResult>>(GET_EARN_ORDER_HISTORY, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetStakedPositionResult>?> GetStakedPosition(string category, string? productId = null, string? coin = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId),
                ("coin", coin)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetStakedPositionResult>>(GET_STAKED_POSITION, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetEarnAprHistoryResult>?> GetEarnAprHistory(string category, string productId, long? startTime = null, long? endTime = null)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category },
                { "productId", productId }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime)
            );

            var result = await this.SendPublicAsync<GeneralResponse<GetEarnAprHistoryResult>>(GET_EARN_APR_HISTORY, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetEarnHourlyYieldResult>?> GetEarnHourlyYield(string category, string? productId = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetEarnHourlyYieldResult>>(GET_EARN_HOURLY_YIELD, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetEarnYieldHistoryResult>?> GetEarnYieldHistory(string category, string? productId = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetEarnYieldHistoryResult>>(GET_EARN_YIELD_HISTORY, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<ModifyEarnPositionResult>?> ModifyEarnPosition(string category, int productId, int positionId, int autoReinvest)
        {
            var body = new Dictionary<string, object>
            {
                { "category", category },
                { "productId", productId },
                { "positionId", positionId },
                { "autoReinvest", autoReinvest }
            };

            var result = await this.SendSignedAsync<GeneralResponse<ModifyEarnPositionResult>>(MODIFY_EARN_POSITION, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<GeneralResponse<GetFixedTermProductInfoResult>?> GetFixedTermProductInfo(string? coin = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("coin", coin));

            var result = await this.SendPublicAsync<GeneralResponse<GetFixedTermProductInfoResult>>(GET_FIXED_TERM_PRODUCT, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<PlaceFixedTermOrderResult>?> PlaceFixedTermOrder(string productId, string category, string coin, string amount, string accountType, string orderLinkId, bool? autoInvest = null)
        {
            var body = new Dictionary<string, object>
            {
                { "productId", productId },
                { "category", category },
                { "coin", coin },
                { "amount", amount },
                { "accountType", accountType },
                { "orderLinkId", orderLinkId }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("autoInvest", autoInvest)
            );

            var result = await this.SendSignedAsync<GeneralResponse<PlaceFixedTermOrderResult>>(PLACE_FIXED_TERM_ORDER, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<GeneralResponse<GetFixedTermOrderListResult>?> GetFixedTermOrderList(string? orderType = null, string? productId = null, string? category = null, string? orderId = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("orderType", orderType),
                ("productId", productId),
                ("category", category),
                ("orderId", orderId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetFixedTermOrderListResult>>(GET_FIXED_TERM_ORDER, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetFixedTermPositionResult>?> GetFixedTermPosition(string? productId = null, string? category = null, string? coin = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId),
                ("category", category),
                ("coin", coin)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetFixedTermPositionResult>>(GET_FIXED_TERM_POSITION, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<RedeemFixedTermResult>?> RedeemFixedTerm(string productId, string category, string positionId)
        {
            var body = new Dictionary<string, object>
            {
                { "productId", productId },
                { "category", category },
                { "positionId", positionId }
            };

            var result = await this.SendSignedAsync<GeneralResponse<RedeemFixedTermResult>>(REDEEM_FIXED_TERM, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<GeneralResponse<SetFixedTermAutoInvestResult>?> SetFixedTermAutoInvest(string productId, string category, string positionId, string status)
        {
            var body = new Dictionary<string, object>
            {
                { "productId", productId },
                { "category", category },
                { "positionId", positionId },
                { "status", status }
            };

            var result = await this.SendSignedAsync<GeneralResponse<SetFixedTermAutoInvestResult>>(SET_FIXED_TERM_AUTO_INVEST, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<GeneralResponse<GetTokenProductResult>?> GetTokenProduct(string coin)
        {
            var query = new Dictionary<string, object> { { "coin", coin } };
            var result = await this.SendPublicAsync<GeneralResponse<GetTokenProductResult>>(GET_TOKEN_PRODUCT, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<PlaceTokenOrderResult>?> PlaceTokenOrder(string coin, string orderLinkId, string orderType, string amount, string accountType)
        {
            var body = new Dictionary<string, object>
            {
                { "coin", coin },
                { "orderLinkId", orderLinkId },
                { "orderType", orderType },
                { "amount", amount },
                { "accountType", accountType }
            };

            var result = await this.SendSignedAsync<GeneralResponse<PlaceTokenOrderResult>>(PLACE_TOKEN_ORDER, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<GeneralResponse<GetTokenOrderListResult>?> GetTokenOrderList(string coin, string? orderLinkId = null, string? orderId = null, string? orderType = null, long? startTime = null, long? endTime = null, string? cursor = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "coin", coin } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("orderLinkId", orderLinkId),
                ("orderId", orderId),
                ("orderType", orderType),
                ("startTime", startTime),
                ("endTime", endTime),
                ("cursor", cursor),
                ("limit", limit)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetTokenOrderListResult>>(GET_TOKEN_ORDER, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetTokenPositionResult>?> GetTokenPosition(string coin)
        {
            var query = new Dictionary<string, object> { { "coin", coin } };
            var result = await this.SendSignedAsync<GeneralResponse<GetTokenPositionResult>>(GET_TOKEN_POSITION, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetTokenYieldResult>?> GetTokenYield(string coin, long? startTime = null, long? endTime = null, string? cursor = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "coin", coin } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime),
                ("cursor", cursor),
                ("limit", limit)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetTokenYieldResult>>(GET_TOKEN_YIELD, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetTokenHourlyYieldResult>?> GetTokenHourlyYield(string coin, long? startTime = null, long? endTime = null, string? cursor = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "coin", coin } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime),
                ("cursor", cursor),
                ("limit", limit)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetTokenHourlyYieldResult>>(GET_TOKEN_HOURLY_YIELD, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetTokenHistoryAprResult>?> GetTokenHistoryApr(string coin, int range)
        {
            var query = new Dictionary<string, object>
            {
                { "coin", coin },
                { "range", range }
            };

            var result = await this.SendPublicAsync<GeneralResponse<GetTokenHistoryAprResult>>(GET_TOKEN_HISTORY_APR, HttpMethod.Get, query: query);
            return result;
        }

        private const string ADD_LIQUIDITY = "/v5/earn/liquidity-mining/add-liquidity";
        /// <summary>
        /// Add Liquidity to a liquidity mining product.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="orderLinkId">User-defined order link ID</param>
        /// <param name="quoteAccountType">Quote account type</param>
        /// <param name="baseAccountType">Base account type</param>
        /// <param name="quoteAmount">Quote amount</param>
        /// <param name="baseAmount">Base amount</param>
        /// <param name="leverage">Leverage</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/liquidity-mining/add-liquidity"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> AddLiquidity(string productId, string orderLinkId, string? quoteAccountType = null, string? baseAccountType = null, string? quoteAmount = null, string? baseAmount = null, string? leverage = null)
        {
            var query = new Dictionary<string, object>
            {
                { "productId", productId },
                { "orderLinkId", orderLinkId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("quoteAccountType", quoteAccountType),
                ("baseAccountType", baseAccountType),
                ("quoteAmount", quoteAmount),
                ("baseAmount", baseAmount),
                ("leverage", leverage)
            );
            var result = await this.SendSignedAsync<string>(ADD_LIQUIDITY, HttpMethod.Post, query: query);
            return result;
        }

        private const string ADD_MARGIN = "/v5/earn/liquidity-mining/add-margin";
        /// <summary>
        /// Add Margin to a liquidity mining position.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="orderLinkId">User-defined order link ID</param>
        /// <param name="positionId">Position ID</param>
        /// <param name="amount">Amount to add as margin</param>
        /// <param name="quoteAccountType">Quote account type</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/liquidity-mining/add-margin"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> AddMargin(string productId, string orderLinkId, string positionId, string amount, string quoteAccountType)
        {
            var query = new Dictionary<string, object>
            {
                { "productId", productId },
                { "orderLinkId", orderLinkId },
                { "positionId", positionId },
                { "amount", amount },
                { "quoteAccountType", quoteAccountType }
            };
            var result = await this.SendSignedAsync<string>(ADD_MARGIN, HttpMethod.Post, query: query);
            return result;
        }

        private const string CLAIM_LIQUIDITY_INTEREST = "/v5/earn/liquidity-mining/claim-interest";
        /// <summary>
        /// Claim Interest earned from a liquidity mining product.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/liquidity-mining/claim-interest"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> ClaimLiquidityInterest(string productId)
        {
            var query = new Dictionary<string, object>
            {
                { "productId", productId }
            };
            var result = await this.SendSignedAsync<string>(CLAIM_LIQUIDITY_INTEREST, HttpMethod.Post, query: query);
            return result;
        }

        private const string GET_ADVANCE_EARN_ORDER = "/v5/earn/advance/order";
        /// <summary>
        /// Get advance earn orders.
        /// </summary>
        /// <param name="category">Product category</param>
        /// <param name="productId">Product ID</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="orderLinkId">Client order link ID</param>
        /// <param name="startTime">Start timestamp in ms</param>
        /// <param name="endTime">End timestamp in ms</param>
        /// <param name="limit">Page size</param>
        /// <param name="cursor">Pagination cursor</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/advance/order"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetAdvanceEarnOrder(string category, long? productId = null, string? orderId = null, string? orderLinkId = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId),
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_ADVANCE_EARN_ORDER, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_ADVANCE_EARN_POSITION = "/v5/earn/advance/position";
        /// <summary>
        /// Get advance earn positions.
        /// </summary>
        /// <param name="category">Product category</param>
        /// <param name="productId">Product ID</param>
        /// <param name="coin">Coin name</param>
        /// <param name="limit">Page size</param>
        /// <param name="cursor">Pagination cursor</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/advance/position"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetAdvanceEarnPosition(string category, long? productId = null, string? coin = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId),
                ("coin", coin),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_ADVANCE_EARN_POSITION, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_ADVANCE_EARN_PRODUCT = "/v5/earn/advance/product";
        /// <summary>
        /// Get advance earn product info.
        /// </summary>
        /// <param name="category">Product category</param>
        /// <param name="coin">Coin name</param>
        /// <param name="duration">Product duration</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/advance/product"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetAdvanceEarnProduct(string category, string? coin = null, string? duration = null)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("duration", duration)
            );
            var result = await this.SendPublicAsync<string>(GET_ADVANCE_EARN_PRODUCT, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_ADVANCE_EARN_PRODUCT_EXTRA_INFO = "/v5/earn/advance/product-extra-info";
        /// <summary>
        /// Get advance earn product extra info.
        /// </summary>
        /// <param name="category">Product category</param>
        /// <param name="productId">Product ID</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/advance/product-extra-info"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetAdvanceEarnProductExtraInfo(string category, long? productId = null)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId)
            );
            var result = await this.SendPublicAsync<string>(GET_ADVANCE_EARN_PRODUCT_EXTRA_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_DOUBLE_WIN_LEVERAGE = "/v5/earn/advance/double-win-leverage";
        /// <summary>
        /// Get double win leverage for a product with the given price range.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="initialPrice">Initial price</param>
        /// <param name="lowerPrice">Lower price bound</param>
        /// <param name="upperPrice">Upper price bound</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/advance/double-win-leverage"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetDoubleWinLeverage(long productId, string initialPrice, string lowerPrice, string upperPrice)
        {
            var query = new Dictionary<string, object>
            {
                { "productId", productId },
                { "initialPrice", initialPrice },
                { "lowerPrice", lowerPrice },
                { "upperPrice", upperPrice }
            };
            var result = await this.SendSignedAsync<string>(GET_DOUBLE_WIN_LEVERAGE, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_HOLD_TO_EARN_PRODUCT = "/v5/earn/hold-to-earn/product";
        /// <summary>
        /// Get the hold-to-earn product list.
        /// </summary>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/hold-to-earn/product"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetHoldToEarnProduct()
        {
            var result = await this.SendPublicAsync<string>(GET_HOLD_TO_EARN_PRODUCT, HttpMethod.Get);
            return result;
        }

        private const string GET_HOLD_TO_EARN_YIELD_HISTORY = "/v5/earn/hold-to-earn/yield-history";
        /// <summary>
        /// Get the hold-to-earn yield history.
        /// </summary>
        /// <param name="limit">Page size</param>
        /// <param name="timeStart">Start timestamp in ms</param>
        /// <param name="timeEnd">End timestamp in ms</param>
        /// <param name="cursor">Pagination cursor</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/hold-to-earn/yield-history"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetHoldToEarnYieldHistory(long limit, long? timeStart = null, long? timeEnd = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
            {
                { "limit", limit }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("timeStart", timeStart),
                ("timeEnd", timeEnd),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_HOLD_TO_EARN_YIELD_HISTORY, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_LIQUIDITY_MINING_LIQUIDATION_RECORDS = "/v5/earn/liquidity-mining/liquidation-records";
        /// <summary>
        /// Get Liquidation Records for liquidity mining.
        /// </summary>
        /// <param name="baseCoin">Base coin</param>
        /// <param name="quoteCoin">Quote coin</param>
        /// <param name="startTime">Start timestamp (ms)</param>
        /// <param name="endTime">End timestamp (ms)</param>
        /// <param name="limit">Limit for data size per page</param>
        /// <param name="cursor">Cursor for pagination</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/liquidity-mining/liquidation-records"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetLiquidityMiningLiquidationRecords(string? baseCoin = null, string? quoteCoin = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("baseCoin", baseCoin),
                ("quoteCoin", quoteCoin),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_LIQUIDITY_MINING_LIQUIDATION_RECORDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_LIQUIDITY_MINING_ORDERS = "/v5/earn/liquidity-mining/order";
        /// <summary>
        /// Get Order History for liquidity mining.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <param name="orderLinkId">User-defined order link ID</param>
        /// <param name="productId">Product ID</param>
        /// <param name="orderType">Order type</param>
        /// <param name="status">Order status</param>
        /// <param name="startTime">Start timestamp (ms)</param>
        /// <param name="endTime">End timestamp (ms)</param>
        /// <param name="limit">Limit for data size per page</param>
        /// <param name="cursor">Cursor for pagination</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/liquidity-mining/order"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetLiquidityMiningOrders(string? orderId = null, string? orderLinkId = null, string? productId = null, string? orderType = null, string? status = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("productId", productId),
                ("orderType", orderType),
                ("status", status),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_LIQUIDITY_MINING_ORDERS, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_LIQUIDITY_MINING_POSITIONS = "/v5/earn/liquidity-mining/position";
        /// <summary>
        /// Get Active Positions for liquidity mining.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="baseCoin">Base coin</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/liquidity-mining/position"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetLiquidityMiningPositions(string? productId = null, string? baseCoin = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId),
                ("baseCoin", baseCoin)
            );
            var result = await this.SendSignedAsync<string>(GET_LIQUIDITY_MINING_POSITIONS, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_LIQUIDITY_MINING_PRODUCTS = "/v5/earn/liquidity-mining/product";
        /// <summary>
        /// Get Liquidity Mining Product List.
        /// </summary>
        /// <param name="baseCoin">Base coin</param>
        /// <param name="quoteCoin">Quote coin</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/liquidity-mining/product"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetLiquidityMiningProducts(string? baseCoin = null, string? quoteCoin = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("baseCoin", baseCoin),
                ("quoteCoin", quoteCoin)
            );
            var result = await this.SendPublicAsync<string>(GET_LIQUIDITY_MINING_PRODUCTS, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_LIQUIDITY_MINING_YIELD_RECORDS = "/v5/earn/liquidity-mining/yield-records";
        /// <summary>
        /// Get Yield Claim Records for liquidity mining.
        /// </summary>
        /// <param name="baseCoin">Base coin</param>
        /// <param name="quoteCoin">Quote coin</param>
        /// <param name="startTime">Start timestamp (ms)</param>
        /// <param name="endTime">End timestamp (ms)</param>
        /// <param name="limit">Limit for data size per page</param>
        /// <param name="cursor">Cursor for pagination</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/liquidity-mining/yield-records"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetLiquidityMiningYieldRecords(string? baseCoin = null, string? quoteCoin = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("baseCoin", baseCoin),
                ("quoteCoin", quoteCoin),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_LIQUIDITY_MINING_YIELD_RECORDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_RWA_NAV_CHART = "/v5/earn/rwa/nav-chart";
        /// <summary>
        /// Get the RWA NAV chart for a product.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="startTime">Start timestamp in ms</param>
        /// <param name="endTime">End timestamp in ms</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/rwa/nav-chart"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetRwaNavChart(long productId, long? startTime = null, long? endTime = null)
        {
            var query = new Dictionary<string, object>
            {
                { "productId", productId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime)
            );
            var result = await this.SendPublicAsync<string>(GET_RWA_NAV_CHART, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_RWA_ORDER_LIST = "/v5/earn/rwa/order";
        /// <summary>
        /// Get the RWA order list.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <param name="orderLinkId">Client order link ID</param>
        /// <param name="orderType">Order type</param>
        /// <param name="productId">Product ID</param>
        /// <param name="startTime">Start timestamp in ms</param>
        /// <param name="endTime">End timestamp in ms</param>
        /// <param name="limit">Page size</param>
        /// <param name="cursor">Pagination cursor</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/rwa/order"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetRwaOrderList(string? orderId = null, string? orderLinkId = null, string? orderType = null, long? productId = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("orderType", orderType),
                ("productId", productId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(GET_RWA_ORDER_LIST, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_RWA_POSITION_LIST = "/v5/earn/rwa/position";
        /// <summary>
        /// Get the RWA position list.
        /// </summary>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/rwa/position"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetRwaPositionList()
        {
            var result = await this.SendSignedAsync<string>(GET_RWA_POSITION_LIST, HttpMethod.Get);
            return result;
        }

        private const string GET_RWA_PRODUCT_LIST = "/v5/earn/rwa/product";
        /// <summary>
        /// Get the RWA product list.
        /// </summary>
        /// <param name="coin">Coin name</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/rwa/product"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetRwaProductList(string? coin = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );
            var result = await this.SendPublicAsync<string>(GET_RWA_PRODUCT_LIST, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_SMART_LEVERAGE_REDEEM_EST_AMOUNT_LIST = "/v5/earn/advance/get-redeem-est-amount-list";
        /// <summary>
        /// Get smart leverage redeem estimation for the given positions.
        /// </summary>
        /// <param name="category">Product category</param>
        /// <param name="positionIds">Position IDs to estimate</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/advance/get-redeem-est-amount-list"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> GetSmartLeverageRedeemEstAmountList(string category, List<Dictionary<string, object>> positionIds)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category },
                { "positionIds", positionIds }
            };
            var result = await this.SendSignedAsync<string>(GET_SMART_LEVERAGE_REDEEM_EST_AMOUNT_LIST, HttpMethod.Get, query: query);
            return result;
        }

        private const string LIST_EARN_COUPONS = "/v5/earn/coupons";
        /// <summary>
        /// List earn coupons for the given category.
        /// </summary>
        /// <param name="category">Product category</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/coupons"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> ListEarnCoupons(string category)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category }
            };
            var result = await this.SendSignedAsync<string>(LIST_EARN_COUPONS, HttpMethod.Get, query: query);
            return result;
        }

        private const string PLACE_ADVANCE_EARN_ORDER = "/v5/earn/advance/place-order";
        /// <summary>
        /// Place an advance earn order.
        /// </summary>
        /// <param name="category">Product category</param>
        /// <param name="productId">Product ID</param>
        /// <param name="orderType">Order type</param>
        /// <param name="amount">Order amount</param>
        /// <param name="accountType">Account type</param>
        /// <param name="coin">Coin name</param>
        /// <param name="orderLinkId">Client-supplied order link ID</param>
        /// <param name="dualAssetsExtra">Dual assets extra fields</param>
        /// <param name="interestCard">Interest card extra fields</param>
        /// <param name="smartLeverageStakeExtra">Smart leverage stake extra fields</param>
        /// <param name="smartLeverageRedeemExtra">Smart leverage redeem extra fields</param>
        /// <param name="doubleWinStakeExtra">Double win stake extra fields</param>
        /// <param name="doubleWinRedeemExtra">Double win redeem extra fields</param>
        /// <param name="discountBuyExtra">Discount buy extra fields</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/advance/place-order"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PlaceAdvanceEarnOrder(string category, long productId, string orderType, string amount, string accountType, string coin, string orderLinkId, Dictionary<string, object>? dualAssetsExtra = null, Dictionary<string, object>? interestCard = null, Dictionary<string, object>? smartLeverageStakeExtra = null, Dictionary<string, object>? smartLeverageRedeemExtra = null, Dictionary<string, object>? doubleWinStakeExtra = null, Dictionary<string, object>? doubleWinRedeemExtra = null, Dictionary<string, object>? discountBuyExtra = null)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category },
                { "productId", productId },
                { "orderType", orderType },
                { "amount", amount },
                { "accountType", accountType },
                { "coin", coin },
                { "orderLinkId", orderLinkId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("dualAssetsExtra", dualAssetsExtra),
                ("interestCard", interestCard),
                ("smartLeverageStakeExtra", smartLeverageStakeExtra),
                ("smartLeverageRedeemExtra", smartLeverageRedeemExtra),
                ("doubleWinStakeExtra", doubleWinStakeExtra),
                ("doubleWinRedeemExtra", doubleWinRedeemExtra),
                ("discountBuyExtra", discountBuyExtra)
            );
            var result = await this.SendSignedAsync<string>(PLACE_ADVANCE_EARN_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        private const string PLACE_RWA_ORDER = "/v5/earn/rwa/place-order";
        /// <summary>
        /// Place a RWA stake or redeem order.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="orderType">Order type (stake or redeem)</param>
        /// <param name="coin">Coin name</param>
        /// <param name="orderLinkId">Client-supplied order link ID</param>
        /// <param name="stakeAmount">Stake amount</param>
        /// <param name="redeemShares">Redeem shares</param>
        /// <param name="accountType">Account type</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/rwa/place-order"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PlaceRwaOrder(long productId, string orderType, string coin, string orderLinkId, string? stakeAmount = null, string? redeemShares = null, string? accountType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "productId", productId },
                { "orderType", orderType },
                { "coin", coin },
                { "orderLinkId", orderLinkId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("stakeAmount", stakeAmount),
                ("redeemShares", redeemShares),
                ("accountType", accountType)
            );
            var result = await this.SendSignedAsync<string>(PLACE_RWA_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_ASSET_TREND = "/v5/earn/pwm/investment-plan/asset-trend";
        /// <summary>
        /// Get plan asset trend time series.
        /// </summary>
        /// <param name="planId">Plan ID</param>
        /// <param name="startTime">Start timestamp in ms</param>
        /// <param name="endTime">End timestamp in ms</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/investment-plan/asset-trend"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmAssetTrend(string planId, long? startTime = null, long? endTime = null)
        {
            var query = new Dictionary<string, object>
            {
                { "planId", planId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime)
            );
            var result = await this.SendSignedAsync<string>(PWM_ASSET_TREND, HttpMethod.Get, query: query);
            return result;
        }

        private const string PWM_CLAIM = "/v5/earn/pwm/investment-plan/claim";
        /// <summary>
        /// Claim available funds from an investment plan.
        /// </summary>
        /// <param name="planId">Plan ID</param>
        /// <param name="orderLinkId">Client-generated order link ID for idempotency</param>
        /// <param name="toAccountType">Destination account type</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/investment-plan/claim"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmClaim(string planId, string orderLinkId, string? toAccountType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "planId", planId },
                { "orderLinkId", orderLinkId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("toAccountType", toAccountType)
            );
            var result = await this.SendSignedAsync<string>(PWM_CLAIM, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_CREATE_CUSTOM_PLAN = "/v5/earn/pwm/customize-plan/create";
        /// <summary>
        /// Create Custom Investment Plan (Direct Mode)
        /// </summary>
        /// <param name="products">List of products to include in the custom plan</param>
        /// <param name="orderLinkId">User-defined order ID</param>
        /// <param name="accountType">Account type</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/customize-plan/create"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmCreateCustomPlan(List<Dictionary<string, object>> products, string orderLinkId, string? accountType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "products", products },
                { "orderLinkId", orderLinkId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("accountType", accountType)
            );
            var result = await this.SendSignedAsync<string>(PWM_CREATE_CUSTOM_PLAN, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_FUND_NAV = "/v5/earn/pwm/investment-plan/fund-nav";
        /// <summary>
        /// Get fund historical net asset value (NAV).
        /// </summary>
        /// <param name="fundId">Fund ID</param>
        /// <param name="startTime">Start timestamp in ms</param>
        /// <param name="endTime">End timestamp in ms</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/investment-plan/fund-nav"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmFundNav(string fundId, long? startTime = null, long? endTime = null)
        {
            var query = new Dictionary<string, object>
            {
                { "fundId", fundId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime)
            );
            var result = await this.SendSignedAsync<string>(PWM_FUND_NAV, HttpMethod.Get, query: query);
            return result;
        }

        private const string PWM_FUND_TRANSFER = "/v5/earn/pwm/fund-transfer";
        /// <summary>
        /// Transfer funds between custody sub-accounts.
        /// </summary>
        /// <param name="transferId">Client-generated transfer ID for idempotency</param>
        /// <param name="fromUserId">Source user ID</param>
        /// <param name="toUserId">Destination user ID</param>
        /// <param name="amount">Transfer amount</param>
        /// <param name="coin">Coin symbol</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/fund-transfer"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmFundTransfer(string transferId, long fromUserId, long toUserId, string amount, string coin)
        {
            var query = new Dictionary<string, object>
            {
                { "transferId", transferId },
                { "fromUserId", fromUserId },
                { "toUserId", toUserId },
                { "amount", amount },
                { "coin", coin }
            };
            var result = await this.SendSignedAsync<string>(PWM_FUND_TRANSFER, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_GET_NEW_PLAN_DETAIL = "/v5/earn/pwm/investment-plan/new-plan";
        /// <summary>
        /// Get detail for a pending-subscription investment plan.
        /// </summary>
        /// <param name="planId">Plan ID</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/investment-plan/new-plan"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmGetNewPlanDetail(string planId)
        {
            var query = new Dictionary<string, object>
            {
                { "planId", planId }
            };
            var result = await this.SendSignedAsync<string>(PWM_GET_NEW_PLAN_DETAIL, HttpMethod.Get, query: query);
            return result;
        }

        private const string PWM_GET_PLAN_DETAIL = "/v5/earn/pwm/investment-plan/detail";
        /// <summary>
        /// Get plan detail for an active or closed investment plan.
        /// </summary>
        /// <param name="planId">Plan ID</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/investment-plan/detail"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmGetPlanDetail(string planId)
        {
            var query = new Dictionary<string, object>
            {
                { "planId", planId }
            };
            var result = await this.SendSignedAsync<string>(PWM_GET_PLAN_DETAIL, HttpMethod.Get, query: query);
            return result;
        }

        private const string PWM_INST_CREATE_FUND = "/v5/earn/pwm/asset-manager/create-fund";
        /// <summary>
        /// Create Pending-Subscription Fund.
        /// </summary>
        /// <param name="fundName">Fund name</param>
        /// <param name="coin">Coin</param>
        /// <param name="profitShareRate">Profit share rate</param>
        /// <param name="managementFeeRate">Management fee rate</param>
        /// <param name="reqLinkId">User-defined request link ID</param>
        /// <param name="fundIntroduction">Fund introduction</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/asset-manager/create-fund"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmInstCreateFund(string fundName, string coin, string profitShareRate, string managementFeeRate, string reqLinkId, string? fundIntroduction = null)
        {
            var query = new Dictionary<string, object>
            {
                { "fundName", fundName },
                { "coin", coin },
                { "profitShareRate", profitShareRate },
                { "managementFeeRate", managementFeeRate },
                { "reqLinkId", reqLinkId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("fundIntroduction", fundIntroduction)
            );
            var result = await this.SendSignedAsync<string>(PWM_INST_CREATE_FUND, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_INST_CREATE_INVESTMENT_PLAN = "/v5/earn/pwm/asset-manager/create-investment-plan";
        /// <summary>
        /// Create Investment Plan for Client.
        /// </summary>
        /// <param name="accountUid">Client account UID</param>
        /// <param name="planName">Plan name</param>
        /// <param name="planType">Plan type</param>
        /// <param name="investmentDistribution">Investment distribution details</param>
        /// <param name="reqLinkId">User-defined request link ID</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/asset-manager/create-investment-plan"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmInstCreateInvestmentPlan(string accountUid, string planName, string planType, List<Dictionary<string, object>> investmentDistribution, string reqLinkId)
        {
            var query = new Dictionary<string, object>
            {
                { "accountUid", accountUid },
                { "planName", planName },
                { "planType", planType },
                { "investmentDistribution", investmentDistribution },
                { "reqLinkId", reqLinkId }
            };
            var result = await this.SendSignedAsync<string>(PWM_INST_CREATE_INVESTMENT_PLAN, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_INST_CREATE_SUB_ACCOUNT = "/v5/earn/pwm/asset-manager/create-sub-account";
        /// <summary>
        /// Create a fund sub-account for custody management.
        /// </summary>
        /// <param name="fundId">Fund ID under which to create the sub-account</param>
        /// <param name="reqLinkId">Client-generated request link ID for idempotency</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/asset-manager/create-sub-account"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmInstCreateSubAccount(string fundId, string reqLinkId)
        {
            var query = new Dictionary<string, object>
            {
                { "fundId", fundId },
                { "reqLinkId", reqLinkId }
            };
            var result = await this.SendSignedAsync<string>(PWM_INST_CREATE_SUB_ACCOUNT, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_INST_GET_INVESTMENT_PLANS = "/v5/earn/pwm/asset-manager/get-investment-plan";
        /// <summary>
        /// Query Institution's Investment Plans.
        /// </summary>
        /// <param name="planId">Plan ID</param>
        /// <param name="status">Plan status</param>
        /// <param name="subscriptionUid">Subscription UID</param>
        /// <param name="limit">Limit for data size per page</param>
        /// <param name="cursor">Cursor for pagination</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/asset-manager/get-investment-plan"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmInstGetInvestmentPlans(string? planId = null, string? status = null, string? subscriptionUid = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("planId", planId),
                ("status", status),
                ("subscriptionUid", subscriptionUid),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(PWM_INST_GET_INVESTMENT_PLANS, HttpMethod.Get, query: query);
            return result;
        }

        private const string PWM_INST_LIST_FUNDS = "/v5/earn/pwm/asset-manager/all-funds";
        /// <summary>
        /// Query Institution's Managed Funds.
        /// </summary>
        /// <param name="fundId">Fund ID</param>
        /// <param name="coin">Coin</param>
        /// <param name="status">Fund status</param>
        /// <param name="limit">Limit for data size per page</param>
        /// <param name="cursor">Cursor for pagination</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/asset-manager/all-funds"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmInstListFunds(string? fundId = null, string? coin = null, string? status = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("fundId", fundId),
                ("coin", coin),
                ("status", status),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(PWM_INST_LIST_FUNDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string PWM_INST_LIST_ORDERS = "/v5/earn/pwm/asset-manager/all-order";
        /// <summary>
        /// Query fund subscription/redemption orders.
        /// </summary>
        /// <param name="fundId">Fund ID filter</param>
        /// <param name="orderType">Order type filter (subscribe/redeem)</param>
        /// <param name="status">Order status filter</param>
        /// <param name="startTime">Start timestamp in ms</param>
        /// <param name="endTime">End timestamp in ms</param>
        /// <param name="limit">Max page size</param>
        /// <param name="cursor">Pagination cursor</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/asset-manager/all-order"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmInstListOrders(string? fundId = null, string? orderType = null, string? status = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("fundId", fundId),
                ("orderType", orderType),
                ("status", status),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(PWM_INST_LIST_ORDERS, HttpMethod.Get, query: query);
            return result;
        }

        private const string PWM_INST_MANAGE_INVESTMENT_PLAN = "/v5/earn/pwm/asset-manager/manage-investment-plan";
        /// <summary>
        /// Update investment plan status and funds allocation.
        /// </summary>
        /// <param name="planId">Investment plan ID</param>
        /// <param name="reqLinkId">Client-generated request link ID for idempotency</param>
        /// <param name="updateStatus">New plan status</param>
        /// <param name="updateFunds">Updated fund allocation entries</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/asset-manager/manage-investment-plan"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmInstManageInvestmentPlan(string planId, string reqLinkId, string? updateStatus = null, List<Dictionary<string, object>>? updateFunds = null)
        {
            var query = new Dictionary<string, object>
            {
                { "planId", planId },
                { "reqLinkId", reqLinkId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("updateStatus", updateStatus),
                ("updateFunds", updateFunds)
            );
            var result = await this.SendSignedAsync<string>(PWM_INST_MANAGE_INVESTMENT_PLAN, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_INST_MANAGE_ORDER = "/v5/earn/pwm/asset-manager/manage-order";
        /// <summary>
        /// Approve or reject a fund subscription/redemption order.
        /// </summary>
        /// <param name="orderId">Order ID to manage</param>
        /// <param name="action">Action to take (approve/reject)</param>
        /// <param name="reqLinkId">Client-generated request link ID for idempotency</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/asset-manager/manage-order"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmInstManageOrder(string orderId, string action, string reqLinkId)
        {
            var query = new Dictionary<string, object>
            {
                { "orderId", orderId },
                { "action", action },
                { "reqLinkId", reqLinkId }
            };
            var result = await this.SendSignedAsync<string>(PWM_INST_MANAGE_ORDER, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_INST_SETTLE_PROFIT = "/v5/earn/pwm/asset-manager/settle-profit";
        /// <summary>
        /// Execute Profit Settlement for an institution-managed fund.
        /// </summary>
        /// <param name="fundId">Fund ID</param>
        /// <param name="reqLinkId">User-defined request link ID</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/asset-manager/settle-profit"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmInstSettleProfit(string fundId, string reqLinkId)
        {
            var query = new Dictionary<string, object>
            {
                { "fundId", fundId },
                { "reqLinkId", reqLinkId }
            };
            var result = await this.SendSignedAsync<string>(PWM_INST_SETTLE_PROFIT, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_INVEST_MORE = "/v5/earn/pwm/investment-plan/invest-more";
        /// <summary>
        /// Invest more into an active investment plan.
        /// </summary>
        /// <param name="planId">Plan ID</param>
        /// <param name="category">Product category</param>
        /// <param name="productId">Product ID</param>
        /// <param name="amount">Additional investment amount</param>
        /// <param name="orderLinkId">Client-generated order link ID for idempotency</param>
        /// <param name="accountType">Source account type</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/investment-plan/invest-more"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmInvestMore(string planId, string category, string productId, string amount, string orderLinkId, string? accountType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "planId", planId },
                { "category", category },
                { "productId", productId },
                { "amount", amount },
                { "orderLinkId", orderLinkId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("accountType", accountType)
            );
            var result = await this.SendSignedAsync<string>(PWM_INVEST_MORE, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_LIST_INVESTMENT_PLANS = "/v5/earn/pwm/investment-plan/all";
        /// <summary>
        /// List investment plans.
        /// </summary>
        /// <param name="planId">Plan ID filter</param>
        /// <param name="status">Plan status filter</param>
        /// <param name="limit">Max page size</param>
        /// <param name="cursor">Pagination cursor</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/investment-plan/all"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmListInvestmentPlans(string? planId = null, string? status = null, long? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("planId", planId),
                ("status", status),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(PWM_LIST_INVESTMENT_PLANS, HttpMethod.Get, query: query);
            return result;
        }

        private const string PWM_LIST_ORDER = "/v5/earn/pwm/investment-plan/order";
        /// <summary>
        /// List Investment Plan Orders
        /// </summary>
        /// <param name="planId">Investment plan ID</param>
        /// <param name="category">Product category</param>
        /// <param name="type">Order type</param>
        /// <param name="status">Order status</param>
        /// <param name="startTime">Start timestamp in ms</param>
        /// <param name="endTime">End timestamp in ms</param>
        /// <param name="limit">Max number of items per page</param>
        /// <param name="cursor">Pagination cursor</param>
        /// <param name="orderLinkId">User-defined order ID</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/investment-plan/order"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmListOrder(string? planId = null, string? category = null, string? type = null, string? status = null, long? startTime = null, long? endTime = null, long? limit = null, string? cursor = null, string? orderLinkId = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("planId", planId),
                ("category", category),
                ("type", type),
                ("status", status),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor),
                ("orderLinkId", orderLinkId)
            );
            var result = await this.SendSignedAsync<string>(PWM_LIST_ORDER, HttpMethod.Get, query: query);
            return result;
        }

        private const string PWM_LIST_PRODUCT_CARDS = "/v5/earn/pwm/customize-plan/product";
        /// <summary>
        /// List Available Product Cards (Direct Mode)
        /// </summary>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/customize-plan/product"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmListProductCards()
        {
            var result = await this.SendPublicAsync<string>(PWM_LIST_PRODUCT_CARDS, HttpMethod.Get);
            return result;
        }

        private const string PWM_QUERY_FUND_TRANSFER_RESULT = "/v5/earn/pwm/query-fund-transfer-result";
        /// <summary>
        /// Query fund transfer records between custody sub-accounts.
        /// </summary>
        /// <param name="transferId">Transfer ID filter</param>
        /// <param name="fromUserId">Source user ID filter</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/query-fund-transfer-result"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmQueryFundTransferResult(string? transferId = null, long? fromUserId = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("transferId", transferId),
                ("fromUserId", fromUserId)
            );
            var result = await this.SendSignedAsync<string>(PWM_QUERY_FUND_TRANSFER_RESULT, HttpMethod.Get, query: query);
            return result;
        }

        private const string PWM_REDEEM = "/v5/earn/pwm/investment-plan/redeem";
        /// <summary>
        /// Redeem from an investment plan.
        /// </summary>
        /// <param name="planId">Plan ID</param>
        /// <param name="category">Product category</param>
        /// <param name="productId">Product ID</param>
        /// <param name="orderLinkId">Client-generated order link ID for idempotency</param>
        /// <param name="shares">Shares to redeem</param>
        /// <param name="amount">Amount to redeem</param>
        /// <param name="positionId">Position ID to redeem</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/investment-plan/redeem"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmRedeem(string planId, string category, string productId, string orderLinkId, string? shares = null, string? amount = null, long? positionId = null)
        {
            var query = new Dictionary<string, object>
            {
                { "planId", planId },
                { "category", category },
                { "productId", productId },
                { "orderLinkId", orderLinkId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("shares", shares),
                ("amount", amount),
                ("positionId", positionId)
            );
            var result = await this.SendSignedAsync<string>(PWM_REDEEM, HttpMethod.Post, query: query);
            return result;
        }

        private const string PWM_SUBSCRIBE = "/v5/earn/pwm/investment-plan/subscribe";
        /// <summary>
        /// One-click subscribe to a pending investment plan.
        /// </summary>
        /// <param name="planId">Plan ID</param>
        /// <param name="orderLinkId">Client-generated order link ID for idempotency</param>
        /// <param name="accountType">Source account type</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/pwm/investment-plan/subscribe"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> PwmSubscribe(string planId, string orderLinkId, string? accountType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "planId", planId },
                { "orderLinkId", orderLinkId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("accountType", accountType)
            );
            var result = await this.SendSignedAsync<string>(PWM_SUBSCRIBE, HttpMethod.Post, query: query);
            return result;
        }

        private const string REINVEST_LIQUIDITY = "/v5/earn/liquidity-mining/reinvest";
        /// <summary>
        /// Reinvest Interest earned from a liquidity mining position.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="orderLinkId">User-defined order link ID</param>
        /// <param name="positionId">Position ID</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/liquidity-mining/reinvest"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> ReinvestLiquidity(string productId, string orderLinkId, string positionId)
        {
            var query = new Dictionary<string, object>
            {
                { "productId", productId },
                { "orderLinkId", orderLinkId },
                { "positionId", positionId }
            };
            var result = await this.SendSignedAsync<string>(REINVEST_LIQUIDITY, HttpMethod.Post, query: query);
            return result;
        }

        private const string REMOVE_LIQUIDITY = "/v5/earn/liquidity-mining/remove-liquidity";
        /// <summary>
        /// Remove Liquidity from a liquidity mining position.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="orderLinkId">User-defined order link ID</param>
        /// <param name="positionId">Position ID</param>
        /// <param name="removeRate">Remove rate</param>
        /// <param name="removeType">Remove type</param>
        /// <see href="https://bybit-exchange.github.io/docs/v5/earn/liquidity-mining/remove-liquidity"/>
        /// <returns>Request results as JSON string.</returns>
        public async Task<string?> RemoveLiquidity(string productId, string orderLinkId, string positionId, long? removeRate = null, string? removeType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "productId", productId },
                { "orderLinkId", orderLinkId },
                { "positionId", positionId }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("removeRate", removeRate),
                ("removeType", removeType)
            );
            var result = await this.SendSignedAsync<string>(REMOVE_LIQUIDITY, HttpMethod.Post, query: query);
            return result;
        }
    }
}
