using System.Net;

using Bybit.Api.ApiServiceImp;

using Moq;
using Moq.Protected;

using Xunit;

namespace Bybit.Api.Test.Tests;

public class MarketDataTest
{
    #region CheckServerTime
    [Fact]
    public async Task CheckServerTime_ResponseAsync()
    {
        var responseContent = "{\"timeSecond\":1499827319559}";
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

        Assert.Equal(responseContent, result);
    }
    #endregion
}