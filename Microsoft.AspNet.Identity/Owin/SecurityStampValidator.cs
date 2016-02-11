using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;

namespace Microsoft.AspNet.Identity.Owin
{
    public static class SecurityStampValidator
    {
        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser>(TimeSpan validateInterval, 
            Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentity) where TManager : UserManager<TUser, string> 
            where TUser : class, IUser<string>
        {
            return OnValidateIdentity(validateInterval, regenerateIdentity, id => id.GetUserId());
        }

        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser, TKey>(TimeSpan validateInterval, 
            Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentityCallback, Func<ClaimsIdentity, TKey> getUserIdCallback) 
            where TManager : UserManager<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            if (getUserIdCallback == null)
            {
                throw new ArgumentNullException(nameof(getUserIdCallback));
            }
            return async delegate (CookieValidateIdentityContext context) {
                var utcNow = DateTimeOffset.UtcNow;
                if (context.Options?.SystemClock != null)
                {
                    utcNow = context.Options.SystemClock.UtcNow;
                }
                var issuedUtc = context.Properties.IssuedUtc;
                var validate = !issuedUtc.HasValue;
                if (issuedUtc.HasValue)
                {
                    var span = utcNow.Subtract(issuedUtc.Value);
                    validate = span > validateInterval;
                }
                if (validate)
                {
                    var userManager = context.OwinContext.GetUserManager<TManager>();
                    var userId = getUserIdCallback(context.Identity);
                    if ((userManager != null) && (userId != null))
                    {
                        var user = await userManager.FindByIdAsync(userId).WithCurrentCulture();
                        var reject = true;
                        if ((user != null) && userManager.SupportsUserSecurityStamp)
                        {
                            var str2 = context.Identity.FindFirstValue(Constants.DefaultSecurityStampClaimType);
                            var introduced22 = await userManager.GetSecurityStampAsync(userId).WithCurrentCulture();
                            if (str2 == introduced22)
                            {
                                reject = false;
                                if (regenerateIdentityCallback != null)
                                {
                                    var asyncVariable0 = await (regenerateIdentityCallback(userManager, user)).WithCurrentCulture<ClaimsIdentity>();
                                    if (asyncVariable0 != null)
                                    {
                                        context.Properties.IssuedUtc = null;
                                        context.Properties.ExpiresUtc = null;
                                        context.OwinContext.Authentication.SignIn(context.Properties, asyncVariable0);
                                    }
                                }
                            }
                        }
                        if (reject)
                        {
                            context.RejectIdentity();
                            context.OwinContext.Authentication.SignOut(context.Options.AuthenticationType);
                        }
                    }
                }
            };
        }
    }
}
