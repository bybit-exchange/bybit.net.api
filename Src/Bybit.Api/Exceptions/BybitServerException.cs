

namespace bybit.net.api
{
        /// <summary>
        /// Bybit exception class for any errors throw as a result internal server issues.
        /// </summary>
        public class BybitServerException : BybitHttpException
        {
            public BybitServerException(string message)
            : base(message)
            {
                this.Message = message;
            }

            public BybitServerException(string message, Exception innerException)
            : base(message, innerException)
            {
                this.Message = message;
            }

            public new string Message { get; protected set; }
        }
    }
