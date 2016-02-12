using System;

namespace Microsoft.Owin
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class OwinStartupAttribute : Attribute
    {
        public OwinStartupAttribute(Type startupType) : this(string.Empty, startupType, string.Empty)
        {
        }

        public OwinStartupAttribute(string friendlyName, Type startupType) : this(friendlyName, startupType, string.Empty)
        {
        }

        public OwinStartupAttribute(Type startupType, string methodName) : this(string.Empty, startupType, methodName)
        {
        }

        public OwinStartupAttribute(string friendlyName, Type startupType, string methodName)
        {
            if (friendlyName == null)
            {
                throw new ArgumentNullException(nameof(friendlyName));
            }
            if (startupType == null)
            {
                throw new ArgumentNullException(nameof(startupType));
            }
            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }
            FriendlyName = friendlyName;
            StartupType = startupType;
            MethodName = methodName;
        }

        public string FriendlyName { get; private set; }

        public string MethodName { get; private set; }

        public Type StartupType { get; private set; }
    }
}
