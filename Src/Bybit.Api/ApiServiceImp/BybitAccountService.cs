using Bybit.Api.Models;
using Bybit.Api.Models.Account;
using Bybit.Api.Models.Lending;
using Bybit.Api.Services;
using Bybit.Api.Utils;

namespace Bybit.Api.ApiServiceImp;

public class BybitAccountService : BybitApiService
{
    public BybitAccountService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
    {
    }

    public BybitAccountService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
    {
    }

    private const string WALLET_BALANCE = "/v5/account/wallet-balance";
    /// <summary>
    /// Obtain wallet balance, query asset information of each currency, and account risk rate information. By default, currency information with assets or liabilities of 0 is not returned.
    /// The trading of UTA inverse contracts is conducted through the CONTRACT wallet.
    /// To get Funding wallet balance, please go to this endpoint
    /// </summary>
    /// <param name="accountType"></param>
    /// <param name="coin"></param>
    /// <returns> Account Balance </returns>
    public Task<string?> GetAccountBalance(AccountType accountType, string? coin = null)
    {
        var query = new Dictionary<string, object> { { "accountType", accountType.Value } };

        BybitParametersUtils.AddOptionalParameters(query,
            ("coin", coin)
        );
        return SendSignedAsync<string>(WALLET_BALANCE, HttpMethod.Get, query: query);
    }

    private const string UPGRADE_UTA = "/v5/account/wallet-balance";
    /// <summary>
    /// Upgrade to Unified Account
    /// </summary>
    /// <returns> UTA Result</returns>
    public Task<string?> UpgradeAccount()
    {
        var query = new Dictionary<string, object>();

        return SendSignedAsync<string>(UPGRADE_UTA, HttpMethod.Post, query: query);
    }

    private const string ACCOUNT_BROWSE_HISTORY = "/v5/account/borrow-history";
    /// <summary>
    /// Get interest records, sorted in reverse order of creation time. UTA account
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="limit"></param>
    /// <param name="cursor"></param>
    /// <returns>Borrow History</returns>
    public Task<string?> GetAccountBorrowHistory(string? currency = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
    {
        var query = new Dictionary<string, object>();

        BybitParametersUtils.AddOptionalParameters(query,
            ("currency", currency),
            ("startTime", startTime),
            ("endTime", endTime),
            ("limit", limit),
            ("cursor", cursor)
        );
        return SendSignedAsync<string>(ACCOUNT_BROWSE_HISTORY, HttpMethod.Get, query: query);
    }

    private const string ACCOUNT_COLLATERAL_INFO = "/v5/account/collateral-info";
    /// <summary>
    /// Get the collateral information of the current unified margin account, including loan interest rate, loanable amount, collateral conversion rate, whether it can be mortgaged as margin, etc.
    /// </summary>
    /// <param name="currency"></param>
    /// <returns>Collateral Info</returns>
    public Task<string?> GetAccountCollateralInfo(string? currency = null)
    {
        var query = new Dictionary<string, object>();

        BybitParametersUtils.AddOptionalParameters(query,
            ("currency", currency)
        );
        return SendSignedAsync<string>(ACCOUNT_COLLATERAL_INFO, HttpMethod.Get, query: query);
    }

    private const string SET_COLLATERAL_COIN = "/v5/account/set-collateral-switch";
    /// <summary>
    /// You can decide whether the assets in the Unified account needs to be collateral coins.
    /// </summary>
    /// <returns>None</returns>
    public Task<string?> SetAccountCollateralCoin(string? coin, CollateralSwitch? collateralSwitch = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("coin", coin),
            ("collateralSwitch", collateralSwitch?.Value)
        );
        return SendSignedAsync<string>(SET_COLLATERAL_COIN, HttpMethod.Post, query: query);
    }

