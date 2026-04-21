# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.3.0] - Unreleased

### Added
- Added `BybitAccountService.GetAccountInstrumentsInfo(...)` for `GET /v5/account/instruments-info`.
- Added `BybitAccountService.ManualRepayWithoutAssetConversion(...)` for `POST /v5/account/no-convert-repay`.
- Added `BybitAccountService.GetOptionAssetInfo()` for `GET /v5/account/option-asset-info`.
- Added `BybitAccountService.GetPayInfo(...)` for `GET /v5/account/pay-info`.
- Added `BybitAccountService.SetDeltaNeutralMode(...)` for `POST /v5/account/set-delta-mode`.
- Added `BybitAccountService.GetTradeInfoForAnalysis(...)` for `GET /v5/account/trade-info-for-analysis`.
- Added `BybitAccountService.GetTransferableAmount(...)` for `GET /v5/account/withdrawal`.
- Added `BybitAssetService.GetFundingAccountTransactionHistory(...)` for `GET /v5/asset/fundinghistory`.
- Added `BybitAssetService.GetPortfolioMarginInfo(...)` for `GET /v5/asset/portfolio-margin`.
- Added `BybitAssetService.GetTotalMembersAssets(...)` for `GET /v5/asset/total-members-assets`.
- Added `BybitAssetService.GetAssetOverview(...)` for `GET /v5/asset/asset-overview`.
- Added `BybitAssetService.GetWithdrawalAddressList(...)` for `GET /v5/asset/withdraw/query-address`.
- Added small balance convert methods for `GET /v5/asset/covert/small-balance-list`, `POST /v5/asset/covert/get-quote`, `POST /v5/asset/covert/small-balance-execute`, and `GET /v5/asset/covert/small-balance-history`.
- Added fiat convert methods for `GET /v5/fiat/balance-query`, `GET /v5/fiat/query-coin-list`, `POST /v5/fiat/quote-apply`, `POST /v5/fiat/trade-execute`, `GET /v5/fiat/trade-query`, `GET /v5/fiat/query-trade-history`, and `GET /v5/fiat/reference-price`.
- Added `BybitBrokerService.GetBrokerAccountInfo()` for `GET /v5/broker/account-info`.
- Added `BybitBrokerService.GetBrokerSubMemberDepositRecords(...)` for `GET /v5/broker/asset/query-sub-member-deposit-record`.
- Added `BybitBrokerService.GetBrokerAllRateLimits(...)` for `GET /v5/broker/apilimit/query-all`.
- Added `BybitBrokerService.GetBrokerRateLimitCap()` for `GET /v5/broker/apilimit/query-cap`.
- Added `BybitBrokerService.SetBrokerRateLimit(...)` for `POST /v5/broker/apilimit/set`.
- Added `BybitBrokerService.GetBrokerVoucherSpec(...)` for `POST /v5/broker/award/info`.
- Added `BybitBrokerService.IssueBrokerVoucher(...)` for `POST /v5/broker/award/distribute-award`.
- Added `BybitBrokerService.GetIssuedBrokerVoucher(...)` for `POST /v5/broker/award/distribution-record`.
- Added easy earn methods for `GET /v5/earn/apr-history`, `GET /v5/earn/hourly-yield`, `GET /v5/earn/yield`, and `POST /v5/earn/position/modify`.
- Added fixed-term earn methods for `GET /v5/earn/fixed-term/product`, `POST /v5/earn/fixed-term/place-order`, `GET /v5/earn/fixed-term/order`, `GET /v5/earn/fixed-term/position`, `POST /v5/earn/fixed-term/redeem`, and `POST /v5/earn/fixed-term/position/auto-invest`.
- Added token earn methods for `GET /v5/earn/token/product`, `POST /v5/earn/token/place-order`, `GET /v5/earn/token/order`, `GET /v5/earn/token/position`, `GET /v5/earn/token/yield`, `GET /v5/earn/token/hourly-yield`, and `GET /v5/earn/token/history-apr`.
- Added `BybitLendingService.RepayInsLoan(...)` for `POST /v5/ins-loan/repay-loan`.
- Added `BybitMarketDataService.GetRpiOrderbook(...)` for `GET /v5/market/rpi_orderbook`.
- Added `BybitMarketDataService.GetAdlAlert(...)` for `GET /v5/market/adlAlert`.
- Added `BybitMarketDataService.GetIndexPriceComponents(...)` for `GET /v5/market/index-price-components`.
- Added `BybitMarketDataService.GetFeeGroupInfo(...)` for `GET /v5/market/fee-group-info`.
- Added typed account request models for manual repay and delta mode operations.
- Added typed affiliate response models for affiliate user list and affiliate user info endpoints.
- Added typed account response models for newly implemented account endpoints and updated account mutations.
- Added typed asset response models for transfer, funding, portfolio margin, withdrawal address, small balance convert, fiat convert, delivery, settlement, exchange record, and allowed deposit endpoints.
- Added typed broker response and request models for earnings, account info, subaccount deposits, rate limits, and voucher endpoints.
- Added typed earn response models for shared earn, fixed-term earn, and BYUSDT token earn endpoints.
- Added typed lending response models for institutional lending and legacy C2C lending endpoints.
- Added typed market response models for recent trades, open interest, insurance pool, delivery price, index price components, ADL alerts, and fee group info.
- Added account endpoint tests covering new routes and request payload mapping.
- Added asset endpoint tests covering route selection, payload mapping, and public access behavior.
- Added broker endpoint tests covering current broker routes and request payload mapping.
- Added earn endpoint tests covering shared earn, fixed-term, and token endpoint routing and payload mapping.
- Added lending endpoint tests covering current OTC routes, public access behavior, and corrected legacy lending routes.
- Added affiliate endpoint tests covering the affiliate date-filter query mapping and typed response mapping.

