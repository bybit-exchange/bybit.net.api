namespace bybit.net.api.Models.Trade
{
    public struct TriggerDirection
    {
        public int Value { get; private set; }

        public static readonly TriggerDirection TriggerOnRise = new(1);
        public static readonly TriggerDirection TriggerOnFall = new(2);

        private TriggerDirection(int value)
        {
            Value = value;
        }
    }
}
