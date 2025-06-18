namespace bybit.net.api.Models.Lending
{
    public struct OperateType
    {
        private OperateType(string value)
        {
            Value = value;
        }

        public static OperateType BIND { get => new("0"); }
        public static OperateType UNBIND { get => new("1"); }
        public string Value { get; private set; }
        public static implicit operator string(OperateType operateType) => operateType.Value;
        public override readonly string ToString() => Value.ToString();
    }
}