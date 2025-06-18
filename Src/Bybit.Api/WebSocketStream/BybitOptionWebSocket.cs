using System.Net.WebSockets;

using Bybit.Api.Utils;
using Bybit.Api.Websockets;

namespace Bybit.Api.WebSocketStream;

public class BybitOptionWebSocket : BybitWebSocket
{
    public BybitOptionWebSocket(bool useTestNet = false, int pingIntevral = 20, int receiveBufferSize = 8192, string? apiKey = null, string? apiSecret = null, bool debugMode = false)
        : base(new BybitWebSocketHandler(new ClientWebSocket()), GetStreamUrl(useTestNet), pingIntevral, receiveBufferSize, apiKey, apiSecret, debugMode)
    {
    }

    public BybitOptionWebSocket(IBybitWebSocketHandler handler, bool useTestNet = false, int pingIntevral = 20, int receiveBufferSize = 8192, string? apiKey = null, string? apiSecret = null, bool debugMode = false)
        : base(handler, GetStreamUrl(useTestNet), pingIntevral, receiveBufferSize, apiKey, apiSecret, debugMode)
    {
    }

    private static string GetStreamUrl(bool useTestNet)
    {
        return !useTestNet ? BybitConstants.OPTION_MAINNET : BybitConstants.OPTION_TESTNET;
    }
}