using bybit.net.api;
using bybit.net.api.ApiServiceImp;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class BaseUrlFallbackTests
    {
        [Fact]
        public async Task PublicRequest_WithNullUrl_FallsBackToMainnet()
        {
            HttpRequestMessage? capturedRequest = null;
            var handler = MockHandler(request => capturedRequest = request,
                "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{\"timeSecond\":\"1\",\"timeNano\":\"1\"},\"time\":1}");

            var client = new HttpClient(handler.Object);
            var service = new BybitMarketDataService(client, url: null);

            await service.GetServerTime();

            Assert.NotNull(capturedRequest);
            Assert.Equal(BybitConstants.HTTP_MAINNET_URL + "/v5/market/time",
                capturedRequest!.RequestUri!.AbsoluteUri);
        }

        [Fact]
        public async Task SignedRequest_WithNullUrl_FallsBackToMainnet()
        {
            HttpRequestMessage? capturedRequest = null;
            var handler = MockHandler(request => capturedRequest = request,
                "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"list\":[],\"nextPageCursor\":\"\"},\"time\":1}");

            var client = new HttpClient(handler.Object);
            var service = new BybitAffiliateService(client, apiKey: "k", apiSecret: "s", url: null);

            await service.GetAffiliateUserList(size: 1);

            Assert.NotNull(capturedRequest);
            Assert.StartsWith(BybitConstants.HTTP_MAINNET_URL, capturedRequest!.RequestUri!.AbsoluteUri);
        }

        [Fact]
        public async Task Request_WithExplicitTestnetUrl_UsesTestnet()
        {
            HttpRequestMessage? capturedRequest = null;
            var handler = MockHandler(request => capturedRequest = request,
                "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{\"timeSecond\":\"1\",\"timeNano\":\"1\"},\"time\":1}");

            var client = new HttpClient(handler.Object);
            var service = new BybitMarketDataService(client, url: BybitConstants.HTTP_TESTNET_URL);

            await service.GetServerTime();

            Assert.NotNull(capturedRequest);
            Assert.StartsWith(BybitConstants.HTTP_TESTNET_URL, capturedRequest!.RequestUri!.AbsoluteUri);
        }

        private static Mock<HttpMessageHandler> MockHandler(Action<HttpRequestMessage> onSend, string responseJson)
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) => onSend(request))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseJson),
                });
            return handler;
        }
    }
}
