using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Owin.BuilderProperties
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Capabilities
    {
        public Capabilities(IDictionary<string, object> dictionary)
        {
            Dictionary = dictionary;
        }

        public IDictionary<string, object> Dictionary { get; }

        public string SendFileVersion
        {
            get
            {
                return Get<string>(OwinConstants.SendFiles.Version);
            }
            set
            {
                Set(OwinConstants.SendFiles.Version, value);
            }
        }

        public string WebSocketVersion
        {
            get
            {
                return Get<string>(OwinConstants.WebSocket.Version);
            }
            set
            {
                Set(OwinConstants.WebSocket.Version, value);
            }
        }
        public static Capabilities Create()
        {
            return new Capabilities(new Dictionary<string, object>());
        }

        public bool Equals(Capabilities other)
        {
            return Equals(Dictionary, other.Dictionary);
        }

        public override bool Equals(object obj)
        {
            return ((obj is Capabilities) && Equals((Capabilities)obj));
        }

        public override int GetHashCode()
        {
            return Dictionary?.GetHashCode() ?? 0;
        }

        public static bool operator ==(Capabilities left, Capabilities right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Capabilities left, Capabilities right)
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

        public Capabilities Set(string key, object value)
        {
            Dictionary[key] = value;
            return this;
        }
    }
}
