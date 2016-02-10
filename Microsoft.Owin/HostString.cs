using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Owin
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HostString : IEquatable<HostString>
    {
        private readonly string _value;

        public HostString(string value)
        {
            _value = value;
        }

        public string Value => _value;

        public override string ToString()
        {
            return ToUriComponent();
        }

        public string ToUriComponent()
        {
            int num;
            if (string.IsNullOrEmpty(_value))
            {
                return string.Empty;
            }
            if (_value.IndexOf('[') >= 0)
            {
                return _value;
            }
            if ((((num = _value.IndexOf(':')) >= 0) && (num < (_value.Length - 1))) && (_value.IndexOf(':', num + 1) >= 0))
            {
                return ("[" + _value + "]");
            }
            if (num >= 0)
            {
                var str = _value.Substring(num);
                var mapping = new IdnMapping();
                return (mapping.GetAscii(_value, 0, num) + str);
            }
            var mapping2 = new IdnMapping();
            return mapping2.GetAscii(_value);
        }

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

        public static HostString FromUriComponent(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return new HostString(uri.GetComponents(UriComponents.NormalizedHost | UriComponents.HostAndPort, UriFormat.Unescaped));
        }

        public bool Equals(HostString other)
        {
            return string.Equals(_value, other._value, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return ((obj is HostString) && Equals((HostString)obj));
        }

        public override int GetHashCode()
        {
            return _value == null ? 0 : StringComparer.OrdinalIgnoreCase.GetHashCode(_value);
        }

        public static bool operator ==(HostString left, HostString right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HostString left, HostString right)
        {
            return !left.Equals(right);
        }
    }
}
