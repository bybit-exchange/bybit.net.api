namespace bybit.net.api.Models
{
    public struct MemberType
    {
        private MemberType(int value)
        {
            Value = value;
        }

        public static MemberType NormalSubAccount => new MemberType(1);
        public static MemberType CustodialSubAccount => new MemberType(6);

        public int Value { get; private set; }

        public override string ToString() => Value.ToString();

        public static implicit operator int(MemberType memberType) => memberType.Value;
    }
}
