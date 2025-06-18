namespace bybit.net.api.Models.Account
{
    public struct SetMarginMode
    {
        private SetMarginMode(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static SetMarginMode ISOLATED_MARGIN => new("ISOLATED_MARGIN");
        public static SetMarginMode REGULAR_MARGIN => new("REGULAR_MARGIN");
        public static SetMarginMode PORTFOLIO_MARGIN => new("PORTFOLIO_MARGIN");

        public override readonly string ToString() => Value;

        public static implicit operator string(SetMarginMode marginMode) => marginMode.Value;
    }

}
