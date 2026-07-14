using bybit.net.api.ApiServiceImp;
using Moq;
using Moq.Protected;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace bybit.api.test.Tests
{
    public class P2PServiceSigningTests
    {
        [Fact]
        public async Task GetAllOrders_SignsExactJsonBody()
        {
            const string apiKey = "test-key";
            const string apiSecret = "test-secret";
            const string recvWindow = "5000";
            var responseContent = "{\"ret_code\":0,\"ret_msg\":\"SUCCESS\",\"result\":{\"count\":0,\"items\":[]},\"ext_code\":\"\",\"ext_info\":{},\"time_now\":\"1\"}";
            HttpRequestMessage? capturedRequest = null;
            string? capturedBody = null;

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Post &&
                        request.RequestUri!.AbsolutePath == "/v5/p2p/order/simplifyList"),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    capturedRequest = request;
                    capturedBody = request.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
                })
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitP2PService(client, apiKey, apiSecret, recvWindow: recvWindow);

            var result = await service.GetAllOrders(1, 5);

            Assert.NotNull(result);
            Assert.NotNull(capturedRequest);
            Assert.NotNull(capturedBody);

            var body = capturedBody!;
            var timestamp = capturedRequest!.Headers.GetValues("X-BAPI-TIMESTAMP").Single();
            var signature = capturedRequest.Headers.GetValues("X-BAPI-SIGN").Single();
            var expectedSignature = Sign($"{timestamp}{apiKey}{recvWindow}{body}", apiSecret);

            Assert.Equal("{\"page\":1,\"size\":5}", body);
            Assert.Equal(expectedSignature, signature);
        }

        [Fact]
        public async Task SignedGet_SignsExactQueryString()
        {
            const string apiKey = "test-key";
            const string apiSecret = "test-secret";
            const string recvWindow = "5000";
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"list\":[],\"nextPageCursor\":\"\"},\"time\":1}";
            HttpRequestMessage? capturedRequest = null;

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/affiliate/aff-user-list"),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) => capturedRequest = request)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAffiliateService(client, apiKey, apiSecret, recvWindow: recvWindow);

            var result = await service.GetAffiliateUserList(
                size: 10,
                cursor: "abc",
                needDeposit: true,
                need30: true,
                need365: false,
                startDate: "2025-10-21",
                endDate: "2025-10-22");

            Assert.NotNull(result);
            Assert.NotNull(capturedRequest);

            var queryString = capturedRequest!.RequestUri!.Query.TrimStart('?');
            var timestamp = capturedRequest.Headers.GetValues("X-BAPI-TIMESTAMP").Single();
            var signature = capturedRequest.Headers.GetValues("X-BAPI-SIGN").Single();
            var expectedSignature = Sign($"{timestamp}{apiKey}{recvWindow}{queryString}", apiSecret);

            Assert.Equal("size=10&cursor=abc&needDeposit=True&need30=True&need365=False&startDate=2025-10-21&endDate=2025-10-22", queryString);
            Assert.Equal(expectedSignature, signature);
        }

        private static string Sign(string data, string secret)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
        }
    }
}
