using bybit.net.api.Models.Asset;
using bybit.net.api.Models.Lending;
using bybit.net.api.Models.SpotMargin;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitSpotMarginService : BybitApiService
    {
        public BybitSpotMarginService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitSpotMarginService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        #region Spot Margin Trade Classic
        private const string CLASSICAL_SPOT_MARGIN_COIN = "/v5/spot-cross-margin-trade/pledge-token";
        /// <summary>
        /// Get Margin Coin Info
        /// </summary>
        /// <param name="coin"></param>
        /// <returns>Get Margin Coin Info</returns>
        public async Task<string?> GetSpotMarginCoin(string? coin = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );
            var result = await this.SendPublicAsync<string>(CLASSICAL_SPOT_MARGIN_COIN, HttpMethod.Get, query: query);
            return result;
        }

        private const string CLASSICAL_BORROWABLE_COIN = "/v5/spot-cross-margin-trade/borrow-token";
        /// <summary>
        /// Get Borrowable Coin Info
        /// </summary>
        /// <param name="coin"></param>
        /// <returns>Get Borrowable Coin Info</returns>
        public async Task<string?> GetSpotMarginBorrowableCoin(string? coin = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );
            var result = await this.SendPublicAsync<string>(CLASSICAL_BORROWABLE_COIN, HttpMethod.Get, query: query);
            return result;
        }

        private const string CLASSICAL_INTEREST_QUOTA = "/v5/spot-cross-margin-trade/borrow-token";
        /// <summary>
        /// Covers: Margin trade (Classic Account)
        /// </summary>
        /// <param name="coin"></param>
        /// <returns>Get Interest & Quota</returns>
        public async Task<string?> GetSpotMarginInterestQuota(string? coin = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );
            var result = await this.SendSignedAsync<string>(CLASSICAL_INTEREST_QUOTA, HttpMethod.Get, query: query);
            return result;
        }

        private const string CLASSICAL_LOAN_INFO = "/v5/spot-cross-margin-trade/account";
        /// <summary>
        /// Covers: Margin trade (Classic Account)
        /// </summary>
        /// <returns>Get Loan Account Info</returns>
        public async Task<string?> GetSpotMarginLoanInfo()
        {
            var query = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<string>(CLASSICAL_LOAN_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string CLASSICAL_MARGIN_BORROW = "/v5/spot-cross-margin-trade/loan";
        /// <summary>
        /// Covers: Margin trade (Classic Account)
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="qty"></param>
        /// <returns>Borrow</returns>
        public async Task<string?> BorrowSpotMarginLoan(string coin, string qty)
        {
            var query = new Dictionary<string, object> { { "coin", coin }, { "qty", qty } };
            var result = await this.SendSignedAsync<string>(CLASSICAL_MARGIN_BORROW, HttpMethod.Post, query: query);
            return result;
        }

        private const string CLASSICAL_MARGIN_REPAY = "/v5/spot-cross-margin-trade/repay";
        /// <summary>
        /// Covers: Margin trade (Classic Account)
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="qty"></param>
        /// <param name="completeRepayment"></param>
        /// <returns>Repay</returns>
        public async Task<string?> RepaySpotMarginLoan(string coin, string qty, CompleteRepayment? completeRepayment = null)
        {
            var query = new Dictionary<string, object> { { "coin", coin }, { "qty", qty } };
            BybitParametersUtils.AddOptionalParameters(query,
                ("completeRepayment", completeRepayment?.Value)
            );
            var result = await this.SendSignedAsync<string>(CLASSICAL_MARGIN_REPAY, HttpMethod.Post, query: query);
            return result;
        }

        private const string CLASSICAL_TOOGLE_MARGIN = "/v5/spot-cross-margin-trade/switch";
        /// <summary>
        /// Turn on / off spot margin trade
        /// Covers: Margin trade(Classic Account)
        /// </summary>
        /// <returns>Toggle Margin Trade</returns>
        public async Task<string?> SwitchSpotMarginMode(SwitchStatus switchStatus)
        {
            var query = new Dictionary<string, object> { { "swicth", switchStatus.Value } };
            var result = await this.SendSignedAsync<string>(CLASSICAL_TOOGLE_MARGIN, HttpMethod.Post, query: query);
            return result;
        }

        private const string CLASSICAL_PEPAYMENTS_ORDERS = "/v5/spot-cross-margin-trade/repay-history";
        /// <summary>
        /// Covers: Margin trade (Classic Account)
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <returns>Get Repayment Order Detail</returns>
        public async Task<string?> GetSpotMarginRepaymentOrders(string? coin = null, long? startTime = null, long? endTime = null, int? limit = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit)
            );
            var result = await this.SendSignedAsync<string>(CLASSICAL_PEPAYMENTS_ORDERS, HttpMethod.Get, query: query);
            return result;
        }

        private const string CLASSICAL_BORROW_ORDERS = "/v5/spot-cross-margin-trade/orders";
        /// <summary>
        /// Covers: Margin trade (Classic Account)
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="status"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <returns>Get Borrow Order Detail</returns>
        public async Task<string?> GetSpotMarginBorrowOrders(string? coin = null, SpotMarginStatus? status = null, long? startTime = null, long? endTime = null, int? limit = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("status", status?.Value),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit)
            );
            var result = await this.SendSignedAsync<string>(CLASSICAL_BORROW_ORDERS, HttpMethod.Get, query: query);
            return result;
        }
        #endregion

        #region Spot Margin Trade UTA
        private const string UTA_SPOT_MARGIN_DATA = "/v5/spot-margin-trade/data";
        private const string CLASSICAL_SPOT_MARGIN_DATA = "/v5/spot-margin-trade/data";
        /// <summary>
        /// This margin data is for Unified account in particular.
        /// </summary>
        /// <param name="vipLevel"></param>
        /// <param name="currency"></param>
        /// <returns>spot margin data</returns>
        public async Task<string?> GetSpotMarginVipData(bool isUta, VipLevel? vipLevel = null, string? currency = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("vipLevel", vipLevel?.Value),
                ("currency", currency)
            );
            var result = await this.SendPublicAsync<string>(isUta ? UTA_SPOT_MARGIN_DATA : CLASSICAL_SPOT_MARGIN_DATA, HttpMethod.Get, query: query);
            return result;
        }

        private const string TOOGLE_MARGIN_TRADE = "/v5/spot-margin-trade/switch-mode";
        /// <summary>
        /// Turn on / off spot margin trade
        /// Covers: Margin trade(Unified Account)
        /// Your account needs to activate spot margin first; i.e., you must have finished the quiz on web / app.
        /// </summary>
        /// <param name="spotMarginMode"></param>
        /// <returns>Toggle Margin Trade</returns>
        public async Task<string?> SwitchSpotMarginMode(SpotMarginMode spotMarginMode)
        {
            var query = new Dictionary<string, object> { { "spotMarginMode", spotMarginMode.Value } };
            var result = await this.SendSignedAsync<string>(TOOGLE_MARGIN_TRADE, HttpMethod.Post, query: query);
            return result;
        }

        /// <summary>
        /// Turn on / off spot margin trade
        /// Covers: Margin trade(Unified Account)
        /// Your account needs to activate spot margin first; i.e., you must have finished the quiz on web / app.
        /// </summary>
        /// <param name="spotMarginMode"></param>
        /// <returns>Toggle Margin Trade</returns>
        public async Task<string?> GetSpotMarginVipData(SpotMarginMode spotMarginMode)
        {
            var result = await SwitchSpotMarginMode(spotMarginMode);
            return result;
        }

        private const string SET_SPOT_MARGIN_LEVERAGE = "/v5/spot-margin-trade/set-leverage";
        /// <summary>
        /// Set the user's maximum leverage in spot cross margin
        /// Covers: Margin trade(Unified Account)
        /// Your account needs to activate spot margin first; i.e., you must have finished the quiz on web / app.
        /// </summary>
        /// <param name="leverage"></param>
        /// <param name="currency"></param>
        /// <returns>Set Leverage</returns>
        public async Task<string?> SetSpotMarginLeverage(string leverage, string? currency = null)
        {
            var query = new Dictionary<string, object> { { "leverage", leverage } };
            BybitParametersUtils.AddOptionalParameters(query,
                ("currency", currency)
            );
            var result = await this.SendSignedAsync<string>(SET_SPOT_MARGIN_LEVERAGE, HttpMethod.Post, query: query);
            return result;
        }

        private const string SPOT_MARGIN_STATE = "/v5/spot-margin-trade/state";
        /// <summary>
        /// Query the Spot margin status and leverage of Unified account
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <returns>Get Status And Leverage</returns>
        public async Task<string?> GetSpotMarginState()
        {
            var query = new Dictionary<string, object> { };
            var result = await this.SendSignedAsync<string>(SPOT_MARGIN_STATE, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_CURRENCY_DATA = "/v5/spot-margin-trade/currency-data";

        /// <summary>
        /// Get currency data
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>Currency data</returns>
        public async Task<string?> GetSpotMarginCurrencyData(string? currency = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("currency", currency)
            );

            var result = await this.SendSignedAsync<string>(GET_CURRENCY_DATA, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_COIN_STATE = "/v5/spot-margin-trade/coinstate";

        /// <summary>
        /// Get coin state
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>Coin state</returns>
        public async Task<string?> GetSpotMarginCoinState(string? currency = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("currency", currency)
            );

            var result = await this.SendSignedAsync<string>(GET_COIN_STATE, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_POSITION_TIERS = "/v5/spot-margin-trade/position-tiers";

        /// <summary>
        /// Get Position Tiers
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>Position tiers</returns>
        public async Task<string?> GetSpotMarginPositionTiers(string? currency = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("currency", currency)
            );

            var result = await this.SendSignedAsync<string>(GET_POSITION_TIERS, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_MAX_BORROWABLE = "/v5/spot-margin-trade/max-borrowable";

        /// <summary>
        /// Get Max Borrowable Amount
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>Max borrowable amount</returns>
        public async Task<string?> GetSpotMarginMaxBorrowable(string currency)
        {
            var query = new Dictionary<string, object> { { "currency", currency } };
            var result = await this.SendSignedAsync<string>(GET_MAX_BORROWABLE, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_REPAYMENT_AVAILABLE_AMOUNT = "/v5/spot-margin-trade/repayment-available-amount";

        /// <summary>
        /// Get Available Amount to Repay
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>Repayment available amount</returns>
        public async Task<string?> GetSpotMarginRepaymentAvailableAmount(string currency)
        {
            var query = new Dictionary<string, object> { { "currency", currency } };
            var result = await this.SendSignedAsync<string>(GET_REPAYMENT_AVAILABLE_AMOUNT, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_TIERED_COLLATERAL_RATIO = "/v5/spot-margin-trade/collateral";

        /// <summary>
        /// Get Tiered Collateral Ratio
        /// UTA loan tiered collateral ratios. Public endpoint.
        /// </summary>
        /// <param name="currency">Optional coin, uppercase</param>
        /// <returns></returns>
        public async Task<string?> GetTieredCollateralRatio(string? currency = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("currency", currency));

            var result = await this.SendPublicAsync<string>(GET_TIERED_COLLATERAL_RATIO, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_HISTORICAL_INTEREST_RATE = "/v5/spot-margin-trade/interest-rate-history";

        /// <summary>
        /// Get Historical Interest Rate
        /// Auth required. Unified account only. Up to 6 months history.
        /// If time range omitted, returns last 7 days. If provided, endTime - startTime <= 30 days.
        /// </summary>
        /// <param name="currency">Coin, uppercase</param>
        /// <param name="vipLevel">VIP level (encode spaces, e.g., "No%20VIP")</param>
        /// <param name="startTime">ms</param>
        /// <param name="endTime">ms</param>
        /// <returns></returns>
        public async Task<string?> GetHistoricalInterestRate(
            string currency,
            string? vipLevel = null,
            long? startTime = null,
            long? endTime = null)
        {
            var query = new Dictionary<string, object> { { "currency", currency } };

            BybitParametersUtils.AddOptionalParameters(query,
                ("vipLevel", vipLevel),
                ("startTime", startTime),
                ("endTime", endTime)
            );

            var result = await this.SendSignedAsync<string>(GET_HISTORICAL_INTEREST_RATE, HttpMethod.Get, query: query);
            return result;
        }

        private const string SET_AUTO_REPAY_MODE = "/v5/spot-margin-trade/set-auto-repay-mode";

        /// <summary>
        /// Set Auto Repay Mode
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="autoRepayMode">1: On, 0: Off</param>
        /// <param name="currency"></param>
        /// <returns>Auto repay mode</returns>
        public async Task<string?> SetAutoRepayMode(string autoRepayMode, string? currency = null)
        {
            var query = new Dictionary<string, object> { { "autoRepayMode", autoRepayMode } };
            BybitParametersUtils.AddOptionalParameters(query,
                ("currency", currency)
            );

            var result = await this.SendSignedAsync<string>(SET_AUTO_REPAY_MODE, HttpMethod.Post, query: query);
            return result;
        }

        private const string GET_AUTO_REPAY_MODE = "/v5/spot-margin-trade/get-auto-repay-mode";

        /// <summary>
        /// Get Auto Repay Mode
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>Auto repay mode</returns>
        public async Task<string?> GetAutoRepayMode(string? currency = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("currency", currency)
            );

            var result = await this.SendSignedAsync<string>(GET_AUTO_REPAY_MODE, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_FIXED_BORROW_ORDER_QUOTE = "/v5/spot-margin-trade/fixedborrow-order-quote";

        /// <summary>
        /// Get Fixed-Rate Borrow Order Quote
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="orderCurrency"></param>
        /// <param name="term"></param>
        /// <param name="orderBy"></param>
        /// <param name="sort"></param>
        /// <param name="limit"></param>
        /// <returns>Fixed-rate borrow order quote</returns>
        public async Task<string?> GetFixedBorrowOrderQuote(string orderCurrency, string? term = null, string? orderBy = null, int? sort = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "orderCurrency", orderCurrency } };
            BybitParametersUtils.AddOptionalParameters(query,
                ("term", term),
                ("orderBy", orderBy),
                ("sort", sort),
                ("limit", limit)
            );

            var result = await this.SendSignedAsync<string>(GET_FIXED_BORROW_ORDER_QUOTE, HttpMethod.Get, query: query);
            return result;
        }

        private const string FIXED_BORROW = "/v5/spot-margin-trade/fixedborrow";

        /// <summary>
        /// Fixed-Rate Borrow
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="orderCurrency"></param>
        /// <param name="orderAmount"></param>
        /// <param name="annualRate"></param>
        /// <param name="term"></param>
        /// <param name="repayType"></param>
        /// <returns>Fixed-rate borrow</returns>
        public async Task<string?> FixedBorrow(string orderCurrency, string orderAmount, string annualRate, string term, string? repayType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "orderCurrency", orderCurrency },
                { "orderAmount", orderAmount },
                { "annualRate", annualRate },
                { "term", term }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("repayType", repayType)
            );

            var result = await this.SendSignedAsync<string>(FIXED_BORROW, HttpMethod.Post, query: query);
            return result;
        }

        private const string FIXED_BORROW_RENEW = "/v5/spot-margin-trade/fixedborrow-renew";

        /// <summary>
        /// Renew Fixed-Rate Borrow
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="qty"></param>
        /// <returns>Renew fixed-rate borrow</returns>
        public async Task<string?> RenewFixedBorrow(string loanId, string? qty = null)
        {
            var query = new Dictionary<string, object> { { "loanId", loanId } };
            BybitParametersUtils.AddOptionalParameters(query,
                ("qty", qty)
            );

            var result = await this.SendSignedAsync<string>(FIXED_BORROW_RENEW, HttpMethod.Post, query: query);
            return result;
        }

        private const string GET_FIXED_BORROW_ORDER_INFO = "/v5/spot-margin-trade/fixedborrow-order-info";

        /// <summary>
        /// Get Fixed-Rate Borrow Order Info
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderCurrency"></param>
        /// <param name="state"></param>
        /// <param name="term"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Fixed-rate borrow order info</returns>
        public async Task<string?> GetFixedBorrowOrderInfo(string? orderId = null, string? orderCurrency = null, string? state = null, string? term = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("orderCurrency", orderCurrency),
                ("state", state),
                ("term", term),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<string>(GET_FIXED_BORROW_ORDER_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_FIXED_BORROW_CONTRACT_INFO = "/v5/spot-margin-trade/fixedborrow-contract-info";

        /// <summary>
        /// Get Fixed-Rate Borrow Contract Info
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderCurrency"></param>
        /// <param name="term"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Fixed-rate borrow contract info</returns>
        public async Task<string?> GetFixedBorrowContractInfo(string? orderId = null, string? orderCurrency = null, string? term = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("orderCurrency", orderCurrency),
                ("term", term),
                ("limit", limit),
                ("cursor", cursor)
            );

            var result = await this.SendSignedAsync<string>(GET_FIXED_BORROW_CONTRACT_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string GET_LIABILITY = "/v5/spot-margin-trade/liability";

        /// <summary>
        /// Get Liability Info
        /// Covers: Margin trade(Unified Account)
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>Liability info</returns>
        public async Task<string?> GetSpotMarginLiability(string currency)
        {
            var query = new Dictionary<string, object> { { "currency", currency } };
            var result = await this.SendSignedAsync<string>(GET_LIABILITY, HttpMethod.Get, query: query);
            return result;
        }

        #endregion

        #region Spot Leverage Token
        private const string LEVERQGE_TOKEN_INFO = "/v5/spot-lever-token/info";
        /// <summary>
        /// Query leverage token information
        /// </summary>
        /// <param name="ltCoin"></param>
        /// <returns>Query leverage token information</returns>
        public async Task<string?> GetSpotLeverageTokenInfo(string? ltCoin = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("ltCoin", ltCoin)
            );
            var result = await this.SendSignedAsync<string>(LEVERQGE_TOKEN_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string LEVERQGE_TOKEN_MARKET = "/v5/spot-lever-token/reference";
        /// <summary>
        /// Get leverage token market information
        /// </summary>
        /// <param name="ltCoin"></param>
        /// <returns>Get leverage token market information</returns>
        public async Task<string?> GetSpotLeverageTokenMarket(string? ltCoin = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("ltCoin", ltCoin)
            );
            var result = await this.SendSignedAsync<string>(LEVERQGE_TOKEN_MARKET, HttpMethod.Get, query: query);
            return result;
        }

        private const string LEVERQGE_TOKEN_PURCHASE = "/v5/spot-lever-token/purchase";
        /// <summary>
        /// Purchase levearge token
        /// </summary>
        /// <param name="ltCoin"></param>
        /// <param name="ltAmount"></param>
        /// <param name="serialNo"></param>
        /// <returns>Purchase levearge token</returns>
        public async Task<string?> PurchaseSpotLeverageToken(string? ltCoin = null, string? ltAmount = null, string? serialNo = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("ltCoin", ltCoin),
                ("ltAmount", ltAmount),
                ("serialNo", serialNo)
            );
            var result = await this.SendSignedAsync<string>(LEVERQGE_TOKEN_PURCHASE, HttpMethod.Post, query: query);
            return result;
        }

        private const string LEVERQGE_TOKEN_REDEEM = "/v5/spot-lever-token/redeem";
        /// <summary>
        /// Redeem leverage token
        /// </summary>
        /// <param name="ltCoin"></param>
        /// <param name="quantity"></param>
        /// <param name="serialNo"></param>
        /// <returns>Redeem leverage token</returns>
        public async Task<string?> RedeemSpotLeverageToken(string? ltCoin = null, string? quantity = null, string? serialNo = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
               ("ltCoin", ltCoin),
                ("quantity", quantity),
                ("serialNo", serialNo)
            );
            var result = await this.SendSignedAsync<string>(LEVERQGE_TOKEN_REDEEM, HttpMethod.Post, query: query);
            return result;
        }

        private const string LEVERQGE_TOKEN_RECORDS = "/v5/spot-lever-token/order-record";
        /// <summary>
        /// Get purchase or redeem history
        /// </summary>
        /// <param name="ltCoin"></param>
        /// <param name="orderId"></param>
        /// <param name="ltOrderType"></param>
        /// <param name="serialNo"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <returns>Get purchase or redeem history</returns>
        public async Task<string?> GetSpotLeverageTokenRecords(string? ltCoin = null, string? orderId = null, string? ltOrderType = null, string? serialNo = null, long? startTime = null, long? endTime = null, int? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("ltCoin", ltCoin),
                ("orderId", orderId),
                ("ltOrderType", ltOrderType),
                ("serialNo", serialNo),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("cursor", cursor)
            );
            var result = await this.SendSignedAsync<string>(LEVERQGE_TOKEN_RECORDS, HttpMethod.Get, query: query);
            return result;
        }
        #endregion
    }
}
