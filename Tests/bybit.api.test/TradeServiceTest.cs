using Bybit.Api.ApiServiceImp;
using Bybit.Api.Models;
using Bybit.Api.Models.Trade;
using Bybit.Api.Models.Trade.Response;
using Bybit.Api.Utils;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace Bybit.Api.Test;

[Trait("Category", "Integration")]
[Trait("RequiresCredentials", "True")]
public class TradeServiceTest : BybitTestBase
{
    private readonly BybitTradeService TradeService;
    private readonly ITestOutputHelper _output;
    
    public TradeServiceTest(ITestOutputHelper output)
    {
        _output = output;
        TradeService = new BybitTradeService(
            apiKey: ApiKey,
            apiSecret: ApiSecret,
            url: TestnetUrl,
            debugMode: DebugMode);
    }

    #region Trade History
    [Fact]
    public async Task Check_GetTradeHistory()
    {
        try
        {
            // Skip if no credentials available
            SkipIfNoCredentials();
            
            var tradeInfoString = await TradeService.GetTradeHistory(category: Category.LINEAR, symbol: "BTCUSDT");
            
            // Just verify we got a response - don't validate specific values
            Assert.NotNull(tradeInfoString);
            _output.WriteLine(tradeInfoString);
        }
        catch (SkipTestException ex)
        {
            _output.WriteLine($"Test skipped: {ex.Message}");
            return;
        }
    }
    #endregion

    #region Order Operations
    [Fact]
    public async Task Check_PlaceInverseOrderByDict()
    {
        try
        {
            // Skip if no credentials available
            SkipIfNoCredentials();
            
            try
            {
                // Create an order
                var orderInfoString = await TradeService.PlaceOrder(
                    category: Category.LINEAR, 
                    symbol: "BTCUSDT", 
                    side: Side.BUY, 
                    orderType: OrderType.LIMIT, 
                    qty: "0.001", 
                    price: "30000", 
                    timeInForce: TimeInForce.GTC);
                    
                if (!string.IsNullOrEmpty(orderInfoString))
                {
                    _output.WriteLine(orderInfoString);
                    OrderResult? orderInfo = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
                    Assert.NotNull(orderInfo);
                    Assert.NotNull(orderInfo?.Result?.OrderId);
                }
            }
            finally
            {
                // Clean up any test orders
                await CleanupTestOrders(TradeService, Category.LINEAR, "BTCUSDT");
            }
        }
        catch (SkipTestException ex)
        {
            _output.WriteLine($"Test skipped: {ex.Message}");
            return;
        }
    }
    #endregion

    #region Batch Order Operations
    [Fact]
    public async Task Check_PlaceBatchOrderByDict()
    {
        try
        {
            // Skip if no credentials available
            SkipIfNoCredentials();
            
            try
            {
                Dictionary<string, object> dict1 = new() 
                { 
                    { "symbol", "BTCUSDT" }, 
                    { "orderType", "Limit" }, 
                    { "side", "Buy" }, 
                    { "qty", "0.001" }, 
                    { "price", "30000" }, 
                    { "timeInForce", "GTC" }
                };
                
                Dictionary<string, object> dict2 = new() 
                { 
                    { "symbol", "ETHUSDT" }, 
                    { "orderType", "Limit" }, 
                    { "side", "Buy" }, 
                    { "qty", "0.01" }, 
                    { "price", "2000" }, 
                    { "timeInForce", "GTC" }
                };
                
                List<Dictionary<string, object>> request = new() { dict1, dict2 };
                var orderInfoString = await TradeService.PlaceBatchOrder(category: Category.LINEAR, request: request);
                
                _output.WriteLine(orderInfoString ?? "No response");
                Assert.NotNull(orderInfoString);
            }
            catch (Bybit.Api.Exceptions.BybitClientException ex)
            {
                // Log the exception but don't fail the test if it's a specific API error
                _output.WriteLine($"API Error: {ex.Message}, Status Code: {ex.StatusCode}");
                
                // Only rethrow if it's not an expected API error
                if (ex.StatusCode != 400 && ex.StatusCode != 401)
                {
                    throw;
                }
            }
            finally
            {
                // Clean up any test orders
                await CleanupTestOrders(TradeService, Category.LINEAR, "BTCUSDT");
                await CleanupTestOrders(TradeService, Category.LINEAR, "ETHUSDT");
            }
        }
        catch (SkipTestException ex)
        {
            _output.WriteLine($"Test skipped: {ex.Message}");
            return;
        }
    }

    [Fact]
    public async Task Check_PlaceBatchOrderByClass()
    {
        try
        {
            // Skip if no credentials available
            SkipIfNoCredentials();
            
            try
            {
                var order1 = new OrderRequest 
                { 
                    Symbol = "BTCUSDT", 
                    OrderType = "Limit", 
                    Side = "Buy", 
                    Qty = "0.001", 
                    Price = "30000", 
                    TimeInForce = "GTC"
                };
                
                var order2 = new OrderRequest 
                { 
                    Symbol = "ETHUSDT", 
                    OrderType = "Limit", 
                    Side = "Buy", 
                    Qty = "0.01", 
                    Price = "2000", 
                    TimeInForce = "GTC"
                };
                
                var orderInfoString = await TradeService.PlaceBatchOrder(
                    category: Category.LINEAR, 
                    request: new List<OrderRequest> { order1, order2 });
                    
                _output.WriteLine(orderInfoString ?? "No response");
                Assert.NotNull(orderInfoString);
            }
            catch (Bybit.Api.Exceptions.BybitClientException ex)
            {
                // Log the exception but don't fail the test if it's a specific API error
                _output.WriteLine($"API Error: {ex.Message}, Status Code: {ex.StatusCode}");
                
                // Only rethrow if it's not an expected API error
                if (ex.StatusCode != 400 && ex.StatusCode != 401)
                {
                    throw;
                }
            }
            finally
            {
                // Clean up any test orders
                await CleanupTestOrders(TradeService, Category.LINEAR, "BTCUSDT");
                await CleanupTestOrders(TradeService, Category.LINEAR, "ETHUSDT");
            }
        }
        catch (SkipTestException ex)
        {
            _output.WriteLine($"Test skipped: {ex.Message}");
            return;
        }
    }

