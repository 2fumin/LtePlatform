using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Owin.BuilderProperties
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Address
    {
        public Address(IDictionary<string, object> dictionary)
        {
            Dictionary = dictionary;
        }

        public Address(string scheme, string host, string port, string path) : this(new Dictionary<string, object>())
        {
            Scheme = scheme;
            Host = host;
            Port = port;
            Path = path;
        }

        public IDictionary<string, object> Dictionary { get; }

        public string Scheme
        {
            get
            {
                return Get<string>("scheme");
            }
            set
            {
                Set("scheme", value);
            }
        }
        public string Host
        {
            get
            {
                return Get<string>("host");
            }
            set
            {
                Set("host", value);
            }
        }
        public string Port
        {
            get
            {
                return Get<string>("port");
            }
            set
            {
                Set("port", value);
            }
        }
        public string Path
        {
            get
            {
                return Get<string>("path");
            }
            set
            {
                Set("path", value);
            }
        }
        public static Address Create()
        {
            return new Address(new Dictionary<string, object>());
        }

        public bool Equals(Address other)
        {
            return Equals(Dictionary, other.Dictionary);
        }

        public override bool Equals(object obj)
        {
            return ((obj is Address) && Equals((Address)obj));
        }

        public override int GetHashCode()
        {
            return Dictionary?.GetHashCode() ?? 0;
        }

        public static bool operator ==(Address left, Address right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !left.Equals(right);
        }

        public T Get<T>(string key)
        {
            object obj2;
            if (!Dictionary.TryGetValue(key, out obj2))
            {
                return default(T);
            }
            return (T)obj2;
        }

        public Address Set(string key, object value)
        {
            Dictionary[key] = value;
            return this;
        }
    }
}
