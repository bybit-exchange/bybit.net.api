namespace bybit.net.api.Models.Account
{
    public struct AutoAddMargin
    {
        private AutoAddMargin(int value)
        {
            Value = value;
        }

        public static AutoAddMargin OFF => new(0);
        public static AutoAddMargin ON => new(1);
        public int Value { get; private set; }
        public override readonly string ToString() => Value.ToString();
        public static implicit operator int(AutoAddMargin autoAddMargin) => autoAddMargin.Value;
    }
}
