namespace bybit.net.api.Models.Lending
{
    public struct BizType
    {
        private BizType(string value)
        {
            Value = value;
        }

        public static BizType SPOT { get => new("SPOT"); }
        public static BizType DERIVATIVES { get => new("DERIVATIVES"); }
        public static BizType OPTIONS { get => new("OPTIONS"); }
        public string Value { get; private set; }
        public static implicit operator string(BizType bizType) => bizType.Value;
        public override readonly string ToString() => Value.ToString();
    }
}