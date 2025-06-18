namespace bybit.net.api.Models.Position
{
    public struct PositionMode
    {
        private PositionMode(int value)
        {
            Value = value;
        }

        public static PositionMode OneWay => new(0);
        public static PositionMode HedgeMode => new(3);
        public int Value { get; private set; }
        public override string ToString() => Value.ToString();
        public static implicit operator int(PositionMode positionMode) => positionMode.Value;
    }

}
