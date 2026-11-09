using bybit.net.api.ApiServiceImp;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class SpotMarginServiceEndpointTests
    {
        private const string SuccessResponse = "{\"retCode\":0,\"retMsg\":\"OK\"}";

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

        [Fact]
        public async Task QueryFixedBorrowPenaltyInterest_UsesDocumentedEndpointAndQuery()
        {
            var handler = new SignedRequestRecordingHandler(SuccessResponse);
            var client = new HttpClient(handler);
            var service = new BybitSpotMarginService(client, "key", "secret", "https://api.bybit.com");

            await service.QueryFixedBorrowPenaltyInterest("order-id", "USDT", "20", "next");

            Assert.Equal(HttpMethod.Get, handler.Method);
            Assert.Equal("/v5/spot-margin-trade/fixedborrow-interest-accrued", handler.Path);
            Assert.Equal("orderId=order-id&orderCurrency=USDT&limit=20&cursor=next", handler.Query);
        }

        [Fact]
        public async Task CancelFixedBorrowOrder_UsesDocumentedEndpointAndBody()
        {
            var handler = new SignedRequestRecordingHandler(SuccessResponse);
            var client = new HttpClient(handler);
            var service = new BybitSpotMarginService(client, "key", "secret", "https://api.bybit.com");

            await service.CancelFixedBorrowOrder("order-id");

            Assert.Equal(HttpMethod.Post, handler.Method);
            Assert.Equal("/v5/spot-margin-trade/fixedborrow-order-cancel", handler.Path);
            Assert.Equal("{\"orderId\":\"order-id\"}", handler.Body);
        }
    }
}
