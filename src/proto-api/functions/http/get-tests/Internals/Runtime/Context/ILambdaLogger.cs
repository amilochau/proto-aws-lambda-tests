namespace Milochau.Proto.Http.GetTests.Internals.Runtime.Context
{
    /// <summary>
    /// Lambda runtime logger.
    /// </summary>
    public interface ILambdaLogger
    {
        /// <summary>
        /// Logs a message to AWS CloudWatch Logs.
        /// 
        /// Logging will not be done:
        ///  If the role provided to the function does not have sufficient permissions.
        /// </summary>
        /// <param name="message"></param>
        void Log(string message);

        /// <summary>
        /// Logs a message, followed by the current line terminator, to AWS CloudWatch Logs.
        /// 
        /// Logging will not be done:
        ///  If the role provided to the function does not have sufficient permissions.
        /// </summary>
        /// <param name="message"></param>
        void LogLine(string message);

        /// <summary>
        /// Log message catagorized by the given log level
        /// <para>
        /// To configure the minimum log level set the AWS_LAMBDA_HANDLER_LOG_LEVEL environment variable. The value should be set
        /// to one of the values in the LogLevel enumeration. The default minimum log level is "Information".
        /// </para>
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        void Log(LogLevel level, string message);

        /// <summary>
        /// Log trace message
        /// <para>
        /// To configure the minimum log level set the AWS_LAMBDA_HANDLER_LOG_LEVEL environment variable. The value should be set
        /// to one of the values in the LogLevel enumeration. The default minimum log level is "Information".
        /// </para>
        /// </summary>
        /// <param name="message"></param>
        void LogTrace(string message) => Log(LogLevel.Trace, message);

        /// <summary>
        /// Log debug message
        /// <para>
        /// To configure the minimum log level set the AWS_LAMBDA_HANDLER_LOG_LEVEL environment variable. The value should be set
        /// to one of the values in the LogLevel enumeration. The default minimum log level is "Information".
        /// </para>
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message) => Log(LogLevel.Debug, message);

        /// <summary>
        /// Log information message
        /// <para>
        /// To configure the minimum log level set the AWS_LAMBDA_HANDLER_LOG_LEVEL environment variable. The value should be set
        /// to one of the values in the LogLevel enumeration. The default minimum log level is "Information".
        /// </para>
        /// </summary>
        /// <param name="message"></param>
        void LogInformation(string message) => Log(LogLevel.Information, message);

        /// <summary>
        /// Log warning message
        /// <para>
        /// To configure the minimum log level set the AWS_LAMBDA_HANDLER_LOG_LEVEL environment variable. The value should be set
        /// to one of the values in the LogLevel enumeration. The default minimum log level is "Information".
        /// </para>
        /// </summary>
        /// <param name="message"></param>
        void LogWarning(string message) => Log(LogLevel.Warning, message);

        /// <summary>
        /// Log error message
        /// <para>
        /// To configure the minimum log level set the AWS_LAMBDA_HANDLER_LOG_LEVEL environment variable. The value should be set
        /// to one of the values in the LogLevel enumeration. The default minimum log level is "Information".
        /// </para>
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message) => Log(LogLevel.Error, message);

        /// <summary>
        /// Log critical message
        /// <para>
        /// To configure the minimum log level set the AWS_LAMBDA_HANDLER_LOG_LEVEL environment variable. The value should be set
        /// to one of the values in the LogLevel enumeration. The default minimum log level is "Information".
        /// </para>
        /// </summary>
        /// <param name="message"></param>
        void LogCritical(string message) => Log(LogLevel.Critical, message);
    }
}
