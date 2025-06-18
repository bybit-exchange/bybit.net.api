namespace bybit.net.api.Models.Trade
{
    public struct TimeInForce
    {
        private TimeInForce(string value)
        {
            Value = value;
        }

        public static TimeInForce GTC { get => new("GTC"); }
        public static TimeInForce FOK { get => new("FOK"); }
        public static TimeInForce IOC { get => new("IOC"); }
        public static TimeInForce POSTONLY { get => new("PostOnly"); }
        public string Value { get; private set; }
        public static implicit operator string(TimeInForce enm) => enm.Value;
        public override string ToString() => Value.ToString();
    }
}
