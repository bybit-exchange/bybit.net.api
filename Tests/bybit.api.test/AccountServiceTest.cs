using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models;
using bybit.net.api;
using Newtonsoft.Json;
using Xunit;
using bybit.net.api.Models.Lending;
using bybit.net.api.Models.Account.Response;
using bybit.net.api.Models.Account;
using Newtonsoft.Json.Linq;

namespace bybit.api.test
{
    [Trait("Category", "Integration")]
    public class AccountServiceTest
    {
        private static string TestnetApiKey =>
            Environment.GetEnvironmentVariable("BYBIT_TESTNET_API_KEY") ?? string.Empty;
        private static string TestnetApiSecret =>
            Environment.GetEnvironmentVariable("BYBIT_TESTNET_API_SECRET") ?? string.Empty;

        readonly BybitAccountService AccountService = new(
            apiKey: TestnetApiKey,
            apiSecret: TestnetApiSecret,
            url: BybitConstants.HTTP_TESTNET_URL,
            debugMode: false);
        #region Get Collateral Info
        [Fact]
        public async Task Check_GetCollateralInfo()
        {
            var collateralInfoString = await AccountService.GetAccountCollateralInfo("XRP");
            await Console.Out.WriteLineAsync(collateralInfoString);
            AssertSuccessfulResponse(collateralInfoString);
        }
        #endregion

        #region Set Collateral Info
        [Fact]
        public async Task Check_SetCollateralInfo()
        {
            var collateralInfoString = await AccountService.SetAccountCollateralCoin("XRP", CollateralSwitch.ON);
            await Console.Out.WriteLineAsync(collateralInfoString);
            AssertSuccessfulResponse(collateralInfoString);
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
            await Console.Out.WriteLineAsync(collateralInfoString);
            AssertSuccessfulResponse(collateralInfoString);
            var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<BatchSetCollateralCoinResult>>(collateralInfoString!);
            Assert.NotNull(generalResponse?.Result?.collateralCoinEntries);
        }

        [Fact]
        public async Task Check_BatchSetCollateralInfoByClass()
        {
            var coin1 = new SetCollateralCoinRequest { coin = "MATIC", collateralSwitch = "OFF", };
            var coin2 = new SetCollateralCoinRequest { coin = "SOL", collateralSwitch = "OFF", };
            var collateralInfoString = await AccountService.BatchSetAccountCollateralCoin(request: new List<SetCollateralCoinRequest> { coin1, coin2 });
            await Console.Out.WriteLineAsync(collateralInfoString);
            AssertSuccessfulResponse(collateralInfoString);
            var generalResponse = JsonConvert.DeserializeObject<GeneralResponse<BatchSetCollateralCoinResult>>(collateralInfoString!);
            Assert.NotNull(generalResponse?.Result?.collateralCoinEntries);
        }
        #endregion

        private static void AssertSuccessfulResponse(string? response)
        {
            Assert.False(string.IsNullOrWhiteSpace(response));
            Assert.Equal(0, JObject.Parse(response!)["retCode"]?.Value<int>());
        }
    }
}
