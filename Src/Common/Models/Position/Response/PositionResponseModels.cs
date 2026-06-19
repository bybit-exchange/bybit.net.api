namespace bybit.net.api.Models.Position.Response
{
    public class GetPositionInfoResult
    {
        public string? category { get; set; }
        public string? nextPageCursor { get; set; }
        public List<PositionInfo>? list { get; set; }
    }

    public class PositionInfo
    {
        public int? positionIdx { get; set; }
        public int? riskId { get; set; }
        public string? riskLimitValue { get; set; }
        public string? symbol { get; set; }
        public string? side { get; set; }
        public string? size { get; set; }
        public string? avgPrice { get; set; }
        public string? positionValue { get; set; }
        public int? autoAddMargin { get; set; }
        public string? positionStatus { get; set; }
        public string? leverage { get; set; }
        public string? breakEvenPrice { get; set; }
        public string? markPrice { get; set; }
        public string? liqPrice { get; set; }
        public string? positionIM { get; set; }
        public string? positionIMByMp { get; set; }
        public string? positionMM { get; set; }
        public string? positionMMByMp { get; set; }
        public string? takeProfit { get; set; }
        public string? stopLoss { get; set; }
        public string? trailingStop { get; set; }
        public string? sessionAvgPrice { get; set; }
        public string? delta { get; set; }
        public string? gamma { get; set; }
        public string? vega { get; set; }
        public string? theta { get; set; }
        public string? unrealisedPnl { get; set; }
        public string? curRealisedPnl { get; set; }
        public string? cumRealisedPnl { get; set; }
        public int? adlRankIndicator { get; set; }
        public string? createdTime { get; set; }
        public string? updatedTime { get; set; }
        public long? seq { get; set; }
        public bool? isReduceOnly { get; set; }
        public string? mmrSysUpdatedTime { get; set; }
        public string? leverageSysUpdatedTime { get; set; }
        public string? tpslMode { get; set; }
        public string? bustPrice { get; set; }
        public string? positionBalance { get; set; }
        public int? tradeMode { get; set; }
    }

    public class PositionMarginResult
    {
        public string? category { get; set; }
        public string? symbol { get; set; }
        public int? positionIdx { get; set; }
        public int? riskId { get; set; }
        public string? riskLimitValue { get; set; }
        public string? size { get; set; }
        public string? avgPrice { get; set; }
        public string? liqPrice { get; set; }
        public string? bustPrice { get; set; }
        public string? markPrice { get; set; }
        public string? positionValue { get; set; }
        public string? leverage { get; set; }
        public int? autoAddMargin { get; set; }
        public string? positionStatus { get; set; }
        public string? positionIM { get; set; }
        public string? positionMM { get; set; }
        public string? takeProfit { get; set; }
        public string? stopLoss { get; set; }
        public string? trailingStop { get; set; }
        public string? unrealisedPnl { get; set; }
        public string? cumRealisedPnl { get; set; }
        public string? createdTime { get; set; }
        public string? updatedTime { get; set; }
    }

    public class GetClosedPnlResult
    {
        public string? category { get; set; }
        public List<ClosedPnl>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class ClosedPnl
    {
        public string? symbol { get; set; }
        public string? orderId { get; set; }
        public string? side { get; set; }
        public string? qty { get; set; }
        public string? orderPrice { get; set; }
        public string? orderType { get; set; }
        public string? execType { get; set; }
        public string? closedSize { get; set; }
        public string? cumEntryValue { get; set; }
        public string? avgEntryPrice { get; set; }
        public string? cumExitValue { get; set; }
        public string? avgExitPrice { get; set; }
        public string? closedPnl { get; set; }
        public string? fillCount { get; set; }
        public string? leverage { get; set; }
        public string? openFee { get; set; }
        public string? closeFee { get; set; }
        public string? createdTime { get; set; }
        public string? updatedTime { get; set; }
    }

    public class GetMovePositionHistoryResult
    {
        public List<MovePositionHistory>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }

    public class MovePositionHistory
    {
        public string? blockTradeId { get; set; }
        public string? category { get; set; }
        public string? orderId { get; set; }
        public long? userId { get; set; }
        public string? symbol { get; set; }
        public string? side { get; set; }
        public string? price { get; set; }
        public string? qty { get; set; }
        public string? execFee { get; set; }
        public string? status { get; set; }
        public string? execId { get; set; }
        public int? resultCode { get; set; }
        public string? resultMessage { get; set; }
        public long? createdAt { get; set; }
        public long? updatedAt { get; set; }
        public string? rejectParty { get; set; }
    }
}
