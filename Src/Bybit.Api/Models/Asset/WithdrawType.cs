namespace bybit.net.api.Models.Asset
{
    public struct WithdrawType
    {
        private WithdrawType(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public static WithdrawType OnChain => new(0);
        public static WithdrawType OffChain => new(1);
        public static WithdrawType All => new(2);

        public override readonly string ToString() => Value.ToString();

        public static implicit operator int(WithdrawType type) => type.Value;
    }

}
