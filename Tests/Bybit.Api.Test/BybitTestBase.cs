using Bybit.Api.ApiServiceImp;
using Bybit.Api.Models;
using Bybit.Api.Models.Trade;
using Bybit.Api.Models.Trade.Response;
using Bybit.Api.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Xunit;

namespace Bybit.Api.Test;

/// <summary>
/// Base class for Bybit API tests providing common functionality and configuration
/// </summary>
public class BybitTestBase
{
    protected readonly string ApiKey;
    protected readonly string ApiSecret;
    protected readonly string TestnetUrl = BybitConstants.HTTP_TESTNET_URL;
    protected readonly bool DebugMode = true;
    
    public BybitTestBase()
    {
        // Try to load from secrets.json first
        var (configKey, configSecret) = LoadSecretsFromFile();
        
        // If not found in file, try environment variables
        ApiKey = configKey ?? 
                 Environment.GetEnvironmentVariable("BYBIT_API_KEY") ?? 
                 "SyBcSH2gfAn8ED7N0s";
                 
        ApiSecret = configSecret ?? 
                    Environment.GetEnvironmentVariable("BYBIT_API_SECRET") ?? 
                    "e59GraUyfvDV1N5N1ZKEfFnfraxKJRiByzz9";
    }
    
    /// <summary>
    /// Tries to load API credentials from secrets.json file
    /// </summary>
    private (string? ApiKey, string? ApiSecret) LoadSecretsFromFile()
    {
        try
        {
            string? assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string? directoryPath = Path.GetDirectoryName(assemblyLocation);
            
            if (directoryPath == null) return (null, null);
            
            // Look in both test directory and project directory
            string[] possiblePaths = 
            {
                Path.Combine(directoryPath, "secrets.json"),
                Path.Combine(directoryPath, "..\\..\\..\\", "secrets.json")
            };
            
            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    var config = JObject.Parse(json);
                    var apiKey = config["BybitTestSettings"]?["ApiKey"]?.ToString();
                    var apiSecret = config["BybitTestSettings"]?["ApiSecret"]?.ToString();
                    
                    if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(apiSecret))
                    {
                        return (apiKey, apiSecret);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading secrets file: {ex.Message}");
        }
        
        return (null, null);
    }
    
    /// <summary>
    /// Helper method to create a test order for testing order operations
    /// </summary>
    protected async Task<string?> CreateTestOrder(
        BybitTradeService service, 
        Category category, 
        string symbol, 
        Side side, 
        decimal price, 
        decimal qty)
    {
        var resp = await service.PlaceOrder(
            category: category,
            symbol: symbol,
            side: side,
            orderType: OrderType.LIMIT,
            qty: qty.ToString(),
            price: price.ToString(),
            timeInForce: TimeInForce.GTC);
            
        if (string.IsNullOrEmpty(resp))
            return null;
            
        var result = JsonConvert.DeserializeObject<OrderResult>(resp);
        return result?.Result?.OrderId;
    }
    
    /// <summary>
    /// Helper method to cleanup test orders for a specific symbol
    /// </summary>
    protected async Task CleanupTestOrders(
        BybitTradeService service, 
        Category category, 
        string symbol)
    {
        await service.CancelAllOrder(category: category, symbol: symbol);
    }
    
    /// <summary>
    /// Skip a test if API credentials are not available
    /// </summary>
    protected void SkipIfNoCredentials()
    {
        if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(ApiSecret))
        {
            throw new SkipTestException("API credentials not available");
        }
    }
    
    /// <summary>
    /// Custom exception for skipping tests
    /// </summary>
    public class SkipTestException : Exception
    {
        public SkipTestException(string message) : base(message) { }
    }
}