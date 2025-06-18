namespace bybit.net.api.Models
{
    public struct UserFrozen
    {
        private UserFrozen(int value)
        {
            Value = value;
        }

        public static UserFrozen UNFREEZE => new(0);
        public static UserFrozen FREEZE => new(1);

        public int Value { get; private set; }

        public override readonly string ToString() => Value.ToString();

        public static implicit operator int(UserFrozen userFrozen) => userFrozen.Value;
    }
}
