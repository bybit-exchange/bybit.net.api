namespace bybit.net.api.Models.Position
{
    public struct ExecType
    {
        private ExecType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static ExecType Trade => new("Trade");
        public static ExecType AdlTrade => new("AdlTrade"); // Auto-Deleveraging
        public static ExecType Funding => new("Funding"); //  Funding fee
        public static ExecType BustTrade => new("BustTrade"); // Liquidation
        public static ExecType Delivery => new("Delivery");//  USDC futures delivery
        public static ExecType BlockTrade => new("BlockTrade");

        public override readonly string ToString() => Value;

        public static implicit operator string(ExecType execType) => execType.Value;
    }

}
