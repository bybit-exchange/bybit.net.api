using bybit.net.api.Models;
using bybit.net.api.Models.CryptoLoan;
using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitNewCryptoLoanService : BybitApiService
    {
        public BybitNewCryptoLoanService(string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitNewCryptoLoanService(HttpClient httpClient, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitNewCryptoLoanService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitNewCryptoLoanService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string GET_BORROWABLE_COINS = "/v5/crypto-loan-common/loanable-data";
        private const string GET_COLLATERAL_COINS = "/v5/crypto-loan-common/collateral-data";
        private const string GET_MAX_ALLOWED_COLLATERAL_REDUCTION_AMOUNT = "/v5/crypto-loan-common/max-collateral-amount";
        private const string GET_MAX_LOAN_AMOUNT = "/v5/crypto-loan-common/max-loan";
        private const string ADJUST_COLLATERAL_AMOUNT = "/v5/crypto-loan-common/adjust-ltv";
        private const string GET_COLLATERAL_ADJUSTMENT_HISTORY = "/v5/crypto-loan-common/adjustment-history";
        private const string GET_CRYPTO_LOAN_POSITION = "/v5/crypto-loan-common/position";

        private const string BORROW_FLEXIBLE_LOAN = "/v5/crypto-loan-flexible/borrow";
        private const string REPAY_FLEXIBLE_LOAN = "/v5/crypto-loan-flexible/repay";
        private const string REPAY_COLLATERAL_FLEXIBLE = "/v5/crypto-loan-flexible/repay-collateral";
        private const string GET_FLEXIBLE_LOANS = "/v5/crypto-loan-flexible/ongoing-coin";
        private const string GET_FLEXIBLE_BORROW_ORDERS_HISTORY = "/v5/crypto-loan-flexible/borrow-history";
        private const string GET_FLEXIBLE_REPAYMENT_ORDERS_HISTORY = "/v5/crypto-loan-flexible/repayment-history";

        private const string GET_SUPPLYING_MARKET_FIXED = "/v5/crypto-loan-fixed/supply-order-quote";
        private const string GET_BORROWING_MARKET_FIXED = "/v5/crypto-loan-fixed/borrow-order-quote";
        private const string CREATE_BORROW_ORDER_FIXED = "/v5/crypto-loan-fixed/borrow";
        private const string CREATE_SUPPLY_ORDER_FIXED = "/v5/crypto-loan-fixed/supply";
        private const string CANCEL_BORROW_ORDER_FIXED = "/v5/crypto-loan-fixed/borrow-order-cancel";
        private const string CANCEL_SUPPLY_ORDER_FIXED = "/v5/crypto-loan-fixed/supply-order-cancel";
        private const string GET_BORROW_CONTRACT_INFO_FIXED = "/v5/crypto-loan-fixed/borrow-contract-info";
        private const string GET_SUPPLY_CONTRACT_INFO_FIXED = "/v5/crypto-loan-fixed/supply-contract-info";
        private const string GET_BORROW_ORDER_INFO_FIXED = "/v5/crypto-loan-fixed/borrow-order-info";
        private const string GET_SUPPLY_ORDER_INFO_FIXED = "/v5/crypto-loan-fixed/supply-order-info";
        private const string REPAY_FIXED_LOAN = "/v5/crypto-loan-fixed/fully-repay";
        private const string REPAY_COLLATERAL_FIXED = "/v5/crypto-loan-fixed/repay-collateral";
        private const string GET_REPAYMENT_HISTORY_FIXED = "/v5/crypto-loan-fixed/repayment-history";
        private const string RENEW_FIXED_LOAN = "/v5/crypto-loan-fixed/renew";
        private const string GET_RENEW_ORDER_INFO_FIXED = "/v5/crypto-loan-fixed/renew-info";

        public async Task<GeneralResponse<GetBorrowableCoinsResult>?> GetBorrowableCoins(string? vipLevel = null, string? currency = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("vipLevel", vipLevel), ("currency", currency));
            return await SendPublicAsync<GeneralResponse<GetBorrowableCoinsResult>>(GET_BORROWABLE_COINS, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetCollateralCoinsResult>?> GetCollateralCoins(string? currency = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("currency", currency));
            return await SendPublicAsync<GeneralResponse<GetCollateralCoinsResult>>(GET_COLLATERAL_COINS, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetMaxAllowedCollateralReductionAmountResult>?> GetMaxAllowedCollateralReductionAmount(string currency)
        {
            var query = new Dictionary<string, object> { { "currency", currency } };
            return await SendSignedAsync<GeneralResponse<GetMaxAllowedCollateralReductionAmountResult>>(GET_MAX_ALLOWED_COLLATERAL_REDUCTION_AMOUNT, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetMaxLoanAmountResult>?> GetMaxLoanAmount(string currency, List<MaxLoanCollateralItem>? collateralList = null)
        {
            var body = new Dictionary<string, object> { { "currency", currency } };
            BybitParametersUtils.AddOptionalParameters(body, ("collateralList", collateralList));
            return await SendSignedAsync<GeneralResponse<GetMaxLoanAmountResult>>(GET_MAX_LOAN_AMOUNT, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<AdjustCollateralAmountResult>?> AdjustCollateralAmount(string currency, string amount, string direction)
        {
            var body = new Dictionary<string, object>
            {
                { "currency", currency },
                { "amount", amount },
                { "direction", direction }
            };
            return await SendSignedAsync<GeneralResponse<AdjustCollateralAmountResult>>(ADJUST_COLLATERAL_AMOUNT, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<GetCollateralAdjustmentHistoryResult>?> GetCollateralAdjustmentHistory(string? adjustId = null, string? collateralCurrency = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("adjustId", adjustId), ("collateralCurrency", collateralCurrency), ("limit", limit), ("cursor", cursor));
            return await SendSignedAsync<GeneralResponse<GetCollateralAdjustmentHistoryResult>>(GET_COLLATERAL_ADJUSTMENT_HISTORY, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetCryptoLoanPositionResult>?> GetCryptoLoanPosition()
        {
            return await SendSignedAsync<GeneralResponse<GetCryptoLoanPositionResult>>(GET_CRYPTO_LOAN_POSITION, HttpMethod.Get);
        }

        public async Task<GeneralResponse<CryptoLoanOrderIdResult>?> BorrowFlexibleLoan(string loanCurrency, string loanAmount, List<CryptoLoanCollateralItem>? collateralList = null)
        {
            var body = new Dictionary<string, object>
            {
                { "loanCurrency", loanCurrency },
                { "loanAmount", loanAmount }
            };
            BybitParametersUtils.AddOptionalParameters(body, ("collateralList", collateralList));
            return await SendSignedAsync<GeneralResponse<CryptoLoanOrderIdResult>>(BORROW_FLEXIBLE_LOAN, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<CryptoLoanRepayIdResult>?> RepayFlexibleLoan(string loanCurrency, string amount)
        {
            var body = new Dictionary<string, object> { { "loanCurrency", loanCurrency }, { "amount", amount } };
            return await SendSignedAsync<GeneralResponse<CryptoLoanRepayIdResult>>(REPAY_FLEXIBLE_LOAN, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<object>?> RepayCollateralFlexible(string loanCurrency, string collateralCoin, string amount)
        {
            var body = new Dictionary<string, object>
            {
                { "loanCurrency", loanCurrency },
                { "collateralCoin", collateralCoin },
                { "amount", amount }
            };
            return await SendSignedAsync<GeneralResponse<object>>(REPAY_COLLATERAL_FLEXIBLE, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<GetFlexibleLoansResult>?> GetFlexibleLoans(string? loanCurrency = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("loanCurrency", loanCurrency));
            return await SendSignedAsync<GeneralResponse<GetFlexibleLoansResult>>(GET_FLEXIBLE_LOANS, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetFlexibleBorrowOrdersHistoryResult>?> GetFlexibleBorrowOrdersHistory(string? orderId = null, string? loanCurrency = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("orderId", orderId), ("loanCurrency", loanCurrency), ("limit", limit), ("cursor", cursor));
            return await SendSignedAsync<GeneralResponse<GetFlexibleBorrowOrdersHistoryResult>>(GET_FLEXIBLE_BORROW_ORDERS_HISTORY, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetFlexibleRepaymentOrdersHistoryResult>?> GetFlexibleRepaymentOrdersHistory(string? repayId = null, string? loanCurrency = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("repayId", repayId), ("loanCurrency", loanCurrency), ("limit", limit), ("cursor", cursor));
            return await SendSignedAsync<GeneralResponse<GetFlexibleRepaymentOrdersHistoryResult>>(GET_FLEXIBLE_REPAYMENT_ORDERS_HISTORY, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetFixedLoanMarketResult>?> GetSupplyingMarketFixed(string orderCurrency, string orderBy, string? term = null, int? sort = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "orderCurrency", orderCurrency }, { "orderBy", orderBy } };
            BybitParametersUtils.AddOptionalParameters(query, ("term", term), ("sort", sort), ("limit", limit));
            return await SendPublicAsync<GeneralResponse<GetFixedLoanMarketResult>>(GET_SUPPLYING_MARKET_FIXED, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetFixedLoanMarketResult>?> GetBorrowingMarketFixed(string orderCurrency, string orderBy, string? term = null, int? sort = null, int? limit = null)
        {
            var query = new Dictionary<string, object> { { "orderCurrency", orderCurrency }, { "orderBy", orderBy } };
            BybitParametersUtils.AddOptionalParameters(query, ("term", term), ("sort", sort), ("limit", limit));
            return await SendPublicAsync<GeneralResponse<GetFixedLoanMarketResult>>(GET_BORROWING_MARKET_FIXED, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<CryptoLoanOrderIdResult>?> CreateBorrowOrderFixed(string orderCurrency, string orderAmount, string annualRate, string term, string? autoRepay = null, string? repayType = null, List<CryptoLoanCollateralItem>? collateralList = null)
        {
            var body = new Dictionary<string, object>
            {
                { "orderCurrency", orderCurrency },
                { "orderAmount", orderAmount },
                { "annualRate", annualRate },
                { "term", term }
            };
            BybitParametersUtils.AddOptionalParameters(body, ("autoRepay", autoRepay), ("repayType", repayType), ("collateralList", collateralList));
            return await SendSignedAsync<GeneralResponse<CryptoLoanOrderIdResult>>(CREATE_BORROW_ORDER_FIXED, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<CryptoLoanOrderIdResult>?> CreateSupplyOrderFixed(string orderCurrency, string orderAmount, string annualRate, string term)
        {
            var body = new Dictionary<string, object>
            {
                { "orderCurrency", orderCurrency },
                { "orderAmount", orderAmount },
                { "annualRate", annualRate },
                { "term", term }
            };
            return await SendSignedAsync<GeneralResponse<CryptoLoanOrderIdResult>>(CREATE_SUPPLY_ORDER_FIXED, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<object>?> CancelBorrowOrderFixed(string orderId)
        {
            var body = new Dictionary<string, object> { { "orderId", orderId } };
            return await SendSignedAsync<GeneralResponse<object>>(CANCEL_BORROW_ORDER_FIXED, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<object>?> CancelSupplyOrderFixed(string orderId)
        {
            var body = new Dictionary<string, object> { { "orderId", orderId } };
            return await SendSignedAsync<GeneralResponse<object>>(CANCEL_SUPPLY_ORDER_FIXED, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<GetBorrowContractInfoFixedResult>?> GetBorrowContractInfoFixed(string? orderId = null, string? loanId = null, string? orderCurrency = null, string? term = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("orderId", orderId), ("loanId", loanId), ("orderCurrency", orderCurrency), ("term", term), ("limit", limit), ("cursor", cursor));
            return await SendSignedAsync<GeneralResponse<GetBorrowContractInfoFixedResult>>(GET_BORROW_CONTRACT_INFO_FIXED, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetSupplyContractInfoFixedResult>?> GetSupplyContractInfoFixed(string? orderId = null, string? supplyId = null, string? supplyCurrency = null, string? term = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("orderId", orderId), ("supplyId", supplyId), ("supplyCurrency", supplyCurrency), ("term", term), ("limit", limit), ("cursor", cursor));
            return await SendSignedAsync<GeneralResponse<GetSupplyContractInfoFixedResult>>(GET_SUPPLY_CONTRACT_INFO_FIXED, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetBorrowOrderInfoFixedResult>?> GetBorrowOrderInfoFixed(string? orderId = null, string? orderCurrency = null, string? state = null, string? term = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("orderId", orderId), ("orderCurrency", orderCurrency), ("state", state), ("term", term), ("limit", limit), ("cursor", cursor));
            return await SendSignedAsync<GeneralResponse<GetBorrowOrderInfoFixedResult>>(GET_BORROW_ORDER_INFO_FIXED, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<GetSupplyOrderInfoFixedResult>?> GetSupplyOrderInfoFixed(string? orderId = null, string? orderCurrency = null, string? state = null, string? term = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("orderId", orderId), ("orderCurrency", orderCurrency), ("state", state), ("term", term), ("limit", limit), ("cursor", cursor));
            return await SendSignedAsync<GeneralResponse<GetSupplyOrderInfoFixedResult>>(GET_SUPPLY_ORDER_INFO_FIXED, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<CryptoLoanRepayIdResult>?> RepayFixedLoan(string? loanId = null, string? loanCurrency = null)
        {
            var body = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(body, ("loanId", loanId), ("loanCurrency", loanCurrency));
            return await SendSignedAsync<GeneralResponse<CryptoLoanRepayIdResult>>(REPAY_FIXED_LOAN, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<object>?> RepayCollateralFixed(string loanCurrency, string collateralCoin, string amount, string? loanId = null)
        {
            var body = new Dictionary<string, object>
            {
                { "loanCurrency", loanCurrency },
                { "collateralCoin", collateralCoin },
                { "amount", amount }
            };
            BybitParametersUtils.AddOptionalParameters(body, ("loanId", loanId));
            return await SendSignedAsync<GeneralResponse<object>>(REPAY_COLLATERAL_FIXED, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<GetRepaymentHistoryFixedResult>?> GetRepaymentHistoryFixed(string? repayId = null, string? loanCurrency = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("repayId", repayId), ("loanCurrency", loanCurrency), ("limit", limit), ("cursor", cursor));
            return await SendSignedAsync<GeneralResponse<GetRepaymentHistoryFixedResult>>(GET_REPAYMENT_HISTORY_FIXED, HttpMethod.Get, query: query);
        }

        public async Task<GeneralResponse<CryptoLoanOrderIdResult>?> RenewFixedLoan(string loanId, List<CryptoLoanCollateralItem>? collateralList = null)
        {
            var body = new Dictionary<string, object> { { "loanId", loanId } };
            BybitParametersUtils.AddOptionalParameters(body, ("collateralList", collateralList));
            return await SendSignedAsync<GeneralResponse<CryptoLoanOrderIdResult>>(RENEW_FIXED_LOAN, HttpMethod.Post, query: body);
        }

        public async Task<GeneralResponse<GetRenewOrderInfoFixedResult>?> GetRenewOrderInfoFixed(string? orderId = null, string? orderCurrency = null, string? limit = null, string? cursor = null)
        {
            var query = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(query, ("orderId", orderId), ("orderCurrency", orderCurrency), ("limit", limit), ("cursor", cursor));
            return await SendSignedAsync<GeneralResponse<GetRenewOrderInfoFixedResult>>(GET_RENEW_ORDER_INFO_FIXED, HttpMethod.Get, query: query);
        }
    }
}
