namespace bybit.net.api.Models.Market
{
    public struct MarketInterval
    {
        public string Value { get; private set; }

        private MarketInterval(string interval) => Value = interval;

        public static MarketInterval OneMinute => new("1");
        public static MarketInterval ThreeMinutes => new("3");
        public static MarketInterval FiveMinutes => new("5");
        public static MarketInterval FifteenMinutes => new("15");
        public static MarketInterval ThirtyMinutes => new("30");
        public static MarketInterval OneHour => new("60");
        public static MarketInterval TwoHours => new("120");
        public static MarketInterval FourHours => new("240");
        public static MarketInterval SixHours => new("360");
        public static MarketInterval TwelveHours => new("720");
        public static MarketInterval Daily => new("D");
        public static MarketInterval Monthly => new("M");
        public static MarketInterval Weekly => new("W");

        public override readonly string ToString() => Value;
        public static implicit operator string(MarketInterval interval) => interval.Value;
    }

}
