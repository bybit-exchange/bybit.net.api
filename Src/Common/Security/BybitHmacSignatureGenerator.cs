using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace bybit.net.api
{
    public class BybitHmacSignatureGenerator : IBybitSignatureService
    {
        private readonly string? apikey;
        private readonly string? apiSecret;
        private readonly string currentTimeStamp;
        private readonly string? recWindow;

        public BybitHmacSignatureGenerator(string? apikey, string? apiSecret, string currentTimeStamp, string recWindow)
        {
            this.apikey = apikey;
            this.apiSecret = apiSecret;
            this.currentTimeStamp = currentTimeStamp;
            this.recWindow = recWindow;
        }

        
        /// <summary>
        /// Generate signature for http post request
        /// </summary>
        /// <param name="parameters">query parameters</param>
        /// <returns>signature</returns>
        public string GeneratePostSignature(IDictionary<string, object> parameters)
        {
            string paramJson = JsonConvert.SerializeObject(parameters);
            return GeneratePostSignature(paramJson);
        }

        public string GeneratePostSignature(string body)
        {
            string paramJson = body;
            string rawData = $"{currentTimeStamp}{apikey}{recWindow}{paramJson}";
            return Sign(rawData);
        }

        public string GeneratePostSignature(byte[] body)
        {
            byte[] prefix = Encoding.UTF8.GetBytes($"{currentTimeStamp}{apikey}{recWindow}");
            byte[] rawData = new byte[prefix.Length + body.Length];
            Buffer.BlockCopy(prefix, 0, rawData, 0, prefix.Length);
            Buffer.BlockCopy(body, 0, rawData, prefix.Length, body.Length);
            return Sign(rawData);
        }

        /// <summary>
        /// Generate signature for http get request
        /// </summary>
        /// <param name="parameters">query parameters</param>
        /// <returns>signature</returns>
        public string GenerateGetSignature(IDictionary<string, object> parameters)
        {
            string queryString = GenerateQueryString(parameters);
            return GenerateGetSignature(queryString);
        }

        public string GenerateGetSignature(string queryString)
        {
            string rawData = $"{currentTimeStamp}{apikey}{recWindow}{queryString}";
            return Sign(rawData);
        }

        private string Sign(string data) => Sign(Encoding.UTF8.GetBytes(data));

        private string Sign(byte[] data)
        {
            try
            {
                if (string.IsNullOrEmpty(apikey) || string.IsNullOrEmpty(apiSecret))
                    throw new BybitClientException("Please set your api key and api secret", -1);
                using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
                byte[] computedHash = hmac.ComputeHash(data);
                return BitConverter.ToString(computedHash).Replace("-", "").ToLower();
            }
            catch (Exception ex)
            {
                throw new BybitClientException("Failed to calculate HMAC-SHA256", -1, ex);
            }
        }

        private string GenerateQueryString(IDictionary<string, object> parameters)
        {
            return string.Join("&", parameters
                        .OrderBy(p => p.Key) // Ensure parameters are sorted alphabetically
                        .Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value.ToString()!)}"));
        }
    }
}
