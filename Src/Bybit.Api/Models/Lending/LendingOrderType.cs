namespace bybit.net.api.Models.Lending
{
    public struct LendingOrderType
    {
        public int Value { get; private set; }
        private LendingOrderType(int value) => Value = value;

        public static LendingOrderType DEPOSIT => new(1);
        public static LendingOrderType REDEMPTION => new(2);
        public static LendingOrderType PAYMENT_PROCEED => new(3);

        public static implicit operator string(LendingOrderType enm) => enm.Value.ToString();
        public override readonly string ToString() => Value.ToString();
    }
}
