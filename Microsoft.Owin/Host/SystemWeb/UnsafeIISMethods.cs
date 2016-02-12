using System;
using System.Reflection;

namespace Microsoft.Owin.Host.SystemWeb
{
    internal static class UnsafeIISMethods
    {
        private static readonly Lazy<UnsafeIISMethodsWrapper> IIS = new Lazy<UnsafeIISMethodsWrapper>(() => new UnsafeIISMethodsWrapper());

        public static bool CanDetectAppDomainRestart => (IIS.Value.CheckConfigChanged != null);

        public static bool RequestedAppDomainRestart
        {
            get
            {
                if (IIS.Value.CheckConfigChanged == null)
                {
                    return false;
                }
                return !IIS.Value.CheckConfigChanged();
            }
        }

        private class UnsafeIISMethodsWrapper
        {
            public UnsafeIISMethodsWrapper()
            {
                var type = Type.GetType("System.Web.Hosting.UnsafeIISMethods, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
                var method = type?.GetMethod("MgdHasConfigChanged", BindingFlags.NonPublic | BindingFlags.Static);
                if (method == null) return;
                try
                {
                    CheckConfigChanged = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), method);
                }
                catch (ArgumentException)
                {
                }
                catch (MissingMethodException)
                {
                }
                catch (MethodAccessException)
                {
                }
            }

            public Func<bool> CheckConfigChanged { get; }
        }
    }
}
