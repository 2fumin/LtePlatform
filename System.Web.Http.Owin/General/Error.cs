using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.Http
{
    internal static class Error
    {
        private const string HttpScheme = "http";
        private const string HttpsScheme = "https";

        internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
        {
            return new ArgumentException(Format(messageFormat, messageArgs));
        }

        internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
        {
            return new ArgumentException(Format(messageFormat, messageArgs), parameterName);
        }

        internal static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, object actualValue, object minValue)
        {
            return new ArgumentOutOfRangeException(parameterName, actualValue, Format(Owin.Properties.Resources.ArgumentMustBeGreaterThanOrEqualTo, minValue));
        }

        internal static ArgumentOutOfRangeException ArgumentMustBeLessThanOrEqualTo(string parameterName, object actualValue, object maxValue)
        {
            return new ArgumentOutOfRangeException(parameterName, actualValue, Format(Owin.Properties.Resources.ArgumentMustBeLessThanOrEqualTo, maxValue));
        }

        internal static ArgumentNullException ArgumentNull(string parameterName)
        {
            return new ArgumentNullException(parameterName);
        }

        internal static ArgumentNullException ArgumentNull(string parameterName, string messageFormat, params object[] messageArgs)
        {
            return new ArgumentNullException(parameterName, Format(messageFormat, messageArgs));
        }

        internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
        {
            return Argument(parameterName, Owin.Properties.Resources.ArgumentNullOrEmpty, parameterName);
        }

        internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName, object actualValue, string messageFormat, params object[] messageArgs)
        {
            return new ArgumentOutOfRangeException(parameterName, actualValue, Format(messageFormat, messageArgs));
        }

        internal static ArgumentException ArgumentUriHasQueryOrFragment(string parameterName, Uri actualValue)
        {
            return new ArgumentException(Format(Owin.Properties.Resources.ArgumentUriHasQueryOrFragment, actualValue), parameterName);
        }

        internal static ArgumentException ArgumentUriNotAbsolute(string parameterName, Uri actualValue)
        {
            return new ArgumentException(Format(Owin.Properties.Resources.ArgumentInvalidAbsoluteUri, actualValue), parameterName);
        }

        internal static ArgumentException ArgumentUriNotHttpOrHttpsScheme(string parameterName, Uri actualValue)
        {
            return new ArgumentException(Format(Owin.Properties.Resources.ArgumentInvalidHttpUriScheme, actualValue, HttpScheme, HttpsScheme), parameterName);
        }

        internal static string Format(string format, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, format, args);
        }

        internal static ArgumentException InvalidEnumArgument(string parameterName, int invalidValue, Type enumClass)
        {
            return new InvalidEnumArgumentException(parameterName, invalidValue, enumClass);
        }

        internal static InvalidOperationException InvalidOperation(string messageFormat, params object[] messageArgs)
        {
            return new InvalidOperationException(Format(messageFormat, messageArgs));
        }

        internal static InvalidOperationException InvalidOperation(Exception innerException, string messageFormat, params object[] messageArgs)
        {
            return new InvalidOperationException(Format(messageFormat, messageArgs), innerException);
        }

        internal static KeyNotFoundException KeyNotFound()
        {
            return new KeyNotFoundException();
        }

        internal static KeyNotFoundException KeyNotFound(string messageFormat, params object[] messageArgs)
        {
            return new KeyNotFoundException(Format(messageFormat, messageArgs));
        }

        internal static NotSupportedException NotSupported(string messageFormat, params object[] messageArgs)
        {
            return new NotSupportedException(Format(messageFormat, messageArgs));
        }

        internal static ObjectDisposedException ObjectDisposed(string messageFormat, params object[] messageArgs)
        {
            return new ObjectDisposedException(null, Format(messageFormat, messageArgs));
        }

        internal static OperationCanceledException OperationCanceled()
        {
            return new OperationCanceledException();
        }

        internal static OperationCanceledException OperationCanceled(string messageFormat, params object[] messageArgs)
        {
            return new OperationCanceledException(Format(messageFormat, messageArgs));
        }

        internal static ArgumentNullException PropertyNull(object value)
        {
            return new ArgumentNullException(nameof(value));
        }
    }
}
