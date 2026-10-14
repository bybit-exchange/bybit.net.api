using System.Security.Cryptography;
using System.Text;
using System.Net;

namespace bybit.api.test.Tests
{
    internal static class SigningTestUtil
    {
        public static string Sign(string data, string secret)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
        }
    }

    internal sealed class SignedRequestRecordingHandler : HttpMessageHandler
    {
        private readonly string responseContent;

        public SignedRequestRecordingHandler(string responseContent)
        {
            this.responseContent = responseContent;
        }

        public HttpMethod? Method { get; private set; }
        public string Path { get; private set; } = string.Empty;
        public string Body { get; private set; } = string.Empty;
        public string Timestamp { get; private set; } = string.Empty;
        public string Signature { get; private set; } = string.Empty;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Method = request.Method;
            Path = request.RequestUri!.AbsolutePath;
            Body = request.Content is null
                ? string.Empty
                : await request.Content.ReadAsStringAsync(cancellationToken);
            Timestamp = GetHeader(request, "X-BAPI-TIMESTAMP");
            Signature = GetHeader(request, "X-BAPI-SIGN");

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseContent),
            };
        }

        private static string GetHeader(HttpRequestMessage request, string name) =>
            request.Headers.TryGetValues(name, out var values) ? values.Single() : string.Empty;
    }
}
