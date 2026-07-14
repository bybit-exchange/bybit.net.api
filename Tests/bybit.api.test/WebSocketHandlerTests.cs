using bybit.net.api.Websockets;
using System.Net.WebSockets;
using System.Text;
using Xunit;

namespace bybit.api.test
{
    public class WebSocketHandlerTests
    {
        [Fact]
        public async Task Check_SubscribesToOrderBookAndReceivesData()
        {
            // Arrange
            var clientWebSocket = new ClientWebSocket();
            var webSocketHandler = new BybitWebSocketHandler(clientWebSocket);
            var cts = new CancellationTokenSource();
            var buffer = new byte[1024];

            // Connect
            await webSocketHandler.ConnectAsync(new Uri("wss://stream-testnet.bybit.com/v5/public/linear"), cts.Token);

            // Subscribe to the topic
            var subscribeMessage = "{\"op\":\"subscribe\",\"args\":[\"orderbook.50.BTCUSDT\"]}";
            await webSocketHandler.SendAsync(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(subscribeMessage)),
                WebSocketMessageType.Text,
                true,
                cts.Token);

            // Act (Read the message)
            WebSocketReceiveResult result = await webSocketHandler.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

            // Assert
            Assert.Contains("\"orderbook.50.BTCUSDT\"", receivedMessage); // Use a JSON parser for robust testing

            // Clean up
            cts.Cancel();
            webSocketHandler.Dispose();
        }
    }
}
