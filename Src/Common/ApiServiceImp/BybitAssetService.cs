using bybit.net.api.Models;
using bybit.net.api.Models.Account;
using bybit.net.api.Models.Asset;
using bybit.net.api.Models.Position;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitAssetService : BybitApiService
    {
        public BybitAssetService(string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitAssetService(HttpClient httpClient, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitAssetService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitAssetService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string GET_DELIVERY_RECORD = "/v5/asset/delivery-record";

        /// <summary>
        /// Get Delivery Record
        /// Query delivery records for Futures and Options. Sorted by deliveryTime desc.
        /// </summary>
        /// <param name="category">UTA2.0: inverse, linear, option. UTA1.0: linear, option</param>
        /// <param name="symbol">optional</param>
        /// <param name="startTime">ms</param>
        /// <param name="endTime">ms</param>
        /// <param name="expDate">e.g., 25MAR22</param>
        /// <param name="limit">[1,50], default 20</param>
        /// <param name="cursor">pagination cursor</param>
        /// <returns></returns>
        public async Task<GeneralResponse<GetDeliveryRecordResult>?> GetDeliveryRecord(
            string category,
            string? symbol = null,
            long? startTime = null,
            long? endTime = null,
            string? expDate = null,
            int? limit = null,
            string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("startTime", startTime),
                ("endTime", endTime),
                ("expDate", expDate),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetDeliveryRecordResult>>(GET_DELIVERY_RECORD, HttpMethod.Get, query: query);
            return result;
        }


        private const string COIN_EXCHANGE_RECORDS = "/v5/asset/exchange/order-record";
        /// <summary>
        ///  Query the coin exchange records.
        /// This endpoint currently is not available to get data after 12 Mar 2023. We will make it fully available later.
        /// CAUTION : You may have a long delay of this endpoint.
        /// </summary>
        /// <param name="fromCoin"></param>
        /// <param name="toCoin"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Coin Exchange Records</returns>
        public async Task<GeneralResponse<GetCoinExchangeRecordsResult>?> GetCoinExchangeRecords(string? fromCoin = null, string? toCoin = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("fromCoin", fromCoin),
                ("toCoin", toCoin),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<GeneralResponse<GetCoinExchangeRecordsResult>>(COIN_EXCHANGE_RECORDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string USDC_SETTLEMENT = "/v5/asset/settlement-record";
        /// <summary>
        /// Query session settlement records of USDC perpetual and futures
        /// Unified account covers: USDC perpetual / USDC futures
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Usdc Settlements</returns>
        public async Task<GeneralResponse<GetUsdcSettlementResult>?> GetAssetUsdcSettlement(Category category, string? symbol = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<GeneralResponse<GetUsdcSettlementResult>>(USDC_SETTLEMENT, HttpMethod.Get, query: query);
            return result;
        }

        private const string ASSET_INFO = "/v5/asset/transfer/query-asset-info";
        /// <summary>
        /// For now, it can query SPOT only.
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="coin"></param>
        /// <returns>Asset Info</returns>
        public async Task<string?> GetAssetInfo(AccountType accountType, string? coin = null)
        {
            var query = new Dictionary<string, object> { { "accountType", accountType.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );
            var result = await this.SendSignedAsync<string>(ASSET_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string ALL_ASSETS_INFO = "/v5/asset/transfer/query-account-coins-balance";
        /// <summary>
        /// You could get all coin balance of all account types under the master account, and sub account.
        /// It is not allowed to get master account coin balance via sub account api key.
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="coin"></param>
        /// <param name="memberId"></param>
        /// <param name="withBonus"></param>
        /// <returns>All assets info</returns>
        public async Task<string?> GetAllAssetBalance(AccountType accountType, string? coin = null, string? memberId = null, WithBonus? withBonus = null)
        {
            var query = new Dictionary<string, object> { { "accountType", accountType.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("memberId", memberId),
                ("withBonus", withBonus?.Value)
            );
            var result = await this.SendSignedAsync<string>(ALL_ASSETS_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string SINGLE_ASSET_INFO = "/v5/asset/transfer/query-account-coin-balance";
        /// <summary>
        /// Query the balance of a specific coin in a specific account type. Supports querying sub UID's balance. Also, you can check the transferable amount from master to sub account, sub to master account or sub to sub account, especially for user who has INS loan.
        /// Sub account cannot query master account balance
        /// Sub account can only check its own balance
        /// Master account can check its own and its sub UIDs balance
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="coin"></param>
        /// <param name="toAccountType"></param>
        /// <param name="memberId"></param>
        /// <param name="toMemberId"></param>
        /// <param name="withBonus"></param>
        /// <param name="withTransferSafeAmount"></param>
        /// <param name="withLtvTransferSafeAmount"></param>
        /// <returns>Single Coin Balance</returns>
        public async Task<string?> GetSingleAssetBalance(AccountType accountType, string coin, AccountType? toAccountType = null, string? memberId = null, string? toMemberId = null, WithBonus? withBonus = null, WithBonus? withTransferSafeAmount = null, WithBonus? withLtvTransferSafeAmount = null)
        {
            var query = new Dictionary<string, object> { { "accountType", accountType.Value }, { "coin", coin } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("toAccountType", toAccountType?.Value),
                ("toMemberId", toMemberId),
                ("coin", coin),
                ("memberId", memberId),
                ("withBonus", withBonus?.Value),
                ("withTransferSafeAmount", withTransferSafeAmount?.Value),
                ("withLtvTransferSafeAmount", withLtvTransferSafeAmount?.Value)
            );
            var result = await this.SendSignedAsync<string>(SINGLE_ASSET_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string TRANSABLE_COIN = "/v5/asset/transfer/query-transfer-coin-list";
        /// <summary>
        /// Query the transferable coin list between each account type
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="toAccountType"></param>
        /// <returns>List of coin</returns>
        public async Task<GeneralResponse<GetTransferableCoinResult>?> GetTransferableCoin(AccountType accountType, AccountType toAccountType)
        {
            var query = new Dictionary<string, object> { { "fromAccountType", accountType.Value }, { "toAccountType", toAccountType.Value } };
            var result = await this.SendSignedAsync<GeneralResponse<GetTransferableCoinResult>>(TRANSABLE_COIN, HttpMethod.Get, query: query);
            return result;
        }

        private const string INTERNAL_TRANSFER = "/v5/asset/transfer/inter-transfer";
        /// <summary>
        /// Create the internal transfer between different account types under the same UID.
        /// Each account type has its own acceptable coins, e.g, you cannot transfer USDC from SPOT to CONTRACT.Please refer to transferable coin list API to find out more.
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="toAccountType"></param>
        /// <param name="transferId"></param>
        /// <param name="coin"></param>
        /// <param name="amount"></param>
        /// <returns>transferId</returns>
        public async Task<GeneralResponse<CreateTransferResult>?> CreateInternalTransfer(AccountType accountType, AccountType toAccountType, string coin, string amount, string? transferId = null)
        {
            var query = new Dictionary<string, object> { { "fromAccountType", accountType.Value }, { "toAccountType", toAccountType.Value }, { "coin", coin }, { "amount", amount } };
            if (string.IsNullOrEmpty(transferId)) transferId = BybitParametersUtils.GenerateTransferId();
            query.Add("transferId", transferId);
            var result = await this.SendSignedAsync<GeneralResponse<CreateTransferResult>>(INTERNAL_TRANSFER, HttpMethod.Post, query: query);
            return result;
        }

        private const string UNIVERSAL_TRANSFER = "/v5/asset/transfer/universal-transfer";
        /// <summary>
        /// Transfer between sub-sub or main-sub.
        /// Can use master or sub acct api key to request
        /// To use sub acct api key, it must have "SubMemberTransferList" permission
        /// When use sub acct api key, it can only transfer to main account
        /// If you encounter errorCode: 131228 and msg: your balance is not enough, please go to Get Single Coin Balance to check transfer safe amount.
        /// You can not transfer between the same UID
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="toAccountType"></param>
        /// <param name="fromMemberId"></param>
        /// <param name="toMemberId"></param>
        /// <param name="coin"></param>
        /// <param name="amount"></param>
        /// <returns>transferId</returns>
        public async Task<GeneralResponse<CreateTransferResult>?> CreateUniversalTransfer(AccountType accountType, AccountType toAccountType, string fromMemberId, string toMemberId, string coin, string amount, string? transferId = null)
        {
            var query = new Dictionary<string, object> { { "fromAccountType", accountType.Value }, { "toAccountType", toAccountType.Value }, { "fromMemberId", fromMemberId }, { "toMemberId", toMemberId }, { "coin", coin }, { "amount", amount } };
            if (string.IsNullOrEmpty(transferId)) transferId = BybitParametersUtils.GenerateTransferId();
            query.Add("transferId", transferId);
            var result = await this.SendSignedAsync<GeneralResponse<CreateTransferResult>>(UNIVERSAL_TRANSFER, HttpMethod.Post, query: query);
            return result;
        }

        private const string INTERNAL_TRANSFER_RECORDS = "/v5/asset/transfer/query-inter-transfer-list";
        /// <summary>
        /// Query the internal transfer records between different account types under the same UID.
        /// </summary>
        /// <param name="transferId"></param>
        /// <param name="coin"></param>
        /// <param name="transferStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Internal Transfers</returns>
        public async Task<GeneralResponse<GetTransferRecordsResult>?> GetInternalTransferRecords(string? transferId = null, string? coin = null, TransferStatus? transferStatus = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("transferId", transferId),
                ("coin", coin),
                ("status", transferStatus?.Value),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<GeneralResponse<GetTransferRecordsResult>>(INTERNAL_TRANSFER_RECORDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string UNIVERSAL_TRANSFER_RECORDS = "/v5/asset/transfer/query-universal-transfer-list";
        /// <summary>
        /// Query universal transfer records
        /// Main acct api key or Sub acct api key are both supported
        /// Main acct api key needs "SubMemberTransfer" permission
        /// Sub acct api key needs "SubMemberTransferList" permission
        /// </summary>
        /// <param name="transferId"></param>
        /// <param name="coin"></param>
        /// <param name="transferStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Universal Transfers</returns>
        public async Task<GeneralResponse<GetTransferRecordsResult>?> GetUniversalTransferRecords(string? transferId = null, string? coin = null, TransferStatus? transferStatus = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("transferId", transferId),
                ("coin", coin),
                ("status", transferStatus?.Value),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<GeneralResponse<GetTransferRecordsResult>>(UNIVERSAL_TRANSFER_RECORDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string SUB_UIDS = "/v5/asset/transfer/query-sub-member-list";
        /// <summary>
        /// Query the sub UIDs under a main UID
        /// Can query by the master UID's api key only
        /// </summary>
        /// <returns>List Sub uid</returns>
        public async Task<string?> GetAssetSubMembers()
        {
            var query = new Dictionary<string, object> { };
            var result = await this.SendSignedAsync<string>(SUB_UIDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string ALLOW_DEPOSIT_COINS = "/v5/asset/deposit/query-allowed-list";
        /// <summary>
        /// Query allowed deposit coin information. To find out paired chain of coin, please refer coin info api.
        /// This is an endpoint that does not need authentication
        /// </summary>
        /// <param name="chain"></param>
        /// <param name="coin"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Deposited Coin Info</returns>
        public async Task<GeneralResponse<GetAllowedDepositCoinInfoResult>?> GetAssetAllowedDepositInfo(string? chain = null, string? coin = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("chain", chain),
                ("coin", coin),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendPublicAsync<GeneralResponse<GetAllowedDepositCoinInfoResult>>(ALLOW_DEPOSIT_COINS, HttpMethod.Get, query: query);
            return result;
        }

        private const string SET_DEPOSIT_ACCOUNT = "/v5/asset/deposit/deposit-to-account";
        /// <summary>
        /// Set auto transfer account after deposit. The same function as the setting for Deposit on web GUI
        /// Your funds will be deposited into FUND wallet by default. You can set the wallet for auto-transfer after deposit by this API.
        /// Only main UID can access.
        /// Unified trading account has FUND, UNIFIED, CONTRACT(for inverse derivatives)
        /// Unified margin account has FUND, UNIFIED, CONTRACT(for inverse derivatives), SPOT
        /// Classic account has FUND, OPTION(USDC account), CONTRACT(for inverse derivatives and derivatives), SPOT
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns>status</returns>
        public async Task<string?> SetAssetDepositAccount(AccountType accountType)
        {
            var query = new Dictionary<string, object> { { "accountType", accountType.Value } };
            var result = await this.SendSignedAsync<string>(SET_DEPOSIT_ACCOUNT, HttpMethod.Post, query: query);
            return result;
        }

        private const string DEPOSIT_RECORDS = "/v5/asset/deposit/query-record";
        /// <summary>
        /// endTime - startTime should be less than 30 days. Query last 30 days records by default.
        /// Can use main or sub UID api key to query deposit records respectively.
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>deposit records</returns>
        public async Task<string?> GetAssetDepositRecords(string? coin = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(DEPOSIT_RECORDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string SUB_DEPOSIT_RECORDS = "/v5/asset/deposit/query-sub-member-record";
        /// <summary>
        /// endTime - startTime should be less than 30 days. Queries for the last 30 days worth of records by default.
        /// </summary>
        /// <param name="subMemberId"></param>
        /// <param name="coin"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Query subaccount's deposit records by main UID's API key.</returns>
        public async Task<string?> GetAssetSubDepositRecords(string subMemberId, string? coin = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "subMemberId", subMemberId } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(SUB_DEPOSIT_RECORDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string INTERNAL_DEPOSIT_RECORDS = "/v5/asset/deposit/query-internal-record";
        /// <summary>
        /// The maximum difference between the start time and the end time is 30 days.
        /// Support to get deposit records by Master or Sub Member Api Key
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Query deposit records within the Bybit platform. These transactions are not on the blockchain.</returns>
        public async Task<string?> GetAssetInternalDepositRecords(string? coin = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(INTERNAL_DEPOSIT_RECORDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string MASTER_DEPOSIT_ADDRESS = "/v5/asset/deposit/query-address";
        /// <summary>
        /// Query the deposit address information of MASTER account.
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="chainType"></param>
        /// <returns>Master Deposit Address</returns>
        public async Task<string?> GetAssetMasterDepositAddress(string coin, string? chainType = null)
        {
            var query = new Dictionary<string, object> { { "coin", coin } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("chainType", chainType)
            );
            var result = await this.SendSignedAsync<string>(MASTER_DEPOSIT_ADDRESS, HttpMethod.Get, query: query);
            return result;
        }

        private const string SUB_DEPOSIT_ADDRESS = "/v5/asset/deposit/query-sub-member-address";
        /// <summary>
        /// Query the deposit address information of SUB account.
        /// Can use master UID's api key only
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="chainType"></param>
        /// <param name="subMemberId"></param>
        /// <returns>Sub Deposit Address</returns>
        public async Task<string?> GetAssetSubDepositAddress(string coin, string chainType, string subMemberId)
        {
            var query = new Dictionary<string, object> { { "coin", coin }, { "chainType", chainType }, { "subMemberId", subMemberId } };
            var result = await this.SendSignedAsync<string>(SUB_DEPOSIT_ADDRESS, HttpMethod.Get, query: query);
            return result;
        }

        private const string COIN_INFO = "/v5/asset/coin/query-info";
        /// <summary>
        /// Query coin information, including chain information, withdraw and deposit status.
        /// </summary>
        /// <param name="coin"></param>
        /// <returns>Coin Info</returns>
        public async Task<string?> GetAssetCoinInfo(string? coin = null)
        {
            var query = new Dictionary<string, object> { };
            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );
            var result = await this.SendSignedAsync<string>(COIN_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_EXCHANGE_ENTITY_LIST = "/v5/asset/withdraw/vasp/list";

        /// <summary>
        /// Get Exchange Entity List
        /// Returns exchange entity info for withdrawals (e.g., for KOR KYC users).
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetExchangeEntityList()
        {
            var result = await this.SendSignedAsync<string>(GET_EXCHANGE_ENTITY_LIST, HttpMethod.Get);
            return result;
        }

        private const string WITHDRAW_RECORDS = "/v5/asset/withdraw/query-record";
        /// <summary>
        /// endTime - startTime should be less than 30 days. Query last 30 days records by default.
        /// Can query by the master UID's api key only
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="withdrawID"></param>
        /// <param name="withdrawType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Get Withdrawal Records</returns>
        public async Task<string?> GetAssetWithdrawRecords(string? coin = null, string? withdrawID = null, WithdrawType? withdrawType = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { };
            BybitParametersUtils.AddOptionalParameters(query,
                ("withdrawID", withdrawID),
                ("coin", coin),
                ("withdrawType", withdrawType?.Value),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(WITHDRAW_RECORDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string WITHDRAWABLE_AMOUNT = "/v5/asset/withdraw/withdrawable-amount";
        /// <summary>
        /// How can partial funds be subject to delayed withdrawal requests?
        /// On-chain deposit: If the number of on-chain confirmations has not reached a risk-controlled level, a portion of the funds will be frozen for a period of time until they are unfrozen.
        /// Buying crypto: If there is a risk, the funds will be frozen for a certain period of time and cannot be withdrawn.
        /// </summary>
        /// <param name="coin"></param>
        /// <returns>Get Withdrawable Amount</returns>
        public async Task<string?> GetAssetWithdrawableAmount(string coin)
        {
            var query = new Dictionary<string, object> { { "coin", coin } };
            var result = await this.SendSignedAsync<string>(WITHDRAWABLE_AMOUNT, HttpMethod.Get, query: query);
            return result;
        }

        private const string CREATE_WITHDRAW = "/v5/asset/withdraw/create";

        /// <summary>
        /// Withdraw
        /// Create a withdrawal. If the target is a Bybit internal address, the system may perform an off-chain transfer.
        /// chain is required when forceChain=0 or 1. For forceChain=2, address must be a Bybit main UID.
        /// </summary>
        /// <param name="coin">uppercase</param>
        /// <param name="address">wallet address or UID when forceChain=2</param>
        /// <param name="amount">withdraw amount</param>
        /// <param name="timestamp">current ms timestamp for replay protection</param>
        /// <param name="accountType">FUND, UTA, FUND,UTA, or SPOT</param>
        /// <param name="chain">required if forceChain=0 or 1</param>
        /// <param name="tag">required if address uses tag/memo</param>
        /// <param name="forceChain">0 default internal if possible, 1 force on-chain, 2 use UID</param>
        /// <param name="feeType">0 input is net received, 1 auto-deduct fee</param>
        /// <param name="requestId">idempotency key</param>
        /// <param name="beneficiary">travel rule info</param>
        /// <returns></returns>
        public async Task<string?> PlaceAssetWithdraw(
            string coin,
            string address,
            string amount,
            long timestamp,
            string accountType,
            string? chain = null,
            string? tag = null,
            int? forceChain = null,
            int? feeType = null,
            string? requestId = null,
            Beneficiary? beneficiary = null)
        {
            var body = new Dictionary<string, object>
            {
                { "coin", coin },
                { "address", address },
                { "amount", amount },
                { "timestamp", timestamp },
                { "accountType", accountType }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("chain", chain),
                ("tag", tag),
                ("forceChain", forceChain),
                ("feeType", feeType),
                ("requestId", requestId)
            );

            if (beneficiary != null)
                body["beneficiary"] = beneficiary;

            var result = await this.SendSignedAsync<string>(CREATE_WITHDRAW, HttpMethod.Post, query: body);
            return result;
        }

        private const string REQUEST_QUOTE = "/v5/asset/exchange/quote-apply";

        /// <summary>
        /// Request a Quote
        /// Apply for a convert quote. Expires in ~15 seconds.
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="fromCoin"></param>
        /// <param name="toCoin"></param>
        /// <param name="requestCoin">Same as fromCoin</param>
        /// <param name="requestAmount">Amount to sell</param>
        /// <param name="fromCoinType">crypto</param>
        /// <param name="toCoinType">crypto</param>
        /// <param name="paramType">opFrom for broker</param>
        /// <param name="paramValue">Broker ID</param>
        /// <param name="requestId">Custom ID, max 36</param>
        /// <returns></returns>
        public async Task<string?> RequestQuote(
            string accountType,
            string fromCoin,
            string toCoin,
            string requestCoin,
            string requestAmount,
            string? fromCoinType = null,
            string? toCoinType = null,
            string? paramType = null,
            string? paramValue = null,
            string? requestId = null)
        {
            var body = new Dictionary<string, object>
            {
                { "accountType", accountType },
                { "fromCoin", fromCoin },
                { "toCoin", toCoin },
                { "requestCoin", requestCoin },
                { "requestAmount", requestAmount }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("fromCoinType", fromCoinType),
                ("toCoinType", toCoinType),
                ("paramType", paramType),
                ("paramValue", paramValue),
                ("requestId", requestId)
            );

            var result = await this.SendSignedAsync<string>(REQUEST_QUOTE, HttpMethod.Post, query: body);
            return result;
        }

        private const string CONFIRM_QUOTE = "/v5/asset/exchange/convert-execute";

        /// <summary>
        /// Confirm a Quote
        /// Confirm the convert quote before it expires. Exchange is async; check final status via the query API.
        /// </summary>
        /// <param name="quoteTxId">Quote transaction ID from Request a Quote</param>
        /// <returns></returns>
        public async Task<string?> ConfirmQuote(string quoteTxId)
        {
            var body = new Dictionary<string, object>
            {
                { "quoteTxId", quoteTxId }
            };

            var result = await this.SendSignedAsync<string>(CONFIRM_QUOTE, HttpMethod.Post, query: body);
            return result;
        }

        private const string GET_CONVERT_HISTORY = "/v5/asset/exchange/query-convert-history";

        /// <summary>
        /// Get Convert History
        /// Returns confirmed convert quotes created via API.
        /// </summary>
        /// <param name="accountType">Comma-separated wallet types or null for all</param>
        /// <param name="index">Page number, starts from 1</param>
        /// <param name="limit">Page size, up to 100</param>
        /// <returns></returns>
        public async Task<string?> GetConvertHistory(string? accountType = null, int? index = null, int? limit = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("accountType", accountType),
                ("index", index),
                ("limit", limit)
            );

            var result = await this.SendSignedAsync<string>(GET_CONVERT_HISTORY, HttpMethod.Get, query: query);
            return result;
        }


        private const string GET_CONVERT_STATUS = "/v5/asset/exchange/convert-result-query";

        /// <summary>
        /// Get Convert Status
        /// Query the exchange result by quoteTxId.
        /// </summary>
        /// <param name="quoteTxId">Quote tx ID</param>
        /// <param name="accountType">Wallet type</param>
        /// <returns></returns>
        public async Task<string?> GetConvertStatus(string quoteTxId, string accountType)
        {
            var query = new Dictionary<string, object>
            {
                { "quoteTxId", quoteTxId },
                { "accountType", accountType }
            };

            var result = await this.SendSignedAsync<string>(GET_CONVERT_STATUS, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_CONVERT_COIN_LIST = "/v5/asset/exchange/query-coin-list";

        /// <summary>
        /// Get Convert Coin List
        /// Query the list of coins available to convert to/from.
        /// </summary>
        /// <param name="accountType">
        /// eb_convert_funding | eb_convert_uta | eb_convert_spot | eb_convert_contract | eb_convert_inverse
        /// </param>
        /// <param name="coin">
        /// From-coin when side=1 filters toCoin list; ignored when side=0
        /// </param>
        /// <param name="side">0: fromCoin list, 1: toCoin list</param>
        /// <returns></returns>
        public async Task<string?> GetConvertCoinList(string accountType, string? coin = null, int? side = null)
        {
            var query = new Dictionary<string, object> { { "accountType", accountType } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("side", side)
            );

            var result = await this.SendSignedAsync<string>(GET_CONVERT_COIN_LIST, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_FUNDING_HISTORY = "/v5/asset/fundinghistory";

        public async Task<GeneralResponse<GetFundingHistoryResult>?> GetFundingAccountTransactionHistory(string? createTimeFrom = null, string? createTimeTo = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("createTimeFrom", createTimeFrom),
                ("createTimeTo", createTimeTo),
                ("limit", limit),
                ("cursor", cursor)
            );

            return await this.SendSignedAsync<GeneralResponse<GetFundingHistoryResult>>(GET_FUNDING_HISTORY, HttpMethod.Get, query: query);
        }

        private const string GET_PORTFOLIO_MARGIN_INFO = "/v5/asset/portfolio-margin";

        public async Task<GeneralResponse<GetPortfolioMarginInfoResult>?> GetPortfolioMarginInfo(string? baseCoin = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("baseCoin", baseCoin)
            );

            return await this.SendSignedAsync<GeneralResponse<GetPortfolioMarginInfoResult>>(GET_PORTFOLIO_MARGIN_INFO, HttpMethod.Get, query: query);
        }

        private const string GET_TOTAL_MEMBERS_ASSETS = "/v5/asset/total-members-assets";

        public async Task<GeneralResponse<GetTotalMembersAssetsResult>?> GetTotalMembersAssets(string? coin = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );

            return await this.SendSignedAsync<GeneralResponse<GetTotalMembersAssetsResult>>(GET_TOTAL_MEMBERS_ASSETS, HttpMethod.Get, query: query);
        }

        private const string GET_ASSET_OVERVIEW = "/v5/asset/asset-overview";

        public async Task<GeneralResponse<GetAssetOverviewResult>?> GetAssetOverview(string? memberId = null, string? valuationCurrency = null, string? accountType = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("memberId", memberId),
                ("valuationCurrency", valuationCurrency),
                ("accountType", accountType)
            );

            return await this.SendSignedAsync<GeneralResponse<GetAssetOverviewResult>>(GET_ASSET_OVERVIEW, HttpMethod.Get, query: query);
        }

        private const string GET_WITHDRAWAL_ADDRESS_LIST = "/v5/asset/withdraw/query-address";

        public async Task<GeneralResponse<GetWithdrawalAddressListResult>?> GetWithdrawalAddressList(string? coin = null, string? chain = null, int? addressType = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("chain", chain),
                ("addressType", addressType),
                ("limit", limit),
                ("cursor", cursor)
            );

            return await this.SendSignedAsync<GeneralResponse<GetWithdrawalAddressListResult>>(GET_WITHDRAWAL_ADDRESS_LIST, HttpMethod.Get, query: query);
        }

        private const string GET_SMALL_BALANCE_COINS = "/v5/asset/covert/small-balance-list";

        public async Task<GeneralResponse<GetSmallBalanceCoinsResult>?> GetSmallBalanceCoins(string accountType, string? fromCoin = null)
        {
            var query = new Dictionary<string, object>
            {
                { "accountType", accountType }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("fromCoin", fromCoin)
            );

            return await this.SendSignedAsync<GeneralResponse<GetSmallBalanceCoinsResult>>(GET_SMALL_BALANCE_COINS, HttpMethod.Get, query: query);
        }

        private const string REQUEST_SMALL_BALANCE_QUOTE = "/v5/asset/covert/get-quote";

        public async Task<GeneralResponse<RequestSmallBalanceQuoteResult>?> RequestSmallBalanceQuote(string accountType, List<string> fromCoinList, string toCoin)
        {
            var body = new Dictionary<string, object>
            {
                { "accountType", accountType },
                { "fromCoinList", fromCoinList },
                { "toCoin", toCoin }
            };

            return await this.SendSignedAsync<GeneralResponse<RequestSmallBalanceQuoteResult>>(REQUEST_SMALL_BALANCE_QUOTE, HttpMethod.Post, query: body);
        }

        private const string CONFIRM_SMALL_BALANCE_QUOTE = "/v5/asset/covert/small-balance-execute";

        public async Task<GeneralResponse<ConfirmSmallBalanceQuoteResult>?> ConfirmSmallBalanceQuote(string quoteId)
        {
            var body = new Dictionary<string, object>
            {
                { "quoteId", quoteId }
            };

            return await this.SendSignedAsync<GeneralResponse<ConfirmSmallBalanceQuoteResult>>(CONFIRM_SMALL_BALANCE_QUOTE, HttpMethod.Post, query: body);
        }

        private const string GET_SMALL_BALANCE_HISTORY = "/v5/asset/covert/small-balance-history";

        public async Task<GeneralResponse<GetSmallBalanceExchangeHistoryResult>?> GetSmallBalanceExchangeHistory(string? accountType = null, string? quoteId = null, string? startTime = null, string? endTime = null, string? cursor = null, string? size = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("accountType", accountType),
                ("quoteId", quoteId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("cursor", cursor),
                ("size", size)
            );

            return await this.SendSignedAsync<GeneralResponse<GetSmallBalanceExchangeHistoryResult>>(GET_SMALL_BALANCE_HISTORY, HttpMethod.Get, query: query);
        }

        private const string GET_FIAT_BALANCE = "/v5/fiat/balance-query";

        public async Task<GeneralResponse<List<GetFiatBalanceResult>>?> GetFiatBalance(string? currency = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("currency", currency)
            );

            return await this.SendSignedAsync<GeneralResponse<List<GetFiatBalanceResult>>>(GET_FIAT_BALANCE, HttpMethod.Get, query: query);
        }

        private const string GET_FIAT_TRADING_PAIR_LIST = "/v5/fiat/query-coin-list";

        public async Task<GeneralResponse<GetFiatTradingPairListResult>?> GetFiatTradingPairList(string? side = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("side", side)
            );

            return await this.SendSignedAsync<GeneralResponse<GetFiatTradingPairListResult>>(GET_FIAT_TRADING_PAIR_LIST, HttpMethod.Get, query: query);
        }

        private const string REQUEST_FIAT_QUOTE = "/v5/fiat/quote-apply";

        public async Task<GeneralResponse<RequestFiatQuoteResult>?> RequestFiatQuote(string fromCoin, string fromCoinType, string toCoin, string toCoinType, string requestAmount, string? requestCoinType = null)
        {
            var body = new Dictionary<string, object>
            {
                { "fromCoin", fromCoin },
                { "fromCoinType", fromCoinType },
                { "toCoin", toCoin },
                { "toCoinType", toCoinType },
                { "requestAmount", requestAmount }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("requestCoinType", requestCoinType)
            );

            return await this.SendSignedAsync<GeneralResponse<RequestFiatQuoteResult>>(REQUEST_FIAT_QUOTE, HttpMethod.Post, query: body);
        }

        private const string CONFIRM_FIAT_QUOTE = "/v5/fiat/trade-execute";

        public async Task<GeneralResponse<ConfirmFiatQuoteResult>?> ConfirmFiatQuote(string quoteTxId, string subUserId, string? webhookUrl = null, string? merchantRequestId = null)
        {
            var body = new Dictionary<string, object>
            {
                { "quoteTxId", quoteTxId },
                { "subUserId", subUserId }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("webhookUrl", webhookUrl),
                ("MerchantRequestId", merchantRequestId)
            );

            return await this.SendSignedAsync<GeneralResponse<ConfirmFiatQuoteResult>>(CONFIRM_FIAT_QUOTE, HttpMethod.Post, query: body);
        }

        private const string GET_FIAT_CONVERT_STATUS = "/v5/fiat/trade-query";

        public async Task<GeneralResponse<GetFiatConvertStatusResult>?> GetFiatConvertStatus(string? tradeNo = null, string? merchantRequestId = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("tradeNo", tradeNo),
                ("merchantRequestId", merchantRequestId)
            );

            return await this.SendSignedAsync<GeneralResponse<GetFiatConvertStatusResult>>(GET_FIAT_CONVERT_STATUS, HttpMethod.Get, query: query);
        }

        private const string GET_FIAT_CONVERT_HISTORY = "/v5/fiat/query-trade-history";

        public async Task<GeneralResponse<List<GetFiatConvertStatusResult>>?> GetFiatConvertHistory(int? index = null, int? limit = null, string? startTime = null, string? endTime = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("index", index),
                ("limit", limit),
                ("startTime", startTime),
                ("endTime", endTime)
            );

            return await this.SendSignedAsync<GeneralResponse<List<GetFiatConvertStatusResult>>>(GET_FIAT_CONVERT_HISTORY, HttpMethod.Get, query: query);
        }

        private const string GET_FIAT_REFERENCE_PRICE = "/v5/fiat/reference-price";

        public async Task<GeneralResponse<GetFiatReferencePriceResult>?> GetFiatReferencePrice(string symbol)
        {
            var query = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await this.SendSignedAsync<GeneralResponse<GetFiatReferencePriceResult>>(GET_FIAT_REFERENCE_PRICE, HttpMethod.Get, query: query);
        }



        private const string CANCEL_WITHDRAW = "/v5/asset/withdraw/cancel";
        /// <summary>
        /// Cancel the withdrawal
        /// Can query by the master UID's api key only
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status</returns>
        public async Task<string?> CancelAssetWithdrawal(string id)
        {
            var query = new Dictionary<string, object> { { "id", id } };
            var result = await this.SendSignedAsync<string>(CANCEL_WITHDRAW, HttpMethod.Post, query: query);
            return result;
        }
    }
}
