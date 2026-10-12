using bybit.net.api.ApiServiceImp;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class QueryCollectionContractTests
    {
        [Fact]
        public async Task ListEarnAutoSavings_RepeatsCoinsAndSignsExactQuery()
        {
            const string apiKey = "test-key";
            const string apiSecret = "test-secret";
            const string recvWindow = "5000";
            var handler = new RecordingHandler();
            var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitEarnService(
                client,
                apiKey: apiKey,
                apiSecret: apiSecret,
                recvWindow: recvWindow);

            await service.ListEarnAutoSavings(new[] { "BTC", "ETH" });

            Assert.Equal("coins=BTC&coins=ETH", handler.Query);
            Assert.Equal(
                SigningTestUtil.Sign($"{handler.Timestamp}{apiKey}{recvWindow}{handler.Query}", apiSecret),
                handler.Signature);
        }

        [Fact]
        public async Task GetOptionBaseCoins_AcceptsCommaSeparatedTypes()
        {
            var handler = new RecordingHandler();
            var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.bybit.com") };

            await new BybitMarketDataService(client).GetOptionBaseCoins("0,2");

            Assert.Equal("underlyingType=0%2C2", handler.Query);
        }

        private sealed class RecordingHandler : HttpMessageHandler
        {
            public string Query { get; private set; } = string.Empty;
            public string Timestamp { get; private set; } = string.Empty;
            public string Signature { get; private set; } = string.Empty;

            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                Query = request.RequestUri!.Query.TrimStart('?');
                Timestamp = GetHeader(request, "X-BAPI-TIMESTAMP");
                Signature = GetHeader(request, "X-BAPI-SIGN");

                return Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"retCode\":0,\"retMsg\":\"OK\"}"),
                });
            }

            private static string GetHeader(HttpRequestMessage request, string name) =>
                request.Headers.TryGetValues(name, out var values) ? values.Single() : string.Empty;
        }
    }
}
