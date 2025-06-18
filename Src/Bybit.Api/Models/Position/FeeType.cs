namespace bybit.net.api.Models.Position
{
    public struct FeeType
    {
        private FeeType(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public static FeeType ManualCalculation => new(0);
        public static FeeType AutomaticDeduction => new(1);

        public override readonly string ToString() => Value.ToString();

        public static implicit operator int(FeeType type) => type.Value;
    }

}