    private const string BATCH_SET_COLLATERAL_COIN = "/v5/account/set-collateral-switch-batch";
    /// <summary>
    /// Batch Set Collateral Coin
    /// </summary>
    /// <param name="request"></param>
    /// /// <param name="coin">Coin name</param>
    /// /// <param name="collateralSwitch">ON: switch on collateral, OFF: switch off collateral</param>
    /// <returns>Batch Set Collateral Coin</returns>
    public Task<string?> BatchSetAccountCollateralCoin(List<Dictionary<string, object>> request)
    {
        var query = new Dictionary<string, object>
        {
            { "request", request }
        };

        return SendSignedAsync<string>(BATCH_SET_COLLATERAL_COIN, HttpMethod.Post, query: query);
    }

    /// <summary>
    /// Batch Set Collateral Coin
    /// </summary>
    /// <param name="request"></param>
    /// /// <param name="coin">Coin name</param>
    /// /// <param name="collateralSwitch">ON: switch on collateral, OFF: switch off collateral</param>
    /// <returns>Batch Set Collateral Coin</returns>
    public Task<string?> BatchSetAccountCollateralCoin(List<SetCollateralCoinRequest> request)
    {
        var query = new Dictionary<string, object>
        {
            { "request", request }
        };

        return SendSignedAsync<string>(BATCH_SET_COLLATERAL_COIN, HttpMethod.Post, query: query);
    }

