namespace bybit.net.api.Models.Position
{
    public class MovePositionRequest
    {
        public string? category { get; set; } // Product type: Unified account or Classic account
        public string? symbol { get; set; } // Symbol name
        public string? price { get; set; }
        public string? side { get; set; }
        public string? qty { get; set; }
    }
}
