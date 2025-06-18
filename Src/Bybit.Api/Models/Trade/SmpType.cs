namespace bybit.net.api.Models.Trade
{
    public struct SmpType
    {
        private SmpType(string value)
        {
            Value = value;
        }

        public static SmpType None => new("None");
        public static SmpType CancelMaker => new("CancelMaker");
        public static SmpType CancelTaker => new("CancelTaker");
        public static SmpType CancelBoth => new("CancelBoth");
        public string Value { get; private set; }
        public override string ToString() => Value;
        public static implicit operator string(SmpType smpType) => smpType.Value;
    }
}
