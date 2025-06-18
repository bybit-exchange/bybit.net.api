namespace bybit.net.api
{
    public class BybitHttpException : Exception
    {
        public BybitHttpException()
        {
        }

        public BybitHttpException(string? message) : base(message)
        {
        }

        public BybitHttpException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public int StatusCode { get; set; }

        public Dictionary<string, IEnumerable<string>>? Headers { get; set; }
    }
}