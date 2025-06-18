using bybit.net.api.Models.Lending;
using bybit.net.api.Services;
using System.Collections.Specialized;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitLendingService : BybitApiService
    {
        public BybitLendingService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitLendingService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        #region Instituion Lending
        private const string INS_PRODUCT_INFO = "/v5/ins-loan/product-infos";
        /// <summary>
        /// This endpoint can be queried without api key and secret, then it returns public product data
        /// If your uid is bound with OTC loan product, then you can get your private product data by calling the endpoint with api key and secret
        /// If your uid is not bound with OTC loan product but api key and secret are also passed, it will return public data only
        /// </summary>
        /// <param name="productId"></param>
        /// <returns> Get Product Info </returns>
        public async Task<string?> GetInsLoanInfo(string? productId = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId)
            );
            var result = await this.SendSignedAsync<string>(INS_PRODUCT_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string INS_MARGIN_COIN = "/v5/ins-loan/ensure-tokens-convert";
        /// <summary>
        /// This endpoint can be queried without api key and secret, then it returns public margin data
        /// If your uid is bound with OTC loan product, then you can get your private margin data by calling the endpoint with api key and secret
        /// If your uid is not bound with OTC loan product but api key and secret are also passed, it will return public data only
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Get Margin Coin Info</returns>
        public async Task<string?> GetInsMarginCoinInfo(string? productId = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId)
            );
            var result = await this.SendSignedAsync<string>(INS_MARGIN_COIN, HttpMethod.Get, query: query);
            return result;
        }

        private const string INS_LOAN_ORDERS = "/v5/ins-loan/ensure-tokens-convert";
        /// <summary>
        /// Get the past 2 years data by default
        /// Get up to the past 2 years of data
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <returns>Get Loan Orders</returns>
        public async Task<string?> GetInsLoanOrders(string? orderId = null, long? startTime = null, long? endTime = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit)
            );
            var result = await this.SendSignedAsync<string>(INS_LOAN_ORDERS, HttpMethod.Get, query: query);
            return result;
        }

        private const string INS_LOAN_REPAY_ORDERS = "/v5/ins-loan/ensure-tokens-convert";
        /// <summary>
        /// Get the past 2 years data by default
        /// Get up to the past 2 years of data
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <returns>Get Repay Orders</returns>
        public async Task<string?> GetInsLoanRepayOrders(long? startTime = null, long? endTime = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit)
            );
            var result = await this.SendSignedAsync<string>(INS_LOAN_REPAY_ORDERS, HttpMethod.Get, query: query);
            return result;
        }

        private const string INS_LOAN_TO_VALUE = "/v5/ins-loan/ltv-convert";
        /// <summary>
        /// Get LTV
        /// </summary>
        /// <returns> Loan To Value</returns>
        public async Task<string?> GetInsLoanToValue()
        {
            var query = new Dictionary<string, object> { };
            var result = await this.SendSignedAsync<string>(INS_LOAN_TO_VALUE, HttpMethod.Get, query: query);
            return result;
        }

        private const string UPDATE_UID_TO_INS_LOAN = "/v5/ins-loan/association-uid";
        /// <summary>
        /// For the institutional loan product, you can bind new UIDs to the risk unit or unbind UID from the risk unit.
        /// </summary>
        /// <returns> Bind Or Unbind UID </returns>
        public async Task<string?> UpdateInsLoanUID(string uid, OperateType operate)
        {
            var query = new Dictionary<string, object> { {"uid", uid}, {"operate", operate.Value} };
            var result = await this.SendSignedAsync<string>(UPDATE_UID_TO_INS_LOAN, HttpMethod.Post, query: query);
            return result;
        }
        #endregion

        #region C2C Lending
        private const string C2C_LENDING_INFO = "/v5/lending/info";
        /// <summary>
        /// Get the basic information of lending coins
        /// All v5/lending APIs need SPOT permission.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns> Get Lending Coin Info </returns>
        public async Task<string?> GetC2CLendingCoinInfo(string? coin = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );
            var result = await this.SendSignedAsync<string>(C2C_LENDING_INFO, HttpMethod.Get, query: query);
            return result;
        }

        private const string C2C_DEPOSIT_FUND = "/v5/lending/purchase";
        /// <summary>
        /// Lending funds to Bybit asset pool
        /// normal & UMA account: deduct funds from Spot wallet
        /// UTA account: deduct funds from Unified wallet
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="quantity"></param>
        /// <param name="serialNo"></param>
        /// <returns>Deposit Funds</returns>
        public async Task<string?> C2CDepositFund(string? coin = null, string? quantity = null, string? serialNo = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("quantity", quantity),
                ("serialNo", serialNo)
            );
            var result = await this.SendSignedAsync<string>(C2C_DEPOSIT_FUND, HttpMethod.Post, query: query);
            return result;
        }

        private const string C2C_REDEEM_FUND = "/v5/lending/redeem";
        /// <summary>
        /// Withdraw funds from the Bybit asset pool.
        /// There will be two redemption records: one for the redeemed quantity, and the other one is for the total interest occurred.
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="quantity"></param>
        /// <param name="serialNo"></param>
        /// <returns>Redeem Funds</returns>
        public async Task<string?> C2CRedeemFund(string? coin = null, string? quantity = null, string? serialNo = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("quantity", quantity),
                ("serialNo", serialNo)
            );
            var result = await this.SendSignedAsync<string>(C2C_REDEEM_FUND, HttpMethod.Post, query: query);
            return result;
        }

        private const string C2C_CANCEL_REDEEM = " /v5/lending/redeem-cancel";
        /// <summary>
        /// Withdraw funds from the Bybit asset pool.
        /// There will be two redemption records: one for the redeemed quantity, and the other one is for the total interest occurred.
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="quantity"></param>
        /// <param name="serialNo"></param>
        /// <returns>Redeem Funds</returns>
        public async Task<string?> C2CCancelRedeem(string? coin = null, string? orderId = null, string? serialNo = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("orderId", orderId),
                ("serialNo", serialNo)
            );
            var result = await this.SendSignedAsync<string>(C2C_CANCEL_REDEEM, HttpMethod.Post, query: query);
            return result;
        }

        private const string C2C_LENDING_ORDERS = "/v5/ins-loan/ensure-tokens-convert";
        /// <summary>
        /// Get the past 2 years data by default
        /// Get up to the past 2 years of data
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="orderId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <returns>Get Loan Orders</returns>
        public async Task<string?> GetC2CLendingOrders(string? coin = null, string? orderId = null, long? startTime = null, long? endTime = null, int? limit = null, LendingOrderType? orderType = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("orderId", orderId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("orderType", orderType?.Value)
            );
            var result = await this.SendSignedAsync<string>(C2C_LENDING_ORDERS, HttpMethod.Get, query: query);
            return result;
        }

        private const string C2C_LENDING_ACCOUNT = "/v5/lending/account";
        /// <summary>
        /// Get Lending Account Info
        /// </summary>
        /// <param name="coin"></param>
        /// <returns>Get Lending Account Info</returns>
        public async Task<string?> GetC2CLendingOrders(string? coin = null)
        {
            var query = new Dictionary<string, object> { };

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );
            var result = await this.SendSignedAsync<string>(C2C_LENDING_ACCOUNT, HttpMethod.Get, query: query);
            return result;
        }
        #endregion
    }
}