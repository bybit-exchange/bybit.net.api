using Bybit.Api.Models.Trade;

using Xunit;

namespace Bybit.Api.Test.Models;

public class TimeInForce_Tests
{
    [Fact]
    public void ToString_Matches_Value()
    {
        var model = TimeInForce.GTC;

        Assert.Equal(model.Value.ToString(), model.ToString());
    }
}