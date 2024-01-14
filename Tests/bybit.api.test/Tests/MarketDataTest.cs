using bybit.net.api.ApiServiceImp;
using Moq;
using Moq.Protected;
using System.Net;
using bybit.net.api;
using bybit.net.api.Models;
using bybit.net.api.Models.Market.Response;
using Newtonsoft.Json;
using Xunit;

namespace bybit.api.test.Tests
{
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
        
        
        #region CheckServerTime
        [Fact]
        public async Task CheckInstruments_ResponseParsing()
        {
            var responseContent = @"{
              ""retCode"": 0,
              ""retMsg"": ""OK"",
              ""result"": {
                  ""category"": ""linear"",
                  ""list"": [
                    {
                      ""symbol"": ""BTCUSDT"",
                      ""contractType"": ""LinearPerpetual"",
                      ""status"": ""Trading"",
                      ""baseCoin"": ""BTC"",
                      ""quoteCoin"": ""USDT"",
                      ""launchTime"": ""1584230400000"",
                      ""deliveryTime"": ""0"",
                      ""deliveryFeeRate"": """",
                      ""priceScale"": ""2"",
                      ""leverageFilter"": {
                        ""minLeverage"": ""1"",
                        ""maxLeverage"": ""100.00"",
                        ""leverageStep"": ""0.01""
                      },
                      ""priceFilter"": {
                        ""minPrice"": ""0.10"",
                        ""maxPrice"": ""199999.80"",
                        ""tickSize"": ""0.10""
                      },
                      ""lotSizeFilter"": {
                        ""maxOrderQty"": ""100.000"",
                        ""minOrderQty"": ""0.001"",
                        ""qtyStep"": ""0.001"",
                        ""postOnlyMaxOrderQty"": ""1000.000""
                      },
                      ""unifiedMarginTrade"": true,
                      ""fundingInterval"": 480,
                      ""settleCoin"": ""USDT"",
                      ""copyTrading"": ""both""
                    }
                  ],
                  ""nextPageCursor"": """"
                },
                ""retExtInfo"": {},
                ""time"": 1705270563383
            }";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .SetupSendAsync("/v5/market/instruments-info", HttpMethod.Get)
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

            var result = await market.GetInstrumentInfo(Category.LINEAR);
            Assert.NotNull(result);
            var parsed = JsonConvert.DeserializeObject<GeneralResponse<InstrumentsResult>>(result);
            Assert.NotNull(parsed);
            Assert.Equal(0, parsed.RetCode);
            Assert.Equal("OK", parsed.RetMsg);
            Assert.NotNull(parsed.Result);
            Assert.NotNull(parsed.Result.List);

            // Check all fields in string response correspond to object result
            Assert.Equal(responseContent, result);
            Assert.Equal(ProductType.Linear, parsed.Result.Category);
            Assert.Equal("BTCUSDT", parsed.Result.List[0].Symbol);
            Assert.Equal(ContractType.LinearPerpetual, parsed.Result.List[0].ContractType);
            Assert.Equal(InstrumentStatusType.Trading, parsed.Result.List[0].Status);
            Assert.Equal("BTC", parsed.Result.List[0].BaseCoin);
            Assert.Equal("USDT", parsed.Result.List[0].QuoteCoin);
            Assert.Equal(1584230400000, parsed.Result.List[0].LaunchTime);
            Assert.Equal(0, parsed.Result.List[0].DeliveryTime);
            Assert.Null(parsed.Result.List[0].DeliveryFeeRate);
            Assert.Equal(2, parsed.Result.List[0].PriceScale);
            Assert.Equal(1, parsed.Result.List[0].LeverageFilter.MinLeverage);
            Assert.Equal(100, parsed.Result.List[0].LeverageFilter.MaxLeverage);
            Assert.Equal(0.01m, parsed.Result.List[0].LeverageFilter.LeverageStep);
            Assert.Equal(0.1m, parsed.Result.List[0].PriceFilter.MinPrice);
            Assert.Equal(199999.8m, parsed.Result.List[0].PriceFilter.MaxPrice);
            Assert.Equal(0.1m, parsed.Result.List[0].PriceFilter.TickSize);
            Assert.Equal(100, parsed.Result.List[0].LotSizeFilter.MaxOrderQty);
            Assert.Equal(0.001m, parsed.Result.List[0].LotSizeFilter.MinOrderQty);
            Assert.Equal(0.001m, parsed.Result.List[0].LotSizeFilter.QtyStep);
            Assert.Equal(1000, parsed.Result.List[0].LotSizeFilter.PostOnlyMaxOrderQty);
            Assert.True(parsed.Result.List[0].UnifiedMarginTrade);
            Assert.Equal(480, parsed.Result.List[0].FundingInterval);
            Assert.Equal("USDT", parsed.Result.List[0].SettleCoin);
            Assert.Equal(CopyTradingType.Both, parsed.Result.List[0].CopyTrading);
        }
        #endregion
    }
}