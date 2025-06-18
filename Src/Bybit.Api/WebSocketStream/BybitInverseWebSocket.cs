using System.Net.WebSockets;

using Bybit.Api.Utils;
using Bybit.Api.Websockets;

namespace Bybit.Api.WebSocketStream;

public class BybitInverseWebSocket : BybitWebSocket
{
    public BybitInverseWebSocket(bool useTestNet = false, int pingIntevral = 20, int receiveBufferSize = 8192, string? apiKey = null, string? apiSecret = null, bool debugMode = false)
        : base(new BybitWebSocketHandler(new ClientWebSocket()), GetStreamUrl(useTestNet), pingIntevral, receiveBufferSize, apiKey, apiSecret, debugMode)
    {
    }

    public BybitInverseWebSocket(IBybitWebSocketHandler handler, bool useTestNet = false, int pingIntevral = 20, int receiveBufferSize = 8192, string? apiKey = null, string? apiSecret = null, bool debugMode = false)
        : base(handler, GetStreamUrl(useTestNet), pingIntevral, receiveBufferSize, apiKey, apiSecret, debugMode)
    {
    }

    private static string GetStreamUrl(bool useTestNet)
    {
        return !useTestNet ? BybitConstants.INVERSE_MAINNET : BybitConstants.INVERSE_TESTNET;
    }
}