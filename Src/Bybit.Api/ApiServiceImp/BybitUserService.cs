using bybit.net.api.Models;
using bybit.net.api.Models.Account;
using bybit.net.api.Models.Position;
using bybit.net.api.Models.Trade;
using bybit.net.api.Models.User;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitUserService : BybitApiService
    {
        public BybitUserService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitUserService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        #region Upgrade HIstory
        private const string PREUPGRADE_ORDER_HISTORY = "/v5/pre-upgrade/order/history";
        /// <summary>
        /// After the account is upgraded to a Unified account, you can get the orders which occurred before the upgrade.
        /// can get all status in 7 days
        /// can only get filled orders beyond 7 days
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="baseCoin"></param>
        /// <param name="orderId"></param>
        /// <param name="orderLinkId"></param>
        /// <param name="orderFilter"></param>
        /// <param name="orderStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Get Pre-upgrade Order History</returns>
        public async Task<string?> GetPreUpgradeOrderHistory(Category category, string? symbol = null, string? baseCoin = null, string? orderId = null, string? orderLinkId = null, string? orderFilter = null,
                                                          OrderStatus? orderStatus = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("orderFilter", orderFilter),
                ("orderStatus", orderStatus?.Status),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(PREUPGRADE_ORDER_HISTORY, HttpMethod.Get, query: query);
            return result;
        }

        private const string PREUPGRADE_TRADE_HISTORY = "/v5/pre-upgrade/execution/list";
        /// <summary>
        /// Get users' execution records which occurred before you upgraded the account to a Unified account, sorted by execTime in descending order
        /// For now, it supports to query USDT perpetual, USDC perpetual, Inverse perpetual and futures, Option.
        /// Response items will have sorting issues When 'execTime' is the same.This issue is currently being optimized and will be released at the end of October. If you want to receive real-time execution information, Use the websocket stream (recommended).
        /// You may have multiple executions in a single order.
        /// You can query by symbol, baseCoin, orderId and orderLinkId, and if you pass multiple params, the system will process them according to this priority: orderId > orderLinkId > symbol > baseCoin.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="baseCoin"></param>
        /// <param name="orderId"></param>
        /// <param name="orderLinkId"></param>
        /// <param name="execType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Get Pre-upgrade Trade History</returns>
        public async Task<string?> GetPreUpgradeTradeHistory(Category category, string? symbol = null, string? baseCoin = null, string? orderId = null, string? orderLinkId = null,
                                                          ExecType? execType = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("baseCoin", baseCoin),
                ("orderId", orderId),
                ("orderLinkId", orderLinkId),
                ("execType", execType?.Value),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(PREUPGRADE_TRADE_HISTORY, HttpMethod.Get, query: query);
            return result;
        }

        private const string PREUPGRADE_CLOSE_PNL = "/v5/pre-upgrade/position/closed-pnl";
        /// <summary>
        /// Query user's closed profit and loss records from before you upgraded the account to a Unified account. The results are sorted by createdTime in descending order.
        /// For now, it only supports to query USDT perpetual, Inverse perpetual and futures.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Get Pre-upgrade Closed PnL</returns>
        public async Task<string?> GetPreUpgradeClosePnl(Category category, string symbol, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value }, { "symbol", symbol}
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(PREUPGRADE_CLOSE_PNL, HttpMethod.Get, query: query);
            return result;
        }

        private const string PREUPGRADE_TRANSACTION_LOG = "/v5/pre-upgrade/position/closed-pnl";
        /// <summary>
        /// Query transaction logs which occurred in the USDC Derivatives wallet before the account was upgraded to a Unified account.
        /// You can get USDC Perpetual, Option records.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="baseCoin"></param>
        /// <param name="type"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Get Pre-upgrade Transaction Log</returns>
        public async Task<string?> GetPreUpgradeTransactionLog(Category category, string? baseCoin = null, TransactionType? type = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("baseCoin", baseCoin),
                ("type", type?.Value),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(PREUPGRADE_TRANSACTION_LOG, HttpMethod.Get, query: query);
            return result;
        }

        private const string PREUPGRADE_ASSET_DELIVERY = "/v5/pre-upgrade/asset/delivery-record";
        /// <summary>
        /// Query delivery records of Option before you upgraded the account to a Unified account, sorted by deliveryTime in descending order
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="expDate"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Get Pre-upgrade Option Delivery Record</returns>
        public async Task<string?> GetPreUpgradeAssetDeliveryRecord(Category category, string? symbol = null, string? expDate = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("expDate", expDate),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(PREUPGRADE_ASSET_DELIVERY, HttpMethod.Get, query: query);
            return result;
        }

        private const string PREUPGRADE_USDC_SETTLEMENT = "/v5/pre-upgrade/asset/settlement-record";
        public async Task<string?> GetPreUpgradeUsdcSettlement(Category category, string? symbol = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "category", category.Value },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(PREUPGRADE_USDC_SETTLEMENT, HttpMethod.Get, query: query);
            return result;
        }
        #endregion

        #region User Service
        private const string CREATE_SUB_API_KEY = "/v5/user/create-sub-api";
        /// <summary>
        /// To create new API key for those newly created sub UID. Use master user's api key only.
        /// The API key must have one of the below permissions in order to call this endpoint..
        /// master API key: "Account Transfer", "Subaccount Transfer", "Withdrawal"
        /// </summary>
        /// <param name="subuid"></param>
        /// <param name="permissions"></param>
        /// <param name="note"></param>
        /// <param name="ReadOnly"></param>
        /// <param name="ips"></param>
        /// <returns>Create Sub UID API Key</returns>
        public async Task<string?> CreateUserSubApiKey(int subuid, ReadOnly readOnly, SubUserPermissions permissions, string? note = null, string? ips = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "subuid", subuid },
                            { "permissions", permissions },
                            {"readOnly", readOnly.Value},
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("ips", ips),
                ("note", note)
            );
            var result = await this.SendSignedAsync<string>(CREATE_SUB_API_KEY, HttpMethod.Post, query: query);
            return result;
        }

        /// <summary>
        /// To create new API key for those newly created sub UID. Use master user's api key only.
        /// The API key must have one of the below permissions in order to call this endpoint..
        /// master API key: "Account Transfer", "Subaccount Transfer", "Withdrawal"
        /// </summary>
        /// <param name="subuid"></param>
        /// <param name="permissions"></param>
        /// <param name="note"></param>
        /// <param name="ReadOnly"></param>
        /// <param name="ips"></param>
        /// <returns>Create Sub UID API Key</returns>
        public async Task<string?> CreateUserSubApiKey(int subuid, ReadOnly readOnly, Dictionary<string, List<string>> permissions, string? note = null, string? ips = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "subuid", subuid },
                            { "permissions", permissions },
                            {"readOnly", readOnly.Value},
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("ips", ips),
                ("note", note)
            );
            var result = await this.SendSignedAsync<string>(CREATE_SUB_API_KEY, HttpMethod.Post, query: query);
            return result;
        }

        private const string UPDATE_MASTER_API_KEY = "/v5/user/update-api";
        /// <summary>
        /// Modify the settings of master api key. Use the api key pending to be modified to call the endpoint. Use master user's api key only.
        /// The API key must have one of the below permissions in order to call this endpoint..
        /// master API key: "Account Transfer", "Subaccount Transfer", "Withdrawal"
        /// Only the api key that calls this interface can be modified
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="note"></param>
        /// <param name="ReadOnly"></param>
        /// <param name="ips"></param>
        /// <returns>Master API Key</returns>
        public async Task<string?> ModifyUserMasterApiKey(MasterUserPermissions? permissions = null, string? note = null, ReadOnly? readOnly = null, string? ips = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("permissions", permissions),
                ("ips", ips),
                ("ReadOnly", readOnly?.Value),
                ("note", note)
            );
            var result = await this.SendSignedAsync<string>(UPDATE_MASTER_API_KEY, HttpMethod.Post, query: query);
            return result;
        }

        /// <summary>
        /// Modify the settings of master api key. Use the api key pending to be modified to call the endpoint. Use master user's api key only.
        /// The API key must have one of the below permissions in order to call this endpoint..
        /// master API key: "Account Transfer", "Subaccount Transfer", "Withdrawal"
        /// Only the api key that calls this interface can be modified
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="note"></param>
        /// <param name="ReadOnly"></param>
        /// <param name="ips"></param>
        /// <returns>Master API Key</returns>
        public async Task<string?> ModifyUserMasterApiKey(Dictionary<string, List<string>>? permissions = null, string? note = null, ReadOnly? readOnly = null, string? ips = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("permissions", permissions),
                ("ips", ips),
                ("ReadOnly", readOnly?.Value),
                ("note", note)
            );
            var result = await this.SendSignedAsync<string>(UPDATE_MASTER_API_KEY, HttpMethod.Post, query: query);
            return result;
        }

        private const string UPDATE_SUB_API_KEY = "/v5/user/update-sub-api";
        /// <summary>
        /// Modify the settings of sub api key. Use the sub account api key pending to be modified to call the endpoint or use master account api key to manage its sub account api key.
        /// The API key must have one of the below permissions in order to call this endpoint
        /// sub API key: "Account Transfer", "Sub Member Transfer"
        /// master API Key: "Account Transfer", "Sub Member Transfer", "Withdrawal"
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="note"></param>
        /// <param name="ReadOnly"></param>
        /// <param name="ips"></param>
        /// <returns>Sub API Key</returns>
        public async Task<string?> ModifyUserSubApiKey(string? apikey = null, SubUserPermissions? permissions = null, string? note = null, ReadOnly? readOnly = null, string? ips = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("apikey", apikey),
                ("permissions", permissions),
                ("ips", ips),
                ("ReadOnly", readOnly?.Value),
                ("note", note)
            );
            var result = await this.SendSignedAsync<string>(UPDATE_SUB_API_KEY, HttpMethod.Post, query: query);
            return result;
        }

        /// <summary>
        /// Modify the settings of sub api key. Use the sub account api key pending to be modified to call the endpoint or use master account api key to manage its sub account api key.
        /// The API key must have one of the below permissions in order to call this endpoint
        /// sub API key: "Account Transfer", "Sub Member Transfer"
        /// master API Key: "Account Transfer", "Sub Member Transfer", "Withdrawal"
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="note"></param>
        /// <param name="ReadOnly"></param>
        /// <param name="ips"></param>
        /// <returns>Sub API Key</returns>
        public async Task<string?> ModifyUserSubApiKey(string? apikey = null, Dictionary<string, List<string>>? permissions = null, string? note = null, ReadOnly? readOnly = null, string? ips = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("apikey", apikey),
                ("permissions", permissions),
                ("ips", ips),
                ("ReadOnly", readOnly?.Value),
                ("note", note)
            );
            var result = await this.SendSignedAsync<string>(UPDATE_SUB_API_KEY, HttpMethod.Post, query: query);
            return result;
        }

        private const string CREATE_SUB_USER = "/v5/user/create-sub-member";
        /// <summary>
        /// Create a new sub user id. Use master user's api key only.
        /// The API key must have one of the below permissions in order to call this endpoint..
        /// master API key: "Account Transfer", "Subaccount Transfer", "Withdrawal"
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="memberType"></param>
        /// <param name="switch"></param>
        /// <param name="isUta"></param>
        /// <param name="note"></param>
        /// <returns>new user member</returns>
        public async Task<string?> CreateUserSubMember(string username, MemberType memeberType, string? password = null, SwitchLogin? switchLogin = null, IsUta? isUta = null, string? note = null)
        {
            var query = new Dictionary<string, object>
                        {
                            { "username", username },
                            { "memeberType", memeberType.Value },
                        };

            BybitParametersUtils.AddOptionalParameters(query,
                ("password", password),
                ("switch", switchLogin?.Value),
                ("isUta", isUta?.Value),
                ("note", note)
            );
            var result = await this.SendSignedAsync<string>(CREATE_SUB_USER, HttpMethod.Post, query: query);
            return result;
        }

        private const string SUB_UID_LIST = "/v5/user/create-sub-member";
        /// <summary>
        /// Get all sub UID of master account. Use master user's api key only.
        /// The API key must have one of the below permissions in order to call this endpoint..
        /// master API key: "Account Transfer", "Subaccount Transfer", "Withdrawal"
        /// </summary>
        /// <returns>sub member list</returns>
        public async Task<string?> GetUserSubUidList()
        {
            var query = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<string>(SUB_UID_LIST, HttpMethod.Get, query: query);
            return result;
        }

        private const string FROZEN_SUB_MEMBER = "/v5/user/frozen-sub-member";
        /// <summary>
        /// Freeze Sub UID. Use master user's api key only.
        /// The API key must have one of the below permissions in order to call this endpoint..
        /// master API key: "Account Transfer", "Subaccount Transfer", "Withdrawal"
        /// </summary>
        /// <param name="subuid"></param>
        /// <param name="userFrozen"></param>
        /// <returns>None</returns>
        public async Task<string?> FrozenUserSubMemeber(int subuid, UserFrozen userFrozen)
        {
            var query = new Dictionary<string, object> { { "subuid", subuid }, { "userFrozen", userFrozen.Value } };
            var result = await this.SendSignedAsync<string>(FROZEN_SUB_MEMBER, HttpMethod.Post, query: query);
            return result;
        }

        private const string API_kEY_INFO = "/v5/user/query-api";
        /// <summary>
        /// Get the information of the api key. Use the api key pending to be checked to call the endpoint. Both master and sub user's api key are applicable.
        /// Any permission can access this endpoint.
        /// </summary>
        /// <returns>Get API Key Information</returns>
        public async Task<string?> GetUserApiKeyInfo()
        {
            var query = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<string>(API_kEY_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string UID_WALLET_TYPE = "/v5/user/query-api";
        /// <summary>
        /// Get available wallet types for the master account or sub account
        /// Master api key: you can get master account and appointed sub account available wallet types, and support up to 200 sub UID in one request.
        /// Sub api key: you can get its own available wallet types
        /// "FUND" - If you never deposit or transfer capital into it, this wallet type will not be shown in the array, but your account indeed has this wallet.
        /// ["SPOT", "OPTION", "FUND", "CONTRACT"] : Classic account and Funding wallet was operated before
        /// ["SPOT","OPTION","CONTRACT"] : Classic account and Funding wallet is never operated
        /// ["SPOT","UNIFIED","FUND","CONTRACT"] : UMA account and Funding wallet was operated before. (No UMA account after we forced upgrade to UTA)
        /// ["SPOT","UNIFIED","CONTRACT"] : UMA account and Funding wallet is never operated. (No UMA account after we forced upgrade to UTA)
        /// ["UNIFIED""FUND","CONTRACT"] : UTA account and Funding wallet was operated before.
        /// ["UNIFIED","CONTRACT"] : UTA account and Funding wallet is never operated.
        /// </summary>
        /// <param name="memberIds"></param>
        /// <returns>Get UID Wallet Type</returns>
        public async Task<string?> GetUsersWalletType(string? memberIds = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("memberIds", memberIds)
            );
            var result = await this.SendSignedAsync<string>(UID_WALLET_TYPE, HttpMethod.Get, query: query);
            return result;
        }

        private const string DELETE_MASTER_KEY = "/v5/user/delete-api";
        /// <summary>
        /// Delete the api key of master account. Use the api key pending to be delete to call the endpoint. Use master user's api key only.
        /// The API key must have one of the below permissions in order to call this endpoint..
        /// master API key: "Account Transfer", "Subaccount Transfer", "Withdrawal"
        /// BE CAREFUL! The API key used to call this interface will be invalid immediately.
        /// </summary>
        /// <returns>None</returns>
        public async Task<string?> DeleteUserMasterKey()
        {
            var query = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<string>(DELETE_MASTER_KEY, HttpMethod.Post, query: query);
            return result;
        }

        private const string DELETE_SUB_KEY = "/v5/user/delete-sub-api";
        /// <summary>
        /// Delete the api key of sub account. Use the sub api key pending to be delete to call the endpoint or use the master api key to delete corresponding sub account api key
        /// The API key must have one of the below permissions in order to call this endpoint.
        /// sub API key: "Account Transfer", "Sub Member Transfer"
        /// master API Key: "Account Transfer", "Sub Member Transfer", "Withdrawal"
        /// BE CAREFUL! The Sub account API key will be invalid immediately after calling the endpoint.
        /// </summary>
        /// <param name="apikey"></param>
        /// <returns>None</returns>
        public async Task<string?> DeleteSubUserKey(string? apikey)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("apikey", apikey)
            );
            var result = await this.SendSignedAsync<string>(DELETE_SUB_KEY, HttpMethod.Post, query: query);
            return result;
        }

        private const string AFFILIATE_USER_INFO = "/v5/user/aff-customer-info";
        /// <summary>
        /// This API is used for affiliate to get their users information
        /// Use master UID only
        /// The api key can only have "Affiliate" permission
        /// The transaction volume and deposit amount are the total amount of the user done on Bybit, and have nothing to do with commission settlement.Any transaction volume data related to commission settlement is subject to the Affiliate Portal.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>Get Affiliate User Info</returns>
        public async Task<string?> GetAffiliateUserInfo(string uid)
        {
            var query = new Dictionary<string, object> { { "uid", uid } };
            var result = await this.SendSignedAsync<string>(AFFILIATE_USER_INFO, HttpMethod.Get, query: query);
            return result;
        }
        #endregion
    }
}
