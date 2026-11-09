using bybit.net.api.ApiServiceImp;
using Xunit;

namespace bybit.api.test.Tests;

public class StrategyServiceContractTests
{
    private const string SuccessResponse = "{\"retCode\":0,\"retMsg\":\"success\"}";

    [Fact]
    public async Task CreateStrategy_UsesDocumentedPostEndpointAndBody()
    {
        var handler = new SignedRequestRecordingHandler(SuccessResponse);
        var client = new HttpClient(handler);
        var service = new BybitStrategyService(client, "key", "secret", "https://api.bybit.com");

        await service.CreateStrategy(new Dictionary<string, object>
        {
            { "symbol", "BTCUSDT" },
            { "strategyType", "twap" }
        });

        Assert.Equal(HttpMethod.Post, handler.Method);
        Assert.Equal("/v5/strategy/create", handler.Path);
        Assert.Equal("{\"symbol\":\"BTCUSDT\",\"strategyType\":\"twap\"}", handler.Body);
    }

    [Fact]
    public async Task QueryStrategyList_UsesDocumentedGetEndpointAndFilters()
    {
        var handler = new SignedRequestRecordingHandler(SuccessResponse);
        var client = new HttpClient(handler);
        var service = new BybitStrategyService(client, "key", "secret", "https://api.bybit.com");

        await service.QueryStrategyList(
            "strategy-id", "running", "BTCUSDT", "linear", "twap", 1, 2, 20, "next");

        Assert.Equal(HttpMethod.Get, handler.Method);
        Assert.Equal("/v5/strategy/list", handler.Path);
        Assert.Equal(
            "strategyId=strategy-id&status=running&symbol=BTCUSDT&category=linear&strategyType=twap&beginTimeE0=1&endTimeE0=2&pageSize=20&cursor=next",
            handler.Query);
    }

    [Fact]
    public async Task QueryStrategyOrderList_UsesDocumentedLowerCamelCaseFilters()
    {
        var handler = new SignedRequestRecordingHandler(SuccessResponse);
        var client = new HttpClient(handler);
        var service = new BybitStrategyService(client, "key", "secret", "https://api.bybit.com");

        await service.QueryStrategyOrderList(
            "strategy-id", null, null, 1, 2, 20, "next", "twap");

        Assert.Equal(
            "strategyId=strategy-id&beginTimeE0=1&endTimeE0=2&pageSize=20&cursor=next&strategyType=twap",
            handler.Query);
    }

    [Fact]
    public async Task StopStrategy_UsesDocumentedPostEndpointAndBody()
    {
        var handler = new SignedRequestRecordingHandler(SuccessResponse);
        var client = new HttpClient(handler);
        var service = new BybitStrategyService(client, "key", "secret", "https://api.bybit.com");

        await service.StopStrategy("strategy-id");

        Assert.Equal(HttpMethod.Post, handler.Method);
        Assert.Equal("/v5/strategy/stop", handler.Path);
        Assert.Equal("{\"strategyId\":\"strategy-id\"}", handler.Body);
    }
}
