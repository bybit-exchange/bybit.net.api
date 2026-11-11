using bybit.net.api.ApiServiceImp;
using Xunit;

namespace bybit.api.test.Tests;

public class EventServiceContractTests
{
    [Fact]
    public async Task GetEcOrderHistory_UsesDocumentedContract()
    {
        var (service, handler) = CreateService();

        await service.GetEcOrderHistory(symbol: "BTC-EVENT", orderId: "order-1");

        AssertRequest(handler, HttpMethod.Get, "/v5/event/order-list");
        Assert.Contains("symbol=BTC-EVENT", handler.Query);
        Assert.Contains("orderId=order-1", handler.Query);
    }

    [Fact]
    public async Task GetEcActiveOrders_UsesDocumentedContract()
    {
        var (service, handler) = CreateService();

        await service.GetEcActiveOrders(symbol: "BTC-EVENT", cursor: "next");

        AssertRequest(handler, HttpMethod.Get, "/v5/event/order-realtime");
        Assert.Contains("symbol=BTC-EVENT", handler.Query);
        Assert.Contains("cursor=next", handler.Query);
    }

    [Fact]
    public async Task GetEcPositionInfo_UsesDocumentedContract()
    {
        var (service, handler) = CreateService();

        await service.GetEcPositionInfo(baseCoin: "BTC", settleCoin: "USDT");

        AssertRequest(handler, HttpMethod.Get, "/v5/event/positions");
        Assert.Contains("baseCoin=BTC", handler.Query);
        Assert.Contains("settleCoin=USDT", handler.Query);
    }

    [Fact]
    public async Task GetEcSettlements_UsesDocumentedContract()
    {
        var (service, handler) = CreateService();

        await service.GetEcSettlements("BTC-EVENT", orderId: "order-1");

        AssertRequest(handler, HttpMethod.Get, "/v5/event/settlements");
        Assert.Contains("symbol=BTC-EVENT", handler.Query);
        Assert.Contains("orderId=order-1", handler.Query);
    }

    [Fact]
    public async Task GetEcTradeHistory_UsesDocumentedContract()
    {
        var (service, handler) = CreateService();

        await service.GetEcTradeHistory("BTC-EVENT", orderLinkId: "link-1");

        AssertRequest(handler, HttpMethod.Get, "/v5/event/trades");
        Assert.Contains("symbol=BTC-EVENT", handler.Query);
        Assert.Contains("orderLinkId=link-1", handler.Query);
    }

    private static (BybitEventService Service, SignedRequestRecordingHandler Handler) CreateService()
    {
        var handler = new SignedRequestRecordingHandler("{\"retCode\":0,\"retMsg\":\"OK\"}");
        var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.bybit.com") };
        return (new BybitEventService(client, "key", "secret"), handler);
    }

    private static void AssertRequest(
        SignedRequestRecordingHandler handler,
        HttpMethod method,
        string path)
    {
        Assert.Equal(method, handler.Method);
        Assert.Equal(path, handler.Path);
    }
}
