using bybit.net.api.Models;
using bybit.net.api.Models.Account;
using bybit.net.api.Models.Asset;
using bybit.net.api.Models.Position;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitAssetService : BybitApiService
    {
        public BybitAssetService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitAssetService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
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
        public async Task<string?> GetCoinExchangeRecords(string? fromCoin = null, string? toCoin = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("fromCoin", fromCoin),
                ("toCoin", toCoin),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(COIN_EXCHANGE_RECORDS, HttpMethod.Get, query: query);
            return result;
        }

        private const string ASSET_DELIVERY_RECORDS = "/v5/asset/exchange/order-record";
        /// <summary>
        /// Query delivery records of USDC futures and Options, sorted by deliveryTime in descending order
        /// Unified account covers: USDC futures / Option
        /// </summary>
        /// <param name="category"></param>
        /// <param name="symbol"></param>
        /// <param name="expDate"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Delivery Records</returns>
        public async Task<string?> GetAssetDeliveryRecords(Category category, string? symbol = null, string? expDate = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("expDate", expDate),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(ASSET_DELIVERY_RECORDS, HttpMethod.Get, query: query);
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
        public async Task<string?> GetAssetUsdcSettlement(Category category, string? symbol = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { { "category", category.Value } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("symbol", symbol),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(USDC_SETTLEMENT, HttpMethod.Get, query: query);
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
        public async Task<string?> GetInternalTransferableCoins(AccountType accountType, AccountType toAccountType)
        {
            var query = new Dictionary<string, object> { { "accountType", accountType.Value }, { "toAccountType", toAccountType.Value } };
            var result = await this.SendSignedAsync<string>(TRANSABLE_COIN, HttpMethod.Get, query: query);
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
        public async Task<string?> CreateInternalTransfer(AccountType accountType, AccountType toAccountType, string coin, string amount, string? transferId = null)
        {
            var query = new Dictionary<string, object> { { "accountType", accountType.Value }, { "toAccountType", toAccountType.Value }, { "coin", coin }, { "amount", amount } };
            if (string.IsNullOrEmpty(transferId)) transferId = BybitParametersUtils.GenerateTransferId();
            query.Add("transferId", transferId);
            var result = await this.SendSignedAsync<string>(INTERNAL_TRANSFER, HttpMethod.Post, query: query);
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
        public async Task<string?> CreateUniversalTransfer(AccountType accountType, AccountType toAccountType, string fromMemberId, string toMemberId, string coin, string amount, string? transferId = null)
        {
            var query = new Dictionary<string, object> { { "accountType", accountType.Value }, { "toAccountType", toAccountType.Value }, { "fromMemberId", fromMemberId }, { "toMemberId", toMemberId }, { "coin", coin }, { "amount", amount } };
            if (string.IsNullOrEmpty(transferId)) transferId = BybitParametersUtils.GenerateTransferId();
            query.Add("transferId", transferId);
            var result = await this.SendSignedAsync<string>(UNIVERSAL_TRANSFER, HttpMethod.Post, query: query);
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
        public async Task<string?> GetInternalTransferRecords(string? transferId = null, string? coin = null, TransferStatus? transferStatus = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("transferId", transferId),
                ("coin", coin),
                ("transferStatus", transferStatus?.Value),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(INTERNAL_TRANSFER_RECORDS, HttpMethod.Get, query: query);
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
        public async Task<string?> GetUniversalTransferRecords(string? transferId = null, string? coin = null, TransferStatus? transferStatus = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("transferId", transferId),
                ("coin", coin),
                ("transferStatus", transferStatus?.Value),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(UNIVERSAL_TRANSFER_RECORDS, HttpMethod.Get, query: query);
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
        public async Task<string?> GetAssetAllowedDepositInfo(string? chain = null, string? coin = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("chain", chain),
                ("coin", coin),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(ALLOW_DEPOSIT_COINS, HttpMethod.Get, query: query);
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

        private const string DEPOSIT_RECORDS = "/v5/asset/deposit/deposit-to-account";
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

        private const string INTERNAL_DEPOSIT_RECORDS = "/v5/asset/deposit/query-sub-member-record";
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

        private const string ASSET_WITHDRAW = "/v5/asset/withdraw/create";
        /// <summary>
        /// Withdraw assets from your Bybit account. You can make an off-chain transfer if the target wallet address is from Bybit. This means that no blockchain fee will be charged.
        /// UTA does not have SPOT account
        /// How do I know if my account is a UTA account? Call this endpoint, and if uta=1, then it is a UTA account.
        /// Make sure you have whitelisted your wallet address here
        /// Can query by the master UID's api key only
        /// feeType = 0:
        /// withdrawPercentageFee != 0: handlingFee = inputAmount / (1 - withdrawPercentageFee) * withdrawPercentageFee + withdrawFee
        /// withdrawPercentageFee = 0: handlingFee = withdrawFee
        /// feeType = 1:
        /// withdrawPercentageFee != 0: handlingFee = withdrawFee + (inputAmount - withdrawFee) * withdrawPercentageFee
        /// withdrawPercentageFee = 0: handlingFee = withdrawFee
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="chain"></param>
        /// <param name="address"></param>
        /// <param name="amount"></param>
        /// <param name="timestamp"></param>
        /// <param name="tag"></param>
        /// <param name="forceChain"></param>
        /// <param name="accountType"></param>
        /// <param name="feeType"></param>
        /// <returns>id</returns>
        public async Task<string?> PlaceAssetWithdraw(string coin, string chain, string address, string amount, long timestamp, string? tag = null, int? forceChain = null, AccountType? accountType = null, FeeType? feeType = null)
        {
            var query = new Dictionary<string, object> { { "coin", coin }, { "chain", chain }, { "address", address }, { "amount", amount }, { "timestamp", timestamp } };
            BybitParametersUtils.AddOptionalParameters(query,
                ("tag", tag),
                ("forceChain", forceChain),
                ("accountType", accountType?.Value),
                ("feeType", feeType?.Value)
            );
            var result = await this.SendSignedAsync<string>(ASSET_WITHDRAW, HttpMethod.Post, query: query);
            return result;
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
