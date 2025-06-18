namespace bybit.net.api.Models.Trade
{
    public struct Side
    {
        private Side(string value)
        {
            Value = value;
        }

        public static Side BUY { get => new("Buy"); }
        public static Side SELL { get => new("Sell"); }
        public string Value { get; private set; }
        public static implicit operator string(Side enm) => enm.Value;
        public override readonly string ToString() => Value.ToString();
    }
}
