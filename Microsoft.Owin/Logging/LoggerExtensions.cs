using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Owin.Logging
{
    public static class LoggerExtensions
    {
        private static readonly Func<object, Exception, string> TheMessage = (message, error) => ((string)message);

        private static readonly Func<object, Exception, string> TheMessageAndError =
            (message, error) => string.Format(CultureInfo.CurrentCulture, "{0}\r\n{1}", new[] {message, error});
        
        public static bool IsEnabled(this ILogger logger, TraceEventType eventType)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            return logger.WriteCore(eventType, 0, null, null, null);
        }

        public static void WriteCritical(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Critical, 0, message, null, TheMessage);
        }

        public static void WriteCritical(this ILogger logger, string message, Exception error)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Critical, 0, message, error, TheMessageAndError);
        }

        public static void WriteError(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Error, 0, message, null, TheMessage);
        }

        public static void WriteError(this ILogger logger, string message, Exception error)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Error, 0, message, error, TheMessageAndError);
        }

        public static void WriteInformation(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Information, 0, message, null, TheMessage);
        }

        public static void WriteVerbose(this ILogger logger, string data)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Verbose, 0, data, null, TheMessage);
        }

        public static void WriteWarning(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Warning, 0,
                string.Format(CultureInfo.InvariantCulture, message, args), null,
                TheMessage);
        }

        public static void WriteWarning(this ILogger logger, string message, Exception error)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Warning, 0, message, error, TheMessageAndError);
        }
    }
}
