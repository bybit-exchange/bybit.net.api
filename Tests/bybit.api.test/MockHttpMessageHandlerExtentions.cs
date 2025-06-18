using Moq.Protected;

namespace Bybit.Api.Test;

public static class MockHttpMessageHandlerExtentions
{
    public static Moq.Language.Flow.ISetup<HttpMessageHandler, Task<HttpResponseMessage>> SetupSendAsync(this IProtectedMock<HttpMessageHandler> mock, string absolutePath, HttpMethod method)
    {
        return mock.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(rm => rm.Method == method && rm.RequestUri!.AbsolutePath == absolutePath), ItExpr.IsAny<CancellationToken>());
    }
}