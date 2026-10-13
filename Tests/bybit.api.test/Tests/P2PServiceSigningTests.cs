using bybit.net.api.ApiServiceImp;
using Moq;
using Moq.Protected;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace bybit.api.test.Tests
{
    public class P2PServiceSigningTests
    {
        [Fact]
        public async Task GetAllOrders_SignsExactJsonBody()
        {
            const string apiKey = "test-key";
            const string apiSecret = "test-secret";
            const string recvWindow = "5000";
            var handler = new JsonRecordingHandler();
            var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitP2PService(client, apiKey, apiSecret, recvWindow: recvWindow);

            var result = await service.GetAllOrders(1, 5);

            Assert.NotNull(result);
            var expectedSignature = Sign(
                $"{handler.Timestamp}{apiKey}{recvWindow}{handler.Body}",
                apiSecret);

            Assert.Equal("{\"page\":1,\"size\":5}", handler.Body);
            Assert.Equal(expectedSignature, handler.Signature);
        }

        [Fact]
        public async Task SignedGet_SignsExactQueryString()
        {
            const string apiKey = "test-key";
            const string apiSecret = "test-secret";
            const string recvWindow = "5000";
            var responseContent = "{\"retCode\":0,\"retMsg\":\"\",\"result\":{\"list\":[],\"nextPageCursor\":\"\"},\"time\":1}";
            HttpRequestMessage? capturedRequest = null;

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(request =>
                        request.Method == HttpMethod.Get &&
                        request.RequestUri!.AbsolutePath == "/v5/affiliate/aff-user-list"),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) => capturedRequest = request)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent),
                });

            var client = new HttpClient(handler.Object) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitAffiliateService(client, apiKey, apiSecret, recvWindow: recvWindow);

            var result = await service.GetAffiliateUserList(
                size: 10,
                cursor: "abc",
                needDeposit: true,
                need30: true,
                need365: false,
                startDate: "2025-10-21",
                endDate: "2025-10-22");

            Assert.NotNull(result);
            Assert.NotNull(capturedRequest);

            var queryString = capturedRequest!.RequestUri!.Query.TrimStart('?');
            var timestamp = capturedRequest.Headers.GetValues("X-BAPI-TIMESTAMP").Single();
            var signature = capturedRequest.Headers.GetValues("X-BAPI-SIGN").Single();
            var expectedSignature = Sign($"{timestamp}{apiKey}{recvWindow}{queryString}", apiSecret);

            Assert.Equal("size=10&cursor=abc&needDeposit=True&need30=True&need365=False&startDate=2025-10-21&endDate=2025-10-22", queryString);
            Assert.Equal(expectedSignature, signature);
        }

        [Fact]
        public async Task UploadChatFile_SendsMultipartFileContent()
        {
            const string apiKey = "test-key";
            const string apiSecret = "test-secret";
            const string recvWindow = "5000";
            var handler = new MultipartRecordingHandler();
            var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.bybit.com") };
            var service = new BybitP2PService(client, apiKey, apiSecret, recvWindow: recvWindow);
            var fileBytes = new byte[] { 0x89, 0x50, 0x4E, 0x47 };
            var filePath = Path.GetTempFileName();
            await File.WriteAllBytesAsync(filePath, fileBytes);

            try
            {
                await service.UploadChatFile(filePath, "image/png");

                Assert.Equal("multipart/form-data", handler.ContentType);
                var multipartText = Encoding.Latin1.GetString(handler.Body);
                Assert.Contains("upload_file", multipartText);
                Assert.Contains(Path.GetFileName(filePath), multipartText);
                Assert.True(handler.Body.AsSpan().IndexOf(fileBytes) >= 0);

                var signaturePayload = Encoding.UTF8
                    .GetBytes($"{handler.Timestamp}{apiKey}{recvWindow}")
                    .Concat(handler.Body)
                    .ToArray();
                Assert.Equal(Sign(signaturePayload, apiSecret), handler.Signature);
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        private static string Sign(string data, string secret)
            => Sign(Encoding.UTF8.GetBytes(data), secret);

        private static string Sign(byte[] data, string secret)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            return BitConverter.ToString(hmac.ComputeHash(data)).Replace("-", "").ToLower();
        }

        private sealed class MultipartRecordingHandler : HttpMessageHandler
        {
            public string? ContentType { get; private set; }
            public byte[] Body { get; private set; } = Array.Empty<byte>();
            public string Timestamp { get; private set; } = string.Empty;
            public string Signature { get; private set; } = string.Empty;

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                ContentType = request.Content?.Headers.ContentType?.MediaType;
                Body = await request.Content!.ReadAsByteArrayAsync(cancellationToken);
                Timestamp = request.Headers.GetValues("X-BAPI-TIMESTAMP").Single();
                Signature = request.Headers.GetValues("X-BAPI-SIGN").Single();

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"retCode\":0,\"retMsg\":\"OK\"}"),
                };
            }
        }

        private sealed class JsonRecordingHandler : HttpMessageHandler
        {
            public string Body { get; private set; } = string.Empty;
            public string Timestamp { get; private set; } = string.Empty;
            public string Signature { get; private set; } = string.Empty;

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.Equal("/v5/p2p/order/simplifyList", request.RequestUri!.AbsolutePath);
                Body = await request.Content!.ReadAsStringAsync(cancellationToken);
                Timestamp = request.Headers.GetValues("X-BAPI-TIMESTAMP").Single();
                Signature = request.Headers.GetValues("X-BAPI-SIGN").Single();

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"ret_code\":0,\"ret_msg\":\"SUCCESS\",\"result\":{\"count\":0,\"items\":[]},\"ext_code\":\"\",\"ext_info\":{},\"time_now\":\"1\"}"),
                };
            }
        }
    }
}
