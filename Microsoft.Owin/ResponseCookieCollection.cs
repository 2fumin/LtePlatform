using System;
using System.Globalization;
using System.Linq;

namespace Microsoft.Owin
{
    public class ResponseCookieCollection
    {
        public ResponseCookieCollection(IHeaderDictionary headers)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            Headers = headers;
        }

        public void Append(string key, string value)
        {
            Headers.AppendValues("Set-Cookie", Uri.EscapeDataString(key) + "=" + Uri.EscapeDataString(value) + "; path=/");
        }

        public void Append(string key, string value, CookieOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var flag = !string.IsNullOrEmpty(options.Domain);
            var flag2 = !string.IsNullOrEmpty(options.Path);
            var hasValue = options.Expires.HasValue;
            string[] strArray = { Uri.EscapeDataString(key), "=", Uri.EscapeDataString(value ?? string.Empty), !flag ? null : "; domain=", !flag ? null : options.Domain, !flag2 ? null : "; path=", !flag2 ? null : options.Path, !hasValue ? null : "; expires=", !hasValue ? null : (options.Expires.Value.ToString("ddd, dd-MMM-yyyy HH:mm:ss ", CultureInfo.InvariantCulture) + "GMT"), !options.Secure ? null : "; secure", !options.HttpOnly ? null : "; HttpOnly" };
            var str = string.Concat(strArray);
            Headers.AppendValues("Set-Cookie", str);
        }

        public void Delete(string key)
        {
            Func<string, bool> predicate = value => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase);
            string[] values = { Uri.EscapeDataString(key) + "=; expires=Thu, 01-Jan-1970 00:00:00 GMT" };
            var source = Headers.GetValues("Set-Cookie");
            if ((source == null) || (source.Count == 0))
            {
                Headers.SetValues("Set-Cookie", values);
            }
            else
            {
                Func<string, bool> func = value => !predicate(value);
                Headers.SetValues("Set-Cookie", source.Where(func).Concat(values).ToArray());
            }
        }

        public void Delete(string key, CookieOptions options)
        {
            Func<string, bool> rejectPredicate;
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var flag = !string.IsNullOrEmpty(options.Domain);
            var flag2 = !string.IsNullOrEmpty(options.Path);
            if (flag)
            {
                Func<string, bool> func = value =>
                    value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase) &&
                    (value.IndexOf("domain=" + options.Domain, StringComparison.OrdinalIgnoreCase) != -1);
                rejectPredicate = func;
            }
            else if (flag2)
            {
                Func<string, bool> func2 = value =>
                    value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase) &&
                    (value.IndexOf("path=" + options.Path, StringComparison.OrdinalIgnoreCase) != -1);
                rejectPredicate = func2;
            }
            else
            {
                Func<string, bool> func3 = value => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase);
                rejectPredicate = func3;
            }
            var values = Headers.GetValues("Set-Cookie");
            if (values != null)
            {
                Func<string, bool> predicate = value => !rejectPredicate(value);
                Headers.SetValues("Set-Cookie", values.Where(predicate).ToArray());
            }
            var options2 = new CookieOptions
            {
                Path = options.Path,
                Domain = options.Domain,
                Expires = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            };
            Append(key, string.Empty, options2);
        }

        private IHeaderDictionary Headers { get; }
    }
}
