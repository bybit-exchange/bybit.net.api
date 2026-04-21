namespace bybit.net.api.Models.Account
{
    public struct RepaymentType
    {
        private RepaymentType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static RepaymentType All => new("ALL");

        public static RepaymentType Fixed => new("FIXED");

        public static RepaymentType Flexible => new("FLEXIBLE");

        public override readonly string ToString() => Value;

        public static implicit operator string(RepaymentType repaymentType) => repaymentType.Value;
    }
}
