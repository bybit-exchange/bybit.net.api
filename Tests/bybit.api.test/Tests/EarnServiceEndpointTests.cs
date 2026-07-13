using bybit.net.api.ApiServiceImp;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class EarnServiceEndpointTests
    {
        [Fact]
        public async Task GetEarnOrderHistory_UsesCurrentOptionalParameters()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"list\":[],\"nextPageCursor\":\"c1\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/earn/order" &&
                        request.RequestUri.Query.Contains("category=OnChain") &&
                        request.RequestUri.Query.Contains("productId=8") &&
                        request.RequestUri.Query.Contains("startTime=1") &&
                        request.RequestUri.Query.Contains("endTime=2") &&
                        request.RequestUri.Query.Contains("limit=10") &&
                        request.RequestUri.Query.Contains("cursor=abc")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitEarnService(client, apiKey: "key", apiSecret: "secret");
            var result = await service.GetEarnOrderHistory("OnChain", productId: "8", startTime: 1, endTime: 2, limit: 10, cursor: "abc");

            Assert.NotNull(result);
            Assert.Equal("c1", result!.Result!.NextPageCursor);
        }

        [Fact]
        public async Task GetEarnAprHistory_IsPublic()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"list\":[{\"timestamp\":\"1773705600000\",\"apr\":\"15%\"}]},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/earn/apr-history" &&
                        !request.Headers.Contains("X-BAPI-API-KEY") &&
                        !request.Headers.Contains("X-BAPI-SIGN")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitEarnService(client);
            var result = await service.GetEarnAprHistory("OnChain", "8");

            Assert.NotNull(result);
            Assert.Equal("15%", result!.Result!.List![0].Apr);
        }

        [Fact]
        public async Task PlaceFixedTermOrder_UsesExpectedPath()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"orderId\":\"o1\",\"orderLinkId\":\"link1\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/earn/fixed-term/place-order", HttpMethod.Post)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitEarnService(client, apiKey: "key", apiSecret: "secret");
            var result = await service.PlaceFixedTermOrder("724", "FixedTermSaving", "USDT", "201", "FUND", "usdt-fixdearn-001");

            Assert.NotNull(result);
            Assert.Equal("o1", result!.Result!.OrderId);
        }

        [Fact]
        public async Task AddLiquidity_SerializesPostBodyAndSigns()
        {
            const string apiKey = "test-key";
            const string apiSecret = "test-secret";
            const string recvWindow = "5000";
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"orderId\":\"o1\",\"orderLinkId\":\"link1\"},\"time\":1}";
            HttpRequestMessage? capturedRequest = null;

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Post &&
                        request.RequestUri!.AbsolutePath == "/v5/earn/liquidity-mining/add-liquidity"),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) => capturedRequest = request)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitEarnService(client, apiKey: apiKey, apiSecret: apiSecret, recvWindow: recvWindow);
            var result = await service.AddLiquidity(
                productId: "p1",
                orderLinkId: "link1",
                quoteAccountType: "FUND",
                baseAccountType: "FUND",
                quoteAmount: "100",
                baseAmount: "0.5",
                leverage: "3");

            Assert.NotNull(result);
            Assert.NotNull(capturedRequest);

            var body = await capturedRequest!.Content!.ReadAsStringAsync();
            Assert.Equal(
                "{\"productId\":\"p1\",\"orderLinkId\":\"link1\",\"quoteAccountType\":\"FUND\",\"baseAccountType\":\"FUND\",\"quoteAmount\":\"100\",\"baseAmount\":\"0.5\",\"leverage\":\"3\"}",
                body);

            var timestamp = capturedRequest.Headers.GetValues("X-BAPI-TIMESTAMP").Single();
            var signature = capturedRequest.Headers.GetValues("X-BAPI-SIGN").Single();
            Assert.Equal(SigningTestUtil.Sign($"{timestamp}{apiKey}{recvWindow}{body}", apiSecret), signature);
        }

        [Fact]
        public async Task SetFixedTermAutoInvest_UsesExpectedPayload()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Post &&
                        request.RequestUri!.AbsolutePath == "/v5/earn/fixed-term/position/auto-invest" &&
                        request.Content != null &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"positionId\":\"19454\"") &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"status\":\"Enable\"")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitEarnService(client, apiKey: "key", apiSecret: "secret");
            var result = await service.SetFixedTermAutoInvest("23", "FundPool", "19454", "Enable");

            Assert.NotNull(result);
            Assert.Equal(0, result!.RetCode);
        }

        [Fact]
        public async Task GetTokenProduct_IsPublic()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"productId\":\"1\",\"coin\":\"BYUSDT\",\"mintFeeRateE8\":\"0\",\"redeemFeeRateE8\":\"100000\",\"minInvestment\":\"1\",\"userHolding\":\"\",\"leftQuota\":\"\",\"canMint\":false,\"savingsBalance\":\"\",\"aprE8\":\"60000000\",\"bonusAprE8\":\"0\",\"bonusMaxAmount\":\"\",\"baseCoinPrecision\":4,\"tokenPrecision\":4},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/earn/token/product" &&
                        !request.Headers.Contains("X-BAPI-API-KEY") &&
                        !request.Headers.Contains("X-BAPI-SIGN")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitEarnService(client);
            var result = await service.GetTokenProduct("BYUSDT");

            Assert.NotNull(result);
            Assert.Equal("BYUSDT", result!.Result!.Coin);
        }

        [Fact]
        public async Task GetTokenHistoryApr_UsesExpectedPath()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"list\":[{\"timestamp\":\"1774569600\",\"aprE8\":\"2000000\"}]},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/earn/token/history-apr", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitEarnService(client);
            var result = await service.GetTokenHistoryApr("BYUSDT", 1);

            Assert.NotNull(result);
            Assert.Equal("2000000", result!.Result!.List![0].AprE8);
        }
    }
}
