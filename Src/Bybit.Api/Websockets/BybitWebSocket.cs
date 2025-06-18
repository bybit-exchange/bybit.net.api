using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using Bybit.Api.Exceptions;
using Bybit.Api.Utils;

using Newtonsoft.Json;

namespace Bybit.Api.Websockets;

public class BybitWebSocket : IAsyncDisposable
{
    private readonly IBybitWebSocketHandler _handler;
    private readonly List<Func<string, Task>> _onMessageReceivedFunctions;
    private readonly List<CancellationTokenRegistration> _onMessageReceivedCancellationTokenRegistrations;
    private CancellationTokenSource? _loopCancellationTokenSource;
    private readonly string _url;
    private readonly int _pingInterval;
    private readonly int _receiveBufferSize;
    private readonly string? _apiKey;
    private readonly string? _apiSecret;
    private readonly string? _maxAliveTime; // Valid only for private channel
    private readonly bool _debugMode;

    protected BybitWebSocket(IBybitWebSocketHandler handler,
        string url,
        int pingInterval = 20,
        int receiveBufferSize = 8192,
        string? apiKey = null,
        string? apiSecret = null,
        bool debugMode = false,
        string? maxAliveTime = null)
    {
        _handler = handler;
        _url = url;
        _receiveBufferSize = receiveBufferSize;
        _apiKey = apiKey;
        _apiSecret = apiSecret;
        _debugMode = debugMode;
        _pingInterval = pingInterval;
        _maxAliveTime = maxAliveTime;
        _onMessageReceivedFunctions = [];
        _onMessageReceivedCancellationTokenRegistrations = [];
    }

