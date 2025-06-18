using Bybit.Api.Services;
using Bybit.Api.Utils;

namespace Bybit.Api.Test.Mocks;

public class MockBybitService : BybitService
{
    public MockBybitService(HttpClient httpClient, string? apiKey = null, string? apiSecret = null, string? url = BybitConstants.HTTP_MAINNET_URL)
        : base(httpClient:httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url)
    {
    }

    public new async Task<T?> SendPublicAsync<T>(string requestUri, HttpMethod httpMethod, Dictionary<string, object>? query = null)
    {
        return await base.SendPublicAsync<T>(requestUri, httpMethod, query: query);
    }

    public new async Task<T?> SendSignedAsync<T>(string requestUri, HttpMethod httpMethod, Dictionary<string, object>? query = null)
    {
        return await base.SendSignedAsync<T>(requestUri, httpMethod, query: query);
    }
}