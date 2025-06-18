namespace bybit.net.api.Models.Trade
{
    public struct OrderStatus
    {
        public string Status { get; private set; }

        private OrderStatus(string status) => Status = status;

        public static OrderStatus Created => new("Created");
        public static OrderStatus New => new("New");
        public static OrderStatus Rejected => new("Rejected");
        public static OrderStatus PartiallyFilled => new("PartiallyFilled");
        public static OrderStatus PartiallyFilledCanceled => new("PartiallyFilledCanceled");
        public static OrderStatus Filled => new("Filled");
        public static OrderStatus Cancelled => new("Cancelled");
        public static OrderStatus Untriggered => new("Untriggered");
        public static OrderStatus Triggered => new("Triggered");
        public static OrderStatus Deactivated => new("Deactivated");
        public static OrderStatus Active => new("Active");

        public override readonly string ToString() => Status;
    }

}
