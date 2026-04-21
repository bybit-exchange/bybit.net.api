namespace bybit.net.api.Models.SpotMargin
{
    public struct SpotMarginMode
    {
        private SpotMarginMode(int value)
        {
            Value = value;
        }

        public static SpotMarginMode ON => new(1);
        public static SpotMarginMode OFF => new(0);

        public int Value { get; private set; }

        public override readonly string ToString() => Value.ToString();

        public static implicit operator int(SpotMarginMode switchValue) => switchValue.Value;
    }
}
