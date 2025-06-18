using Bybit.Api.Interfaces;
using Bybit.Api.Models;
using Bybit.Api.Models.Position;
using Bybit.Api.Models.Trade;

namespace Bybit.Api.Test.Mocks;

/// <summary>
/// Mock implementation of trade service for tests that don't need real API interactions
/// </summary>
public class MockTradeService : ITradeService
{
    public Task<string?> PlaceOrder(
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
        OrderType? slOrderType = null)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""orderId"":""mock-order-id"",""orderLinkId"":""mock-link-id""}}");
    }
    
    public Task<string?> GetTradeHistory(
        Category category, 
        string? symbol = null, 
        string? orderId = null,
        string? orderLinkId = null,
        string? baseCoin = null,
        long? startTime = null,
        long? endTime = null,
        ExecType? execType = null,
        int? limit = null,
        string? cursor = null)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""list"":[{""symbol"":""BTCUSDT"",""execFee"":""0.00023767"",""execId"":""mock-exec-id"",""execPrice"":""30000"",""execQty"":""0.001"",""execTime"":1746218313916,""execType"":""Trade"",""execValue"":""30.00"",""feeRate"":""0.00075"",""lastLiquidityInd"":""TAKER"",""orderId"":""mock-order-id"",""orderLinkId"":""mock-link-id"",""orderPrice"":""30000"",""orderQty"":""0.001"",""orderType"":""LIMIT"",""side"":""BUY"",""closedSize"":""0"",""leavesQty"":""0"",""blockTradeId"":""""}]}}");
    }
    
    public Task<string?> AmendOrder(
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
        string? slLimitPrice = null)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""orderId"":""" + orderId + @""",""orderLinkId"":""mock-link-id""}}");
    }
    
    public Task<string?> CancelOrder(
        Category category, 
        string symbol, 
        string? orderId = null, 
        string? orderLinkId = null, 
        string? orderFilter = null)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""orderId"":""" + orderId + @""",""orderLinkId"":""mock-link-id""}}");
    }
    
    public Task<string?> CancelAllOrder(
        Category category, 
        string? symbol = null,
        string? baseCoin = null,
        string? settleCoin = null,
        string? orderFilter = null,
        StopOrderType? stopOrderType = null)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""success"":true}}");
    }

    public Task<string?> PlaceBatchOrder(Category category, List<Dictionary<string, object>> request)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""list"":[{""orderId"":""mock-order-1"",""orderLinkId"":""mock-link-1""},{""orderId"":""mock-order-2"",""orderLinkId"":""mock-link-2""}]}}");
    }

    public Task<string?> PlaceBatchOrder(Category category, List<OrderRequest> request)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""list"":[{""orderId"":""mock-order-1"",""orderLinkId"":""mock-link-1""},{""orderId"":""mock-order-2"",""orderLinkId"":""mock-link-2""}]}}");
    }

    public Task<string?> AmendBatchOrder(Category category, List<Dictionary<string, object>> request)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""list"":[{""orderId"":""mock-order-1"",""orderLinkId"":""mock-link-1""},{""orderId"":""mock-order-2"",""orderLinkId"":""mock-link-2""}]}}");
    }

    public Task<string?> AmendBatchOrder(Category category, List<OrderRequest> request)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""list"":[{""orderId"":""mock-order-1"",""orderLinkId"":""mock-link-1""},{""orderId"":""mock-order-2"",""orderLinkId"":""mock-link-2""}]}}");
    }

    public Task<string?> CancelBatchOrder(Category category, List<Dictionary<string, object>> request)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""list"":[{""orderId"":""mock-order-1"",""orderLinkId"":""mock-link-1""},{""orderId"":""mock-order-2"",""orderLinkId"":""mock-link-2""}]}}");
    }

    public Task<string?> CancelBatchOrder(Category category, List<OrderRequest> request)
    {
        return Task.FromResult<string?>(
            @"{""retCode"":0,""retMsg"":""OK"",""result"":{""list"":[{""orderId"":""mock-order-1"",""orderLinkId"":""mock-link-1""},{""orderId"":""mock-order-2"",""orderLinkId"":""mock-link-2""}]}}");
    }
}