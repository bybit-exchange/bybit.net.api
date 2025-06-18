using System;

namespace bybit.net.api.Services
{
    public abstract class BybitApiService : BybitService
    {
        public BybitApiService(HttpClient httpClient, string ? apiKey = null, string? apiSecret = null, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false) 
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow:recvWindow, debugMode: debugMode)
        {
        }

        public BybitApiService(HttpClient httpClient, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }
    }
}
