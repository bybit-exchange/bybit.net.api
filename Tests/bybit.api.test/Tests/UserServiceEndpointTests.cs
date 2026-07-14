using bybit.net.api.ApiServiceImp;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class UserServiceEndpointTests
    {
        [Fact]
        public async Task QueryReferrals_SerializesQueryAndSigns()
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
                        request.RequestUri!.AbsolutePath == "/v5/user/invitation/referrals"),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) => capturedRequest = request)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitUserService(client, apiKey, apiSecret, recvWindow: recvWindow);
            var result = await service.QueryReferrals(cursor: "abc", size: 10, status: "0");

            Assert.NotNull(result);
            Assert.NotNull(capturedRequest);

            var queryString = capturedRequest!.RequestUri!.Query.TrimStart('?');
            Assert.Equal("cursor=abc&size=10&status=0", queryString);

            var timestamp = capturedRequest.Headers.GetValues("X-BAPI-TIMESTAMP").Single();
            var signature = capturedRequest.Headers.GetValues("X-BAPI-SIGN").Single();
            Assert.Equal(SigningTestUtil.Sign($"{timestamp}{apiKey}{recvWindow}{queryString}", apiSecret), signature);
        }

        [Fact]
        public async Task SignAgreement_SerializesPostBodyAndSigns()
        {
            const string apiKey = "test-key";
            const string apiSecret = "test-secret";
            const string recvWindow = "5000";
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{},\"time\":1}";
            HttpRequestMessage? capturedRequest = null;
            string? capturedBody = null;

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Post &&
                        request.RequestUri!.AbsolutePath == "/v5/user/agreement"),
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
            var service = new BybitUserService(client, apiKey, apiSecret, recvWindow: recvWindow);
            var result = await service.SignAgreement(category: 1, agree: true);

            Assert.NotNull(result);
            Assert.NotNull(capturedRequest);

            var body = capturedBody!;
            Assert.Equal("{\"category\":1,\"agree\":true}", body);

            var timestamp = capturedRequest.Headers.GetValues("X-BAPI-TIMESTAMP").Single();
            var signature = capturedRequest.Headers.GetValues("X-BAPI-SIGN").Single();
            Assert.Equal(SigningTestUtil.Sign($"{timestamp}{apiKey}{recvWindow}{body}", apiSecret), signature);
        }
    }
}
