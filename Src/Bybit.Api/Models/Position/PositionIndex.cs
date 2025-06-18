namespace bybit.net.api.Models.Position
{
    public struct PositionIndex
    {
        private PositionIndex(int value)
        {
            Value = value;
        }

        public static PositionIndex OneWayMode => new(0);
        public static PositionIndex HedgeModeBuySide => new(1);
        public static PositionIndex HedgeModeSellSide => new(2);
        public int Value { get; private set; }
        public override readonly string ToString() => Value.ToString();
        public static implicit operator int(PositionIndex positionIdx) => positionIdx.Value;
    }
}
