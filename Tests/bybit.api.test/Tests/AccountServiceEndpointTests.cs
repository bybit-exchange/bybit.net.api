using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models;
using bybit.net.api.Models.Account;
using bybit.net.api.Models.Account.Response;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class AccountServiceEndpointTests
    {
        [Fact]
        public async Task SetAccountMarginMode_UsesLowerCaseRequestField()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{\"reasons\":[]},\"time\":1}";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Post &&
                        request.RequestUri!.AbsolutePath == "/v5/account/set-margin-mode" &&
                        request.Content != null &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"setMarginMode\":\"PORTFOLIO_MARGIN\"")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri("https://api.bybit.com")
            };

            var accountService = new BybitAccountService(httpClient, "key", "secret");
            var result = await accountService.SetAccountMarginMode(SetMarginMode.PORTFOLIO_MARGIN);

            Assert.NotNull(result);
            Assert.Equal(0, result!.RetCode);
            Assert.Empty(result.Result!.Reasons!);
        }

        [Fact]
        public async Task ManualRepay_MapsRepaymentType()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"success\",\"result\":{\"resultStatus\":\"P\"},\"time\":1}";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Post &&
                        request.RequestUri!.AbsolutePath == "/v5/account/repay" &&
                        request.Content != null &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"repaymentType\":\"ALL\"")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri("https://api.bybit.com")
            };

            var accountService = new BybitAccountService(httpClient, "key", "secret");
            var result = await accountService.ManualRepay("BTC", "0.01", RepaymentType.All);

            Assert.NotNull(result);
            Assert.Equal("P", result!.Result!.ResultStatus);
        }

        [Fact]
        public async Task ManualRepayWithoutAssetConversion_UsesExpectedPath()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"success\",\"result\":{\"resultStatus\":\"SU\"},\"time\":1}";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .SetupSendAsync("/v5/account/no-convert-repay", HttpMethod.Post)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri("https://api.bybit.com")
            };

            var accountService = new BybitAccountService(httpClient, "key", "secret");
            var result = await accountService.ManualRepayWithoutAssetConversion("BTC");

            Assert.NotNull(result);
            Assert.Equal("SU", result!.Result!.ResultStatus);
        }

        [Fact]
        public async Task GetAccountInstrumentsInfo_UsesExpectedPath()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{\"category\":\"spot\",\"list\":[{\"symbol\":\"BTCUSDT\"}]},\"time\":1}";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .SetupSendAsync("/v5/account/instruments-info", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri("https://api.bybit.com")
            };

            var accountService = new BybitAccountService(httpClient, "key", "secret");
            var result = await accountService.GetAccountInstrumentsInfo(Category.SPOT, "BTCUSDT");

            Assert.NotNull(result);
            Assert.Equal("spot", result!.Result!.Category);
            Assert.Equal("BTCUSDT", result.Result.List![0].Symbol);
        }

        [Fact]
        public async Task GetTransferableAmount_MapsResponse()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{\"availableWithdrawal\":\"4.5\",\"availableWithdrawalMap\":{\"BTC\":\"4.5\"}},\"time\":1}";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .SetupSendAsync("/v5/account/withdrawal", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri("https://api.bybit.com")
            };

            var accountService = new BybitAccountService(httpClient, "key", "secret");
            var result = await accountService.GetTransferableAmount("BTC");

            Assert.NotNull(result);
            Assert.Equal("4.5", result!.Result!.AvailableWithdrawal);
            Assert.Equal("4.5", result.Result.AvailableWithdrawalMap!["BTC"]);
        }
    }
}