    private const string ACCOUNT_REPAY_LIABILITY = "/v5/account/quick-repayment";
    /// <summary>
    /// You can manually repay the liabilities of Unified account
    /// Applicable: Unified Account Permission: USDC Contracts
    /// </summary>
    /// <returns>Repay Liability</returns>
    public Task<string?> RepayAccountLiability(string? coin = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("coin", coin)
        );
        return SendSignedAsync<string>(ACCOUNT_REPAY_LIABILITY, HttpMethod.Post, query: query);
    }

    private const string COIN_GREEKS = "/v5/asset/coin-greeks";
    /// <summary>
    /// Get current account Greeks information
    /// </summary>
    /// <returns>Coin Greeks</returns>
    public Task<string?> GetAccountCoinGreeks(string? baseCoin = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("baseCoin", baseCoin)
        );
        return SendSignedAsync<string>(COIN_GREEKS, HttpMethod.Get, query: query);
    }

    private const string FREE_RATE = "/v5/account/fee-rate";
    /// <summary>
    /// Get the trading fee rate.
    /// Covers: Spot / USDT perpetual / USDC perpetual / USDC futures / Inverse perpetual / Inverse futures / Options
    /// </summary>
    /// <param name="category"></param>
    /// <param name="symbol"></param>
    /// <param name="baseCoin"></param>
    /// <returns>Free Rates</returns>
    public Task<string?> GetAccountFreeRate(Category category, string? symbol = null, string? baseCoin = null)
    {
        var query = new Dictionary<string, object> { { "category", category.Value } };

        BybitParametersUtils.AddOptionalParameters(query,
            ("symbol", symbol),
            ("baseCoin", baseCoin)
        );
        return SendSignedAsync<string>(FREE_RATE, HttpMethod.Get, query: query);
    }

    private const string ACCOUNT_INFO = "/v5/account/info";
    /// <summary>
    /// Query the margin mode configuration of the account.
    /// Query the margin mode and the upgraded status of account
    /// </summary>
    /// <returns></returns>
    public Task<string?> GetAccountInfo()
    {
        var query = new Dictionary<string, object>();

        return SendSignedAsync<string>(ACCOUNT_INFO, HttpMethod.Get, query: query);
    }

    private const string TRANSACTION_LOG = "/v5/account/transaction-log";
    /// <summary>
    /// Query transaction logs in Unified account.
    /// </summary>
    /// <param name="accountType"></param>
    /// <param name="category"></param>
    /// <param name="currency"></param>
    /// <param name="baseCoin"></param>
    /// <param name="type"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="limit"></param>
    /// <param name="cursor"></param>
    /// <returns>Transaction Data</returns>
    public Task<string?> GetAccountTransaction(AccountType? accountType = null, Category? category = null, string? currency = null, string? baseCoin = null, TransactionType? type = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
    {
        var query = new Dictionary<string, object>();

        BybitParametersUtils.AddOptionalParameters(query,
            ("accountType", accountType?.Value),
            ("category", category?.Value),
            ("currency", currency),
            ("baseCoin", baseCoin),
            ("type", type?.Value),
            ("startTime", startTime),
            ("endTime", endTime),
            ("limit", limit),
            ("cursor", cursor)
        );
        return SendSignedAsync<string>(TRANSACTION_LOG, HttpMethod.Get, query: query);
    }

    private const string ACCOUNT_MARGIN_MODE = "/v5/account/set-margin-mode";
    /// <summary>
    /// Default is regular margin mode
    /// UTA account can be switched between these 3 kinds of margin modes, which is across UID level, working for USDT Perp, USDC Perp, USDC Futures and Options(Option does not support ISOLATED_MARGIN)
    /// Classic account can be switched between REGULAR_MARGIN and PORTFOLIO_MARGIN, only work for USDC Perp and Options trading.
    /// </summary>
    /// <param name="setMarginMode"></param>
    /// <returns>Margin Mode</returns>
    public Task<string?> SetAccountMarginMode(SetMarginMode? setMarginMode = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("SetMarginMode", setMarginMode?.Value)
        );
        return SendSignedAsync<string>(ACCOUNT_MARGIN_MODE, HttpMethod.Post, query: query);
    }

    private const string SET_MMP = "/v5/account/mmp-modify";
    /// <summary>
    /// Send an email to Bybit (financial.inst@bybit.com) or contact your business development (BD) manager to apply for MMP. After processed, the default settings are as below table:
    /// </summary>
    /// <param name="baseCoin"></param>
    /// <param name="window"></param>
    /// <param name="frozenPeriod"></param>
    /// <param name="qtyLimit"></param>
    /// <param name="deltaLimit"></param>
    /// <returns>None</returns>
    public Task<string?> SetMarketMakerProtection(string? baseCoin = null, string? window = null, string? frozenPeriod = null, string? qtyLimit = null, string? deltaLimit = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("baseCoin", baseCoin),
            ("window", window),
            ("frozenPeriod", frozenPeriod),
            ("qtyLimit", qtyLimit),
            ("deltaLimit", deltaLimit)
        );
        return SendSignedAsync<string>(SET_MMP, HttpMethod.Post, query: query);
    }

    private const string RESET_MMP = "/v5/account/mmp-reset";
    /// <summary>
    /// Once the mmp triggered, you can unfreeze the account by this endpoint, then qtyLimit and deltaLimit will be reset to 0.
    /// If the account is not frozen, reset action can also remove previous accumulation, i.e., qtyLimit and deltaLimit will be reset to 0.
    /// </summary>
    /// <param name="baseCoin"></param>
    /// <returns>None</returns>
    public Task<string?> ResetMarketMakerProtection(string? baseCoin = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("baseCoin", baseCoin)
        );
        return SendSignedAsync<string>(RESET_MMP, HttpMethod.Post, query: query);
    }

    private const string SET_SPOT_HEDGE = "/v5/account/set-hedging-mode";
    /// <summary>
    /// You can turn on/off Spot hedging feature in Portfolio margin for Unified account.
    /// Only unified account is applicable
    /// Only portfolio margin mode is applicable
    /// Institutional lending account is not supported
    /// </summary>
    /// <param name="spotHedgeMode"></param>
    /// <returns>Set Spot Hedging</returns>
    public Task<string?> SetSpotHedgingMode(SpotHedgeMode? spotHedgeMode = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("setHedgingMode", spotHedgeMode?.Value)
        );
        return SendSignedAsync<string>(SET_SPOT_HEDGE, HttpMethod.Post, query: query);
    }

    private const string MMP_STATE = "/v5/account/mmp-state";
    /// <summary>
    /// Get MMP State
    /// </summary>
    /// <param name="baseCoin"></param>
    /// <returns>Market Maker State</returns>
    public Task<string?> GetMarketMakerProtectionState(string? baseCoin = null)
    {
        var query = new Dictionary<string, object>();
        BybitParametersUtils.AddOptionalParameters(query,
            ("baseCoin", baseCoin)
        );
        return SendSignedAsync<string>(MMP_STATE, HttpMethod.Get, query: query);
    }
}