### Changed
- Updated `BybitAccountService.SetAccountMarginMode(...)` to send the correct request field name `setMarginMode`.
- Updated `BybitAccountService.ManualRepay(...)` to support the documented `repaymentType` parameter.
- Updated `BybitAccountService.ManualBorrow(...)`, `ManualRepay(...)`, and `SetAccountMarginMode(...)` to return typed models instead of raw JSON strings.
- Updated `BybitAssetService.GetTransferableCoin(...)`, `CreateInternalTransfer(...)`, and `CreateUniversalTransfer(...)` to send `fromAccountType`.
- Updated `BybitAssetService.GetInternalTransferRecords(...)` and `GetUniversalTransferRecords(...)` to send `status` instead of `transferStatus`.
- Updated `BybitAssetService.GetAssetAllowedDepositInfo(...)` to use the public endpoint flow instead of signed authentication.
- Updated `BybitAssetService.GetDeliveryRecord(...)`, `GetCoinExchangeRecords(...)`, and `GetAssetUsdcSettlement(...)` to return typed response models.
- Updated `BybitBrokerService.GetBrokerEarning(...)` to use `GET /v5/broker/earnings-info` with `begin`, `end`, and `uid` instead of the obsolete `earning-record` route and `startTime`/`endTime` fields.
- Updated `BybitEarnService.GetProductInfo(...)`, `PlaceEarnOrder(...)`, `GetEarnOrderHistory(...)`, and `GetStakedPosition(...)` to return typed models instead of raw JSON strings.
- Updated `BybitEarnService.GetEarnOrderHistory(...)` to support the documented `productId`, `startTime`, `endTime`, `limit`, and `cursor` parameters.
- Updated `BybitEarnService` with public constructors so public earn endpoints can be used without API credentials.
- Updated `BybitLendingService.GetInsLoanOrders(...)` and `GetInsLoanRepayOrders(...)` to use the current OTC routes instead of the incorrect `ensure-tokens-convert` placeholder route.
- Updated `BybitLendingService.GetInsLoanInfo(...)` and `GetInsMarginCoinInfo(...)` to use the public endpoint flow and return typed models.
- Updated `BybitLendingService` legacy C2C methods to use typed models and corrected routes for `redeem-cancel` and `history-order`.
- Updated `BybitAffiliateService.GetAffiliateUserList(...)` to support the documented `startDate` and `endDate` parameters.
- Updated `BybitAffiliateService.GetAffiliateUserList(...)` and `GetAffiliateUserInfo(...)` to return typed models instead of raw JSON strings.
- Updated `BybitMarketDataService` public endpoints to return typed `GeneralResponse<T>` models instead of raw JSON strings.
- Updated `BybitMarketDataService.GetInstrumentInfo(...)` to support the documented `symbolType` parameter.
- Updated `BybitMarketDataService.GetMarketOrderbook(...)` to require the documented `symbol` parameter.
- Updated market kline methods to support omitted `category` where the V5 docs define a default.
- Updated `BybitMarketDataService.GetMarketHistoricalVolatility(...)` to support `quoteCoin` and the documented integer `period` request shape.

### Notes
- `GetContractTransactionLogClassic(...)` remains in the SDK because the local documentation marks it as legacy rather than fully removed.
- Removed the stale `GetAssetDeliveryRecords(...)` implementation that pointed to the coin exchange history route instead of the active delivery endpoint.
