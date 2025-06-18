using Bybit.Api.WebSocketStream;

using Xunit;

namespace Bybit.Api.Test;

public class PublicWebsocketTest
{
    readonly BybitLinearWebSocket bybitLinearWebSocket = new(useTestNet: true, pingIntevral: 5);
    #region Check Public Channel Orderbook
    [Fact]
    public async Task Check_OrderBookSubscribe()
    {
        bybitLinearWebSocket.OnMessageReceived(
            (data) =>
                {
                    Console.WriteLine(data);

                    return Task.CompletedTask;
                }, CancellationToken.None);

        await bybitLinearWebSocket.ConnectAsync(new string[] { "orderbook.50.BTCUSDT" }, CancellationToken.None);
    }
    #endregion
}