using Microsoft.Extensions.Logging;

namespace Bybit.Api.Logging;

/// <summary>
/// Bybit message processing logging handler.<para/>
/// A middlewear to listen and log any request or response.
/// </summary>
public sealed class BybitLoggingHandler : MessageProcessingHandler
{
    private const string MessageLogEntry = "Request content: '{Content}'";
    private readonly ILogger _logger;

    public BybitLoggingHandler(ILogger logger)
        : base(new HttpClientHandler())
    {
        _logger = logger;
    }

    public BybitLoggingHandler(ILogger logger, HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
        _logger = logger;
    }

    protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(MessageLogEntry, request);

        LogHttpContent(request.Content, cancellationToken);

        return request;
    }

    protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        _logger.LogInformation(MessageLogEntry, response);

        LogHttpContent(response.Content, cancellationToken);

        return response;
    }

    private void LogHttpContent(HttpContent? httpContent, CancellationToken cancellationToken)
    {
        if (httpContent is null)
        {
            return;
        }

        using var contentStream = httpContent.ReadAsStream(cancellationToken);
        using var contentReader = new StreamReader(contentStream);
        var content = contentReader.ReadToEnd();

        _logger.LogInformation(MessageLogEntry, content);
    }
}