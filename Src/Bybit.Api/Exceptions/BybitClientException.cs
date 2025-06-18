namespace bybit.net.api
{
    public class BybitClientException : BybitHttpException
    {
        public BybitClientException(string message)
        : base(message)
        {
            Message = message;
        }

        public BybitClientException(string message, int code)
        : base(message)
        {
            Code = code;
            Message = message;
        }

        public BybitClientException(string message, int code, Exception innerException)
        : base(message, innerException)
        {
            Code = code;
            Message = message;
        }

        [Newtonsoft.Json.JsonProperty("retCode")]
        public int Code { get; set; }

        [Newtonsoft.Json.JsonProperty("retMsg")]
        public new string Message { get; protected set; }
    }
}
