using bybit.net.api.ApiServiceImp;
using System.Net;
using Xunit;

namespace bybit.api.test.Tests;

public class SpotXServiceContractTests
{
    [Fact]
    public void AuthenticatedConstructors_AcceptPositionalCredentials()
    {
        using var client = new HttpClient(new RecordingHandler());

        _ = new BybitSpotXService("key", "secret");
        _ = new BybitSpotXService(client, "key", "secret");
    }

    [Fact]
    public async Task PublicConstructor_CallsPublicEndpointWithoutAuthenticationHeaders()
    {
        var handler = new RecordingHandler();
        var client = new HttpClient(handler);
        var service = new BybitSpotXService(client, url: "https://api.bybit.com");

        await service.GetLaunchpoolProjectList(1);

        Assert.Equal(HttpMethod.Get, handler.Method);
        Assert.Equal("/v5/spot-x/launchpool/project/list?status=1", handler.PathAndQuery);
        Assert.DoesNotContain(
            handler.HeaderNames,
            name => name.StartsWith("X-BAPI-", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task AuthenticatedConstructor_CallsPrivateEndpointWithAuthenticationHeaders()
    {
        var handler = new RecordingHandler();
        var client = new HttpClient(handler);
        var service = new BybitSpotXService(client, "key", "secret");

        await service.GetLaunchpoolUserCurrentStaking();

        Assert.Contains("X-BAPI-SIGN", handler.HeaderNames);
        Assert.Contains("X-BAPI-API-KEY", handler.HeaderNames);
        Assert.Contains("X-BAPI-SIGN-TYPE", handler.HeaderNames);
        Assert.Contains("X-BAPI-TIMESTAMP", handler.HeaderNames);
        Assert.Contains("X-BAPI-RECV-WINDOW", handler.HeaderNames);
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        public HttpMethod? Method { get; private set; }
        public string PathAndQuery { get; private set; } = string.Empty;
        public IReadOnlyCollection<string> HeaderNames { get; private set; } = Array.Empty<string>();

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Method = request.Method;
            PathAndQuery = request.RequestUri!.PathAndQuery;
            HeaderNames = request.Headers.Select(header => header.Key).ToArray();
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"retCode\":0,\"retMsg\":\"OK\"}")
            });
        }
    }
}
