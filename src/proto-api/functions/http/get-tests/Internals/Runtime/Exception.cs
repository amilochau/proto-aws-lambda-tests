using System;
using System.Collections.Generic;

namespace Milochau.Proto.Http.GetTests.Internals.Runtime
{
    public class ExceptionInfo
    {
        [System.Text.Json.Serialization.JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("stackTrace")]
        public string? StackTrace { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("cause")]
        public ExceptionInfo? InnerException { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("causes")]
        public List<ExceptionInfo> InnerExceptions { get; internal set; } = new List<ExceptionInfo>();

        public ExceptionInfo(Exception exception)
        {
            ErrorMessage = exception.Message;
            StackTrace = exception.StackTrace;

            if (exception.InnerException != null)
            {
                InnerException = new ExceptionInfo(exception.InnerException);
            }

            AggregateException? aggregateException = exception as AggregateException;

            if (aggregateException != null && aggregateException.InnerExceptions != null)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    InnerExceptions.Add(new ExceptionInfo(innerException));
                }
            }
        }
    }

}
