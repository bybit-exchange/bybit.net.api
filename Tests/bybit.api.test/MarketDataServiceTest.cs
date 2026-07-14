using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models.Market;
using bybit.net.api.Models;
using Xunit;
using bybit.net.api;

namespace bybit.api.test
{
    public class MarketDataServiceTest
    {
        readonly BybitMarketDataService marketDataService = new(url: BybitConstants.HTTP_TESTNET_URL);
        #region Market Kline
        [Fact(Skip = "Integration test: hits live Bybit API which geo-blocks CI runners")]
        public async Task CheckMarketKline_ResponseAsync()
        {
            var generalResponse = await marketDataService.GetMarketKline(category: Category.SPOT, symbol: "BTCUSDT", interval: MarketInterval.OneHour, start: 1693785600000, limit: 2);
            if (generalResponse != null)
            {
                var klineInfo = generalResponse?.Result;

                Assert.Equal(0, generalResponse?.RetCode);
                Assert.Equal("OK", generalResponse?.RetMsg);
                Assert.NotNull(klineInfo?.MarketKlineEntries);
            }
        }
        #endregion

        #region Market Tickers
        [Fact(Skip = "Integration test: hits live Bybit API which geo-blocks CI runners")]
        public async Task CheckMarketTcikers_ResponseAsync()
        {
            var generalResponse = await marketDataService.GetMarketTickers(category: Category.SPOT);
            if (generalResponse != null)
            {
                var tickersInfo = generalResponse?.Result;

                Assert.Equal(0, generalResponse?.RetCode);
                Assert.Equal("OK", generalResponse?.RetMsg);
                Assert.NotNull(tickersInfo?.MarketTickerInfoEntries);
            }
        }
        #endregion

        #region Funding Rate
        [Fact(Skip = "Integration test: hits live Bybit API which geo-blocks CI runners")]
        public async Task CheckFundingRate_ResponseAsync()
        {
            var generalResponse = await marketDataService.GetMarketFundingHistory(category: Category.LINEAR, symbol: "BTCUSDT");
            if (generalResponse != null)
            {
                var fundingInfo = generalResponse?.Result;

                Assert.Equal(0, generalResponse?.RetCode);
                Assert.Equal("OK", generalResponse?.RetMsg);
                Assert.NotNull(fundingInfo?.FundingRateEntries);
                Assert.True(fundingInfo?.FundingRateEntries?.Count > 0);
            }
        }
        #endregion
    }
}
