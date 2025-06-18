namespace bybit.net.api.Models.SpotMargin
{
    public struct SpotMarginMode
    {
        private SpotMarginMode(int value)
        {
            Value = value;
        }

        public static SpotMarginMode ON => new(0);
        public static SpotMarginMode OFF => new(1);

        public int Value { get; private set; }

        public override readonly string ToString() => Value.ToString();

        public static implicit operator int(SpotMarginMode switchValue) => switchValue.Value;
    }
}
