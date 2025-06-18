using System.Net.WebSockets;
using System.Text;

using Bybit.Api.Utils;

namespace Bybit.Api.Websockets;

/// <summary>
/// Bybit humble object for <see cref="System.Net.WebSockets.ClientWebSocket" />.
/// </summary>
public class BybitWebSocketHandler : IBybitWebSocketHandler
{
    private static readonly string UserAgent = "bybit.net.api/" + VersionInfo.GetVersion;
    private bool debugEnabled = false;

    private readonly ClientWebSocket webSocket;

    public BybitWebSocketHandler(ClientWebSocket clientWebSocket)
    {
        webSocket = clientWebSocket ?? throw new ArgumentNullException(nameof(clientWebSocket));
        webSocket.Options.SetRequestHeader("User-Agent", UserAgent);
    }

    public WebSocketState State
    {
        get
        {
            return webSocket.State;
        }
    }

    public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
    {
        await webSocket.ConnectAsync(uri, cancellationToken);
    }

    public async Task CloseOutputAsync(WebSocketCloseStatus closeStatus, CancellationToken cancellationToken, string? statusDescription = null)
    {
        await webSocket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);
    }

    public async Task CloseAsync(WebSocketCloseStatus closeStatus, CancellationToken cancellationToken, string? statusDescription = null)
    {
        await webSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
    }

    public async Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
    {
        var receiveResult =  await webSocket.ReceiveAsync(buffer, cancellationToken);
        if (debugEnabled)
        {
            string receivedMessage = Encoding.UTF8.GetString(bytes: buffer.Array!, buffer.Offset, receiveResult.Count);
            Console.WriteLine($"[WebSocket Response] Received message: {receivedMessage}");
            Console.WriteLine($"[WebSocket Response] Message Type: {receiveResult.MessageType}");
        }
        return receiveResult;
    }

    public async Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
    {
        if (debugEnabled)
        {
            string messageToSend = Encoding.UTF8.GetString(buffer.Array!, buffer.Offset, buffer.Count);
            Console.WriteLine($"[WebSocket Request] Sending message: {messageToSend}");
            Console.WriteLine($"[WebSocket Request] Message Type: {messageType}");
            Console.WriteLine($"[WebSocket Request] End Of Message: {endOfMessage}");
        }

        await webSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
    }

    public void Dispose() => webSocket.Dispose();

    public void EnableDebugMode()
    {
        debugEnabled = true;
    }
}