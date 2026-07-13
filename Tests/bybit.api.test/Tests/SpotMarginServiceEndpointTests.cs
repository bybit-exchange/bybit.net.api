using bybit.net.api.ApiServiceImp;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class SpotMarginServiceEndpointTests
    {
        [Fact]
        public async Task GetVipMarginData_IsPublicAndSerializesQuery()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"vipCoinList\":[]},\"time\":1}";
            HttpRequestMessage? capturedRequest = null;

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/spot-margin-trade/data" &&
                        !request.Headers.Contains("X-BAPI-API-KEY") &&
                        !request.Headers.Contains("X-BAPI-SIGN")),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) => capturedRequest = request)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitSpotMarginService(client, apiKey: "key", apiSecret: "secret");
            var result = await service.GetVipMarginData(vipLevel: "vip1", currency: "USDT");

            Assert.NotNull(result);
            Assert.NotNull(capturedRequest);

            var queryString = capturedRequest!.RequestUri!.Query.TrimStart('?');
            Assert.Equal("vipLevel=vip1&currency=USDT", queryString);
        }
    }
}
