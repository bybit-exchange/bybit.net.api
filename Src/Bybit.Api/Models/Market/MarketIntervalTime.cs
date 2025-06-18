namespace bybit.net.api.Models.Market
{
    public struct MarketIntervalTime
    {
        public string Value { get; private set; }

        private MarketIntervalTime(string interval) => Value = interval;

        public static MarketIntervalTime OneMinute => new("5min");
        public static MarketIntervalTime ThreeMinutes => new("15min");
        public static MarketIntervalTime FiveMinutes => new("30min");
        public static MarketIntervalTime FifteenMinutes => new("1h");
        public static MarketIntervalTime ThirtyMinutes => new("4h");
        public static MarketIntervalTime OneHour => new("1d");

        public override readonly string ToString() => Value;
        public static implicit operator string(MarketIntervalTime interval) => interval.Value;
    }

}
