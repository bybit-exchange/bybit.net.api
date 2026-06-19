using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models.Lending;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class LendingServiceEndpointTests
    {
        [Fact]
        public async Task GetInsLoanInfo_IsPublic()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"marginProductInfo\":[]},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/ins-loan/product-infos" &&
                        !request.Headers.Contains("X-BAPI-API-KEY") &&
                        !request.Headers.Contains("X-BAPI-SIGN")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitLendingService(client);
            var result = await service.GetInsLoanInfo();

            Assert.NotNull(result);
            Assert.Empty(result!.Result!.MarginProductInfo!);
        }

        [Fact]
        public async Task GetInsLoanOrders_UsesCurrentRoute()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"loanInfo\":[]},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/ins-loan/loan-order", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitLendingService(client, "key", "secret");
            var result = await service.GetInsLoanOrders(limit: 10);

            Assert.NotNull(result);
            Assert.Empty(result!.Result!.LoanInfo!);
        }

        [Fact]
        public async Task GetInsLoanRepayOrders_UsesCurrentRoute()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"repayInfo\":[]},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/ins-loan/repaid-history", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitLendingService(client, "key", "secret");
            var result = await service.GetInsLoanRepayOrders(limit: 100);

            Assert.NotNull(result);
            Assert.Empty(result!.Result!.RepayInfo!);
        }

        [Fact]
        public async Task RepayInsLoan_UsesExpectedPayload()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"success\",\"result\":{\"repayOrderStatus\":\"P\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Post &&
                        request.RequestUri!.AbsolutePath == "/v5/ins-loan/repay-loan" &&
                        request.Content != null &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"token\":\"USDT\"") &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"quantity\":\"500000\"")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitLendingService(client, "key", "secret");
            var result = await service.RepayInsLoan("USDT", "500000");

            Assert.NotNull(result);
            Assert.Equal("P", result!.Result!.RepayOrderStatus);
        }

        [Fact]
        public async Task C2CCancelRedeem_UsesRouteWithoutLeadingSpace()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{\"orderId\":\"1\",\"serialNo\":\"s1\",\"updatedTime\":\"2\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/lending/redeem-cancel", HttpMethod.Post)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitLendingService(client, "key", "secret");
            var result = await service.C2CCancelRedeem(orderId: "1");

            Assert.NotNull(result);
            Assert.Equal("1", result!.Result!.OrderId);
        }

        [Fact]
        public async Task GetC2CLendingOrders_UsesHistoryOrderRoute()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{\"list\":[]},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/lending/history-order", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitLendingService(client, "key", "secret");
            var result = await service.GetC2CLendingOrders(orderType: LendingOrderType.DEPOSIT);

            Assert.NotNull(result);
            Assert.Empty(result!.Result!.List!);
        }
    }
}
