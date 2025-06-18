using System.Net.WebSockets;

using Bybit.Api.Utils;
using Bybit.Api.Websockets;

namespace Bybit.Api.WebSocketStream;

public class BybitSpotWebSocket : BybitWebSocket
{
    public BybitSpotWebSocket(bool useTestNet = false, int pingIntevral = 20, int receiveBufferSize = 8192, string? apiKey = null, string? apiSecret = null)
        : base(new BybitWebSocketHandler(new ClientWebSocket()), GetStreamUrl(useTestNet), pingIntevral, receiveBufferSize, apiKey, apiSecret)
    {
    }

    public BybitSpotWebSocket(IBybitWebSocketHandler handler, bool useTestNet = false, int pingIntevral = 20, int receiveBufferSize = 8192, string? apiKey = null, string? apiSecret = null)
        : base(handler, GetStreamUrl(useTestNet), pingIntevral, receiveBufferSize, apiKey, apiSecret)
    {
    }

    private static string GetStreamUrl(bool useTestNet)
    {
        return !useTestNet ? BybitConstants.SPOT_MAINNET : BybitConstants.SPOT_TESTNET;
    }
}