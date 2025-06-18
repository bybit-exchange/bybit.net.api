namespace bybit.net.api.Models.Lending
{
    public struct VipLevel
    {
        private VipLevel(string value)
        {
            Value = value;
        }

        public static VipLevel NoVIP => new("No VIP");
        public static VipLevel VIP1 => new("VIP-1");
        public static VipLevel VIP2 => new("VIP-2");
        public static VipLevel VIP3 => new("VIP-3");
        public static VipLevel VIP4 => new("VIP-4");
        public static VipLevel VIP5 => new("VIP-5");
        public static VipLevel VipSupreme => new("VIP-Supreme");
        public static VipLevel Pro1 => new("PRO-1");
        public static VipLevel Pro2 => new("PRO-2");
        public static VipLevel Pro3 => new("PRO-3");
        public static VipLevel Pro4 => new("PRO-4");
        public static VipLevel Pro5 => new("PRO-5");

        public string Value { get; private set; }

        public override string ToString() => Value;

        public static implicit operator string(VipLevel vipLevel) => vipLevel.Value;
    }

}
