using bybit.net.api.ApiServiceImp;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class AffiliateServiceEndpointTests
    {
        [Fact]
        public async Task GetAffiliateUserList_UsesDateFilters()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"list\":[],\"nextPageCursor\":\"16197\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/affiliate/aff-user-list" &&
                        request.RequestUri.Query.Contains("startDate=2025-10-21") &&
                        request.RequestUri.Query.Contains("endDate=2025-10-22")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAffiliateService(client, "key", "secret");
            var result = await service.GetAffiliateUserList(startDate: "2025-10-21", endDate: "2025-10-22");

            Assert.NotNull(result);
            Assert.Equal("16197", result!.Result!.NextPageCursor);
        }

        [Fact]
        public async Task GetAffiliateSubList_SerializesQueryAndSigns()
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
                        request.RequestUri!.AbsolutePath == "/v5/affiliate/affiliate-sub-list"),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) => capturedRequest = request)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAffiliateService(client, apiKey, apiSecret, recvWindow: recvWindow);
            var result = await service.GetAffiliateSubList(
                cursor: "abc",
                size: 10,
                startDate: "2025-10-21",
                endDate: "2025-10-22",
                subAffId: 123);

            Assert.NotNull(result);
            Assert.NotNull(capturedRequest);

            var queryString = capturedRequest!.RequestUri!.Query.TrimStart('?');
            Assert.Equal("cursor=abc&size=10&startDate=2025-10-21&endDate=2025-10-22&subAffId=123", queryString);

            var timestamp = capturedRequest.Headers.GetValues("X-BAPI-TIMESTAMP").Single();
            var signature = capturedRequest.Headers.GetValues("X-BAPI-SIGN").Single();
            Assert.Equal(SigningTestUtil.Sign($"{timestamp}{apiKey}{recvWindow}{queryString}", apiSecret), signature);
        }

        [Fact]
        public async Task GetAffiliateUserInfo_ReturnsTypedResult()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"uid\":\"1087997\",\"vipLevel\":\"5\",\"takerVol30Day\":\"17061.64983\",\"makerVol30Day\":\"10756.454142\",\"tradeVol30Day\":\"27818.103972\",\"depositAmount30Day\":\"0\",\"takerVol365Day\":\"1183752.53919162\",\"makerVol365Day\":\"44349.42819772\",\"tradeVol365Day\":\"1228101.96738934\",\"depositAmount365Day\":\"0\",\"totalWalletBalance\":\"4\",\"depositUpdateTime\":\"2026-02-04 00:00:00\",\"volUpdateTime\":\"2026-02-04 00:00:00\",\"KycLevel\":0,\"tradfiTradeVol30Day\":\"1828890.6352\",\"tradfiTradeVol365Day\":\"1828890.6352\",\"commissions30Day\":{\"USDT\":\"17.0461748\"},\"commissions365Day\":{\"USDT\":\"130.48078429\"}},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/user/aff-customer-info", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAffiliateService(client, "key", "secret");
            var result = await service.GetAffiliateUserInfo("1087997");

            Assert.NotNull(result);
            Assert.Equal("5", result!.Result!.VipLevel);
            Assert.Equal("130.48078429", result.Result.Commissions365Day!["USDT"]);
        }
    }
}
