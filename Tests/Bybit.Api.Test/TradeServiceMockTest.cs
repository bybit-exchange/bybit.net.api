using Bybit.Api.Models;
using Bybit.Api.Models.Trade;
using Bybit.Api.Models.Trade.Response;
using Bybit.Api.Test.Mocks;
using Newtonsoft.Json;
using Xunit;

namespace Bybit.Api.Test;

[Trait("Category", "UnitTest")]
public class TradeServiceMockTest
{
    private readonly MockTradeService tradeService;
    
    public TradeServiceMockTest()
    {
        tradeService = new MockTradeService();
    }
    
    [Fact]
    public async Task Check_PlaceOrder_WithMock()
    {
        var orderInfoString = await tradeService.PlaceOrder(
            category: Category.LINEAR,
            symbol: "BTCUSDT",
            side: Side.BUY,
            orderType: OrderType.LIMIT,
            qty: "0.001",
            price: "30000");
            
        Assert.NotNull(orderInfoString);
        
        var orderResult = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
        Assert.Equal(0, orderResult?.RetCode);
        Assert.Equal("OK", orderResult?.RetMsg);
        Assert.Equal("mock-order-id", orderResult?.Result?.OrderId);
    }
    
    [Fact]
    public async Task Check_GetTradeHistory_WithMock()
    {
        var historyString = await tradeService.GetTradeHistory(
            category: Category.LINEAR,
            symbol: "BTCUSDT");
            
        Assert.NotNull(historyString);
        
        // Verify the mock response structure
        var result = JsonConvert.DeserializeObject<dynamic>(historyString);
        Assert.Equal(0, (int)result.retCode);
        Assert.Equal("OK", (string)result.retMsg);
        
        // Make sure there's at least one trade in the list
        Assert.NotEmpty((System.Collections.IEnumerable)result.result.list);
    }
    
    [Fact]
    public async Task Check_AmendOrder_WithMock()
    {
        string testOrderId = "test-123456";
        
        var orderInfoString = await tradeService.AmendOrder(
            category: Category.LINEAR, 
            symbol: "BTCUSDT", 
            orderId: testOrderId,
            price: "31000", 
            qty: "0.002");
        
        Assert.NotNull(orderInfoString);
        
        var orderResult = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
        Assert.Equal(0, orderResult?.RetCode);
        
        // Verify that the mock implementation returns the same order ID we sent
        Assert.Equal(testOrderId, orderResult?.Result?.OrderId);
    }
    
    [Fact]
    public async Task Check_CancelOrder_WithMock()
    {
        string testOrderId = "test-cancel-123456";
        
        var orderInfoString = await tradeService.CancelOrder(
            category: Category.LINEAR, 
            symbol: "BTCUSDT",
            orderId: testOrderId);
        
        Assert.NotNull(orderInfoString);
        
        var orderResult = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
        Assert.Equal(0, orderResult?.RetCode);
        
        // Verify that the mock implementation returns the same order ID we sent
        Assert.Equal(testOrderId, orderResult?.Result?.OrderId);
    }
}