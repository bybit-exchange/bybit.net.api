using bybit.net.api.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitSpotXService : BybitApiService
    {
        public BybitSpotXService(Uri? url = null, bool debugMode = false, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW)
            : this(httpClient: new HttpClient(), url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitSpotXService(HttpClient httpClient, Uri? url = null, bool debugMode = false, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW)
            : base(httpClient: httpClient, url: GetBaseUrl(url), recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitSpotXService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitSpotXService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: ValidateApiKey(apiKey), apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private static string ValidateApiKey(string apiKey)
        {
            if (Uri.TryCreate(apiKey, UriKind.Absolute, out var uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                throw new ArgumentException(
                    "The API key looks like a URL. Pass the public API base URL as a Uri instead.",
                    nameof(apiKey));
            }

            return apiKey;
        }

        private static string? GetBaseUrl(Uri? url)
        {
            return url?.AbsoluteUri.TrimEnd('/');
        }

        private const string GET_LAUNCHPOOL_PROJECT_LIST = "/v5/spot-x/launchpool/project/list";
        /// <summary>
        /// Get Launchpool project (activity) list
        /// </summary>
        public async Task<string?> GetLaunchpoolProjectList(long status, string? activityCoin = null, string? projectId = null, string? cursor = null, long? limit = null)
        {
            var query = new Dictionary<string, object>
            {
                { "status", status }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("activityCoin", activityCoin),
                ("projectId", projectId),
                ("cursor", cursor),
                ("limit", limit)
            );
            var result = await this.SendPublicAsync<string>(GET_LAUNCHPOOL_PROJECT_LIST, HttpMethod.Get, query: query);
            return result;
        }
        private const string GET_LAUNCHPOOL_USER_ACTIVITY_LOG = "/v5/spot-x/launchpool/user/activity-log";
        /// <summary>
        /// Get user&#x27;s staking operation records
        /// </summary>
        public async Task<string?> GetLaunchpoolUserActivityLog(string? stakeCoin = null, long? type = null, long? status = null, string? startTime = null, string? endTime = null, long? pageSize = null, long? current = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("stakeCoin", stakeCoin),
                ("type", type),
                ("status", status),
                ("startTime", startTime),
                ("endTime", endTime),
                ("pageSize", pageSize),
                ("current", current)
            );
            var result = await this.SendSignedAsync<string>(GET_LAUNCHPOOL_USER_ACTIVITY_LOG, HttpMethod.Post, query: query);
            return result;
        }
        private const string GET_LAUNCHPOOL_USER_CURRENT_STAKING = "/v5/spot-x/launchpool/user/current-staking";
        /// <summary>
        /// Get user&#x27;s active staking positions
        /// </summary>
        public async Task<string?> GetLaunchpoolUserCurrentStaking()
        {
            var result = await this.SendSignedAsync<string>(GET_LAUNCHPOOL_USER_CURRENT_STAKING, HttpMethod.Get);
            return result;
        }
        private const string GET_LAUNCHPOOL_USER_HISTORY = "/v5/spot-x/launchpool/user/history";
        /// <summary>
        /// Get user&#x27;s completed staking position history
        /// </summary>
        public async Task<string?> GetLaunchpoolUserHistory(string? stakeCoin = null, string? rewardCoin = null, string? startTime = null, string? endTime = null, long? pageSize = null, long? current = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("stakeCoin", stakeCoin),
                ("rewardCoin", rewardCoin),
                ("startTime", startTime),
                ("endTime", endTime),
                ("pageSize", pageSize),
                ("current", current)
            );
            var result = await this.SendSignedAsync<string>(GET_LAUNCHPOOL_USER_HISTORY, HttpMethod.Post, query: query);
            return result;
        }
        private const string GET_PUZZLE_PROJECT_LIST = "/v5/spot-x/puzzle/project/list";
        /// <summary>
        /// Get Puzzle project (activity) list
        /// </summary>
        public async Task<string?> GetPuzzleProjectList(long status, string? projectId = null, string? activityCoin = null, string? cursor = null, long? limit = null)
        {
            var query = new Dictionary<string, object>
            {
                { "status", status }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("projectId", projectId),
                ("activityCoin", activityCoin),
                ("cursor", cursor),
                ("limit", limit)
            );
            var result = await this.SendPublicAsync<string>(GET_PUZZLE_PROJECT_LIST, HttpMethod.Get, query: query);
            return result;
        }
        private const string GET_TOKEN_SPLASH_PROJECT_LIST = "/v5/spot-x/token-splash/project/list";
        /// <summary>
        /// Get Token Splash project (activity) list
        /// </summary>
        public async Task<string?> GetTokenSplashProjectList(long status, string? projectId = null, string? activityCoin = null, string? cursor = null, long? limit = null)
        {
            var query = new Dictionary<string, object>
            {
                { "status", status }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("projectId", projectId),
                ("activityCoin", activityCoin),
                ("cursor", cursor),
                ("limit", limit)
            );
            var result = await this.SendPublicAsync<string>(GET_TOKEN_SPLASH_PROJECT_LIST, HttpMethod.Get, query: query);
            return result;
        }
        private const string GET_TOKEN_SPLASH_USER_ACTIVITY_PARAMS = "/v5/spot-x/token-splash/user/activity-params";
        /// <summary>
        /// Get user&#x27;s Token Splash trade-task progress
        /// </summary>
        public async Task<string?> GetTokenSplashUserActivityParams(string? projectId = null, string? activityCoin = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("projectId", projectId),
                ("activityCoin", activityCoin)
            );
            var result = await this.SendSignedAsync<string>(GET_TOKEN_SPLASH_USER_ACTIVITY_PARAMS, HttpMethod.Get, query: query);
            return result;
        }
    }
}
