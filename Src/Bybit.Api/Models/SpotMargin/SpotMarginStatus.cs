namespace bybit.net.api.Models.SpotMargin
{
    public struct SpotMarginStatus
    {
        private SpotMarginStatus(int value)
        {
            Value = value;
        }

        public static SpotMarginStatus ALL_STATUS => new(0); // default
        public static SpotMarginStatus UNCLEARED => new(1);
        public static SpotMarginStatus CLEARED => new(2);

        public int Value { get; private set; }

        public override readonly string ToString() => Value.ToString();

        public static implicit operator int(SpotMarginStatus switchValue) => switchValue.Value;
    }
}
