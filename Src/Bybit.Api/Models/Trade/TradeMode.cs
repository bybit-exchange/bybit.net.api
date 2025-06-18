namespace bybit.net.api.Models.Trade
{
    public struct TradeMode
    {
        private TradeMode(int value)
        {
            Value = value;
        }
        public static TradeMode CrossMargin => new(0);
        public static TradeMode IsolatedMargin => new(1);
        public int Value { get; private set; }
        public override readonly string ToString() => Value.ToString();
        public static implicit operator int(TradeMode tradeMode) => tradeMode.Value;
    }
}
