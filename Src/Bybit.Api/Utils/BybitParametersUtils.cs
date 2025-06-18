using System.Diagnostics;

namespace bybit.net.api
{
    public class BybitParametersUtils
    {
        /// <summary>
        /// Adds optional parameters to the provided query dictionary if they have valid values.
        /// </summary>
        /// <param name="query">The dictionary to which the optional parameters will be added.</param>
        /// <param name="parameters">An array of key-value pairs representing optional parameters.</param>
        public static void AddOptionalParameters(Dictionary<string, object> query, params (string key, object? value)[] parameters)
        {
            foreach (var (key, value) in parameters)
            {
                if (value != null && !(value is string strValue && string.IsNullOrEmpty(strValue)))
                {
                    query.Add(key, value);
                }
            }
        }

        /// <summary>
        /// Ensures that the provided args dictionary contains only one valid key from the specified valid keys.
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void EnsureNoDuplicates(string[] args)
        {
            if (args.Distinct().Count() != args.Length)
            {
                throw new ArgumentException("The provided arguments contain duplicate values.");
            }
        }
        /// <summary>
        /// Generate a UUID
        /// </summary>
        /// <returns>uuid in string</returns>
        public static string GenerateTransferId()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Get current time stamp
        /// </summary>
        /// <returns>timestamp in mille seconds</returns>
        public static string GetCurrentTimeStamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        }

    }
}
