using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin.BuilderProperties
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AppProperties
    {
        public AppProperties(IDictionary<string, object> dictionary)
        {
            Dictionary = dictionary;
        }

        public string OwinVersion
        {
            get
            {
                return Get<string>("owin.Version");
            }
            set
            {
                Set("owin.Version", value);
            }
        }

        public Func<IDictionary<string, object>, Task> DefaultApp
        {
            get
            {
                return Get<Func<IDictionary<string, object>, Task>>("builder.DefaultApp");
            }
            set
            {
                Set("builder.DefaultApp", value);
            }
        }

        public Action<Delegate> AddSignatureConversionDelegate
        {
            get
            {
                return Get<Action<Delegate>>("builder.AddSignatureConversion");
            }
            set
            {
                Set("builder.AddSignatureConversion", value);
            }
        }

        public string AppName
        {
            get
            {
                return Get<string>("host.AppName");
            }
            set
            {
                Set("host.AppName", value);
            }
        }

        public TextWriter TraceOutput
        {
            get
            {
                return Get<TextWriter>("host.TraceOutput");
            }
            set
            {
                Set("host.TraceOutput", value);
            }
        }

        public CancellationToken OnAppDisposing
        {
            get
            {
                return Get<CancellationToken>("host.OnAppDisposing");
            }
            set
            {
                Set("host.OnAppDisposing", value);
            }
        }

        public AddressCollection Addresses
        {
            get
            {
                return new AddressCollection(Get<IList<IDictionary<string, object>>>("host.Addresses"));
            }
            set
            {
                Set("host.Addresses", value.List);
            }
        }

        public Capabilities Capabilities
        {
            get
            {
                return new Capabilities(Get<IDictionary<string, object>>("server.Capabilities"));
            }
            set
            {
                Set("server.Capabilities", value.Dictionary);
            }
        }

        public IDictionary<string, object> Dictionary { get; }

        public bool Equals(AppProperties other)
        {
            return Equals(Dictionary, other.Dictionary);
        }

        public override bool Equals(object obj)
        {
            return ((obj is AppProperties) && Equals((AppProperties)obj));
        }

        public override int GetHashCode()
        {
            return Dictionary?.GetHashCode() ?? 0;
        }

        public static bool operator ==(AppProperties left, AppProperties right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AppProperties left, AppProperties right)
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

        public AppProperties Set(string key, object value)
        {
            Dictionary[key] = value;
            return this;
        }
    }
}
