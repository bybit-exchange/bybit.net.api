using bybit.net.api.Models.RFQ;
using bybit.net.api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitRFQService : BybitApiService
    {
        public BybitRFQService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitRFQService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string CREATE_RQF = "/v5/rfq/create-rfq";

        /// <summary>
        /// Create RFQ — POST /v5/rfq/create-rfq
        /// </summary>
        public async Task<string?> CreateRfq(
            IEnumerable<string> counterparties,
            IEnumerable<RfqLeg> list,
            string? rfqLinkId = null,
            bool? anonymous = null,
            string? strategyType = null)
        {
            var body = new Dictionary<string, object>
            {
                { "counterparties", counterparties?.ToArray() ?? Array.Empty<string>() },
                { "list", list?.ToArray() ?? Array.Empty<RfqLeg>() }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("rfqLinkId", rfqLinkId),
                ("anonymous", anonymous),
                ("strategyType", strategyType)
            );

            var result = await this.SendSignedAsync<string>(
                CREATE_RQF,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string GET_RQF_CONFIG = "/v5/rfq/config";

        /// <summary>
        /// Get RFQ Configuration — GET /v5/rfq/config
        /// </summary>
        public async Task<string?> GetRfqConfig()
        {
            var query = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<string>(
                GET_RQF_CONFIG,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string CANCEL_RQF = "/v5/rfq/cancel-rfq";

        /// <summary>
        /// Cancel RFQ — POST /v5/rfq/cancel-rfq
        /// </summary>
        public async Task<string?> CancelRfq(string? rfqId = null, string? rfqLinkId = null)
        {
            var body = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(body,
                ("rfqId", rfqId),
                ("rfqLinkId", rfqLinkId)
            );

            var result = await this.SendSignedAsync<string>(
                CANCEL_RQF,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string CANCEL_ALL_RQF = "/v5/rfq/cancel-all-rfq";

        /// <summary>
        /// Cancel All RFQs — POST /v5/rfq/cancel-all-rfq
        /// </summary>
        public async Task<string?> CancelAllRfqs()
        {
            var body = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<string>(
                CANCEL_ALL_RQF,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string CREATE_QUOTE = "/v5/rfq/create-quote";

        /// <summary>
        /// Create Quote — POST /v5/rfq/create-quote
        /// </summary>
        public async Task<string?> CreateQuote(
            string rfqId,
            IEnumerable<RfqLeg>? quoteBuyList = null,
            IEnumerable<RfqLeg>? quoteSellList = null,
            string? quoteLinkId = null,
            bool? anonymous = null,
            int? expireIn = null)
        {
            var body = new Dictionary<string, object>
            {
                { "rfqId", rfqId }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("quoteLinkId", quoteLinkId),
                ("anonymous", anonymous),
                ("expireIn", expireIn),
                ("quoteBuyList", quoteBuyList?.ToArray()),
                ("quoteSellList", quoteSellList?.ToArray())
            );

            var result = await this.SendSignedAsync<string>(
                CREATE_QUOTE,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string ACCEPT_OTHER_QUOTE = "/v5/rfq/accept-other-quote";

        /// <summary>
        /// Accept non-LP Quote — POST /v5/rfq/accept-other-quote
        /// </summary>
        public async Task<string?> AcceptOtherQuote(string rfqId)
        {
            var body = new Dictionary<string, object>
            {
                { "rfqId", rfqId }
            };

            var result = await this.SendSignedAsync<string>(
                ACCEPT_OTHER_QUOTE,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string EXECUTE_QUOTE = "/v5/rfq/execute-quote";

        /// <summary>
        /// Execute Quote — POST /v5/rfq/execute-quote
        /// </summary>
        public async Task<string?> ExecuteQuote(string rfqId, string quoteId, string quoteSide)
        {
            var body = new Dictionary<string, object>
            {
                { "rfqId", rfqId },
                { "quoteId", quoteId },
                { "quoteSide", quoteSide }
            };

            var result = await this.SendSignedAsync<string>(
                EXECUTE_QUOTE,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string CANCEL_QUOTE = "/v5/rfq/cancel-quote";

        /// <summary>
        /// Cancel Quote — POST /v5/rfq/cancel-quote
        /// </summary>
        public async Task<string?> CancelQuote(
            string? quoteId = null,
            string? rfqId = null,
            string? quoteLinkId = null)
        {
            var body = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(body,
                ("quoteId", quoteId),
                ("rfqId", rfqId),
                ("quoteLinkId", quoteLinkId)
            );

            var result = await this.SendSignedAsync<string>(
                CANCEL_QUOTE,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string CANCEL_ALL_QUOTES = "/v5/rfq/cancel-all-quotes";

        /// <summary>
        /// Cancel All Quotes — POST /v5/rfq/cancel-all-quotes
        /// </summary>
        public async Task<string?> CancelAllQuotes()
        {
            var body = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<string>(
                CANCEL_ALL_QUOTES,
                HttpMethod.Post,
                query: body);

            return result;
        }

        private const string GET_RFQ_REALTIME = "/v5/rfq/rfq-realtime";

        /// <summary>
        /// Get RFQs (real-time) — GET /v5/rfq/rfq-realtime
        /// </summary>
        public async Task<string?> GetRfqRealtime(
            string? rfqId = null,
            string? rfqLinkId = null,
            string? traderType = null) // quote | request (default: quote)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("rfqId", rfqId),
                ("rfqLinkId", rfqLinkId),
                ("traderType", traderType)
            );

            var result = await this.SendSignedAsync<string>(
                GET_RFQ_REALTIME,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_RQF_LIST = "/v5/rfq/rfq-list";

        /// <summary>
        /// Get RFQs — GET /v5/rfq/rfq-list
        /// </summary>
        public async Task<string?> GetRfqList(
            string? rfqId = null,
            string? rfqLinkId = null,
            string? traderType = null,  // quote | request
            string? status = null,      // Active | Canceled | Filled | Expired | Failed
            int? limit = null,          // [1, 100], default 50
            string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("rfqId", rfqId),
                ("rfqLinkId", rfqLinkId),
                ("traderType", traderType),
                ("status", status),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<string>(
                GET_RQF_LIST,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_QUOTE_REALTIME = "/v5/rfq/quote-realtime";

        /// <summary>
        /// Get Quotes (real-time) — GET /v5/rfq/quote-realtime
        /// </summary>
        public async Task<string?> GetQuoteRealtime(
            string? rfqId = null,
            string? quoteId = null,
            string? quoteLinkId = null,
            string? traderType = null) // quote | request
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("rfqId", rfqId),
                ("quoteId", quoteId),
                ("quoteLinkId", quoteLinkId),
                ("traderType", traderType)
            );

            var result = await this.SendSignedAsync<string>(
                GET_QUOTE_REALTIME,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_QUOTE_LIST = "/v5/rfq/quote-list";

        /// <summary>
        /// Get Quotes — GET /v5/rfq/quote-list
        /// </summary>
        public async Task<string?> GetQuoteList(
            string? rfqId = null,
            string? quoteId = null,
            string? quoteLinkId = null,
            string? traderType = null,  // quote | request
            string? status = null,      // Active | Canceled | PendingFill | Filled | Expired | Failed
            int? limit = null,          // [1,100], default 50
            string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("rfqId", rfqId),
                ("quoteId", quoteId),
                ("quoteLinkId", quoteLinkId),
                ("traderType", traderType),
                ("status", status),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<string>(
                GET_QUOTE_LIST,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_RQF_TRADE_LIST = "/v5/rfq/trade-list";

        /// <summary>
        /// Get Trade History — GET /v5/rfq/trade-list
        /// </summary>
        public async Task<string?> GetRfqTradeList(
            string? rfqId = null,
            string? rfqLinkId = null,
            string? quoteId = null,
            string? quoteLinkId = null,
            string? traderType = null,  // quote | request (default: quote)
            string? status = null,      // Filled | Failed
            int? limit = null,          // [1,100], default 50
            string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("rfqId", rfqId),
                ("rfqLinkId", rfqLinkId),
                ("quoteId", quoteId),
                ("quoteLinkId", quoteLinkId),
                ("traderType", traderType),
                ("status", status),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<string>(
                GET_RQF_TRADE_LIST,
                HttpMethod.Get,
                query: query);

            return result;
        }

        private const string GET_RQF_PUBLIC_TRADES = "/v5/rfq/public-trades";

        /// <summary>
        /// Get Public Trades — GET /v5/rfq/public-trades
        /// </summary>
        public async Task<string?> GetRfqPublicTrades(
            long? startTime = null,
            long? endTime = null,
            int? limit = null,
            string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendPublicAsync<string>(
                GET_RQF_PUBLIC_TRADES,
                HttpMethod.Get,
                query: query);

            return result;
        }
    }
}
