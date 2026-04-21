using bybit.net.api.Models;
using bybit.net.api.Models.Lending;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitLendingService : BybitApiService
    {
        public BybitLendingService(string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitLendingService(HttpClient httpClient, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

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
        private const string INS_MARGIN_COIN = "/v5/ins-loan/ensure-tokens-convert";
        private const string INS_LOAN_ORDERS = "/v5/ins-loan/loan-order";
        private const string INS_LOAN_REPAY_ORDERS = "/v5/ins-loan/repaid-history";
        private const string INS_LOAN_TO_VALUE = "/v5/ins-loan/ltv-convert";
        private const string UPDATE_UID_TO_INS_LOAN = "/v5/ins-loan/association-uid";
        private const string INS_LOAN_REPAY = "/v5/ins-loan/repay-loan";

        public async Task<GeneralResponse<GetInsLoanInfoResult>?> GetInsLoanInfo(string? productId = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId)
            );

            var result = await this.SendPublicAsync<GeneralResponse<GetInsLoanInfoResult>>(INS_PRODUCT_INFO, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetInsMarginCoinInfoResult>?> GetInsMarginCoinInfo(string? productId = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("productId", productId)
            );

            var result = await this.SendPublicAsync<GeneralResponse<GetInsMarginCoinInfoResult>>(INS_MARGIN_COIN, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetInsLoanOrdersResult>?> GetInsLoanOrders(string? orderId = null, long? startTime = null, long? endTime = null, int? limit = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("orderId", orderId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetInsLoanOrdersResult>>(INS_LOAN_ORDERS, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetInsLoanRepayOrdersResult>?> GetInsLoanRepayOrders(long? startTime = null, long? endTime = null, int? limit = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetInsLoanRepayOrdersResult>>(INS_LOAN_REPAY_ORDERS, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetInsLoanToValueResult>?> GetInsLoanToValue()
        {
            var query = new Dictionary<string, object>();
            var result = await this.SendSignedAsync<GeneralResponse<GetInsLoanToValueResult>>(INS_LOAN_TO_VALUE, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<UpdateInsLoanUidResult>?> UpdateInsLoanUID(string uid, OperateType operate)
        {
            var query = new Dictionary<string, object> { { "uid", uid }, { "operate", operate.Value } };
            var result = await this.SendSignedAsync<GeneralResponse<UpdateInsLoanUidResult>>(UPDATE_UID_TO_INS_LOAN, HttpMethod.Post, query: query);
            return result;
        }

        public async Task<GeneralResponse<RepayInsLoanResult>?> RepayInsLoan(string token, string quantity)
        {
            var body = new Dictionary<string, object>
            {
                { "token", token },
                { "quantity", quantity }
            };

            var result = await this.SendSignedAsync<GeneralResponse<RepayInsLoanResult>>(INS_LOAN_REPAY, HttpMethod.Post, query: body);
            return result;
        }
        #endregion

        #region C2C Lending
        private const string C2C_LENDING_INFO = "/v5/lending/info";
        private const string C2C_DEPOSIT_FUND = "/v5/lending/purchase";
        private const string C2C_REDEEM_FUND = "/v5/lending/redeem";
        private const string C2C_CANCEL_REDEEM = "/v5/lending/redeem-cancel";
        private const string C2C_LENDING_ORDERS = "/v5/lending/history-order";
        private const string C2C_LENDING_ACCOUNT = "/v5/lending/account";

        public async Task<GeneralResponse<GetC2CLendingCoinInfoResult>?> GetC2CLendingCoinInfo(string? coin = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetC2CLendingCoinInfoResult>>(C2C_LENDING_INFO, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<C2CLendingOrderResult>?> C2CDepositFund(string coin, string quantity, string? serialNo = null)
        {
            var query = new Dictionary<string, object>
            {
                { "coin", coin },
                { "quantity", quantity }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("serialNo", serialNo)
            );

            var result = await this.SendSignedAsync<GeneralResponse<C2CLendingOrderResult>>(C2C_DEPOSIT_FUND, HttpMethod.Post, query: query);
            return result;
        }

        public async Task<GeneralResponse<C2CRedeemOrderResult>?> C2CRedeemFund(string coin, string quantity, string? serialNo = null)
        {
            var query = new Dictionary<string, object>
            {
                { "coin", coin },
                { "quantity", quantity }
            };

            BybitParametersUtils.AddOptionalParameters(query,
                ("serialNo", serialNo)
            );

            var result = await this.SendSignedAsync<GeneralResponse<C2CRedeemOrderResult>>(C2C_REDEEM_FUND, HttpMethod.Post, query: query);
            return result;
        }

        public async Task<GeneralResponse<C2CCancelRedeemResult>?> C2CCancelRedeem(string? coin = null, string? orderId = null, string? serialNo = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("orderId", orderId),
                ("serialNo", serialNo)
            );

            var result = await this.SendSignedAsync<GeneralResponse<C2CCancelRedeemResult>>(C2C_CANCEL_REDEEM, HttpMethod.Post, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetC2CLendingOrdersResult>?> GetC2CLendingOrders(string? coin = null, string? orderId = null, long? startTime = null, long? endTime = null, int? limit = null, LendingOrderType? orderType = null)
        {
            var query = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(query,
                ("coin", coin),
                ("orderId", orderId),
                ("startTime", startTime),
                ("endTime", endTime),
                ("limit", limit),
                ("orderType", orderType?.Value)
            );

            var result = await this.SendSignedAsync<GeneralResponse<GetC2CLendingOrdersResult>>(C2C_LENDING_ORDERS, HttpMethod.Get, query: query);
            return result;
        }

        public async Task<GeneralResponse<GetC2CLendingAccountInfoResult>?> GetC2CLendingAccountInfo(string coin)
        {
            var query = new Dictionary<string, object> { { "coin", coin } };
            var result = await this.SendSignedAsync<GeneralResponse<GetC2CLendingAccountInfoResult>>(C2C_LENDING_ACCOUNT, HttpMethod.Get, query: query);
            return result;
        }
        #endregion
    }
}
