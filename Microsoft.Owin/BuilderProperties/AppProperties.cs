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
                return Get<string>(OwinConstants.OwinVersion);
            }
            set
            {
                Set(OwinConstants.OwinVersion, value);
            }
        }

        public Func<IDictionary<string, object>, Task> DefaultApp
        {
            get
            {
                return Get<Func<IDictionary<string, object>, Task>>(OwinConstants.Builder.DefaultApp);
            }
            set
            {
                Set(OwinConstants.Builder.DefaultApp, value);
            }
        }

        public Action<Delegate> AddSignatureConversionDelegate
        {
            get
            {
                return Get<Action<Delegate>>(OwinConstants.Builder.AddSignatureConversion);
            }
            set
            {
                Set(OwinConstants.Builder.AddSignatureConversion, value);
            }
        }

        public string AppName
        {
            get
            {
                return Get<string>(OwinConstants.CommonKeys.AppName);
            }
            set
            {
                Set(OwinConstants.CommonKeys.AppName, value);
            }
        }

        public TextWriter TraceOutput
        {
            get
            {
                return Get<TextWriter>(OwinConstants.CommonKeys.TraceOutput);
            }
            set
            {
                Set(OwinConstants.CommonKeys.TraceOutput, value);
            }
        }

        public CancellationToken OnAppDisposing
        {
            get
            {
                return Get<CancellationToken>(OwinConstants.CommonKeys.OnAppDisposing);
            }
            set
            {
                Set(OwinConstants.CommonKeys.OnAppDisposing, value);
            }
        }

        public AddressCollection Addresses
        {
            get
            {
                return new AddressCollection(Get<IList<IDictionary<string, object>>>(OwinConstants.CommonKeys.Addresses));
            }
            set
            {
                Set(OwinConstants.CommonKeys.Addresses, value.List);
            }
        }

        public Capabilities Capabilities
        {
            get
            {
                return new Capabilities(Get<IDictionary<string, object>>(OwinConstants.CommonKeys.Capabilities));
            }
            set
            {
                Set(OwinConstants.CommonKeys.Capabilities, value.Dictionary);
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
