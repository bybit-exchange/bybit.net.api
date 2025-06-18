namespace bybit.net.api.Models.Trade
{
    public struct OrderType
    {
        private OrderType(string value)
        {
            Value = value;
        }

        public static OrderType LIMIT { get => new("Limit"); }
        public static OrderType MARKET { get => new("Market"); }
        public string Value { get; private set; }
        public static implicit operator string(OrderType enm) => enm.Value;
        public override readonly string ToString() => Value.ToString();
    }
}
