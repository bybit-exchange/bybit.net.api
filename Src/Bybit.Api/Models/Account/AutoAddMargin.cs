namespace bybit.net.api.Models.Account
{
    public struct SpotHedgeMode
    {
        private SpotHedgeMode(string value)
        {
            Value = value;
        }

        public static SpotHedgeMode OFF => new("OFF");
        public static SpotHedgeMode ON => new("ON");
        public string Value { get; private set; }
        public override readonly string ToString() => Value.ToString();
        public static implicit operator string(SpotHedgeMode spotHedgeMode) => spotHedgeMode.Value;
    }
}
