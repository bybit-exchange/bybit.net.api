using Bybit.Api.ApiServiceImp;
using Bybit.Api.Models;
using Bybit.Api.Models.Market;
using Bybit.Api.Utils;

using Newtonsoft.Json;

using Xunit;

namespace Bybit.Api.Test;

public class MarketDataServiceTest
{
    readonly BybitMarketDataService marketDataService = new(url: BybitConstants.HTTP_TESTNET_URL);
    #region Market Kline
    [Fact]
    public async Task CheckMarketKline_ResponseAsync()
    {
        var klineInfoString = await marketDataService.GetMarketKline(category: Category.SPOT, symbol: "BTCUSDT", interval: MarketInterval.OneHour, start: 1693785600000, limit: 2);
        if (!string.IsNullOrEmpty(klineInfoString))
        {
            Console.WriteLine(klineInfoString);
            var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<MarketKLineResult>>(klineInfoString);
            var klineInfo = generalResponse?.Result;

            Assert.Equal(0, generalResponse?.RetCode);
            Assert.Equal("OK", generalResponse?.RetMsg);
            Assert.NotNull(klineInfo?.MarketKlineEntries);
        }
    }
    #endregion

    #region Market Tickers
    [Fact]
    public async Task CheckMarketTcikers_ResponseAsync()
    {
        var tickersInfoString = await marketDataService.GetMarketTickers(category: Category.SPOT);
        if (!string.IsNullOrEmpty(tickersInfoString))
        {
            Console.WriteLine(tickersInfoString);
            var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<MarketTickerResult>>(tickersInfoString);
            var tickersInfo = generalResponse?.Result;

            Assert.Equal(0, generalResponse?.RetCode);
            Assert.Equal("OK", generalResponse?.RetMsg);
            Assert.NotNull(tickersInfo?.MarketTickerInfoEntries);
        }
    }
    #endregion

    #region Funding Rate
    [Fact]
    public async Task CheckFundingRate_ResponseAsync()
    {
        var fundingInfoString = await marketDataService.GetMarketFundingHistory(category: Category.LINEAR, symbol: "BTCUSDT");
        if (!string.IsNullOrEmpty(fundingInfoString))
        {
            Console.WriteLine(fundingInfoString);
            var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<FundingRateResult>>(fundingInfoString);
            var fundingInfo = generalResponse?.Result;

            Assert.Equal(0, generalResponse?.RetCode);
            Assert.Equal("OK", generalResponse?.RetMsg);
            Assert.NotNull(fundingInfo?.FundingRateEntries);
            Assert.True(fundingInfo?.FundingRateEntries?.Count > 0);
        }
    }
    #endregion
}