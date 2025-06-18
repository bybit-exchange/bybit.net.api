namespace bybit.net.api.Models.Trade
{
    public struct StopOrderType
    {
        public string OrderType { get; private set; }

        private StopOrderType(string orderType) => OrderType = orderType;

        public static StopOrderType TakeProfit => new("TakeProfit");
        public static StopOrderType StopLoss => new("StopLoss");
        public static StopOrderType TrailingStop => new("TrailingStop");
        public static StopOrderType Stop => new("Stop");
        public static StopOrderType PartialTakeProfit => new("PartialTakeProfit");
        public static StopOrderType PartialStopLoss => new("PartialStopLoss");
        public static StopOrderType TpslOrder => new("tpslOrder");
        public static StopOrderType MmRateClose => new("MmRateClose");

        public override readonly string ToString() => OrderType;
    }

}
