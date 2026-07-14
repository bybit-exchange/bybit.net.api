using bybit.net.api.WebSocketStream;
using Xunit;

namespace bybit.api.test
{
    public class PrivateWebsocketTest
    {
        readonly BybitPrivateWebsocket bybitPrivateWebsocket = new(apiKey: "xxxxxxxxxxx", apiSecret: "xxxxxxxxxxxxxxx", useTestNet: true, pingIntevral: 5, maxAliveTime:"120s");
        #region Check Private channel with max alive time
        [Fact]
        public async Task Check_OrderBookSubscribe()
        {
            bybitPrivateWebsocket.OnMessageReceived(
             (data) =>
             {
                 Console.WriteLine(data);

                 return Task.CompletedTask;
             }, CancellationToken.None);

            await bybitPrivateWebsocket.ConnectAsync(new string[] { "order" }, CancellationToken.None);
        }
        #endregion
    }
}
