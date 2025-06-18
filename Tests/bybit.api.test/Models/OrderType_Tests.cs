using Bybit.Api.Models.Trade;

using Xunit;

namespace Bybit.Api.Test.Models;

public class OrderType_Tests
{
    [Fact]
    public void ToString_Matches_Value()
    {
        var model = OrderType.LIMIT;

        Assert.Equal(model.Value.ToString(), model.ToString());
    }
}