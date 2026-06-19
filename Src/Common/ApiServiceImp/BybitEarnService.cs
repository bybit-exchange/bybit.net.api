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
    }
}
