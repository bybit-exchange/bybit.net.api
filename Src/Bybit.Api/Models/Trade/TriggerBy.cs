namespace bybit.net.api.Models.Trade
{
    public struct TriggerBy
    {
        private TriggerBy(string value)
        {
            Value = value;
        }
        public static TriggerBy LastPrice => new TriggerBy("LastPrice");
        public static TriggerBy IndexPrice => new TriggerBy("IndexPrice");
        public static TriggerBy MarkPrice => new TriggerBy("MarkPrice");
        public string Value { get; private set; }
        public override string ToString() => Value;
        public static implicit operator string(TriggerBy triggerBy) => triggerBy.Value;
    }
}
