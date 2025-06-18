namespace bybit.net.api.Models.SpotMargin
{
    public struct SwitchStatus
    {
        private SwitchStatus(int value)
        {
            Value = value;
        }

        public static SwitchStatus ON => new(0);
        public static SwitchStatus OFF => new(1);

        public int Value { get; private set; }

        public override readonly string ToString() => Value.ToString();

        public static implicit operator int(SwitchStatus switchValue) => switchValue.Value;
    }
}
