using System;

namespace Milochau.Proto.Http.GetTests.Internals.Runtime.Context
{
    internal class LambdaConsoleLogger : ILambdaLogger
    {
        private readonly IConsoleLoggerWriter _consoleLoggerRedirector;

        public LambdaConsoleLogger(IConsoleLoggerWriter consoleLoggerRedirector)
        {
            _consoleLoggerRedirector = consoleLoggerRedirector;
        }

        public void Log(string message)
        {
            Console.Write(message);
        }

        public void LogLine(string message)
        {
            _consoleLoggerRedirector.FormattedWriteLine(message);
        }

        public void Log(LogLevel level, string message)
        {
            _consoleLoggerRedirector.FormattedWriteLine(level, message);
        }
    }
}
