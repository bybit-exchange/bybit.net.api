namespace bybit.net.api.Models.Asset
{
    public struct WithBonus
    {
        private WithBonus(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static WithBonus False => new("0");
        public static WithBonus True => new("1");

        public override readonly string ToString() => Value;

        public static implicit operator string(WithBonus withBonus) => withBonus.Value;
    }

}
