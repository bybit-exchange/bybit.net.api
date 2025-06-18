namespace bybit.net.api.Models
{
    public struct ReadOnly
    {
        private ReadOnly(int value)
        {
            Value = value;
        }

        public static ReadOnly READ_AND_WRITE => new(0);
        public static ReadOnly READ_ONLY => new(1);

        public int Value { get; private set; }

        public override readonly string ToString() => Value.ToString();

        public static implicit operator int(ReadOnly enm) => enm.Value;
    }
}