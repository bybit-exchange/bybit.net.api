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
        public async Task<string?> GetSpotMarginVipData(SpotMarginMode spotMarginMode)
        {
            var query = new Dictionary<string, object> { { "spotMarginMode", spotMarginMode.Value } };
            var result = await this.SendSignedAsync<string>(TOOGLE_MARGIN_TRADE, HttpMethod.Get, query: query);
            return result;
        }

        private const string SET_SPOT_MARGIN_LEVERAGE = "/v5/spot-margin-trade/set-leverage";
        /// <summary>
        /// Set the user's maximum leverage in spot cross margin
        /// Covers: Margin trade(Unified Account)
        /// Your account needs to activate spot margin first; i.e., you must have finished the quiz on web / app.
        /// </summary>
        /// <param name="leverage"></param>
        /// <returns>Set Leverage</returns>
        public async Task<string?> SetSpotMarginLeverage(string leverage)
        {
            var query = new Dictionary<string, object> { { "leverage", leverage } };
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
            var result = await this.SendSignedAsync<string>(SPOT_MARGIN_STATE, HttpMethod.Post, query: query);
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