    // <summary>
    /// Establishes a connection to the WebSocket. If required, sends an authentication and subscription request.
    /// </summary>
    /// <param name="args">Arguments for the subscription request.</param>
    /// <param name="cancellationToken">Token to signal the asynchronous operation to cancel.</param>
    /// <returns>A task that represents the asynchronous connect operation.</returns>
    /// <exception cref="BybitClientException">Thrown when a necessary parameter is missing or when the WebSocket is in an unexpected state.</exception>
    public async Task ConnectAsync(string[] args, CancellationToken cancellationToken)
    {
        if (_handler.State != WebSocketState.Open)
        {
            if (_debugMode)
            {
                _handler.EnableDebugMode();
            }

            _loopCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            if (string.IsNullOrEmpty(_url))
            {
                throw new BybitClientException("Please set up a websocket url", -1);
            }

            var wssUrl = !string.IsNullOrEmpty(_maxAliveTime) && RequiresAuthentication(_url) ? GetWssUrl(_maxAliveTime) : _url;
            await _handler.ConnectAsync(new Uri(wssUrl), cancellationToken)
                .ConfigureAwait(false);

            _ = Task.Run(() => Ping(_loopCancellationTokenSource.Token), cancellationToken);

            if (RequiresAuthentication(_url))
            {
                if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_apiSecret))
                    throw new BybitClientException("Please set up your api key and api secret for private websocket channel", -1);
                await SendAuth(_apiKey, _apiSecret)
                    .ConfigureAwait(false);
            }
            await SendSubscription(args)
                .ConfigureAwait(false);
            await Task.Factory.StartNew(() => ReceiveLoop(_loopCancellationTokenSource.Token, _receiveBufferSize), _loopCancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Disconnects from the WebSocket gracefully.
    /// </summary>
    /// <param name="cancellationToken">Token to signal the asynchronous operation to cancel.</param>
    /// <returns>A task that represents the asynchronous disconnect operation.</returns>
    public async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        _loopCancellationTokenSource?.Cancel();

        if (_handler.State == WebSocketState.Open)
        {
            await _handler.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, cancellationToken);
            await _handler.CloseAsync(WebSocketCloseStatus.NormalClosure, cancellationToken);
        }
    }

    /// <summary>
    /// Registers a callback function to be invoked when a new message is received from the WebSocket.
    /// </summary>
    /// <param name="onMessageReceived">Callback function to handle the received message.</param>
    /// <param name="cancellationToken">Token to signal the callback registration to cancel.</param>
    public void OnMessageReceived(Func<string, Task> onMessageReceived, CancellationToken cancellationToken)
    {
        _onMessageReceivedFunctions.Add(onMessageReceived);

        if (cancellationToken != CancellationToken.None)
        {
            var reg = cancellationToken.Register(() =>
                _onMessageReceivedFunctions.Remove(onMessageReceived));

            _onMessageReceivedCancellationTokenRegistrations.Add(reg);
        }
    }

    /// <summary>
    /// Sends a message to the WebSocket.
    /// </summary>
    /// <param name="message">Message content to send.</param>
    /// <param name="cancellationToken">Token to signal the asynchronous operation to cancel.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    public async Task SendAsync(string message, CancellationToken cancellationToken)
    {
        var byteArray = Encoding.ASCII.GetBytes(message);

        await _handler.SendAsync(new ArraySegment<byte>(byteArray), WebSocketMessageType.Text, true, cancellationToken);
    }

    /// <summary>
    /// CUSTOMISE PRIVATE CONNECTION ALIVE TIME. For private channel, you can customise alive duration by adding a param max_alive_time, the lowest value is 30s (30 seconds),
    /// the highest value is 600s (10 minutes). You can also pass 1m, 2m etc when you try to configure by minute level. e.g., 
    /// wss://stream-testnet.bybit.com/v5/private?max_alive_time=1m.
    /// </summary>
    /// <returns>websocket url</returns>
    private string GetWssUrl(string expression)
    {
        Regex pattern = new("(\\d+)([sm])");
        var match = pattern.Match(expression);
        string wssUrl;

        if (match.Success)
        {
            var timeValue = int.Parse(match.Groups[1].Value);
            var timeUnit = match.Groups[2].Value;
            var isTimeValid = IsTimeValid(timeUnit, timeValue);
            wssUrl = isTimeValid
                         ? $"{_url}?max_alive_time={_maxAliveTime}"
                         : $"{_url}";
        }
        else
        {
            wssUrl = $"{_url}";
        }
        Console.WriteLine(wssUrl);
        return wssUrl;
    }

    /// <summary>
    /// check max alive time expression is valid or not
    /// </summary>
    /// <param name="timeUnit">time unit</param>
    /// <param name="timeValue">time value</param>
    /// <returns>boolean</returns>
    private bool IsTimeValid(string timeUnit, int timeValue)
    {
        return ("s".Equals(timeUnit) && timeValue >= 30 && timeValue <= 600)
               || ("m".Equals(timeUnit) && timeValue >= 1 && timeValue <= 10);
    }

    /// <summary>
    /// Determines if the provided path requires authentication.
    /// </summary>
    /// <param name="path">The API path to be checked.</param>
    /// <returns>True if authentication is required, otherwise False.</returns>
    private bool RequiresAuthentication(string path) => BybitConstants.WEBSOCKET_PRIVATE_MAINNET.Equals(path) ||
                                                        BybitConstants.WEBSOCKET_PRIVATE_TESTNET.Equals(path) ||
                                                        BybitConstants.V3_CONTRACT_PRIVATE.Equals(path) ||
                                                        BybitConstants.V3_UNIFIED_PRIVATE.Equals(path) ||
                                                        BybitConstants.V3_SPOT_PRIVATE.Equals(path);

    /// <summary>
    /// Sends an authentication request to the WebSocket using the provided key and secret.
    /// </summary>
    /// <param name="key">The API key for authentication.</param>
    /// <param name="secret">The API secret for generating the signature.</param>
    /// <returns>A task that represents the asynchronous authentication operation.</returns>
    private async Task SendAuth(string key, string secret)
    {
        var expires = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + 10000;
        var val = $"GET/realtime{expires}";

        var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(val));
        var signature = BitConverter.ToString(hash).Replace("-", "").ToLower();

        var authMessage = new { req_id = BybitParametersUtils.GenerateTransferId(), op = "auth", args = new object[] { key, expires, signature } };
        var authMessageJson = JsonConvert.SerializeObject(authMessage);
        await Console.Out.WriteLineAsync(authMessageJson)
            .ConfigureAwait(false);
        await SendAsync(authMessageJson, CancellationToken.None)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a subscription request to the WebSocket using the provided arguments.
    /// </summary>
    /// <param name="args">Arguments for the subscription request.</param>
    /// <returns>A task that represents the asynchronous subscription operation.</returns>
    private async Task SendSubscription(string[] args)
    {
        BybitParametersUtils.EnsureNoDuplicates(args);
        var subMessage = new { req_id = Guid.NewGuid().ToString(), op = "subscribe", args = args };
        var subMessageJson = JsonConvert.SerializeObject(subMessage);

        await Console.Out.WriteLineAsync($"send subscription {subMessageJson}")
            .ConfigureAwait(false);
        await SendAsync(subMessageJson, CancellationToken.None)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Periodically sends a "ping" message to keep the WebSocket connection alive.
    /// </summary>
    /// <param name="token">Token to signal the asynchronous operation to cancel.</param>
    /// <returns>A task that represents the asynchronous ping operation.</returns>
    private async Task Ping(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(_pingInterval), token);
            if (_handler.State == WebSocketState.Open)
            {
                await SendAsync("{\"op\":\"ping\"}", CancellationToken.None);
                await Console.Out.WriteLineAsync("ping sent");
            }
        }
    }

    /// <summary>
    /// Listens for incoming messages from the WebSocket and processes them.
    /// </summary>
    /// <param name="cancellationToken">Token to signal the asynchronous operation to cancel.</param>
    /// <param name="receiveBufferSize">Size of the buffer used to receive messages.</param>
    /// <returns>A task that represents the asynchronous receive operation.</returns>
    private async Task ReceiveLoop(CancellationToken cancellationToken, int receiveBufferSize = 8192)
    {
        WebSocketReceiveResult receiveResult;
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var buffer = new ArraySegment<byte>(new byte[receiveBufferSize]);
                receiveResult = await _handler.ReceiveAsync(buffer, cancellationToken);

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }

                var content = Encoding.UTF8.GetString(buffer.ToArray(), buffer.Offset, buffer.Count);
                _onMessageReceivedFunctions.ForEach(omrf => omrf(content));
            }
        }
        catch (TaskCanceledException)
        {
            await DisconnectAsync(CancellationToken.None)
                .ConfigureAwait(false);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisconnectAsync(CancellationToken.None)
            .ConfigureAwait(false);
        await CastAndDispose(_handler)
            .ConfigureAwait(false);

        if (_loopCancellationTokenSource != null)
        {
            await CastAndDispose(_loopCancellationTokenSource)
                .ConfigureAwait(false);
        }

        return;

        async static ValueTask CastAndDispose(IDisposable resource)
        {
            if (resource is IAsyncDisposable resourceAsyncDisposable)
            {
                await resourceAsyncDisposable
                    .DisposeAsync()
                    .ConfigureAwait(false);
            }
            else
            {
                resource.Dispose();
            }
        }
    }
}