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
- Added typed account request models for manual repay and delta mode operations.
- Added typed account response models for newly implemented account endpoints and updated account mutations.
- Added account endpoint tests covering new routes and request payload mapping.

### Changed
- Updated `BybitAccountService.SetAccountMarginMode(...)` to send the correct request field name `setMarginMode`.
- Updated `BybitAccountService.ManualRepay(...)` to support the documented `repaymentType` parameter.
- Updated `BybitAccountService.ManualBorrow(...)`, `ManualRepay(...)`, and `SetAccountMarginMode(...)` to return typed models instead of raw JSON strings.

### Notes
- `GetContractTransactionLogClassic(...)` remains in the SDK because the local documentation marks it as legacy rather than fully removed.
