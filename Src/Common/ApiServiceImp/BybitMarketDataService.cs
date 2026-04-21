using bybit.net.api.Models;
using bybit.net.api.Models.Market;
using bybit.net.api.Models.Trade;
using bybit.net.api.Services;
using System;
using System.Collections.Generic;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitMarketDataService : BybitApiService
    {
        public BybitMarketDataService(string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitMarketDataService(HttpClient httpClient, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string CHECK_SERVER_TIME = "/v5/market/time";

        /// <summary>
        /// Get Bybit server time.
        /// </summary>
        public async Task<GeneralResponse<GetBybitServerTimeResult>?> GetServerTime()
        {
            return await SendPublicAsync<GeneralResponse<GetBybitServerTimeResult>>(
                CHECK_SERVER_TIME,
                HttpMethod.Get);
        }

        /// <summary>
        /// Backward-compatible alias for GetServerTime().
        /// </summary>
        public Task<GeneralResponse<GetBybitServerTimeResult>?> CheckServerTime()
        {
            return GetServerTime();
        }

        private const string MARKET_KLINE = "/v5/market/kline";

        /// <summary>
        /// Query historical klines.
        /// </summary>
        public async Task<GeneralResponse<MarketKLineResult>?> GetMarketKline(Category? category = null, string symbol = "", MarketInterval interval = default, long? start = null, long? end = null, int? limit = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "interval", interval }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("category", category),
                ("start", start),
                ("end", end),
                ("limit", limit)
            );

            return await SendPublicAsync<GeneralResponse<MarketKLineResult>>(
                MARKET_KLINE,
                HttpMethod.Get,
                query: query);
        }

        private const string MARK_PRICE_KLINE = "/v5/market/mark-price-kline";

        /// <summary>
        /// Query historical mark price klines.
        /// </summary>
        public async Task<GeneralResponse<MarketKLineResult>?> GetMarkPriceKline(Category? category = null, string symbol = "", MarketInterval interval = default, long? start = null, long? end = null, int? limit = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "interval", interval }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("category", category),
                ("start", start),
                ("end", end),
                ("limit", limit)
            );

            return await SendPublicAsync<GeneralResponse<MarketKLineResult>>(
                MARK_PRICE_KLINE,
                HttpMethod.Get,
                query: query);
        }

        private const string INDEX_PRICE_KLINE = "/v5/market/index-price-kline";

        /// <summary>
        /// Query historical index price klines.
        /// </summary>
        public async Task<GeneralResponse<MarketKLineResult>?> GetIndexPriceKline(Category? category = null, string symbol = "", MarketInterval interval = default, long? start = null, long? end = null, int? limit = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "interval", interval }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("category", category),
                ("start", start),
                ("end", end),
                ("limit", limit)
            );

            return await SendPublicAsync<GeneralResponse<MarketKLineResult>>(
                INDEX_PRICE_KLINE,
                HttpMethod.Get,
                query: query);
        }

        private const string PREMIUM_INDEX_PRICE_KLINE = "/v5/market/premium-index-price-kline";

        /// <summary>
        /// Query historical premium index price klines.
        /// </summary>
        public async Task<GeneralResponse<MarketKLineResult>?> GetPremiumIndexPriceKline(Category? category = null, string symbol = "", MarketInterval interval = default, long? start = null, long? end = null, int? limit = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "interval", interval }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("category", category),
                ("start", start),
                ("end", end),
                ("limit", limit)
            );

            return await SendPublicAsync<GeneralResponse<MarketKLineResult>>(
                PREMIUM_INDEX_PRICE_KLINE,
                HttpMethod.Get,
                query: query);
        }

        private const string INSTRUMENT_INFO = "/v5/market/instruments-info";

        /// <summary>
        /// Get instrument specification for online trading pairs.
        /// </summary>
        public async Task<GeneralResponse<GetInstrumentsInfoResult>?> GetInstrumentInfo(Category category, string? symbol = null, string? symbolType = null, InstrumentStatus? status = null, string? baseCoin = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("symbolType", symbolType),
                ("status", status?.Status),
                ("baseCoin", baseCoin),
                ("limit", limit),
                ("cursor", cursor)
            );

            return await SendPublicAsync<GeneralResponse<GetInstrumentsInfoResult>>(
                INSTRUMENT_INFO,
                HttpMethod.Get,
                query: query);
        }

        private const string MARKET_ORDERBOOK = "/v5/market/orderbook";

        /// <summary>
        /// Query for orderbook depth data.
        /// </summary>
        public async Task<GeneralResponse<GetOrderbookResult>?> GetMarketOrderbook(Category category, string symbol, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "category", category }, { "symbol", symbol } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("limit", limit)
            );

            return await SendPublicAsync<GeneralResponse<GetOrderbookResult>>(
                MARKET_ORDERBOOK,
                HttpMethod.Get,
                query: query);
        }

        private const string RPI_ORDERBOOK = "/v5/market/rpi_orderbook";

        /// <summary>
        /// Query RPI orderbook depth data.
        /// </summary>
        public async Task<GeneralResponse<GetOrderbookResult>?> GetRpiOrderbook(string symbol, int limit, Category? category = null)
        {
            var query = new Dictionary<string, object> { { "symbol", symbol }, { "limit", limit } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("category", category)
            );

            return await SendPublicAsync<GeneralResponse<GetOrderbookResult>>(
                RPI_ORDERBOOK,
                HttpMethod.Get,
                query: query);
        }

        private const string MARKET_TICKERS = "/v5/market/tickers";

        /// <summary>
        /// Get latest tickers.
        /// </summary>
        public async Task<GeneralResponse<MarketTickerResult>?> GetMarketTickers(Category category, string? symbol = null, string? baseCoin = null, string? expDate = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("expDate", expDate)
            );

            return await SendPublicAsync<GeneralResponse<MarketTickerResult>>(
                MARKET_TICKERS,
                HttpMethod.Get,
                query: query);
        }

        private const string MARKET_FUNDING_HISTORY = "/v5/market/funding/history";

        /// <summary>
        /// Get historical funding rates.
        /// </summary>
        public async Task<GeneralResponse<FundingRateResult>?> GetMarketFundingHistory(Category category, string symbol, long? startTime = null, long? endTime = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "category", category }, { "symbol", symbol } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit)
            );

            return await SendPublicAsync<GeneralResponse<FundingRateResult>>(
                MARKET_FUNDING_HISTORY,
                HttpMethod.Get,
                query: query);
        }

        private const string MARKET_RECENT_TRADE = "/v5/market/recent-trade";

        /// <summary>
        /// Get public recent trading history.
        /// </summary>
        public async Task<GeneralResponse<GetRecentTradeResult>?> GetMarketRecentTrade(Category category, string? symbol = null, string? baseCoin = null, OptionType? optionType = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("optionType", optionType?.Value),
                ("limit", limit)
            );

            return await SendPublicAsync<GeneralResponse<GetRecentTradeResult>>(
                MARKET_RECENT_TRADE,
                HttpMethod.Get,
                query: query);
        }

        private const string MARKET_OPEN_INTEREST = "/v5/market/open-interest";

        /// <summary>
        /// Get open interest data.
        /// </summary>
        public async Task<GeneralResponse<GetOpenInterestResult>?> GetMarketOpenInterest(Category category, string symbol, MarketIntervalTime intervalTime, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category }, { "symbol", symbol }, { "intervalTime", intervalTime.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );

            return await SendPublicAsync<GeneralResponse<GetOpenInterestResult>>(
                MARKET_OPEN_INTEREST,
                HttpMethod.Get,
                query: query);
        }

        private const string MARKET_HISTORICAL_VOLATILITY = "/v5/market/historical-volatility";

        /// <summary>
        /// Query option historical volatility.
        /// </summary>
        public async Task<GeneralResponse<GetHistoricalVolatilityResult>?> GetMarketHistoricalVolatility(Category category, string? baseCoin = null, string? quoteCoin = null, int? period = null, long? startTime = null, long? endTime = null)
        {
            if (category.ToString() != Category.OPTION.ToString())
            {
                throw new ArgumentException("Historical volatility is only available for option category.", nameof(category));
            }

            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("baseCoin", baseCoin),
                ("quoteCoin", quoteCoin),
                ("period", period),
                ("startTime", startTime),
                ("endTime", endTime)
            );

            return await SendPublicAsync<GeneralResponse<GetHistoricalVolatilityResult>>(
                MARKET_HISTORICAL_VOLATILITY,
                HttpMethod.Get,
                query: query);
        }

        private const string MARKET_INSURANCE = "/v5/market/insurance";

        /// <summary>
        /// Query insurance pool data.
        /// </summary>
        public async Task<GeneralResponse<GetInsurancePoolResult>?> GetMarketInsurance(string? coin = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );

            return await SendPublicAsync<GeneralResponse<GetInsurancePoolResult>>(
                MARKET_INSURANCE,
                HttpMethod.Get,
                query: query);
        }

        private const string MARKET_RISK_LIMIT = "/v5/market/risk-limit";

        /// <summary>
        /// Query risk limit margin parameters.
        /// </summary>
        public async Task<GeneralResponse<GetRiskLimitResult>?> GetMarketRiskLimit(Category category, string? symbol = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                 ("symbol", symbol)
            );

            return await SendPublicAsync<GeneralResponse<GetRiskLimitResult>>(
                MARKET_RISK_LIMIT,
                HttpMethod.Get,
                query: query);
        }

        private const string MARKET_DELIVERY_PRICE = "/v5/market/delivery-price";

        /// <summary>
        /// Get delivery price.
        /// </summary>
        public async Task<GeneralResponse<GetDeliveryPriceResult>?> GetMarketDeliveryPrice(Category category, string? symbol = null, string? baseCoin = null, string? settleCoin = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                 ("symbol", symbol),
                 ("baseCoin", baseCoin),
                 ("limit", limit),
                 ("cursor", cursor),
                 ("settleCoin", settleCoin)
            );

            return await SendPublicAsync<GeneralResponse<GetDeliveryPriceResult>>(
                MARKET_DELIVERY_PRICE,
                HttpMethod.Get,
                query: query);
        }

        private const string GET_NEW_DELIVERY_PRICE = "/v5/market/new-delivery-price";

        /// <summary>
        /// Get historical option delivery prices.
        /// </summary>
        public async Task<GeneralResponse<GetNewDeliveryPriceResult>?> GetNewDeliveryPrice(Category category, string baseCoin, string? settleCoin = null)
        {
            if (category.ToString() != Category.OPTION.ToString())
            {
                throw new ArgumentException("New delivery price is only available for option category.", nameof(category));
            }

            var query = new Dictionary<string, object>
            {
                { "category", category },
                { "baseCoin", baseCoin }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("settleCoin", settleCoin)
            );

            return await SendPublicAsync<GeneralResponse<GetNewDeliveryPriceResult>>(
                GET_NEW_DELIVERY_PRICE,
                HttpMethod.Get,
                query: query);
        }

        private const string GET_LONG_SHORT_RATIO = "/v5/market/account-ratio";

        /// <summary>
        /// Get long short ratio.
        /// </summary>
        public async Task<GeneralResponse<GetLongShortRatioResult>?> GetLongShortRatio(Category category, string symbol, string period, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
            {
                { "category", category },
                { "symbol", symbol },
                { "period", period }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );

            return await SendPublicAsync<GeneralResponse<GetLongShortRatioResult>>(
                GET_LONG_SHORT_RATIO,
                HttpMethod.Get,
                query: query);
        }

        private const string GET_ORDER_PRICE_LIMIT = "/v5/market/price-limit";

        /// <summary>
        /// Get order price limit.
        /// </summary>
        public async Task<GeneralResponse<GetOrderPriceLimitResult>?> GetOrderPriceLimit(string symbol, Category? category = null)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("category", category)
            );

            return await SendPublicAsync<GeneralResponse<GetOrderPriceLimitResult>>(
                GET_ORDER_PRICE_LIMIT,
                HttpMethod.Get,
                query: query);
        }

        private const string GET_INDEX_PRICE_COMPONENTS = "/v5/market/index-price-components";

        /// <summary>
        /// Get index price components.
        /// </summary>
        public async Task<GeneralResponse<GetIndexPriceComponentsResult>?> GetIndexPriceComponents(string indexName)
        {
            var query = new Dictionary<string, object>
            {
                { "indexName", indexName }
            };

            return await SendPublicAsync<GeneralResponse<GetIndexPriceComponentsResult>>(
                GET_INDEX_PRICE_COMPONENTS,
                HttpMethod.Get,
                query: query);
        }

        private const string GET_ADL_ALERT = "/v5/market/adlAlert";

        /// <summary>
        /// Get ADL alert data.
        /// </summary>
        public async Task<GeneralResponse<GetAdlAlertResult>?> GetAdlAlert(string? symbol = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol)
            );

            return await SendPublicAsync<GeneralResponse<GetAdlAlertResult>>(
                GET_ADL_ALERT,
                HttpMethod.Get,
                query: query);
        }

        private const string GET_FEE_GROUP_INFO = "/v5/market/fee-group-info";

        /// <summary>
        /// Get fee group structure.
        /// </summary>
        public async Task<GeneralResponse<GetFeeGroupInfoResult>?> GetFeeGroupInfo(string productType, string? groupId = null)
        {
            var query = new Dictionary<string, object>
            {
                { "productType", productType }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("groupId", groupId)
            );

            return await SendPublicAsync<GeneralResponse<GetFeeGroupInfoResult>>(
                GET_FEE_GROUP_INFO,
                HttpMethod.Get,
                query: query);
        }
    }
}
