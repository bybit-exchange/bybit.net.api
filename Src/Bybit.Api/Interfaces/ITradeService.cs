using Bybit.Api.Models;
using Bybit.Api.Models.Position;
using Bybit.Api.Models.Trade;

namespace Bybit.Api.Interfaces;

/// <summary>
/// Interface for Bybit Trade API services
/// </summary>
public interface ITradeService
{
    /// <summary>
    /// Gets trade execution history
    /// </summary>
    Task<string?> GetTradeHistory(
        Category category, 
        string? symbol = null, 
        string? orderId = null, 
        string? orderLinkId = null, 
        string? baseCoin = null, 
        long? startTime = null, 
        long? endTime = null, 
        ExecType? execType = null, 
        int? limit = null, 
        string? cursor = null);

    /// <summary>
    /// Place a new order
    /// </summary>
    Task<string?> PlaceOrder(
        Category category,
        string symbol,
        Side side,
        OrderType orderType,
        string qty,
        string? price = null,
        TimeInForce? timeInForce = null,
        int? isLeverage = null,
        int? triggerDirection = null,
        string? orderFilter = null,
        string? triggerPrice = null,
        TriggerBy? triggerBy = null,
        string? orderIv = null,
        int? positionIdx = null,
        string? orderLinkId = null,
        string? takeProfit = null,
        string? stopLoss = null,
        TriggerBy? tpTriggerBy = null,
        TriggerBy? slTriggerBy = null,
        bool? reduceOnly = null,
        bool? closeOnTrigger = null,
        SmpType? smpType = null,
        bool? mmp = null,
        TpslMode? tpslMode = null,
        string? tpLimitPrice = null,
        string? slLimitPrice = null,
        OrderType? tpOrderType = null,
        OrderType? slOrderType = null);

    /// <summary>
    /// Place batch orders
    /// </summary>
    Task<string?> PlaceBatchOrder(Category category, List<Dictionary<string, object>> request);
    
    /// <summary>
    /// Place batch orders using OrderRequest objects
    /// </summary>
    Task<string?> PlaceBatchOrder(Category category, List<OrderRequest> request);

    /// <summary>
    /// Amend an existing order
    /// </summary>
    Task<string?> AmendOrder(
        Category category, 
        string symbol, 
        string? orderId = null, 
        string? orderLinkId = null, 
        string? orderIv = null, 
        string? qty = null, 
        string? price = null,
        string? triggerPrice = null, 
        TriggerBy? triggerBy = null, 
        string? takeProfit = null, 
        string? stopLoss = null, 
        TriggerBy? tpTriggerBy = null, 
        TriggerBy? slTriggerBy = null, 
        string? tpLimitPrice = null, 
        string? slLimitPrice = null);

    /// <summary>
    /// Amend batch orders
    /// </summary>
    Task<string?> AmendBatchOrder(Category category, List<Dictionary<string, object>> request);
    
    /// <summary>
    /// Amend batch orders using OrderRequest objects
    /// </summary>
    Task<string?> AmendBatchOrder(Category category, List<OrderRequest> request);

    /// <summary>
    /// Cancel an order
    /// </summary>
    Task<string?> CancelOrder(
        Category category, 
        string symbol, 
        string? orderId = null, 
        string? orderLinkId = null, 
        string? orderFilter = null);

    /// <summary>
    /// Cancel batch orders
    /// </summary>
    Task<string?> CancelBatchOrder(Category category, List<Dictionary<string, object>> request);
    
    /// <summary>
    /// Cancel batch orders using OrderRequest objects
    /// </summary>
    Task<string?> CancelBatchOrder(Category category, List<OrderRequest> request);

    /// <summary>
    /// Cancel all open orders
    /// </summary>
    Task<string?> CancelAllOrder(
        Category category, 
        string? symbol = null, 
        string? baseCoin = null, 
        string? settleCoin = null, 
        string? orderFilter = null, 
        StopOrderType? stopOrderType = null);
}