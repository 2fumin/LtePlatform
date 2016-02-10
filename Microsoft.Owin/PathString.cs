using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PathString : IEquatable<PathString>
    {
        private static readonly Func<string, string> EscapeDataString;
        public static readonly PathString Empty;

        public PathString(string value)
        {
            if (!string.IsNullOrEmpty(value) && (value[0] != '/'))
            {
                throw new ArgumentException(Resources.Exception_PathMustStartWithSlash, nameof(value));
            }
            Value = value;
        }

        public string Value { get; }

        public bool HasValue => !string.IsNullOrEmpty(Value);

        public override string ToString()
        {
            return ToUriComponent();
        }

        public string ToUriComponent()
        {
            if (!HasValue)
            {
                return string.Empty;
            }
            return RequiresEscaping(Value) ? string.Join("/", Value.Split('/').Select(EscapeDataString)) : Value;
        }

        private static bool RequiresEscaping(string value)
        {
            return
                value.Any(
                    ch =>
                        (((('a' > ch) || (ch > 'z')) && (('A' > ch) || (ch > 'Z'))) &&
                         ((('0' > ch) || (ch > '9')) && ((ch != '/') && (ch != '-')))) && (ch != '_'));
        }

        public static PathString FromUriComponent(string uriComponent)
        {
            return new PathString(Uri.UnescapeDataString(uriComponent));
        }

        public static PathString FromUriComponent(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return new PathString("/" + uri.GetComponents(UriComponents.Path, UriFormat.Unescaped));
        }

        public bool StartsWithSegments(PathString other)
        {
            var str = Value ?? string.Empty;
            var str2 = other.Value ?? string.Empty;
            if (!str.StartsWith(str2, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            if (str.Length != str2.Length)
            {
                return (str[str2.Length] == '/');
            }
            return true;
        }

        public bool StartsWithSegments(PathString other, out PathString remaining)
        {
            var str = Value ?? string.Empty;
            var str2 = other.Value ?? string.Empty;
            if (str.StartsWith(str2, StringComparison.OrdinalIgnoreCase) && ((str.Length == str2.Length) || (str[str2.Length] == '/')))
            {
                remaining = new PathString(str.Substring(str2.Length));
                return true;
            }
            remaining = Empty;
            return false;
        }

        public PathString Add(PathString other)
        {
            return new PathString(Value + other.Value);
        }

        public string Add(QueryString other)
        {
            return (ToUriComponent() + other.ToUriComponent());
        }

        public bool Equals(PathString other)
        {
            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(PathString other, StringComparison comparisonType)
        {
            return string.Equals(Value, other.Value, comparisonType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return ((obj is PathString) && Equals((PathString)obj, StringComparison.OrdinalIgnoreCase));
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        }

        public static bool operator ==(PathString left, PathString right)
        {
            return left.Equals(right, StringComparison.OrdinalIgnoreCase);
        }

        public static bool operator !=(PathString left, PathString right)
        {
            return !left.Equals(right, StringComparison.OrdinalIgnoreCase);
        }

        public static PathString operator +(PathString left, PathString right)
        {
            return left.Add(right);
        }

        public static string operator +(PathString left, QueryString right)
        {
            return left.Add(right);
        }

        static PathString()
        {
            EscapeDataString = Uri.EscapeDataString;
            Empty = new PathString(string.Empty);
        }
    }
}
