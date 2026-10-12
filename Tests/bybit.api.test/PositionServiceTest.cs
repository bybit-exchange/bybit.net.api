using bybit.net.api.Models;
using Xunit;
using bybit.net.api.ApiServiceImp;
using bybit.net.api;
using bybit.net.api.Models.Account.Response;
using bybit.net.api.Models.Account;
using bybit.net.api.Models.Lending;
using Newtonsoft.Json;
using bybit.net.api.Models.Position;
using System.Collections.Generic;

namespace bybit.api.test
{
    [Trait("Category", "Integration")]
    public class PositionServiceTest
    {
        private static string TestnetApiKey =>
            Environment.GetEnvironmentVariable("BYBIT_TESTNET_API_KEY") ?? string.Empty;
        private static string TestnetApiSecret =>
            Environment.GetEnvironmentVariable("BYBIT_TESTNET_API_SECRET") ?? string.Empty;

        readonly BybitPositionService PositionService = new(
            apiKey: TestnetApiKey,
            apiSecret: TestnetApiSecret,
            url: BybitConstants.HTTP_TESTNET_URL,
            debugMode: false);
        #region Poistion GetPositionList
        [Fact]
        public async Task Check_ConfirmPositionInfo()
        {
            var positionInfo = await PositionService.GetPositionInfo(category: Category.INVERSE, symbol: "BTCUSD");
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(positionInfo));
            Assert.NotNull(positionInfo);
        }
        #endregion
        #region Poistion Confirm new risk limit
        [Fact]
        public async Task Check_ConfirmPositionNewRiskLimit()
        {
            var positionInfo = await PositionService.ConfirmPositionRiskLimit(category: Category.LINEAR, symbol:"BTCUSDT");
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(positionInfo));
            Assert.NotNull(positionInfo);
        }
        #endregion

        #region Get Move Position History
        [Fact]
        public async Task Check_MovePositionInfoHistory()
        {
            var positionInfo = await PositionService.GetMovePositionHistory();
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(positionInfo));
            Assert.NotNull(positionInfo);
        }
        #endregion

        #region Move Position
        [Fact]
        public async Task Check_MovePositionByDict()
        {
            Dictionary<string, object> dict1 = new() { { "category", "spot" }, { "symbol", "BTCUSDT" }, { "price", "100" }, { "side", "Sell" }, { "qty", "0.01" } };
            List<Dictionary<string, object>> request = new() { dict1};
            var positionInfo = await PositionService.MovePosition(fromUid: "123456", toUid: "456789", list: request);
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(positionInfo));
            Assert.NotNull(positionInfo);
        }

        [Fact]
        public async Task Check_MovePositionByClass()
        {
            var request = new MovePositionRequest{ category= "spot", symbol="BTCUSDT", price="100",side="Sell",qty="0.01" };
            var positionInfo = await PositionService.MovePosition(fromUid: "123456", toUid: "456789", list: new List<MovePositionRequest> { request });
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(positionInfo));
            Assert.NotNull(positionInfo);
        }
        #endregion
    }
}
