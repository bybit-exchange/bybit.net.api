using Bybit.Api.Models.Trade;

using Xunit;

namespace Bybit.Api.Test.Models;

public class Side_Tests
{
    [Fact]
    public void ToString_Matches_Value()
    {
        var model = Side.BUY;

        Assert.Equal(model.Value.ToString(), model.ToString());
    }
}