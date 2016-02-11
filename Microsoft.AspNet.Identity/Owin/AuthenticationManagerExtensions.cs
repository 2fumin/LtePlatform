using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Microsoft.Owin.Security
{
    public static class AuthenticationManagerExtensions
    {
        public static ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(this IAuthenticationManager manager, string userId)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var identity = new ClaimsIdentity("TwoFactorRememberBrowser");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));
            return identity;
        }

        public static IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes(this IAuthenticationManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return manager.GetAuthenticationTypes(d => (d.Properties != null) && d.Properties.ContainsKey("Caption"));
        }

        public static ClaimsIdentity GetExternalIdentity(this IAuthenticationManager manager, string externalAuthenticationType)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.GetExternalIdentityAsync(externalAuthenticationType));
        }

        public async static Task<ClaimsIdentity> GetExternalIdentityAsync(this IAuthenticationManager manager, string externalAuthenticationType)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var asyncVariable0 = await manager.AuthenticateAsync(externalAuthenticationType).WithCurrentCulture();
            var identity = asyncVariable0?.Identity?.FindFirst(
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") != null
                ? asyncVariable0.Identity
                : null;
            return identity;
        }

        private static ExternalLoginInfo GetExternalLoginInfo(AuthenticateResult result)
        {
            var claim = result?.Identity?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim == null)
            {
                return null;
            }
            var name = result.Identity.Name;
            name = name?.Replace(" ", "");
            var str2 = result.Identity.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            return new ExternalLoginInfo
            {
                ExternalIdentity = result.Identity,
                Login = new UserLoginInfo(claim.Issuer, claim.Value),
                DefaultUserName = name,
                Email = str2
            };
        }

        public static ExternalLoginInfo GetExternalLoginInfo(this IAuthenticationManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(manager.GetExternalLoginInfoAsync);
        }
        
        public static ExternalLoginInfo GetExternalLoginInfo(this IAuthenticationManager manager, string xsrfKey, string expectedValue)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.GetExternalLoginInfoAsync(xsrfKey, expectedValue));
        }

        public async static Task<ExternalLoginInfo> GetExternalLoginInfoAsync(this IAuthenticationManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var result = await manager.AuthenticateAsync("ExternalCookie").WithCurrentCulture();
            return GetExternalLoginInfo(result);
        }

        public async static Task<ExternalLoginInfo> GetExternalLoginInfoAsync(this IAuthenticationManager manager, string xsrfKey, string expectedValue)
        {
            ExternalLoginInfo externalLoginInfo;
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var result = await manager.AuthenticateAsync("ExternalCookie").WithCurrentCulture();
            if (result?.Properties?.Dictionary != null && result.Properties.Dictionary.ContainsKey(xsrfKey) 
                && (result.Properties.Dictionary[xsrfKey] == expectedValue))
            {
                externalLoginInfo = GetExternalLoginInfo(result);
            }
            else
            {
                externalLoginInfo = null;
            }
            return externalLoginInfo;
        }

        public static bool TwoFactorBrowserRemembered(this IAuthenticationManager manager, string userId)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.TwoFactorBrowserRememberedAsync(userId));
        }

        public async static Task<bool> TwoFactorBrowserRememberedAsync(this IAuthenticationManager manager, string userId)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var asyncVariable0 = await manager.AuthenticateAsync("TwoFactorRememberBrowser").WithCurrentCulture();
            return (asyncVariable0?.Identity != null && (asyncVariable0.Identity.GetUserId() == userId));
        }
        
    }
}
