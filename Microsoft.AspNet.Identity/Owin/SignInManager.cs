using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.Owin
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class SignInManager<TUser, TKey> : IDisposable where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
    {
        private string _authType;

        public SignInManager(UserManager<TUser, TKey> userManager, IAuthenticationManager authenticationManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }
            if (authenticationManager == null)
            {
                throw new ArgumentNullException("authenticationManager");
            }
            this.UserManager = userManager;
            this.AuthenticationManager = authenticationManager;
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
            return this.UserManager.CreateIdentityAsync(user, this.AuthenticationType);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            SignInStatus lockedOut;
            TUser user = await ((SignInManager<TUser, TKey>)this).UserManager.FindAsync(loginInfo.Login).WithCurrentCulture<TUser>();
            if (user == null)
            {
                lockedOut = SignInStatus.Failure;
            }
            else
            {
                bool introduced15 = await ((SignInManager<TUser, TKey>)this).UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture<bool>();
                if (introduced15)
                {
                    lockedOut = SignInStatus.LockedOut;
                }
                else
                {
                    lockedOut = await ((SignInManager<TUser, TKey>)this).SignInOrTwoFactor(user, isPersistent).WithCurrentCulture<SignInStatus>();
                }
            }
            return lockedOut;
        }

        public async Task<TKey> GetVerifiedUserIdAsync()
        {
            TKey local;
            AuthenticateResult asyncVariable0 = await ((SignInManager<TUser, TKey>)this).AuthenticationManager.AuthenticateAsync("TwoFactorCookie").WithCurrentCulture<AuthenticateResult>();
            if (((asyncVariable0 != null) && (asyncVariable0.Identity != null)) && !string.IsNullOrEmpty(asyncVariable0.Identity.GetUserId()))
            {
                local = ((SignInManager<TUser, TKey>)this).ConvertIdFromString(asyncVariable0.Identity.GetUserId());
            }
            else
            {
                local = default(TKey);
            }
            return local;
        }

        public async Task<bool> HasBeenVerifiedAsync()
        {
            TKey result = await ((SignInManager<TUser, TKey>)this).GetVerifiedUserIdAsync().WithCurrentCulture<TKey>();
            return (result != null);
        }

        public async virtual Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (((SignInManager<TUser, TKey>)this).UserManager != null)
            {
                TUser user = await ((SignInManager<TUser, TKey>)this).UserManager.FindByNameAsync(userName).WithCurrentCulture<TUser>();
                if (user == null)
                {
                    return SignInStatus.Failure;
                }
                bool introduced27 = await ((SignInManager<TUser, TKey>)this).UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture<bool>();
                if (introduced27)
                {
                    return SignInStatus.LockedOut;
                }
                bool introduced28 = await ((SignInManager<TUser, TKey>)this).UserManager.CheckPasswordAsync(user, password).WithCurrentCulture<bool>();
                if (introduced28)
                {
                    await ((SignInManager<TUser, TKey>)this).UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture<IdentityResult>();
                    return await ((SignInManager<TUser, TKey>)this).SignInOrTwoFactor(user, isPersistent).WithCurrentCulture<SignInStatus>();
                }
                if (shouldLockout)
                {
                    await ((SignInManager<TUser, TKey>)this).UserManager.AccessFailedAsync(user.Id).WithCurrentCulture<IdentityResult>();
                    bool introduced30 = await ((SignInManager<TUser, TKey>)this).UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture<bool>();
                    if (introduced30)
                    {
                        return SignInStatus.LockedOut;
                    }
                }
            }
            return SignInStatus.Failure;
        }

        public async virtual Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            bool flag2;
            TKey userId = await ((SignInManager<TUser, TKey>)this).GetVerifiedUserIdAsync().WithCurrentCulture<TKey>();
            if (userId == null)
            {
                flag2 = false;
            }
            else
            {
                string token = await ((SignInManager<TUser, TKey>)this).UserManager.GenerateTwoFactorTokenAsync(userId, provider).WithCurrentCulture<string>();
                await ((SignInManager<TUser, TKey>)this).UserManager.NotifyTwoFactorTokenAsync(userId, provider, token).WithCurrentCulture<IdentityResult>();
                flag2 = true;
            }
            return flag2;
        }

        public async virtual Task SignInAsync(TUser user, bool isPersistent, bool rememberBrowser)
        {
            ClaimsIdentity userIdentity = await ((SignInManager<TUser, TKey>)this).CreateUserIdentityAsync(user).WithCurrentCulture<ClaimsIdentity>();
            ((SignInManager<TUser, TKey>)this).AuthenticationManager.SignOut(new string[] { "ExternalCookie", "TwoFactorCookie" });
            if (rememberBrowser)
            {
                ClaimsIdentity identity = ((SignInManager<TUser, TKey>)this).AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(((SignInManager<TUser, TKey>)this).ConvertIdToString(user.Id));
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                };
                ((SignInManager<TUser, TKey>)this).AuthenticationManager.SignIn(properties, new ClaimsIdentity[] { userIdentity, identity });
            }
            else
            {
                AuthenticationProperties properties2 = new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                };
                ((SignInManager<TUser, TKey>)this).AuthenticationManager.SignIn(properties2, new ClaimsIdentity[] { userIdentity });
            }
        }

        private async Task<SignInStatus> SignInOrTwoFactor(TUser user, bool isPersistent)
        {
            string userId = Convert.ToString(user.Id);
            bool introduced18 = await ((SignInManager<TUser, TKey>)this).UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture<bool>();
            if (introduced18)
            {
                IList<string> introduced19 = await ((SignInManager<TUser, TKey>)this).UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture<IList<string>>();
                if (introduced19.Count > 0)
                {
                    bool introduced20 = await ((SignInManager<TUser, TKey>)this).AuthenticationManager.TwoFactorBrowserRememberedAsync(userId).WithCurrentCulture<bool>();
                    if (!introduced20)
                    {
                        ClaimsIdentity identity = new ClaimsIdentity("TwoFactorCookie");
                        identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));
                        ((SignInManager<TUser, TKey>)this).AuthenticationManager.SignIn(new ClaimsIdentity[] { identity });
                        return SignInStatus.RequiresVerification;
                    }
                }
            }
            await ((SignInManager<TUser, TKey>)this).SignInAsync(user, isPersistent, false).WithCurrentCulture();
            return SignInStatus.Success;
        }

        public async virtual Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            TKey userId = await ((SignInManager<TUser, TKey>)this).GetVerifiedUserIdAsync().WithCurrentCulture<TKey>();
            if (userId != null)
            {
                TUser user = await ((SignInManager<TUser, TKey>)this).UserManager.FindByIdAsync(userId).WithCurrentCulture<TUser>();
                if (user == null)
                {
                    return SignInStatus.Failure;
                }
                bool introduced29 = await ((SignInManager<TUser, TKey>)this).UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture<bool>();
                if (introduced29)
                {
                    return SignInStatus.LockedOut;
                }
                bool introduced30 = await ((SignInManager<TUser, TKey>)this).UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code).WithCurrentCulture<bool>();
                if (introduced30)
                {
                    await ((SignInManager<TUser, TKey>)this).UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture<IdentityResult>();
                    await ((SignInManager<TUser, TKey>)this).SignInAsync(user, isPersistent, rememberBrowser).WithCurrentCulture();
                    return SignInStatus.Success;
                }
                await ((SignInManager<TUser, TKey>)this).UserManager.AccessFailedAsync(user.Id).WithCurrentCulture<IdentityResult>();
            }
            return SignInStatus.Failure;
        }

        public IAuthenticationManager AuthenticationManager { get; set; }

        public string AuthenticationType
        {
            get
            {
                return (this._authType ?? "ApplicationCookie");
            }
            set
            {
                this._authType = value;
            }
        }

        public UserManager<TUser, TKey> UserManager { get; set; }
        
    }
}
