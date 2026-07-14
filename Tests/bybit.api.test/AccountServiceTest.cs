using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models;
using bybit.net.api;
using Newtonsoft.Json;
using Xunit;
using bybit.net.api.Models.Lending;
using bybit.net.api.Models.Account.Response;
using bybit.net.api.Models.Account;

namespace bybit.api.test
{
    public class AccountServiceTest
    {
        readonly BybitAccountService AccountService = new(apiKey: "X6wmWloIPvaLXAKqv2", apiSecret: "rY1CWGYLHy0AUjdNZqqspvd3Krhp79fHp1sP", url: BybitConstants.HTTP_TESTNET_URL, debugMode: true);
        #region Get Collateral Info
        [Fact]
        public async Task Check_GetCollateralInfo()
        {
            var collateralInfoString = await AccountService.GetAccountCollateralInfo("XRP");
            await Console.Out.WriteLineAsync(collateralInfoString);
            Assert.NotNull(collateralInfoString);
        }
        #endregion

        #region Set Collateral Info
        [Fact]
        public async Task Check_SetCollateralInfo()
        {
            var collateralInfoString = await AccountService.SetAccountCollateralCoin("XRP", CollateralSwitch.ON);
            await Console.Out.WriteLineAsync(collateralInfoString);
            Assert.NotNull(collateralInfoString);
        }
        #endregion

        #region Batch Set Collateral Info
        [Fact]
        public async Task Check_BatchSetCollateralInfoByDict()
        {
            Dictionary<string, object> dict1 = new() { { "coin", "MATIC" }, { "collateralSwitch", "OFF" } };
            Dictionary<string, object> dict2 = new() { { "coin", "SOL" }, { "collateralSwitch", "OFF" } };
            List<Dictionary<string, object>> request = new() { dict1, dict2 };
            var collateralInfoString = await AccountService.BatchSetAccountCollateralCoin(request: request);
            if(!string.IsNullOrEmpty(collateralInfoString))
            {
                await Console.Out.WriteLineAsync(collateralInfoString);
                var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<BatchSetCollateralCoinResult>>(collateralInfoString);
                var collateralInfo = generalResponse?.Result;
                Assert.Equal(0, generalResponse?.RetCode);
                Assert.Equal("SUCCESS", generalResponse?.RetMsg);
                Assert.NotNull(collateralInfo?.collateralCoinEntries);
            }
        }

        [Fact]
        public async Task Check_BatchSetCollateralInfoByClass()
        {
            var coin1 = new SetCollateralCoinRequest { coin = "MATIC", collateralSwitch = "OFF", };
            var coin2 = new SetCollateralCoinRequest { coin = "SOL", collateralSwitch = "OFF", };
            var collateralInfoString = await AccountService.BatchSetAccountCollateralCoin(request: new List<SetCollateralCoinRequest> { coin1, coin2 });
            if (!string.IsNullOrEmpty(collateralInfoString))
            {
                await Console.Out.WriteLineAsync(collateralInfoString);
                var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<BatchSetCollateralCoinResult>>(collateralInfoString);
                var collateralInfo = generalResponse?.Result;
                Assert.Equal(0, generalResponse?.RetCode);
                Assert.Equal("SUCCESS", generalResponse?.RetMsg);
                Assert.NotNull(collateralInfo?.collateralCoinEntries);
            }
        }
        #endregion
    }
}
