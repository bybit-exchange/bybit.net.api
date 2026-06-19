using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models.Broker;
using bybit.net.api.Models.Lending;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class BrokerServiceEndpointTests
    {
        [Fact]
        public async Task GetBrokerEarning_UsesCurrentRouteAndParameters()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"success\",\"result\":{\"totalEarningCat\":{\"convert\":[{\"coin\":\"USDT\",\"earning\":\"1\"}]},\"details\":[],\"nextPageCursor\":\"c1\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/broker/earnings-info" &&
                        request.RequestUri.Query.Contains("bizType=CONVERT") &&
                        request.RequestUri.Query.Contains("begin=20240101") &&
                        request.RequestUri.Query.Contains("end=20240107") &&
                        request.RequestUri.Query.Contains("uid=12345") &&
                        !request.RequestUri.Query.Contains("startTime=") &&
                        !request.RequestUri.Query.Contains("endTime=")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitBrokerService(client, "key", "secret");
            var result = await service.GetBrokerEarning(BizType.CONVERT, "20240101", "20240107", "12345");

            Assert.NotNull(result);
            Assert.Equal("c1", result!.Result!.NextPageCursor);
            Assert.Equal("USDT", result.Result.TotalEarningCat!.Convert![0].Coin);
        }

        [Fact]
        public async Task GetBrokerAccountInfo_UsesExpectedPath()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"success\",\"result\":{\"subAcctQty\":\"2\",\"maxSubAcctQty\":\"20\",\"baseFeeRebateRate\":{\"spot\":\"10.0%\",\"derivatives\":\"10.0%\"},\"markupFeeRebateRate\":{\"spot\":\"6.00%\",\"derivatives\":\"9.00%\",\"convert\":\"3.00%\"},\"ts\":\"1701395633402\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/broker/account-info", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitBrokerService(client, "key", "secret");
            var result = await service.GetBrokerAccountInfo();

            Assert.NotNull(result);
            Assert.Equal("20", result!.Result!.MaxSubAcctQty);
            Assert.Equal("3.00%", result.Result.MarkupFeeRebateRate!.Convert);
        }

        [Fact]
        public async Task SetBrokerRateLimit_UsesExpectedPayload()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"success\",\"result\":{\"result\":[{\"uids\":\"290118\",\"bizType\":\"SPOT\",\"rate\":600,\"success\":true,\"msg\":\"API limit updated successfully\"}]},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Post &&
                        request.RequestUri!.AbsolutePath == "/v5/broker/apilimit/set" &&
                        request.Content != null &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"uids\":\"290118\"") &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"bizType\":\"SPOT\"") &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"rate\":600")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitBrokerService(client, "key", "secret");
            var result = await service.SetBrokerRateLimit(new List<BrokerRateLimitRequestItem>
            {
                new BrokerRateLimitRequestItem
                {
                    Uids = "290118",
                    BizType = BizType.SPOT,
                    Rate = 600
                }
            });

            Assert.NotNull(result);
            Assert.True(result!.Result!.Result![0].Success);
        }

        [Fact]
        public async Task GetBrokerVoucherSpec_UsesExpectedPath()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"id\":\"80209\",\"coin\":\"USDT\",\"amountUnit\":\"AWARD_AMOUNT_UNIT_USD\",\"productLine\":\"PRODUCT_LINE_CONTRACT\",\"subProductLine\":\"SUB_PRODUCT_LINE_CONTRACT_DEFAULT\",\"totalAmount\":\"10000\",\"usedAmount\":\"100\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/broker/award/info", HttpMethod.Post)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitBrokerService(client, "key", "secret");
            var result = await service.GetBrokerVoucherSpec("80209");

            Assert.NotNull(result);
            Assert.Equal("USDT", result!.Result!.Coin);
        }

        [Fact]
        public async Task IssueBrokerVoucher_UsesExpectedPayload()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Post &&
                        request.RequestUri!.AbsolutePath == "/v5/broker/award/distribute-award" &&
                        request.Content != null &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"accountId\":\"2846381\"") &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"awardId\":\"123456\"") &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"brokerId\":\"v-28478\"")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitBrokerService(client, "key", "secret");
            var result = await service.IssueBrokerVoucher("2846381", "123456", "award-001", "100", "v-28478");

            Assert.NotNull(result);
            Assert.Equal(0, result!.RetCode);
        }
    }
}