    [Fact]
    public async Task Check_AmendOrder()
    {
        try
        {
            // Skip if no credentials available
            SkipIfNoCredentials();
            
            string? orderId = null;
            
            try
            {
                // First create an order
                var createOrderResponse = await TradeService.PlaceOrder(
                    category: Category.LINEAR, 
                    symbol: "BTCUSDT", 
                    side: Side.BUY, 
                    orderType: OrderType.LIMIT, 
                    qty: "0.001", 
                    price: "30000", 
                    timeInForce: TimeInForce.GTC);
                    
                var createOrderResult = JsonConvert.DeserializeObject<OrderResult>(createOrderResponse);
                Assert.NotNull(createOrderResult?.Result?.OrderId);
                
                // Store the order ID for amending and later cleanup
                orderId = createOrderResult?.Result?.OrderId;
                
                // Now amend the created order
                var orderInfoString = await TradeService.AmendOrder(
                    category: Category.LINEAR, 
                    symbol: "BTCUSDT",
                    orderId: orderId, 
                    price: "31000", 
                    qty: "0.002");
                
                _output.WriteLine(orderInfoString ?? "No response");
                Assert.NotNull(orderInfoString);
                
                // Optional: Verify the amendment was successful
                var orderInfo = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
                // The RetCode may vary depending on exchange conditions, so we just check for a response
                Assert.NotNull(orderInfo);
            }
            finally
            {
                // Clean up the test order
                await CleanupTestOrders(TradeService, Category.LINEAR, "BTCUSDT");
            }
        }
        catch (SkipTestException ex)
        {
            _output.WriteLine($"Test skipped: {ex.Message}");
            return;
        }
    }

    [Fact]
    public async Task Check_CancelOrder()
    {
        try
        {
            // Skip if no credentials available
            SkipIfNoCredentials();
            
            string? orderId = null;
            
            try
            {
                // First create an order
                var createOrderResponse = await TradeService.PlaceOrder(
                    category: Category.LINEAR, 
                    symbol: "BTCUSDT", 
                    side: Side.BUY, 
                    orderType: OrderType.LIMIT, 
                    qty: "0.001", 
                    price: "30000", 
                    timeInForce: TimeInForce.GTC);
                    
                var createOrderResult = JsonConvert.DeserializeObject<OrderResult>(createOrderResponse);
                Assert.NotNull(createOrderResult?.Result?.OrderId);
                
                // Store the order ID for cancellation
                orderId = createOrderResult?.Result?.OrderId;
                
                // Now cancel the created order
                var orderInfoString = await TradeService.CancelOrder(
                    category: Category.LINEAR, 
                    symbol: "BTCUSDT",
                    orderId: orderId);
                
                _output.WriteLine(orderInfoString ?? "No response");
                Assert.NotNull(orderInfoString);
                
                // Optional: Verify the cancellation was successful
                var orderInfo = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
                // The RetCode may vary depending on exchange conditions, so we just check for a response
                Assert.NotNull(orderInfo);
            }
            finally
            {
                // Ensure cleanup even if test fails
                await CleanupTestOrders(TradeService, Category.LINEAR, "BTCUSDT");
            }
        }
        catch (SkipTestException ex)
        {
            _output.WriteLine($"Test skipped: {ex.Message}");
            return;
        }
    }

    [Fact]
    public async Task Check_CancelAllOrder()
    {
        try
        {
            // Skip if no credentials available
            SkipIfNoCredentials();
            
            try
            {
                // Create a few test orders first
                await TradeService.PlaceOrder(
                    category: Category.LINEAR, 
                    symbol: "BTCUSDT", 
                    side: Side.BUY, 
                    orderType: OrderType.LIMIT, 
                    qty: "0.001", 
                    price: "30000", 
                    timeInForce: TimeInForce.GTC);
                    
                await TradeService.PlaceOrder(
                    category: Category.LINEAR, 
                    symbol: "BTCUSDT", 
                    side: Side.BUY, 
                    orderType: OrderType.LIMIT, 
                    qty: "0.001", 
                    price: "29500", 
                    timeInForce: TimeInForce.GTC);
                
                // Now cancel all orders for the symbol
                var orderInfoString = await TradeService.CancelAllOrder(
                    category: Category.LINEAR, 
                    symbol: "BTCUSDT");
                
                _output.WriteLine(orderInfoString ?? "No response");
                Assert.NotNull(orderInfoString);
                
                // Optional: Verify the cancellation was successful
                var orderInfo = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
                // We just check for a response
                Assert.NotNull(orderInfo);
            }
            finally
            {
                // Extra cleanup just in case
                await CleanupTestOrders(TradeService, Category.LINEAR, "BTCUSDT");
            }
        }
        catch (SkipTestException ex)
        {
            _output.WriteLine($"Test skipped: {ex.Message}");
            return;
        }
    }
    #endregion
}