using Bybit.Api.ApiServiceImp;
using Bybit.Api.Models;
using Bybit.Api.Models.Position;
using Bybit.Api.Utils;

using Xunit;

namespace Bybit.Api.Test;

public class PositionServiceTest
{
    readonly BybitPositionService PositionService = new(apiKey: "X6wmWloIPvaLXAKqv2", apiSecret: "rY1CWGYLHy0AUjdNZqqspvd3Krhp79fHp1sP", url:BybitConstants.HTTP_TESTNET_URL);
    #region Poistion GetPositionList
    [Fact]
    public async Task Check_ConfirmPositionInfo()
    {
        var inversePositionInfoString = await PositionService.GetPositionInfo(category: Category.INVERSE, symbol: "BTCUSD");
        await Console.Out.WriteLineAsync(inversePositionInfoString);
    }
    #endregion
    #region Poistion Confirm new risk limit
    [Fact]
    public async Task Check_ConfirmPositionNewRiskLimit()
    {
        var positionInfoString = await PositionService.ConfirmPositionRiskLimit(category: Category.LINEAR, symbol:"BTCUSDT");
        await Console.Out.WriteLineAsync(positionInfoString);
    }
    #endregion

    #region Get Move Position History
    [Fact]
    public async Task Check_MovePositionInfoHistory()
    {
        var positionInfoString = await PositionService.GetMovePositionHistory();
        await Console.Out.WriteLineAsync(positionInfoString);
        Assert.NotNull(positionInfoString);
    }
    #endregion

    #region Move Position
    [Fact]
    public async Task Check_MovePositionByDict()
    {
        Dictionary<string, object> dict1 = new() { { "category", "spot" }, { "symbol", "BTCUSDT" }, { "price", "100" }, { "side", "Sell" }, { "qty", "0.01" } };
        List<Dictionary<string, object>> request = new() { dict1};
        var positionInfoString = await PositionService.MovePosition(fromUid: "123456", toUid: "456789", list: request);
        if (!string.IsNullOrEmpty(positionInfoString))
        {
            await Console.Out.WriteLineAsync(positionInfoString);    
            Assert.NotNull(positionInfoString);
        }
    }

    [Fact]
    public async Task Check_MovePositionByClass()
    {
        var request = new MovePositionRequest{ category= "spot", symbol="BTCUSDT", price="100",side="Sell",qty="0.01" };
        var positionInfoString = await PositionService.MovePosition(fromUid: "123456", toUid: "456789", list: new List<MovePositionRequest> { request });
        if (!string.IsNullOrEmpty(positionInfoString))
        {
            await Console.Out.WriteLineAsync(positionInfoString);
            Assert.NotNull(positionInfoString);
        }
    }
    #endregion
}