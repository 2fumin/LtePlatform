using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.Owin
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;

    public static class SignInManagerExtensions
    {
        public static ClaimsIdentity CreateUserIdentity<TUser, TKey>(this SignInManager<TUser, TKey> manager, TUser user) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync<ClaimsIdentity>(() => manager.CreateUserIdentityAsync(user));
        }

        public static SignInStatus ExternalSignIn<TUser, TKey>(this SignInManager<TUser, TKey> manager, ExternalLoginInfo loginInfo, bool isPersistent) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync<SignInStatus>(() => manager.ExternalSignInAsync(loginInfo, isPersistent));
        }

        public static TKey GetVerifiedUserId<TUser, TKey>(this SignInManager<TUser, TKey> manager) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync<TKey>(() => manager.GetVerifiedUserIdAsync());
        }

        public static bool HasBeenVerified<TUser, TKey>(this SignInManager<TUser, TKey> manager) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync<bool>(() => manager.HasBeenVerifiedAsync());
        }

        public static SignInStatus PasswordSignIn<TUser, TKey>(this SignInManager<TUser, TKey> manager, string userName, string password, bool isPersistent, bool shouldLockout) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync<SignInStatus>(() => manager.PasswordSignInAsync(userName, password, isPersistent, shouldLockout));
        }

        public static bool SendTwoFactorCode<TUser, TKey>(this SignInManager<TUser, TKey> manager, string provider) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync<bool>(() => manager.SendTwoFactorCodeAsync(provider));
        }

        public static void SignIn<TUser, TKey>(this SignInManager<TUser, TKey> manager, TUser user, bool isPersistent, bool rememberBrowser) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            AsyncHelper.RunSync(() => manager.SignInAsync(user, isPersistent, rememberBrowser));
        }

        public static SignInStatus TwoFactorSignIn<TUser, TKey>(this SignInManager<TUser, TKey> manager, string provider, string code, bool isPersistent, bool rememberBrowser) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            return AsyncHelper.RunSync<SignInStatus>(() => manager.TwoFactorSignInAsync(provider, code, isPersistent, rememberBrowser));
        }
    }
}
