namespace bybit.net.api.Models.Account
{
    public struct DeltaNeutralMode
    {
        private DeltaNeutralMode(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static DeltaNeutralMode Enable => new("1");

        public static DeltaNeutralMode Disable => new("0");

        public override readonly string ToString() => Value;

        public static implicit operator string(DeltaNeutralMode deltaNeutralMode) => deltaNeutralMode.Value;
    }
}
