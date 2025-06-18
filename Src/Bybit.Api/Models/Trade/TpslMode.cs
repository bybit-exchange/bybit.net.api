namespace bybit.net.api.Models.Trade
{
    public struct TpslMode
    {
        private TpslMode(string value)
        {
            Value = value;
        }

        public static TpslMode Full => new("Full");
        public static TpslMode Partial => new("Partial");
        public string Value { get; private set; }
        public override string ToString() => Value;
        public static implicit operator string(TpslMode tpslMode) => tpslMode.Value;
    }

}
