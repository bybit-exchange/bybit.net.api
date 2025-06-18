namespace bybit.net.api.Models.Trade
{
    public class OrderRequest
    {
        public string? Category { get; set; } // Product type: Unified account or Classic account
        public string? Symbol { get; set; } // Symbol name
        public bool? IsLeverage { get; set; } // Whether to borrow: 0 (false) or 1 (true)
        public string? Side { get; set; } // Buy or Sell
        public string? OrderType { get; set; } // Market or Limit
        public string? Qty { get; set; } // Order quantity
        public string? Price { get; set; } // Order price
        public string? TriggerDirection { get; set; } // Conditional order param
        public string? OrderFilter { get; set; } // Order or tpslOrder or StopOrder
        public string? TriggerPrice { get; set; } // Conditional order trigger price
        public string? TriggerBy { get; set; } // Trigger price type: LastPrice, IndexPrice, MarkPrice
        public string? OrderIv { get; set; } // Implied volatility
        public string? TimeInForce { get; set; } // Time in force
        public int? PositionIdx { get; set; } // Position mode identifier
        public string? OrderLinkId { get; set; } // User customised order ID
        public string? OrderId { get; set; } // Server Order Id
        public string? TakeProfit { get; set; } // Take profit price
        public string? StopLoss { get; set; } // Stop loss price
        public string? TpTriggerBy { get; set; } // Price type to trigger take profit
        public string? SlTriggerBy { get; set; } // Price type to trigger stop loss
        public bool? ReduceOnly { get; set; } // Reduce-only order flag
        public bool? CloseOnTrigger { get; set; } // Close on trigger order flag
        public string? SmpType { get; set; } // Smp execution type
        public bool? Mmp { get; set; } // Market maker protection flag
        public string? TpslMode { get; set; } // TP/SL mode: Full or Partial
        public string? TpLimitPrice { get; set; } // Limit order price when take profit is triggered
        public string? SlLimitPrice { get; set; } // Limit order price when stop loss is triggered
        public string? TpOrderType { get; set; } // Order type when take profit is triggered
        public string? SlOrderType { get; set; } // Order type when stop loss is triggered
    }

}
