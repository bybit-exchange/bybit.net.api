namespace bybit.net.api.Models.Account
{
    public struct TransferStatus
    {
        private TransferStatus(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static TransferStatus Success => new("SUCCESS");
        public static TransferStatus Pending => new("PENDING");
        public static TransferStatus Failed => new("FAILED");

        public override readonly string ToString() => Value;

        public static implicit operator string(TransferStatus status) => status.Value;
    }
}
