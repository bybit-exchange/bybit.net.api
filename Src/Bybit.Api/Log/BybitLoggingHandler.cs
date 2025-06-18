using Microsoft.Extensions.Logging;

namespace bybit.net.api
{
    /// <summary>
    /// Bybit message processing logging handler.<para/>
    /// A middlewear to listen and log any request or response.
    /// </summary>
    public class BybitLoggingHandler : MessageProcessingHandler
    {
        private readonly ILogger logger;

        public BybitLoggingHandler(ILogger logger)
        : base(new HttpClientHandler())
        {
            this.logger = logger;
        }

        public BybitLoggingHandler(ILogger logger, HttpMessageHandler innerHandler)
        : base(innerHandler)
        {
            this.logger = logger;
        }

        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            logger.LogInformation(message: request.ToString());

            if (null != request.Content)
            {
                logger.LogInformation(message: request.Content.ReadAsStringAsync(cancellationToken).Result);
            }

            return request;
        }

        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            logger.LogInformation(message: response.ToString());

            if (null != response.Content)
            {
                logger.LogInformation(message: response.Content.ReadAsStringAsync(cancellationToken).Result);
            }

            return response;
        }
    }
}
