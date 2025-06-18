namespace bybit.net.api.Models.Asset
{
    public struct CompleteRepayment
    {
        private CompleteRepayment(int value)
        {
            Value = value;
        }

        public static CompleteRepayment PAY_ALL => new(0);
        public static CompleteRepayment PAL_PARTIEL => new(1);

        public int Value { get; private set; }

        public override readonly string ToString() => Value.ToString();

        public static implicit operator int(CompleteRepayment completeRepayment) => completeRepayment.Value;
    }
}
