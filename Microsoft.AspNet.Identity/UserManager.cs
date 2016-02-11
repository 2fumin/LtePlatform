using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Properties;

namespace Microsoft.AspNet.Identity
{
    public class UserManager<TUser> : UserManager<TUser, string> where TUser : class, IUser<string>
    {
        public UserManager(IUserStore<TUser> store) : base(store)
        {
        }
    }

    public class UserManager<TUser, TKey> : IDisposable where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
    {
        private IClaimsIdentityFactory<TUser, TKey> _claimsFactory;
        private TimeSpan _defaultLockout;
        private bool _disposed;
        private readonly Dictionary<string, IUserTokenProvider<TUser, TKey>> _factors;
        private IPasswordHasher _passwordHasher;
        private IIdentityValidator<string> _passwordValidator;
        private IIdentityValidator<TUser> _userValidator;

        public UserManager(IUserStore<TUser, TKey> store)
        {
            _factors = new Dictionary<string, IUserTokenProvider<TUser, TKey>>();
            _defaultLockout = TimeSpan.Zero;
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            Store = store;
            UserValidator = new UserValidator<TUser, TKey>((UserManager<TUser, TKey>)this);
            PasswordValidator = new MinimumLengthValidator(6);
            PasswordHasher = new Identity.PasswordHasher();
            ClaimsIdentityFactory = new ClaimsIdentityFactory<TUser, TKey>();
        }

