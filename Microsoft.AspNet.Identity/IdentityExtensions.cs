using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.AspNet.Identity
{
    public static class IdentityExtensions
    {
        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var claim = identity.FindFirst(claimType);
            return claim?.Value;
        }

        public static string GetUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var identity2 = identity as ClaimsIdentity;
            return identity2?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        }

        public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var identity2 = identity as ClaimsIdentity;
            var str = identity2?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (str != null)
            {
                return (T)Convert.ChangeType(str, typeof(T), CultureInfo.InvariantCulture);
            }
            return default(T);
        }

        public static string GetUserName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var identity2 = identity as ClaimsIdentity;
            return identity2?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
        }
    }
}
