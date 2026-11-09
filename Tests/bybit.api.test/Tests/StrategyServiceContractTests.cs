using bybit.net.api.ApiServiceImp;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests;

public class StrategyServiceContractTests
{
    [Fact]
    public async Task QueryStrategyOrderList_UsesDocumentedLowerCamelCaseFilters()
    {
        var handler = new RecordingHandler();
        var client = new HttpClient(handler);
        var service = new BybitStrategyService(client, "key", "secret", "https://api.bybit.com");

        await service.QueryStrategyOrderList(
            "strategy-id", null, null, 1, 2, 20, "next", "twap");

        Assert.Equal(
            "strategyId=strategy-id&beginTimeE0=1&endTimeE0=2&pageSize=20&cursor=next&strategyType=twap",
            handler.Query);
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        public string Query { get; private set; } = string.Empty;

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Query = request.RequestUri!.Query.TrimStart('?');
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"retCode\":0,\"retMsg\":\"success\"}")
            });
        }
    }
}
