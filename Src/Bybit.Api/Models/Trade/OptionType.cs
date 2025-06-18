namespace bybit.net.api.Models.Trade
{
    public struct OptionType
    {
        public string Value { get; private set; }
        private OptionType(string value) => Value = value;

        public static OptionType Call => new("CALL");
        public static OptionType Put => new("PUT");

        public static implicit operator string(OptionType enm) => enm.Value;
        public override readonly string ToString() => Value.ToString();
    }
}
