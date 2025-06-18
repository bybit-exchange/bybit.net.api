namespace bybit.net.api.Models.User
{
    public struct IsUta
    {
        private IsUta(bool value)
        {
            Value = value;
        }

        public static IsUta CreateUtaAccount => new IsUta(true);
        public static IsUta CreateClassicAccount => new IsUta(false);

        public bool Value { get; private set; }

        public override readonly string ToString() => Value.ToString();

        public static implicit operator bool(IsUta isUta) => isUta.Value;
    }
}
