using bybit.net.api.Models;
using bybit.net.api.Models.Market;
using bybit.net.api.Models.Trade;
using bybit.net.api.Services;


namespace bybit.net.api.ApiServiceImp
{
    public class BybitMarketDataService : BybitApiService
    {
        public BybitMarketDataService(bool useTestnet = false )
        : this(new HttpClient(), useTestnet)
        {
        }

        public BybitMarketDataService(HttpClient httpClient, bool useTestnet = false)
            : base(httpClient, useTestnet)
        {
        }

        private const string CHECK_SERVER_TIME = "/v5/market/time";

        /// <summary>
        /// Get Bybit Server Time.<para />
        /// </summary>
        /// <returns>Bybit server UTC timestamp.</returns>
        public async Task<string?> CheckServerTime()
        {
            var result = await SendPublicAsync<string>(
                CHECK_SERVER_TIME,
                HttpMethod.Get);

            return result;
        }

        private const string MAKRKET_KLINE = "/v5/market/kline";

        /// <summary>
        /// Query for historical mark price klines. Charts are returned in groups based on the requested interval.
        /// Covers: USDT perpetual / USDC contract / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="limit"></param>
        /// <returns>Market kline</returns>
        public async Task<string?> GetMarketKline(Category category, string symbol, MarketInterval interval, long? start = null, long? end = null, int? limit = null)
        {
            var query = new Dictionary<string, object>
                        {
                    { "category", category },
                    { "symbol", symbol },
                    { "interval", interval }, };
            if (start.HasValue) query.Add("start", start.Value);
            if (end.HasValue) query.Add("end", end.Value);
            if (limit.HasValue) query.Add("limit", limit);
            var result = await SendPublicAsync<string>(
                MAKRKET_KLINE,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string MARK_PRICE_KLINE = "/v5/market/mark-price-kline";
        /// <summary>
        /// Query for historical mark price klines. Charts are returned in groups based on the requested interval.
        /// Covers: USDT perpetual / USDC contract / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="limit"></param>
        /// <returns>Market kline</returns>
        public async Task<string?> GetMarKPricetKline(Category category, string symbol, MarketInterval interval, long? start = null, long? end = null, int? limit = null)
        {
            var query = new Dictionary<string, object>
                        {
                    { "category", category },
                    { "symbol", symbol },
                    { "interval", interval }, };
            if (start.HasValue) query.Add("start", start.Value);
            if (end.HasValue) query.Add("end", end.Value);
            if (limit.HasValue) query.Add("limit", limit);

            var result = await SendPublicAsync<string>(
                MARK_PRICE_KLINE,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string INDEX_PRICE_KLINE = "/v5/market/mark-price-kline";
        /// <summary>
        /// Query for historical index price klines. Charts are returned in groups based on the requested interval.
        /// Covers: USDT perpetual / USDC contract / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="limit"></param>
        /// <returns>Market kline</returns>
        public async Task<string?> GetIndexPricetKline(Category category, string symbol, MarketInterval interval, long? start = null, long? end = null, int? limit = null)
        {
            var query = new Dictionary<string, object>
                        {
                    { "category", category },
                    { "symbol", symbol },
                    { "interval", interval }, };
            if (start.HasValue) query.Add("start", start.Value);
            if (end.HasValue) query.Add("end", end.Value);
            if (limit.HasValue) query.Add("limit", limit);

            var result = await SendPublicAsync<string>(
                INDEX_PRICE_KLINE,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string PREMIUM_INDEX_PRICE_KLINE = "/v5/market/premium-index-price-kline";
        /// <summary>
        /// Query for historical index premium price klines. Charts are returned in groups based on the requested interval.
        /// Covers: USDT perpetual / USDC contract / Inverse contract
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="limit"></param>
        /// <returns>Market kline</returns>
        public async Task<string?> GetPremiumIndexPricetKline(Category category, string symbol, MarketInterval interval, long? start = null, long? end = null, int? limit = null)
        {
            var query = new Dictionary<string, object>
                        {
                    { "category", category },
                    { "symbol", symbol },
                    { "interval", interval }, };
            if (start.HasValue) query.Add("start", start.Value);
            if (end.HasValue) query.Add("end", end.Value);
            if (limit.HasValue) query.Add("limit", limit);

            var result = await SendPublicAsync<string>(
                PREMIUM_INDEX_PRICE_KLINE,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string INSTRUMENT_INFO = "/v5/market/instruments-info";
        /// <summary>
        /// use for the instrument specification of online trading pairs.
        /// Covers: Spot / USDT perpetual / USDC contract / Inverse contract / Option
        ///Spot does not support pagination, so limit, cursor are invalid.
        ///When query by baseCoin, regardless of category= linear or inverse, the result will have USDT perpetual, USDC contract and Inverse contract symbols.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="status"></param>
        /// <param name="baseCoin"></param>
        /// <param name="cursor"></param>
        /// <param name="limit"></param>
        /// <returns>Instrument Info</returns>
        public async Task<string?> GetInstrumentInfo(Category category, string symbol, InstrumentStatus? status = null, string? baseCoin = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category }, { "symbol", symbol }, };

            BybitParametersUtils.AddOptionalParameters(query,
                ("status", status?.Status),
                ("baseCoin", baseCoin),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await SendPublicAsync<string>(
                INSTRUMENT_INFO,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string MARKET_ORDERBOOK = "/v5/market/orderbook";
        /// <summary>
        /// Query for orderbook depth data.
        ///Covers: Spot / USDT perpetual / USDC contract / Inverse contract / Option
        ///future: 200-level of orderbook data
        ///spot: 50-level of orderbook data
        ///option: 25-level of orderbook data
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="limit"></param>
        /// <returns>Market Orderbook</returns>
        public async Task<string?> GetMarketOrderbook(Category category, string? symbol = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("limit", limit)
            );

            var result = await SendPublicAsync<string>(
                MARKET_ORDERBOOK,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string MARKET_TICKERS = "/v5/market/tickers";
        /// <summary>
        /// Get Tickers
        ///Query for the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
        ///Covers: Spot / USDT perpetual / USDC contract / Inverse contract / Option
        ///If category = option, symbol or baseCoin must be passed.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="baseCoin"></param>
        /// <param name="expDate"></param>
        /// <returns>Market Tickers</returns>
        public async Task<string?> GetMarketTickers(Category category, string? symbol = null, string? baseCoin = null, string? expDate = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("expDate", expDate)
            );

            var result = await SendPublicAsync<string>(
                MARKET_TICKERS,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string MARKET_FUNDING_HISTORY = "/v5/market/funding/history";
        /// <summary>
        /// Get Tickers
        ///Query for historical funding rates.Each symbol has a different funding interval.For example, if the interval is 8 hours and the current time is UTC 12, then it returns the last funding rate, which settled at UTC 8.
        ///To query the funding rate interval, please refer to the instruments-info endpoint.
        ///Covers: USDT and USDC perpetual / Inverse perpetual
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <returns>Market Funding History</returns>
        public async Task<string?> GetMarketFundingHistory(Category category, string symbol, long? startTime = null, long? endTime = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "category", category }, { "symbol", symbol } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit)
            );

            var result = await SendPublicAsync<string>(
                MARKET_FUNDING_HISTORY,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string MARKET_RECENT_TRADE = "/v5/market/recent-trade";
        /// <summary>
        /// Get Public Recent Trading History Query recent public trading data in Bybit.
        ///Covers: Spot / USDT perpetual / USDC contract / Inverse contract / Option
        ///You can download archived historical trades here: USDT Perpetual, Inverse Perpetual & Inverse Futures Spot
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="baseCoin"></param>
        /// <param name="optionType"></param>
        /// <param name="limit"></param>
        /// <returns>Market Recent Trades</returns>
        public async Task<string?> GetMarketRecentTrade(Category category, string symbol, string? baseCoin = null, OptionType? optionType = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "category", category }, };

            BybitParametersUtils.AddOptionalParameters(query,
                 ("symbol", symbol),
                 ("baseCoin", baseCoin),
                 ("optionType", optionType?.Value),
                ("limit", limit)
            );

            var result = await SendPublicAsync<string>(
                MARKET_RECENT_TRADE,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string MARKET_OPEN_INTEREST = "/v5/market/open-interest";
        /// <summary>
        /// Get the open interest of each symbol.
        ///Covers: USDT perpetual / USDC contract / Inverse contract
        ///Returns single side data
        ///The upper limit time you can query is the launch time of the symbol.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="intervalTime"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <returns>Market Open Interest</returns>
        public async Task<string?> GetMarketOpenInterest(Category category, string symbol, MarketIntervalTime intervalTime, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category }, { "symbol", symbol }, { "intervalTime", intervalTime.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                 ("startTime", startTime),
                 ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await SendPublicAsync<string>(
                MARKET_OPEN_INTEREST,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string MARKET_HISTORICAL_VOLATILITY = "/v5/market/historical-volatility";
        /// <summary>
        /// Query option historical volatility
        ///Covers: Option
        ///The data is hourly.
        ///If both startTime and endTime are not specified, it will return the most recent 1 hours worth of data.
        ///startTime and endTime are a pair of params. Either both are passed or they are not passed at all.
        ///This endpoint can query the last 2 years worth of data, but make sure[endTime - startTime] <= 30 days.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="baseCoin"></param>
        /// <param name="period"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>Market Historical Volatility</returns>
        public async Task<string?> GetMarketHistoricalVolatility(Category category, string? baseCoin = null, string? period = null, long? startTime = null, long? endTime = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("baseCoin", baseCoin),
                ("period", period),
                 ("startTime", startTime),
                 ("endTime", endTime)
            );

            var result = await SendPublicAsync<string>(
                MARKET_HISTORICAL_VOLATILITY,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string MARKET_INSURANCE = "/v5/market/insurance";
        /// <summary>
        /// Query for Bybit insurance pool data(BTC/USDT/USDC etc). The data is updated every 24 hours.
        /// </summary>
        /// <param name="coin"></param>
        /// <returns>Market Insurance</returns>
        public async Task<string?> GetMarketInsurance(string? coin = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                 ("coin", coin)
            );

            var result = await SendPublicAsync<string>(
                MARKET_INSURANCE,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string MARKET_RISK_LIMIT = "/v5/market/risk-limit";
        /// <summary>
        /// Query for Bybit insurance pool data(BTC/USDT/USDC etc). The data is updated every 24 hours.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <returns>Market Risk Limit</returns>
        public async Task<string?> GetMarketRiskLimit(Category category, string? symbol = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                 ("symbol", symbol)
            );

            var result = await SendPublicAsync<string>(
                MARKET_RISK_LIMIT,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string MARKET_DELIVERY_PRICE = "/v5/market/delivery-price";
        /// <summary>
        /// Get the delivery price.
        /// Covers: USDC futures / Inverse futures / Option
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="baseCoin"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Market Delivery Price</returns>
        public async Task<string?> GetMarketDeliveryPrice(Category category, string? symbol = null, string? baseCoin = null, int? limit =null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                 ("symbol", symbol),
                 ("baseCoin", baseCoin),
                 ("limit", limit),
                 ("cursor", cursor)
            );

            var result = await SendPublicAsync<string>(
                MARKET_DELIVERY_PRICE,
                HttpMethod.Get,
                query: query
                );

            return result;
        }

        private const string MARKET_LONG_SHORT_RATIO = "/v5/market/account-ratio";
        /// <summary>
        /// Get Long Short Ratio
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="limit"></param>
        /// <param name="period"></param>
        /// <returns>Market Delivery Price</returns>
        public async Task<string?> GetMarketDeliveryPrice(Category category, string symbol, string period, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "category", category }, { "symbol", symbol }, { "period", period } };

            BybitParametersUtils.AddOptionalParameters(query,
                 ("limit", limit)
            );

            var result = await SendPublicAsync<string>(
                MARKET_LONG_SHORT_RATIO,
                HttpMethod.Get,
                query: query
                );

            return result;
        }
    }
}
