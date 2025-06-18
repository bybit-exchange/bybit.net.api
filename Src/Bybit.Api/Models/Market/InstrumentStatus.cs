namespace bybit.net.api.Models.Market
{
    public struct InstrumentStatus
    {
        public string Status { get; private set; }

        private InstrumentStatus(string status) => Status = status;

        public static InstrumentStatus PreLaunch => new("PreLaunch");
        public static InstrumentStatus Trading => new("Trading");
        public static InstrumentStatus Settling => new("Settling");
        public static InstrumentStatus Delivering => new("Delivering");
        public static InstrumentStatus Closed => new("Closed");

        public override readonly string ToString() => Status;
        public static implicit operator string(InstrumentStatus instrumentStatus) => instrumentStatus.Status;
    }

}
