using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Microsoft.AspNet.Identity.Owin
{
    public class SignInManager<TUser, TKey> : IDisposable where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
    {
        private string _authType;

        public SignInManager(UserManager<TUser, TKey> userManager, IAuthenticationManager authenticationManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }
            if (authenticationManager == null)
            {
                throw new ArgumentNullException(nameof(authenticationManager));
            }
            UserManager = userManager;
            AuthenticationManager = authenticationManager;
        }

        public virtual TKey ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(TKey);
            }
            return (TKey)Convert.ChangeType(id, typeof(TKey), CultureInfo.InvariantCulture);
        }

        public virtual string ConvertIdToString(TKey id)
        {
            return Convert.ToString(id, CultureInfo.InvariantCulture);
        }

        public virtual Task<ClaimsIdentity> CreateUserIdentityAsync(TUser user)
        {
            return UserManager.CreateIdentityAsync(user, AuthenticationType);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            SignInStatus lockedOut;
            var user = await UserManager.FindAsync(loginInfo.Login).WithCurrentCulture();
            if (user == null)
            {
                lockedOut = SignInStatus.Failure;
            }
            else
            {
                var introduced15 = await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture();
                if (introduced15)
                {
                    lockedOut = SignInStatus.LockedOut;
                }
                else
                {
                    lockedOut = await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
                }
            }
            return lockedOut;
        }

        public async Task<TKey> GetVerifiedUserIdAsync()
        {
            var asyncVariable0 = await AuthenticationManager.AuthenticateAsync("TwoFactorCookie").WithCurrentCulture();
            var local = !string.IsNullOrEmpty(asyncVariable0?.Identity?.GetUserId())
                ? ConvertIdFromString(asyncVariable0.Identity.GetUserId())
                : default(TKey);
            return local;
        }

        public async Task<bool> HasBeenVerifiedAsync()
        {
            var result = await GetVerifiedUserIdAsync().WithCurrentCulture();
            return (result != null);
        }

        public async virtual Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (UserManager != null)
            {
                var user = await UserManager.FindByNameAsync(userName).WithCurrentCulture();
                if (user == null)
                {
                    return SignInStatus.Failure;
                }
                var introduced27 = await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture();
                if (introduced27)
                {
                    return SignInStatus.LockedOut;
                }
                var introduced28 = await UserManager.CheckPasswordAsync(user, password).WithCurrentCulture();
                if (introduced28)
                {
                    await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
                    return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
                }
                if (!shouldLockout) return SignInStatus.Failure;
                await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
                var introduced30 = await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture();
                if (introduced30)
                {
                    return SignInStatus.LockedOut;
                }
            }
            return SignInStatus.Failure;
        }

        public async virtual Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            bool flag2;
            var userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
            if (userId == null)
            {
                flag2 = false;
            }
            else
            {
                var token = await UserManager.GenerateTwoFactorTokenAsync(userId, provider).WithCurrentCulture();
                await UserManager.NotifyTwoFactorTokenAsync(userId, provider, token).WithCurrentCulture();
                flag2 = true;
            }
            return flag2;
        }

        public async virtual Task SignInAsync(TUser user, bool isPersistent, bool rememberBrowser)
        {
            var userIdentity = await CreateUserIdentityAsync(user).WithCurrentCulture();
            AuthenticationManager.SignOut("ExternalCookie", "TwoFactorCookie");
            if (rememberBrowser)
            {
                var identity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(ConvertIdToString(user.Id));
                var properties = new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                };
                AuthenticationManager.SignIn(properties, userIdentity, identity);
            }
            else
            {
                var properties2 = new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                };
                AuthenticationManager.SignIn(properties2, userIdentity);
            }
        }

        private async Task<SignInStatus> SignInOrTwoFactor(TUser user, bool isPersistent)
        {
            var userId = Convert.ToString(user.Id);
            var introduced18 = await UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture();
            if (introduced18)
            {
                var introduced19 = await UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture();
                if (introduced19.Count > 0)
                {
                    var introduced20 = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId).WithCurrentCulture();
                    if (!introduced20)
                    {
                        var identity = new ClaimsIdentity("TwoFactorCookie");
                        identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));
                        AuthenticationManager.SignIn(identity);
                        return SignInStatus.RequiresVerification;
                    }
                }
            }
            await SignInAsync(user, isPersistent, false).WithCurrentCulture();
            return SignInStatus.Success;
        }

        public async virtual Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            var userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
            if (userId == null) return SignInStatus.Failure;
            var user = await UserManager.FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            var introduced29 = await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture();
            if (introduced29)
            {
                return SignInStatus.LockedOut;
            }
            var introduced30 = await UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code).WithCurrentCulture();
            if (introduced30)
            {
                await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
                await SignInAsync(user, isPersistent, rememberBrowser).WithCurrentCulture();
                return SignInStatus.Success;
            }
            await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
            return SignInStatus.Failure;
        }

        public IAuthenticationManager AuthenticationManager { get; set; }

        public string AuthenticationType
        {
            get
            {
                return (_authType ?? "ApplicationCookie");
            }
            set
            {
                _authType = value;
            }
        }

        public UserManager<TUser, TKey> UserManager { get; set; }
        
    }
}
