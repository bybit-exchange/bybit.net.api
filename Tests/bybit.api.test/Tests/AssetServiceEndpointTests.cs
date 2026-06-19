using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models;
using bybit.net.api.Models.Account;
using bybit.net.api.Models.Asset;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class AssetServiceEndpointTests
    {
        [Fact]
        public async Task GetTransferableCoin_UsesFromAccountType()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"success\",\"result\":{\"list\":[\"BTC\"]},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/asset/transfer/query-transfer-coin-list" &&
                        request.RequestUri.Query.Contains("fromAccountType=UNIFIED") &&
                        request.RequestUri.Query.Contains("toAccountType=FUND") &&
                        !request.RequestUri.Query.Contains("accountType=")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAssetService(client, apiKey: "key", apiSecret: "secret");
            var result = await service.GetTransferableCoin(AccountType.Unified, AccountType.Fund);

            Assert.NotNull(result);
            Assert.Equal("BTC", result!.Result!.List![0]);
        }

        [Fact]
        public async Task CreateInternalTransfer_UsesFromAccountType()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"success\",\"result\":{\"transferId\":\"t1\",\"status\":\"SUCCESS\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Post &&
                        request.RequestUri!.AbsolutePath == "/v5/asset/transfer/inter-transfer" &&
                        request.Content != null &&
                        request.Content.ReadAsStringAsync().Result.Contains("\"fromAccountType\":\"UNIFIED\"") &&
                        !request.Content.ReadAsStringAsync().Result.Contains("\"accountType\":")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAssetService(client, apiKey: "key", apiSecret: "secret");
            var result = await service.CreateInternalTransfer(AccountType.Unified, AccountType.Fund, "USDT", "1", "t1");

            Assert.NotNull(result);
            Assert.Equal("SUCCESS", result!.Result!.Status);
        }

        [Fact]
        public async Task GetInternalTransferRecords_UsesStatusQueryKey()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"success\",\"result\":{\"list\":[],\"nextPageCursor\":\"\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/asset/transfer/query-inter-transfer-list" &&
                        request.RequestUri.Query.Contains("status=SUCCESS") &&
                        !request.RequestUri.Query.Contains("transferStatus=")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAssetService(client, apiKey: "key", apiSecret: "secret");
            var result = await service.GetInternalTransferRecords(transferStatus: TransferStatus.Success);

            Assert.NotNull(result);
            Assert.Empty(result!.Result!.List!);
        }

        [Fact]
        public async Task GetAssetAllowedDepositInfo_IsPublic()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"success\",\"result\":{\"rows\":[],\"nextPageCursor\":\"\"},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/asset/deposit/query-allowed-list" &&
                        !request.Headers.Contains("X-BAPI-API-KEY") &&
                        !request.Headers.Contains("X-BAPI-SIGN")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAssetService(client);
            var result = await service.GetAssetAllowedDepositInfo();

            Assert.NotNull(result);
            Assert.Empty(result!.Result!.Rows!);
        }

        [Fact]
        public async Task GetFundingAccountTransactionHistory_UsesExpectedPath()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{\"nextPageCursor\":\"c1\",\"list\":[]},\"time\":1}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/asset/fundinghistory", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAssetService(client, apiKey: "key", apiSecret: "secret");
            var result = await service.GetFundingAccountTransactionHistory(limit: "1");

            Assert.NotNull(result);
            Assert.Equal("c1", result!.Result!.NextPageCursor);
        }

        [Fact]
        public async Task GetFiatReferencePrice_UsesExpectedPath()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"symbol\":\"EUR-USDT\",\"fiat\":\"EUR\",\"crypto\":\"USDT\",\"timestamp\":\"1\",\"buys\":[],\"sells\":[]}}";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .SetupSendAsync("/v5/fiat/reference-price", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAssetService(client, apiKey: "key", apiSecret: "secret");
            var result = await service.GetFiatReferencePrice("EUR-USDT");

            Assert.NotNull(result);
            Assert.Equal("EUR-USDT", result!.Result!.Symbol);
        }
    }
}
