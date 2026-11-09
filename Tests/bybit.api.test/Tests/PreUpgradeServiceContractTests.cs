using bybit.net.api.ApiServiceImp;
using Xunit;

namespace bybit.api.test.Tests;

public class PreUpgradeServiceContractTests
{
    [Fact]
    public async Task GetPreUpgradeTransactionLog_UsesDocumentedContract()
    {
        var handler = new SignedRequestRecordingHandler("{\"retCode\":0,\"retMsg\":\"OK\"}");
        var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.bybit.com") };
        var service = new BybitPreUpgradeService(client, "key", "secret");

        await service.GetPreUpgradeTransactionLog("linear", baseCoin: "BTC");

        Assert.Equal(HttpMethod.Get, handler.Method);
        Assert.Equal("/v5/pre-upgrade/account/transaction-log", handler.Path);
        Assert.Contains("category=linear", handler.Query);
        Assert.Contains("baseCoin=BTC", handler.Query);
    }
}
