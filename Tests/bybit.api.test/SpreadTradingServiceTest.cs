using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bybit.net.api;
using bybit.net.api.ApiServiceImp;
using Xunit;

namespace bybit.api.test
{
    public class SpreadTradingServiceTest
    {
        readonly BybitSpreadTradingService SpreadService = new(
            apiKey: "",
            apiSecret: "",
            url: BybitConstants.HTTP_TESTNET_URL,
            debugMode: true);

        #region Get Instruments Info
        [Fact]
        public async Task Check_GetSpreadInstrumentsInfo()
        {
            var resp = await SpreadService.GetSpreadInstrumentsInfo(
                symbol: null,
                baseCoin: null,
                limit: 10,
                cursor: null);

            await Console.Out.WriteLineAsync(resp);
            Assert.NotNull(resp);
        }
        #endregion
    }
}
