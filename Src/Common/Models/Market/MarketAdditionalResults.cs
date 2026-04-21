using System.Collections.Generic;

namespace bybit.net.api.Models.Market
{
    public class GetRecentTradeResult
    {
        public string? category { get; set; }
        public List<RecentTradeEntry>? list { get; set; }
    }

    public class RecentTradeEntry
    {
        public string? execId { get; set; }
        public string? symbol { get; set; }
        public string? price { get; set; }
        public string? size { get; set; }
        public string? side { get; set; }
        public string? time { get; set; }
        public bool? isBlockTrade { get; set; }
        public bool? isRPITrade { get; set; }
        public string? mP { get; set; }
        public string? iP { get; set; }
        public string? mIv { get; set; }
        public string? iv { get; set; }
        public string? seq { get; set; }
    }

    public class GetOpenInterestResult
    {
        public string? symbol { get; set; }
        public string? category { get; set; }
        public List<OpenInterestEntry>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class OpenInterestEntry
    {
        public string? openInterest { get; set; }
        public string? timestamp { get; set; }
    }

    public class GetInsurancePoolResult
    {
        public string? updatedTime { get; set; }
        public List<InsurancePoolEntry>? list { get; set; }
    }

    public class InsurancePoolEntry
    {
        public string? coin { get; set; }
        public string? symbols { get; set; }
        public string? balance { get; set; }
        public string? value { get; set; }
    }

    public class GetDeliveryPriceResult
    {
        public string? category { get; set; }
        public List<DeliveryPriceEntry>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class DeliveryPriceEntry
    {
        public string? symbol { get; set; }
        public string? deliveryPrice { get; set; }
        public string? deliveryTime { get; set; }
    }

    public class GetIndexPriceComponentsResult
    {
        public string? indexName { get; set; }
        public string? lastPrice { get; set; }
        public string? updateTime { get; set; }
        public List<IndexPriceComponentEntry>? components { get; set; }
    }

    public class IndexPriceComponentEntry
    {
        public string? exchange { get; set; }
        public string? spotPair { get; set; }
        public string? equivalentPrice { get; set; }
        public string? multiplier { get; set; }
        public string? price { get; set; }
        public string? weight { get; set; }
    }

    public class GetAdlAlertResult
    {
        public string? updatedTime { get; set; }
        public List<AdlAlertEntry>? list { get; set; }
    }

    public class AdlAlertEntry
    {
        public string? coin { get; set; }
        public string? symbol { get; set; }
        public string? balance { get; set; }
        public string? maxBalance { get; set; }
        public string? insurancePnlRatio { get; set; }
        public string? pnlRatio { get; set; }
        public string? adlTriggerThreshold { get; set; }
        public string? adlStopRatio { get; set; }
    }

    public class GetFeeGroupInfoResult
    {
        public List<FeeGroupInfoEntry>? list { get; set; }
    }

    public class FeeGroupInfoEntry
    {
        public string? groupName { get; set; }
        public int? weightingFactor { get; set; }
        public int? symbolsNumbers { get; set; }
        public List<string>? symbols { get; set; }
        public FeeRates? feeRates { get; set; }
        public string? updateTime { get; set; }
    }

    public class FeeRates
    {
        public List<FeeRateDetail>? pro { get; set; }
        public List<FeeRateDetail>? marketMaker { get; set; }
    }

    public class FeeRateDetail
    {
        public string? level { get; set; }
        public string? takerFeeRate { get; set; }
        public string? makerFeeRate { get; set; }
        public string? makerRebate { get; set; }
    }
}
