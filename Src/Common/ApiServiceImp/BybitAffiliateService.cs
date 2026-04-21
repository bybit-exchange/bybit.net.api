using bybit.net.api.Models;
using bybit.net.api.Models.Affiliate;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitAffiliateService : BybitApiService
    {
        public BybitAffiliateService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitAffiliateService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string GET_AFFILIATE_USER_LIST = "/v5/affiliate/aff-user-list";
        private const string GET_AFFILIATE_USER_INFO = "/v5/user/aff-customer-info";

        public async Task<GeneralResponse<GetAffiliateUserListResult>?> GetAffiliateUserList(
            int? size = null,
            string? cursor = null,
            bool? needDeposit = null,
            bool? need30 = null,
            bool? need365 = null,
            string? startDate = null,
            string? endDate = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("size", size),
                ("cursor", cursor),
                ("needDeposit", needDeposit),
                ("need30", need30),
                ("need365", need365),
                ("startDate", startDate),
                ("endDate", endDate)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetAffiliateUserListResult>>(GET_AFFILIATE_USER_LIST, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetAffiliateUserInfoResult>?> GetAffiliateUserInfo(string uid)
        {
            var query = new Dictionary<string, object> { { "uid", uid } };
            var result = await this.SendSignedAsync<GeneralResponse<GetAffiliateUserInfoResult>>(GET_AFFILIATE_USER_INFO, HttpMethod.Get, query: query);
            return result;
        }
    }
}
