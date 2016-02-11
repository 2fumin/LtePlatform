using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Owin
{
    /// <summary>
    /// Represents the host portion of a Uri can be used to construct Uri's properly formatted and encoded for use in
    /// HTTP headers.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HostString : IEquatable<HostString>
    {
        /// <summary>
        /// Creates a new HostString without modification. The value should be Unicode rather than punycode, and may have a port.
        /// IPv4 and IPv6 addresses are also allowed, and also may have ports.
        /// </summary>
        /// <param name="value"></param>
        public HostString(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns the original value from the constructor.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Returns the value as normalized by ToUriComponent().
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToUriComponent();
        }

        /// <summary>
        /// Returns the value properly formatted and encoded for use in a URI in a HTTP header.
        /// Any Unicode is converted to punycode.IPv6 addresses will have brackets added if they are missing.
        /// </summary>
        /// <returns></returns>
        public string ToUriComponent()
        {
            int num;
            if (string.IsNullOrEmpty(Value))
            {
                return string.Empty;
            }
            if (Value.IndexOf('[') >= 0)
            {
                return Value;
            }
            if ((((num = Value.IndexOf(':')) >= 0) && (num < (Value.Length - 1))) && (Value.IndexOf(':', num + 1) >= 0))
            {
                return ("[" + Value + "]");
            }
            if (num >= 0)
            {
                var str = Value.Substring(num);
                var mapping = new IdnMapping();
                return (mapping.GetAscii(Value, 0, num) + str);
            }
            var mapping2 = new IdnMapping();
            return mapping2.GetAscii(Value);
        }

        /// <summary>
        /// Creates a new HostString from the given uri component.
        /// Any punycode will be converted to Unicode.
        /// </summary>
        /// <param name="uriComponent"></param>
        /// <returns></returns>
        public static HostString FromUriComponent(string uriComponent)
        {
            int num;
            if (((string.IsNullOrEmpty(uriComponent) || (uriComponent.IndexOf('[') >= 0)) ||
                 ((((num = uriComponent.IndexOf(':')) >= 0) && (num < (uriComponent.Length - 1))) &&
                  (uriComponent.IndexOf(':', num + 1) >= 0))) ||
                (uriComponent.IndexOf("xn--", StringComparison.Ordinal) < 0)) return new HostString(uriComponent);
            if (num >= 0)
            {
                var str = uriComponent.Substring(num);
                var mapping = new IdnMapping();
                uriComponent = mapping.GetUnicode(uriComponent, 0, num) + str;
            }
            else
            {
                uriComponent = new IdnMapping().GetUnicode(uriComponent);
            }
            return new HostString(uriComponent);
        }

        /// <summary>
        /// Creates a new HostString from the host and port of the give Uri instance.
        /// Punycode will be converted to Unicode.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static HostString FromUriComponent(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return new HostString(uri.GetComponents(UriComponents.NormalizedHost | UriComponents.HostAndPort, UriFormat.Unescaped));
        }

        /// <summary>
        /// Compares the equality of the Value property, ignoring case.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(HostString other)
        {
            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Compares against the given object only if it is a HostString.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return ((obj is HostString) && Equals((HostString)obj));
        }

        /// <summary>
        /// Gets a hash code for the value.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value == null ? 0 : StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        }

        /// <summary>
        /// Compares the two instances for equality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(HostString left, HostString right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares the two instances for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(HostString left, HostString right)
        {
            return !left.Equals(right);
        }
    }
}
