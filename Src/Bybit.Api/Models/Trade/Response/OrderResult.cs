namespace bybit.net.api.Models.Trade.Response
{
    public class OrderResult
    {
        public int? RetCode { get; set; }
        public string? RetMsg { get; set; }
        public OrderDetails? Result { get; set; }
        public Dictionary<string, object>? RetExtInfo { get; set; }
        public long? Time { get; set; }
    }
}