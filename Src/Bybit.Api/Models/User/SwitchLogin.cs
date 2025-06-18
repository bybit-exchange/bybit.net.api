namespace bybit.net.api.Models.User
{
    public struct SwitchLogin
    {
        private SwitchLogin(int value)
        {
            Value = value;
        }

        public static SwitchLogin TurnOffQuickLogin => new(0);
        public static SwitchLogin TurnOnQuickLogin => new(1);

        public int Value { get; private set; }

        public override readonly string ToString() => Value.ToString();

        public static implicit operator int(SwitchLogin switchValue) => switchValue.Value;
    }
}