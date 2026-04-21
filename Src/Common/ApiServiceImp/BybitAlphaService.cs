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
    }
}
