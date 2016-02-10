using System;
using System.Runtime.InteropServices;

namespace Microsoft.Owin
{
    [StructLayout(LayoutKind.Sequential)]
    public struct QueryString : IEquatable<QueryString>
    {
        public static readonly QueryString Empty;

        public QueryString(string value)
        {
            Value = value;
        }

        public QueryString(string name, string value)
        {
            Value = Uri.EscapeDataString(name) + '=' + Uri.EscapeDataString(value);
        }

        public string Value { get; }

        public bool HasValue => !string.IsNullOrWhiteSpace(Value);

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
            return ("?" + Value.Replace("#", "%23"));
        }

        public static QueryString FromUriComponent(string uriComponent)
        {
            if (string.IsNullOrEmpty(uriComponent))
            {
                return new QueryString(string.Empty);
            }
            if (uriComponent[0] != '?')
            {
                throw new ArgumentException(Resources.Exception_QueryStringMustStartWithDelimiter, nameof(uriComponent));
            }
            return new QueryString(uriComponent.Substring(1));
        }

        public static QueryString FromUriComponent(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return new QueryString(uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
        }

        public bool Equals(QueryString other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return ((obj is QueryString) && Equals((QueryString)obj));
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }

        public static bool operator ==(QueryString left, QueryString right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(QueryString left, QueryString right)
        {
            return !left.Equals(right);
        }

        static QueryString()
        {
            Empty = new QueryString(string.Empty);
        }
    }
}
