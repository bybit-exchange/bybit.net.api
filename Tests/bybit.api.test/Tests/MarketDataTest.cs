using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models;
using bybit.net.api.Models.Market;
using Moq;
using Moq.Protected;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests
{
    public class MarketDataTest
    {
        #region CheckServerTime
        [Fact]
        public async Task CheckServerTime_ResponseAsync()
        {
            var responseContent = "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{\"timeSecond\":\"1499827319\",\"timeNano\":\"1499827319559000000\"},\"retExtInfo\":{},\"time\":1499827319559}";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .SetupSendAsync("/v5/market/time", HttpMethod.Get)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri("https://api.bybit.com") // Set a valid base address here
            };

            BybitMarketDataService market = new(httpClient);

            var result = await market.CheckServerTime();

            Assert.Equal(0, result?.RetCode);
            Assert.Equal("1499827319", result?.Result?.timeSecond);
        }
        #endregion
    }
}
