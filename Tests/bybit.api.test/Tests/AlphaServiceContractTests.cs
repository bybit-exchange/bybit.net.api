using bybit.net.api.ApiServiceImp;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class AlphaServiceContractTests
    {
        [Fact]
        public async Task GetPredictionPriceHistory_UsesTokenIdAndInterval()
        {
            var (service, handler) = CreateService();

            await service.GetPredictionPriceHistory("token-1", "1H");

            Assert.Equal("{\"tokenId\":\"token-1\",\"interval\":\"1H\"}", handler.Body);
        }

        [Fact]
        public async Task GetPredictionPositionHistory_UsesDocumentedFilters()
        {
            var (service, handler) = CreateService();

            await service.GetPredictionPositionHistory(
                tokenId: "token-1",
                eventId: "event-1",
                result: 1,
                days: 7,
                limit: 20,
                pageIndex: 2);

            Assert.Equal(
                "{\"tokenId\":\"token-1\",\"eventId\":\"event-1\",\"result\":1,\"days\":7,\"limit\":20,\"pageIndex\":2}",
                handler.Body);
        }

        [Fact]
        public async Task GetPredictionSideMarketList_UsesEventAndPagingFilters()
        {
            var (service, handler) = CreateService();

            await service.GetPredictionSideMarketList(
                "event-1",
                sortBy: "Liquidity",
                marketType: 1,
                limit: 10,
                pageIndex: 2);

            Assert.Equal(
                "{\"eventId\":\"event-1\",\"sortBy\":\"Liquidity\",\"marketType\":1,\"limit\":10,\"pageIndex\":2}",
                handler.Body);
        }

        [Fact]
        public async Task GetPredictionGroupStageDetail_UsesGroupName()
        {
            var (service, handler) = CreateService();

            await service.GetPredictionGroupStageDetail(1, "A");

            Assert.Equal("{\"eventType\":1,\"groupName\":\"A\"}", handler.Body);
        }

        [Fact]
        public async Task GetPredictionTimelineStages_RequiresNumericEventType()
        {
            var (service, handler) = CreateService();

            await service.GetPredictionTimelineStages(1);

            Assert.Equal("eventType=1", handler.Query);
        }

        [Fact]
        public async Task GetPredictionPortfolioSummary_UsesEventType()
        {
            var (service, handler) = CreateService();

            await service.GetPredictionPortfolioSummary(1);

            Assert.Equal("{\"eventType\":1}", handler.Body);
        }

        [Fact]
        public async Task GetPredictionPositionList_UsesDocumentedFilters()
        {
            var (service, handler) = CreateService();

            await service.GetPredictionPositionList("token-1", "event-1", 20, 2);

            Assert.Equal(
                "{\"tokenId\":\"token-1\",\"eventId\":\"event-1\",\"limit\":20,\"pageIndex\":2}",
                handler.Body);
        }

        [Fact]
        public async Task GetPredictionMatchList_UsesDocumentedFilters()
        {
            var (service, handler) = CreateService();

            await service.GetPredictionMatchList(1, 2, 1000, 2000, "Groups", 20, 2);

            Assert.Equal(
                "{\"eventType\":1,\"status\":2,\"startTime\":1000,\"endTime\":2000,\"stageCode\":\"Groups\",\"limit\":20,\"pageIndex\":2}",
                handler.Body);
        }

        [Fact]
        public async Task ExecutePredictionBuy_SerializesNumericOrderType()
        {
            var (service, handler) = CreateService();

            await service.ExecutePredictionBuy("token-1", "10", "USDC", 1, "0.05", "event-1");

            Assert.Contains("\"orderType\":1", handler.Body);
        }

        [Fact]
        public async Task ExecutePredictionSell_SerializesNumericOrderType()
        {
            var (service, handler) = CreateService();

            await service.ExecutePredictionSell("token-1", "10", 1, "0.05", "event-1", "USDC");

            Assert.Contains("\"orderType\":1", handler.Body);
        }

        [Fact]
        public async Task GetPredictionOrderEstimate_SerializesNumericEnums()
        {
            var (service, handler) = CreateService();

            await service.GetPredictionOrderEstimate("token-1", 1, "event-1", "10", 1, "USDC");

            Assert.Contains("\"side\":1", handler.Body);
            Assert.Contains("\"orderType\":1", handler.Body);
        }

        [Fact]
        public async Task GetPredictionOrderList_UsesDocumentedFiltersInOrder()
        {
            var (service, handler) = CreateService();

            await service.GetPredictionOrderList(2, "token-1", "event-1", 1, 7, 20, 2);

            Assert.Equal(
                "{\"status\":2,\"tokenId\":\"token-1\",\"eventId\":\"event-1\",\"side\":1,\"days\":7,\"limit\":20,\"pageIndex\":2}",
                handler.Body);
        }

        private static (BybitAlphaService Service, RecordingHandler Handler) CreateService()
        {
            var handler = new RecordingHandler();
            var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.bybit.com") };
            return (new BybitAlphaService(client, "test-key", "test-secret"), handler);
        }

        private sealed class RecordingHandler : HttpMessageHandler
        {
            public string Body { get; private set; } = string.Empty;
            public string Query { get; private set; } = string.Empty;

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                Query = request.RequestUri!.Query.TrimStart('?');
                Body = request.Content is null
                    ? string.Empty
                    : await request.Content.ReadAsStringAsync(cancellationToken);

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"retCode\":0,\"retMsg\":\"OK\"}"),
                };
            }
        }
    }
}
