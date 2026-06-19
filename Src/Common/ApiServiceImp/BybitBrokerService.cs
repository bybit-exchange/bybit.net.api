using bybit.net.api.Models;
using bybit.net.api.Models.Broker;
using bybit.net.api.Models.Lending;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitBrokerService : BybitApiService
    {
        public BybitBrokerService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitBrokerService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string BROKER_EARNING_DATA = "/v5/broker/earnings-info";
        private const string BROKER_ACCOUNT_INFO = "/v5/broker/account-info";
        private const string BROKER_SUB_MEMBER_DEPOSIT_RECORD = "/v5/broker/asset/query-sub-member-deposit-record";
        private const string BROKER_RATE_LIMIT_ALL = "/v5/broker/apilimit/query-all";
        private const string BROKER_RATE_LIMIT_CAP = "/v5/broker/apilimit/query-cap";
        private const string BROKER_RATE_LIMIT_SET = "/v5/broker/apilimit/set";
        private const string BROKER_VOUCHER_INFO = "/v5/broker/award/info";
        private const string BROKER_ISSUE_VOUCHER = "/v5/broker/award/distribute-award";
        private const string BROKER_ISSUED_VOUCHER = "/v5/broker/award/distribution-record";

        public async Task<GeneralResponse<GetBrokerEarningResult>?> GetBrokerEarning(BizType? bizType = null, string? begin = null, string? end = null, string? uid = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("bizType", bizType?.Value),
                ("begin", begin),
                ("end", end),
                ("uid", uid),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetBrokerEarningResult>>(BROKER_EARNING_DATA, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetBrokerAccountInfoResult>?> GetBrokerAccountInfo()
        {
            var query = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<GeneralResponse<GetBrokerAccountInfoResult>>(BROKER_ACCOUNT_INFO, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetBrokerSubMemberDepositRecordsResult>?> GetBrokerSubMemberDepositRecords(
            string? id = null,
            string? txId = null,
            string? subMemberId = null,
            string? coin = null,
            long? startTime = null,
            long? endTime = null,
            int? limit = null,
            string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("id", id),
                ("txID", txId),
                ("subMemberId", subMemberId),
                ("coin", coin),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetBrokerSubMemberDepositRecordsResult>>(BROKER_SUB_MEMBER_DEPOSIT_RECORD, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetBrokerRateLimitAllResult>?> GetBrokerAllRateLimits(int? limit = null, string? cursor = null, string? uids = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("limit", limit),
                ("cursor", cursor),
                ("uids", uids)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetBrokerRateLimitAllResult>>(BROKER_RATE_LIMIT_ALL, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetBrokerRateLimitCapResult>?> GetBrokerRateLimitCap()
        {
            var query = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<GeneralResponse<GetBrokerRateLimitCapResult>>(BROKER_RATE_LIMIT_CAP, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<SetBrokerRateLimitResult>?> SetBrokerRateLimit(List<BrokerRateLimitRequestItem> list)
        {
            var body = new Dictionary<string, object>
            {
                { "list", list }
            };

            var result = await this.SendSignedAsync<GeneralResponse<SetBrokerRateLimitResult>>(BROKER_RATE_LIMIT_SET, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<GeneralResponse<GetBrokerVoucherSpecResult>?> GetBrokerVoucherSpec(string id)
        {
            var body = new Dictionary<string, object>
            {
                { "id", id }
            };

            var result = await this.SendSignedAsync<GeneralResponse<GetBrokerVoucherSpecResult>>(BROKER_VOUCHER_INFO, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<GeneralResponse<IssueBrokerVoucherResult>?> IssueBrokerVoucher(string accountId, string awardId, string specCode, string amount, string brokerId)
        {
            var body = new Dictionary<string, object>
            {
                { "accountId", accountId },
                { "awardId", awardId },
                { "specCode", specCode },
                { "amount", amount },
                { "brokerId", brokerId }
            };

            var result = await this.SendSignedAsync<GeneralResponse<IssueBrokerVoucherResult>>(BROKER_ISSUE_VOUCHER, HttpMethod.Post, query: body);
            return result;
        }

        public async Task<GeneralResponse<GetIssuedBrokerVoucherResult>?> GetIssuedBrokerVoucher(string accountId, string awardId, string specCode, bool? withUsedAmount = null)
        {
            var body = new Dictionary<string, object>
            {
                { "accountId", accountId },
                { "awardId", awardId },
                { "specCode", specCode }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("withUsedAmount", withUsedAmount)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetIssuedBrokerVoucherResult>>(BROKER_ISSUED_VOUCHER, HttpMethod.Post, query: body);
            return result;
        }
    }
}