        public async virtual Task<IdentityResult> AccessFailedAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLockoutStore<TUser, TKey> userLockoutStore = ((UserManager<TUser, TKey>)this).GetUserLockoutStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            int count = await userLockoutStore.IncrementAccessFailedCountAsync(user).WithCurrentCulture<int>();
            if (count >= ((UserManager<TUser, TKey>)this).MaxFailedAccessAttemptsBeforeLockout)
            {
                await userLockoutStore.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.Add(((UserManager<TUser, TKey>)this).DefaultAccountLockoutTimeSpan)).WithCurrentCulture();
                await userLockoutStore.ResetAccessFailedCountAsync(user).WithCurrentCulture();
            }
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
        }

        public async virtual Task<IdentityResult> AddClaimAsync(TKey userId, Claim claim)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserClaimStore<TUser, TKey> claimStore = ((UserManager<TUser, TKey>)this).GetClaimStore();
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            await claimStore.AddClaimAsync(user, claim).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
        }

        public async virtual Task<IdentityResult> AddLoginAsync(TKey userId, UserLoginInfo login)
        {
            IdentityResult result;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLoginStore<TUser, TKey> loginStore = ((UserManager<TUser, TKey>)this).GetLoginStore();
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            TUser existingUser = await ((UserManager<TUser, TKey>)this).FindAsync(login).WithCurrentCulture<TUser>();
            if (existingUser != null)
            {
                result = IdentityResult.Failed(new string[] { Resources.ExternalLoginExists });
            }
            else
            {
                await loginStore.AddLoginAsync(user, login).WithCurrentCulture();
                result = await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
            }
            return result;
        }

        public async virtual Task<IdentityResult> AddPasswordAsync(TKey userId, string password)
        {
            IdentityResult result;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserPasswordStore<TUser, TKey> passwordStore = ((UserManager<TUser, TKey>)this).GetPasswordStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            string hash = await passwordStore.GetPasswordHashAsync(user).WithCurrentCulture<string>();
            if (hash != null)
            {
                result = new IdentityResult(new string[] { Resources.UserAlreadyHasPassword });
            }
            else
            {
                IdentityResult asyncVariable0 = await ((UserManager<TUser, TKey>)this).UpdatePassword(passwordStore, user, password).WithCurrentCulture<IdentityResult>();
                if (!asyncVariable0.Succeeded)
                {
                    result = asyncVariable0;
                }
                else
                {
                    result = await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
                }
            }
            return result;
        }

        public async virtual Task<IdentityResult> AddToRoleAsync(TKey userId, string role)
        {
            IdentityResult result;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserRoleStore<TUser, TKey> userRoleStore = ((UserManager<TUser, TKey>)this).GetUserRoleStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            IList<string> userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture<IList<string>>();
            if (userRoles.Contains(role))
            {
                result = new IdentityResult(new string[] { Resources.UserAlreadyInRole });
            }
            else
            {
                await userRoleStore.AddToRoleAsync(user, role).WithCurrentCulture();
                result = await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
            }
            return result;
        }

        [AsyncStateMachine(typeof(< AddToRolesAsync > d__8c)), DebuggerStepThrough]
        public virtual Task<IdentityResult> AddToRolesAsync(TKey userId, params string[] roles)
        {
            < AddToRolesAsync > d__8c < TUser, TKey > d__c;
            d__c.<> 4__this = (UserManager<TUser, TKey>)this;
            d__c.userId = userId;
            d__c.roles = roles;
            d__c.<> t__builder = AsyncTaskMethodBuilder<IdentityResult>.Create();
            d__c.<> 1__state = -1;
            d__c.<> t__builder.Start << AddToRolesAsync > d__8c < TUser, TKey >> (ref d__c);
            return d__c.<> t__builder.Task;
        }

        public async virtual Task<IdentityResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserPasswordStore<TUser, TKey> store = ((UserManager<TUser, TKey>)this).GetPasswordStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            bool introduced21 = await ((UserManager<TUser, TKey>)this).VerifyPasswordAsync(store, user, currentPassword).WithCurrentCulture<bool>();
            if (introduced21)
            {
                IdentityResult result;
                IdentityResult asyncVariable0 = await ((UserManager<TUser, TKey>)this).UpdatePassword(store, user, newPassword).WithCurrentCulture<IdentityResult>();
                if (!asyncVariable0.Succeeded)
                {
                    result = asyncVariable0;
                }
                else
                {
                    result = await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
                }
                return result;
            }
            return IdentityResult.Failed(new string[] { Resources.PasswordMismatch });
        }

        public async virtual Task<IdentityResult> ChangePhoneNumberAsync(TKey userId, string phoneNumber, string token)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserPhoneNumberStore<TUser, TKey> phoneNumberStore = ((UserManager<TUser, TKey>)this).GetPhoneNumberStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            bool introduced26 = await ((UserManager<TUser, TKey>)this).VerifyChangePhoneNumberTokenAsync(userId, token, phoneNumber).WithCurrentCulture<bool>();
            if (introduced26)
            {
                await phoneNumberStore.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
                await phoneNumberStore.SetPhoneNumberConfirmedAsync(user, true).WithCurrentCulture();
                await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
                return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
            }
            return IdentityResult.Failed(new string[] { Resources.InvalidToken });
        }

        public async virtual Task<bool> CheckPasswordAsync(TUser user, string password)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserPasswordStore<TUser, TKey> store = ((UserManager<TUser, TKey>)this).GetPasswordStore();
            if (user == null)
            {
                return false;
                this.<> 1__state = -1;
            }
            return await ((UserManager<TUser, TKey>)this).VerifyPasswordAsync(store, user, password).WithCurrentCulture<bool>();
        }

        public async virtual Task<IdentityResult> ConfirmEmailAsync(TKey userId, string token)
        {
            IdentityResult result;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserEmailStore<TUser, TKey> emailStore = ((UserManager<TUser, TKey>)this).GetEmailStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            bool introduced20 = await ((UserManager<TUser, TKey>)this).VerifyUserTokenAsync(userId, "Confirmation", token).WithCurrentCulture<bool>();
            if (!introduced20)
            {
                result = IdentityResult.Failed(new string[] { Resources.InvalidToken });
            }
            else
            {
                await emailStore.SetEmailConfirmedAsync(user, true).WithCurrentCulture();
                result = await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
            }
            return result;
        }

        public async virtual Task<IdentityResult> CreateAsync(TUser user)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
            IdentityResult asyncVariable0 = await ((UserManager<TUser, TKey>)this).UserValidator.ValidateAsync(user).WithCurrentCulture<IdentityResult>();
            if (!asyncVariable0.Succeeded)
            {
                return asyncVariable0;
            }
            if (((UserManager<TUser, TKey>)this).UserLockoutEnabledByDefault && ((UserManager<TUser, TKey>)this).SupportsUserLockout)
            {
                await ((UserManager<TUser, TKey>)this).GetUserLockoutStore().SetLockoutEnabledAsync(user, true).WithCurrentCulture();
            }
            await ((UserManager<TUser, TKey>)this).Store.CreateAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        public async virtual Task<IdentityResult> CreateAsync(TUser user, string password)
        {
            IdentityResult result;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserPasswordStore<TUser, TKey> passwordStore = ((UserManager<TUser, TKey>)this).GetPasswordStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            IdentityResult asyncVariable0 = await ((UserManager<TUser, TKey>)this).UpdatePassword(passwordStore, user, password).WithCurrentCulture<IdentityResult>();
            if (!asyncVariable0.Succeeded)
            {
                result = asyncVariable0;
            }
            else
            {
                result = await ((UserManager<TUser, TKey>)this).CreateAsync(user).WithCurrentCulture<IdentityResult>();
            }
            return result;
        }

        public virtual Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return ClaimsIdentityFactory.CreateAsync((UserManager<TUser, TKey>)this, user, authenticationType);
        }

        internal async Task<SecurityToken> CreateSecurityTokenAsync(TKey userId)
        {
            Encoding encoding2;
            string s = await ((UserManager<TUser, TKey>)this).GetSecurityStampAsync(userId).WithCurrentCulture<string>();
            return new SecurityToken(encoding2.GetBytes(s));
        }

        public async virtual Task<IdentityResult> DeleteAsync(TUser user)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            await ((UserManager<TUser, TKey>)this).Store.DeleteAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                Store.Dispose();
                _disposed = true;
            }
        }

        public virtual Task<TUser> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            return GetLoginStore().FindAsync(login);
        }

        public async virtual Task<TUser> FindAsync(string userName, string password)
        {
            TUser local;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByNameAsync(userName).WithCurrentCulture<TUser>();
            if (user == null)
            {
                local = default(TUser);
            }
            else
            {
                bool introduced14 = await ((UserManager<TUser, TKey>)this).CheckPasswordAsync(user, password).WithCurrentCulture<bool>();
                local = introduced14 ? user : default(TUser);
            }
            return local;
        }

        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            IUserEmailStore<TUser, TKey> emailStore = GetEmailStore();
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            return emailStore.FindByEmailAsync(email);
        }

        public virtual Task<TUser> FindByIdAsync(TKey userId)
        {
            ThrowIfDisposed();
            return Store.FindByIdAsync(userId);
        }

        public virtual Task<TUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }
            return Store.FindByNameAsync(userName);
        }

        public async virtual Task<string> GenerateChangePhoneNumberTokenAsync(TKey userId, string phoneNumber)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            SecurityToken securityToken = await ((UserManager<TUser, TKey>)this).CreateSecurityTokenAsync(userId).WithCurrentCulture<SecurityToken>();
            int num2 = Rfc6238AuthenticationService.GenerateCode(securityToken, phoneNumber);
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "D6";
            return num2.ToString(format, provider);
        }

        public virtual Task<string> GenerateEmailConfirmationTokenAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GenerateUserTokenAsync("Confirmation", userId);
        }

        public virtual Task<string> GeneratePasswordResetTokenAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GenerateUserTokenAsync("ResetPassword", userId);
        }

        public async virtual Task<string> GenerateTwoFactorTokenAsync(TKey userId, string twoFactorProvider)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            if (!((UserManager<TUser, TKey>)this)._factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, new object[] { twoFactorProvider }));
            }
            return await ((UserManager<TUser, TKey>)this)._factors[twoFactorProvider].GenerateAsync(twoFactorProvider, (UserManager<TUser, TKey>)this, user).WithCurrentCulture<string>();
        }

        public async virtual Task<string> GenerateUserTokenAsync(string purpose, TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            if (((UserManager<TUser, TKey>)this).UserTokenProvider == null)
            {
                throw new NotSupportedException(Resources.NoTokenProvider);
            }
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await ((UserManager<TUser, TKey>)this).UserTokenProvider.GenerateAsync(purpose, (UserManager<TUser, TKey>)this, user).WithCurrentCulture<string>();
        }

        public async virtual Task<int> GetAccessFailedCountAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLockoutStore<TUser, TKey> userLockoutStore = ((UserManager<TUser, TKey>)this).GetUserLockoutStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await userLockoutStore.GetAccessFailedCountAsync(user).WithCurrentCulture<int>();
        }

        public async virtual Task<IList<Claim>> GetClaimsAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserClaimStore<TUser, TKey> claimStore = ((UserManager<TUser, TKey>)this).GetClaimStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await claimStore.GetClaimsAsync(user).WithCurrentCulture<IList<Claim>>();
        }

        private IUserClaimStore<TUser, TKey> GetClaimStore()
        {
            IUserClaimStore<TUser, TKey> store = Store as IUserClaimStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserClaimStore);
            }
            return store;
        }

        public async virtual Task<string> GetEmailAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserEmailStore<TUser, TKey> emailStore = ((UserManager<TUser, TKey>)this).GetEmailStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await emailStore.GetEmailAsync(user).WithCurrentCulture<string>();
        }

        internal IUserEmailStore<TUser, TKey> GetEmailStore()
        {
            IUserEmailStore<TUser, TKey> store = Store as IUserEmailStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserEmailStore);
            }
            return store;
        }

        public async virtual Task<bool> GetLockoutEnabledAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLockoutStore<TUser, TKey> userLockoutStore = ((UserManager<TUser, TKey>)this).GetUserLockoutStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await userLockoutStore.GetLockoutEnabledAsync(user).WithCurrentCulture<bool>();
        }

        public async virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLockoutStore<TUser, TKey> userLockoutStore = ((UserManager<TUser, TKey>)this).GetUserLockoutStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await userLockoutStore.GetLockoutEndDateAsync(user).WithCurrentCulture<DateTimeOffset>();
        }

        public async virtual Task<IList<UserLoginInfo>> GetLoginsAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLoginStore<TUser, TKey> loginStore = ((UserManager<TUser, TKey>)this).GetLoginStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await loginStore.GetLoginsAsync(user).WithCurrentCulture<IList<UserLoginInfo>>();
        }

        private IUserLoginStore<TUser, TKey> GetLoginStore()
        {
            IUserLoginStore<TUser, TKey> store = Store as IUserLoginStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserLoginStore);
            }
            return store;
        }

        private IUserPasswordStore<TUser, TKey> GetPasswordStore()
        {
            IUserPasswordStore<TUser, TKey> store = Store as IUserPasswordStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserPasswordStore);
            }
            return store;
        }

        public async virtual Task<string> GetPhoneNumberAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserPhoneNumberStore<TUser, TKey> phoneNumberStore = ((UserManager<TUser, TKey>)this).GetPhoneNumberStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await phoneNumberStore.GetPhoneNumberAsync(user).WithCurrentCulture<string>();
        }

        internal IUserPhoneNumberStore<TUser, TKey> GetPhoneNumberStore()
        {
            IUserPhoneNumberStore<TUser, TKey> store = Store as IUserPhoneNumberStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserPhoneNumberStore);
            }
            return store;
        }

        public async virtual Task<IList<string>> GetRolesAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserRoleStore<TUser, TKey> userRoleStore = ((UserManager<TUser, TKey>)this).GetUserRoleStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await userRoleStore.GetRolesAsync(user).WithCurrentCulture<IList<string>>();
        }

        public async virtual Task<string> GetSecurityStampAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserSecurityStampStore<TUser, TKey> securityStore = ((UserManager<TUser, TKey>)this).GetSecurityStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await securityStore.GetSecurityStampAsync(user).WithCurrentCulture<string>();
        }

        private IUserSecurityStampStore<TUser, TKey> GetSecurityStore()
        {
            IUserSecurityStampStore<TUser, TKey> store = Store as IUserSecurityStampStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserSecurityStampStore);
            }
            return store;
        }

        public async virtual Task<bool> GetTwoFactorEnabledAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserTwoFactorStore<TUser, TKey> userTwoFactorStore = ((UserManager<TUser, TKey>)this).GetUserTwoFactorStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await userTwoFactorStore.GetTwoFactorEnabledAsync(user).WithCurrentCulture<bool>();
        }

        internal IUserLockoutStore<TUser, TKey> GetUserLockoutStore()
        {
            IUserLockoutStore<TUser, TKey> store = Store as IUserLockoutStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserLockoutStore);
            }
            return store;
        }

        private IUserRoleStore<TUser, TKey> GetUserRoleStore()
        {
            IUserRoleStore<TUser, TKey> store = Store as IUserRoleStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserRoleStore);
            }
            return store;
        }

        internal IUserTwoFactorStore<TUser, TKey> GetUserTwoFactorStore()
        {
            IUserTwoFactorStore<TUser, TKey> store = Store as IUserTwoFactorStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserTwoFactorStore);
            }
            return store;
        }

        [DebuggerStepThrough, AsyncStateMachine(typeof(< GetValidTwoFactorProvidersAsync > d__103))]
        public virtual Task<IList<string>> GetValidTwoFactorProvidersAsync(TKey userId)
        {
            < GetValidTwoFactorProvidersAsync > d__103 < TUser, TKey > d__;
            d__.<> 4__this = (UserManager<TUser, TKey>)this;
            d__.userId = userId;
            d__.<> t__builder = AsyncTaskMethodBuilder<IList<string>>.Create();
            d__.<> 1__state = -1;
            d__.<> t__builder.Start << GetValidTwoFactorProvidersAsync > d__103 < TUser, TKey >> (ref d__);
            return d__.<> t__builder.Task;
        }

        public async virtual Task<bool> HasPasswordAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserPasswordStore<TUser, TKey> passwordStore = ((UserManager<TUser, TKey>)this).GetPasswordStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await passwordStore.HasPasswordAsync(user).WithCurrentCulture<bool>();
        }

        public async virtual Task<bool> IsEmailConfirmedAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserEmailStore<TUser, TKey> emailStore = ((UserManager<TUser, TKey>)this).GetEmailStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await emailStore.GetEmailConfirmedAsync(user).WithCurrentCulture<bool>();
        }

        public async virtual Task<bool> IsInRoleAsync(TKey userId, string role)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserRoleStore<TUser, TKey> userRoleStore = ((UserManager<TUser, TKey>)this).GetUserRoleStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture<bool>();
        }

        public async virtual Task<bool> IsLockedOutAsync(TKey userId)
        {
            bool flag2;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLockoutStore<TUser, TKey> userLockoutStore = ((UserManager<TUser, TKey>)this).GetUserLockoutStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            bool introduced17 = await userLockoutStore.GetLockoutEnabledAsync(user).WithCurrentCulture<bool>();
            if (!introduced17)
            {
                flag2 = false;
            }
            else
            {
                DateTimeOffset lockoutTime = await userLockoutStore.GetLockoutEndDateAsync(user).WithCurrentCulture<DateTimeOffset>();
                flag2 = lockoutTime >= DateTimeOffset.UtcNow;
            }
            return flag2;
        }

        public async virtual Task<bool> IsPhoneNumberConfirmedAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserPhoneNumberStore<TUser, TKey> phoneNumberStore = ((UserManager<TUser, TKey>)this).GetPhoneNumberStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await phoneNumberStore.GetPhoneNumberConfirmedAsync(user).WithCurrentCulture<bool>();
        }

        private static string NewSecurityStamp()
        {
            return Guid.NewGuid().ToString();
        }

        public async virtual Task<IdentityResult> NotifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            if (!((UserManager<TUser, TKey>)this)._factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, new object[] { twoFactorProvider }));
            }
            await ((UserManager<TUser, TKey>)this)._factors[twoFactorProvider].NotifyAsync(token, (UserManager<TUser, TKey>)this, user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        public virtual void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<TUser, TKey> provider)
        {
            ThrowIfDisposed();
            if (twoFactorProvider == null)
            {
                throw new ArgumentNullException(nameof(twoFactorProvider));
            }
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            TwoFactorProviders[twoFactorProvider] = provider;
        }

        public async virtual Task<IdentityResult> RemoveClaimAsync(TKey userId, Claim claim)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserClaimStore<TUser, TKey> claimStore = ((UserManager<TUser, TKey>)this).GetClaimStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            await claimStore.RemoveClaimAsync(user, claim).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
        }

        public async virtual Task<IdentityResult> RemoveFromRoleAsync(TKey userId, string role)
        {
            IdentityResult result;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserRoleStore<TUser, TKey> userRoleStore = ((UserManager<TUser, TKey>)this).GetUserRoleStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            bool introduced20 = await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture<bool>();
            if (!introduced20)
            {
                result = new IdentityResult(new string[] { Resources.UserNotInRole });
            }
            else
            {
                await userRoleStore.RemoveFromRoleAsync(user, role).WithCurrentCulture();
                result = await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
            }
            return result;
        }
        
        public async virtual Task<IdentityResult> RemoveLoginAsync(TKey userId, UserLoginInfo login)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLoginStore<TUser, TKey> loginStore = ((UserManager<TUser, TKey>)this).GetLoginStore();
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            await loginStore.RemoveLoginAsync(user, login).WithCurrentCulture();
            await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
        }

        public async virtual Task<IdentityResult> RemovePasswordAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserPasswordStore<TUser, TKey> passwordStore = ((UserManager<TUser, TKey>)this).GetPasswordStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            await passwordStore.SetPasswordHashAsync(user, null).WithCurrentCulture();
            await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
        }

        public async virtual Task<IdentityResult> ResetAccessFailedCountAsync(TKey userId)
        {
            IdentityResult success;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLockoutStore<TUser, TKey> userLockoutStore = ((UserManager<TUser, TKey>)this).GetUserLockoutStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            int introduced19 = await ((UserManager<TUser, TKey>)this).GetAccessFailedCountAsync(user.Id).WithCurrentCulture<int>();
            if (introduced19 == 0)
            {
                success = IdentityResult.Success;
            }
            else
            {
                await userLockoutStore.ResetAccessFailedCountAsync(user).WithCurrentCulture();
                success = await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
            }
            return success;
        }

        public async virtual Task<IdentityResult> ResetPasswordAsync(TKey userId, string token, string newPassword)
        {
            IdentityResult result;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            bool introduced21 = await ((UserManager<TUser, TKey>)this).VerifyUserTokenAsync(userId, "ResetPassword", token).WithCurrentCulture<bool>();
            if (!introduced21)
            {
                result = IdentityResult.Failed(new string[] { Resources.InvalidToken });
            }
            else
            {
                IUserPasswordStore<TUser, TKey> passwordStore = ((UserManager<TUser, TKey>)this).GetPasswordStore();
                IdentityResult asyncVariable0 = await ((UserManager<TUser, TKey>)this).UpdatePassword(passwordStore, user, newPassword).WithCurrentCulture<IdentityResult>();
                if (!asyncVariable0.Succeeded)
                {
                    result = asyncVariable0;
                }
                else
                {
                    result = await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
                }
            }
            return result;
        }

        public async virtual Task SendEmailAsync(TKey userId, string subject, string body)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            if (((UserManager<TUser, TKey>)this).EmailService != null)
            {
                IdentityMessage asyncVariable0 = new IdentityMessage();
                IdentityMessage message = asyncVariable0;
                string introduced12 = await ((UserManager<TUser, TKey>)this).GetEmailAsync(userId).WithCurrentCulture<string>();
                message2.Destination = introduced12;
                asyncVariable0.Subject = subject;
                asyncVariable0.Body = body;
                IdentityMessage msg = asyncVariable0;
                await ((UserManager<TUser, TKey>)this).EmailService.SendAsync(msg).WithCurrentCulture();
            }
        }

        public async virtual Task SendSmsAsync(TKey userId, string message)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            if (((UserManager<TUser, TKey>)this).SmsService != null)
            {
                IdentityMessage asyncVariable0 = new IdentityMessage();
                IdentityMessage message = asyncVariable0;
                string introduced12 = await ((UserManager<TUser, TKey>)this).GetPhoneNumberAsync(userId).WithCurrentCulture<string>();
                message2.Destination = introduced12;
                asyncVariable0.Body = message;
                IdentityMessage msg = asyncVariable0;
                await ((UserManager<TUser, TKey>)this).SmsService.SendAsync(msg).WithCurrentCulture();
            }
        }

        public async virtual Task<IdentityResult> SetEmailAsync(TKey userId, string email)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserEmailStore<TUser, TKey> emailStore = ((UserManager<TUser, TKey>)this).GetEmailStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            await emailStore.SetEmailAsync(user, email).WithCurrentCulture();
            await emailStore.SetEmailConfirmedAsync(user, false).WithCurrentCulture();
            await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
        }

        public async virtual Task<IdentityResult> SetLockoutEnabledAsync(TKey userId, bool enabled)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLockoutStore<TUser, TKey> userLockoutStore = ((UserManager<TUser, TKey>)this).GetUserLockoutStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            await userLockoutStore.SetLockoutEnabledAsync(user, enabled).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
        }

        public async virtual Task<IdentityResult> SetLockoutEndDateAsync(TKey userId, DateTimeOffset lockoutEnd)
        {
            IdentityResult result;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserLockoutStore<TUser, TKey> userLockoutStore = ((UserManager<TUser, TKey>)this).GetUserLockoutStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            bool introduced20 = await userLockoutStore.GetLockoutEnabledAsync(user).WithCurrentCulture<bool>();
            if (!introduced20)
            {
                result = IdentityResult.Failed(new string[] { Resources.LockoutNotEnabled });
            }
            else
            {
                await userLockoutStore.SetLockoutEndDateAsync(user, lockoutEnd).WithCurrentCulture();
                result = await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
            }
            return result;
        }

        public async virtual Task<IdentityResult> SetPhoneNumberAsync(TKey userId, string phoneNumber)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserPhoneNumberStore<TUser, TKey> phoneNumberStore = ((UserManager<TUser, TKey>)this).GetPhoneNumberStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            await phoneNumberStore.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
            await phoneNumberStore.SetPhoneNumberConfirmedAsync(user, false).WithCurrentCulture();
            await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
        }

        public async virtual Task<IdentityResult> SetTwoFactorEnabledAsync(TKey userId, bool enabled)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserTwoFactorStore<TUser, TKey> userTwoFactorStore = ((UserManager<TUser, TKey>)this).GetUserTwoFactorStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            await userTwoFactorStore.SetTwoFactorEnabledAsync(user, enabled).WithCurrentCulture();
            await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public async virtual Task<IdentityResult> UpdateAsync(TUser user)
        {
            IdentityResult success;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            IdentityResult asyncVariable0 = await ((UserManager<TUser, TKey>)this).UserValidator.ValidateAsync(user).WithCurrentCulture<IdentityResult>();
            if (!asyncVariable0.Succeeded)
            {
                success = asyncVariable0;
            }
            else
            {
                await ((UserManager<TUser, TKey>)this).Store.UpdateAsync(user).WithCurrentCulture();
                success = IdentityResult.Success;
            }
            return success;
        }

        protected async virtual Task<IdentityResult> UpdatePassword(IUserPasswordStore<TUser, TKey> passwordStore, TUser user, string newPassword)
        {
            IdentityResult success;
            IdentityResult asyncVariable0 = await ((UserManager<TUser, TKey>)this).PasswordValidator.ValidateAsync(newPassword).WithCurrentCulture<IdentityResult>();
            if (!asyncVariable0.Succeeded)
            {
                success = asyncVariable0;
            }
            else
            {
                await passwordStore.SetPasswordHashAsync(user, ((UserManager<TUser, TKey>)this).PasswordHasher.HashPassword(newPassword)).WithCurrentCulture();
                await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
                success = IdentityResult.Success;
            }
            return success;
        }

        public async virtual Task<IdentityResult> UpdateSecurityStampAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            IUserSecurityStampStore<TUser, TKey> securityStore = ((UserManager<TUser, TKey>)this).GetSecurityStore();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            await securityStore.SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture<IdentityResult>();
        }

        internal async Task UpdateSecurityStampInternal(TUser user)
        {
            if (!((UserManager<TUser, TKey>)this).SupportsUserSecurityStamp)
            {
                return;
                this.<> 1__state = -1;
            }
            await ((UserManager<TUser, TKey>)this).GetSecurityStore().SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
        }

        public async virtual Task<bool> VerifyChangePhoneNumberTokenAsync(TKey userId, string token, string phoneNumber)
        {
            bool flag2;
            int code;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            SecurityToken securityToken = await ((UserManager<TUser, TKey>)this).CreateSecurityTokenAsync(userId).WithCurrentCulture<SecurityToken>();
            if ((securityToken != null) && int.TryParse(token, out code))
            {
                flag2 = Rfc6238AuthenticationService.ValidateCode(securityToken, code, phoneNumber);
            }
            else
            {
                flag2 = false;
            }
            return flag2;
        }

        protected async virtual Task<bool> VerifyPasswordAsync(IUserPasswordStore<TUser, TKey> store, TUser user, string password)
        {
            string hashedPassword = await store.GetPasswordHashAsync(user).WithCurrentCulture<string>();
            return (((UserManager<TUser, TKey>)this).PasswordHasher.VerifyHashedPassword(hashedPassword, password) != PasswordVerificationResult.Failed);
        }

        public async virtual Task<bool> VerifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            if (!((UserManager<TUser, TKey>)this)._factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, new object[] { twoFactorProvider }));
            }
            IUserTokenProvider<TUser, TKey> provider = ((UserManager<TUser, TKey>)this)._factors[twoFactorProvider];
            return await provider.ValidateAsync(twoFactorProvider, token, (UserManager<TUser, TKey>)this, user).WithCurrentCulture<bool>();
        }

        public async virtual Task<bool> VerifyUserTokenAsync(TKey userId, string purpose, string token)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            if (((UserManager<TUser, TKey>)this).UserTokenProvider == null)
            {
                throw new NotSupportedException(Resources.NoTokenProvider);
            }
            TUser user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture<TUser>();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { userId }));
            }
            return await ((UserManager<TUser, TKey>)this).UserTokenProvider.ValidateAsync(purpose, token, (UserManager<TUser, TKey>)this, user).WithCurrentCulture<bool>();
        }

        public IClaimsIdentityFactory<TUser, TKey> ClaimsIdentityFactory
        {
            get
            {
                ThrowIfDisposed();
                return _claimsFactory;
            }
            set
            {
                ThrowIfDisposed();
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _claimsFactory = value;
            }
        }

        public TimeSpan DefaultAccountLockoutTimeSpan
        {
            get
            {
                return _defaultLockout;
            }
            set
            {
                _defaultLockout = value;
            }
        }

        public IIdentityMessageService EmailService { get; set; }

        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }

        public IPasswordHasher PasswordHasher
        {
            get
            {
                ThrowIfDisposed();
                return _passwordHasher;
            }
            set
            {
                ThrowIfDisposed();
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _passwordHasher = value;
            }
        }

        public IIdentityValidator<string> PasswordValidator
        {
            get
            {
                ThrowIfDisposed();
                return _passwordValidator;
            }
            set
            {
                ThrowIfDisposed();
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _passwordValidator = value;
            }
        }

        public IIdentityMessageService SmsService { get; set; }

        protected internal IUserStore<TUser, TKey> Store { get; set; }

        public virtual bool SupportsQueryableUsers
        {
            get
            {
                ThrowIfDisposed();
                return (Store is IQueryableUserStore<TUser, TKey>);
            }
        }

        public virtual bool SupportsUserClaim
        {
            get
            {
                ThrowIfDisposed();
                return (Store is IUserClaimStore<TUser, TKey>);
            }
        }

        public virtual bool SupportsUserEmail
        {
            get
            {
                ThrowIfDisposed();
                return (Store is IUserEmailStore<TUser, TKey>);
            }
        }

        public virtual bool SupportsUserLockout
        {
            get
            {
                ThrowIfDisposed();
                return (Store is IUserLockoutStore<TUser, TKey>);
            }
        }

        public virtual bool SupportsUserLogin
        {
            get
            {
                ThrowIfDisposed();
                return (Store is IUserLoginStore<TUser, TKey>);
            }
        }

        public virtual bool SupportsUserPassword
        {
            get
            {
                ThrowIfDisposed();
                return (Store is IUserPasswordStore<TUser, TKey>);
            }
        }

        public virtual bool SupportsUserPhoneNumber
        {
            get
            {
                ThrowIfDisposed();
                return (Store is IUserPhoneNumberStore<TUser, TKey>);
            }
        }

        public virtual bool SupportsUserRole
        {
            get
            {
                ThrowIfDisposed();
                return (Store is IUserRoleStore<TUser, TKey>);
            }
        }

        public virtual bool SupportsUserSecurityStamp
        {
            get
            {
                ThrowIfDisposed();
                return (Store is IUserSecurityStampStore<TUser, TKey>);
            }
        }

        public virtual bool SupportsUserTwoFactor
        {
            get
            {
                ThrowIfDisposed();
                return (Store is IUserTwoFactorStore<TUser, TKey>);
            }
        }

        public IDictionary<string, IUserTokenProvider<TUser, TKey>> TwoFactorProviders
        {
            get
            {
                return _factors;
            }
        }

        public bool UserLockoutEnabledByDefault { get; set; }

        public virtual IQueryable<TUser> Users
        {
            get
            {
                IQueryableUserStore<TUser, TKey> store = Store as IQueryableUserStore<TUser, TKey>;
                if (store == null)
                {
                    throw new NotSupportedException(Resources.StoreNotIQueryableUserStore);
                }
                return store.Users;
            }
        }

        public IUserTokenProvider<TUser, TKey> UserTokenProvider { get; set; }

        public IIdentityValidator<TUser> UserValidator
        {
            get
            {
                ThrowIfDisposed();
                return _userValidator;
            }
            set
            {
                ThrowIfDisposed();
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _userValidator = value;
            }
        }
    }
}
