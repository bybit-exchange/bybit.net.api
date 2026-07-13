using System.Security.Cryptography;
using System.Text;

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
}
