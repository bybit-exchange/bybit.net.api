using bybit.net.api.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitReferralService : BybitApiService
    {
        public BybitReferralService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitReferralService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string GET_REFERRAL_CAMPAIGN_REWARDS = "/v5/referral/campaign/rewards";
        /// <summary>
        /// Get referral rules and campaign task rewards for the current site
        /// </summary>
        public async Task<string?> GetReferralCampaignRewards()
        {
            var result = await this.SendSignedAsync<string>(GET_REFERRAL_CAMPAIGN_REWARDS, HttpMethod.Post);
            return result;
        }
        private const string GET_REFERRAL_COMMISSION_RECORDS = "/v5/referral/commission/records";
        /// <summary>
        /// Get Referral Commission Records
        /// </summary>
        public async Task<string?> GetReferralCommissionRecords(List<long>? userIdList = null, string? startTime = null, string? endTime = null, long? page = null, long? pageSize = null, long? productLine = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("userIdList", userIdList),
                ("startTime", startTime),
                ("endTime", endTime),
                ("page", page),
                ("pageSize", pageSize),
                ("productLine", productLine)
            );
            var result = await this.SendSignedAsync<string>(GET_REFERRAL_COMMISSION_RECORDS, HttpMethod.Post, query: query);
            return result;
        }
        private const string GET_REFERRAL_USER_IDENTITY_PROFILE = "/v5/referral/user/identity-profile";
        /// <summary>
        /// Get Referral User Identity Profile
        /// </summary>
        public async Task<string?> GetReferralUserIdentityProfile()
        {
            var result = await this.SendSignedAsync<string>(GET_REFERRAL_USER_IDENTITY_PROFILE, HttpMethod.Post);
            return result;
        }
        private const string GET_REFERRAL_USER_INVITATIONS = "/v5/referral/user/invitations";
        /// <summary>
        /// Get referral user invitations
        /// </summary>
        public async Task<string?> GetReferralUserInvitations(List<long>? userIdList = null, string? startTime = null, string? endTime = null, long? page = null, long? pageSize = null, long? friendStatus = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("userIdList", userIdList),
                ("startTime", startTime),
                ("endTime", endTime),
                ("page", page),
                ("pageSize", pageSize),
                ("friendStatus", friendStatus)
            );
            var result = await this.SendSignedAsync<string>(GET_REFERRAL_USER_INVITATIONS, HttpMethod.Post, query: query);
            return result;
        }
    }
}
