using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Owin
{
    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static ResourceManager resourceMan;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string Exception_ConversionTakesOneParameter => ResourceManager.GetString("Exception_ConversionTakesOneParameter", resourceCulture);

        internal static string Exception_CookieLimitTooSmall => ResourceManager.GetString("Exception_CookieLimitTooSmall", resourceCulture);

        internal static string Exception_ImcompleteChunkedCookie => ResourceManager.GetString("Exception_ImcompleteChunkedCookie", resourceCulture);

        internal static string Exception_MiddlewareNotSupported => ResourceManager.GetString("Exception_MiddlewareNotSupported", resourceCulture);

        internal static string Exception_MissingOnSendingHeaders => ResourceManager.GetString("Exception_MissingOnSendingHeaders", resourceCulture);

        internal static string Exception_NoConstructorFound => ResourceManager.GetString("Exception_NoConstructorFound", resourceCulture);

        internal static string Exception_NoConversionExists => ResourceManager.GetString("Exception_NoConversionExists", resourceCulture);

        internal static string Exception_PathMustNotEndWithSlash => ResourceManager.GetString("Exception_PathMustNotEndWithSlash", resourceCulture);

        internal static string Exception_PathMustStartWithSlash => ResourceManager.GetString("Exception_PathMustStartWithSlash", resourceCulture);

        internal static string Exception_PathRequired => ResourceManager.GetString("Exception_PathRequired", resourceCulture);

        internal static string Exception_QueryStringMustStartWithDelimiter => ResourceManager.GetString("Exception_QueryStringMustStartWithDelimiter", resourceCulture);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    ResourceManager manager = new ResourceManager("Microsoft.Owin.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}
