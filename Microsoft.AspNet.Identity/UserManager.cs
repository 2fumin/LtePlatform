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
            UserValidator = new UserValidator<TUser, TKey>(this);
            PasswordValidator = new MinimumLengthValidator(6);
            PasswordHasher = new PasswordHasher();
            ClaimsIdentityFactory = new ClaimsIdentityFactory<TUser, TKey>();
        }

        public async virtual Task<IdentityResult> AccessFailedAsync(TKey userId)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            var userLockoutStore = GetUserLockoutStore();
            var user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var count = await userLockoutStore.IncrementAccessFailedCountAsync(user).WithCurrentCulture();
            if (count < MaxFailedAccessAttemptsBeforeLockout) return await UpdateAsync(user).WithCurrentCulture();
            await
                userLockoutStore.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.Add(DefaultAccountLockoutTimeSpan))
                    .WithCurrentCulture();
            await userLockoutStore.ResetAccessFailedCountAsync(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        public async virtual Task<IdentityResult> AddClaimAsync(TKey userId, Claim claim)
        {
            ThrowIfDisposed();
            var claimStore = GetClaimStore();
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await claimStore.AddClaimAsync(user, claim).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        public async virtual Task<IdentityResult> AddLoginAsync(TKey userId, UserLoginInfo login)
        {
            IdentityResult result;
            ThrowIfDisposed();
            var loginStore = GetLoginStore();
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var existingUser = await FindAsync(login).WithCurrentCulture();
            if (existingUser != null)
            {
                result = IdentityResult.Failed(Resources.ExternalLoginExists);
            }
            else
            {
                await loginStore.AddLoginAsync(user, login).WithCurrentCulture();
                result = await UpdateAsync(user).WithCurrentCulture();
            }
            return result;
        }

        public async virtual Task<IdentityResult> AddPasswordAsync(TKey userId, string password)
        {
            IdentityResult result;
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var hash = await passwordStore.GetPasswordHashAsync(user).WithCurrentCulture();
            if (hash != null)
            {
                result = new IdentityResult(Resources.UserAlreadyHasPassword);
            }
            else
            {
                var asyncVariable0 = await UpdatePassword(passwordStore, user, password).WithCurrentCulture();
                if (!asyncVariable0.Succeeded)
                {
                    result = asyncVariable0;
                }
                else
                {
                    result = await UpdateAsync(user).WithCurrentCulture();
                }
            }
            return result;
        }

        public async virtual Task<IdentityResult> AddToRoleAsync(TKey userId, string role)
        {
            IdentityResult result;
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
            if (userRoles.Contains(role))
            {
                result = new IdentityResult(Resources.UserAlreadyInRole);
            }
            else
            {
                await userRoleStore.AddToRoleAsync(user, role).WithCurrentCulture();
                result = await UpdateAsync(user).WithCurrentCulture();
            }
            return result;
        }
        
        public async virtual Task<IdentityResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword)
        {
            ThrowIfDisposed();
            var store = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var introduced21 = await VerifyPasswordAsync(store, user, currentPassword).WithCurrentCulture();
            if (introduced21)
            {
                IdentityResult result;
                var asyncVariable0 = await UpdatePassword(store, user, newPassword).WithCurrentCulture();
                if (!asyncVariable0.Succeeded)
                {
                    result = asyncVariable0;
                }
                else
                {
                    result = await UpdateAsync(user).WithCurrentCulture();
                }
                return result;
            }
            return IdentityResult.Failed(Resources.PasswordMismatch);
        }

        public async virtual Task<IdentityResult> ChangePhoneNumberAsync(TKey userId, string phoneNumber, string token)
        {
            ThrowIfDisposed();
            var phoneNumberStore = GetPhoneNumberStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var introduced26 = await VerifyChangePhoneNumberTokenAsync(userId, token, phoneNumber).WithCurrentCulture();
            if (!introduced26) return IdentityResult.Failed(Resources.InvalidToken);
            await phoneNumberStore.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
            await phoneNumberStore.SetPhoneNumberConfirmedAsync(user, true).WithCurrentCulture();
            await UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        public async virtual Task<bool> CheckPasswordAsync(TUser user, string password)
        {
            ThrowIfDisposed();
            var store = GetPasswordStore();
            if (user == null)
            {
                return false;
            }
            return await VerifyPasswordAsync(store, user, password).WithCurrentCulture();
        }

        public async virtual Task<IdentityResult> ConfirmEmailAsync(TKey userId, string token)
        {
            IdentityResult result;
            ThrowIfDisposed();
            var emailStore = GetEmailStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var introduced20 = await VerifyUserTokenAsync(userId, "Confirmation", token).WithCurrentCulture();
            if (!introduced20)
            {
                result = IdentityResult.Failed(Resources.InvalidToken);
            }
            else
            {
                await emailStore.SetEmailConfirmedAsync(user, true).WithCurrentCulture();
                result = await UpdateAsync(user).WithCurrentCulture();
            }
            return result;
        }

        public async virtual Task<IdentityResult> CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            await UpdateSecurityStampInternal(user).WithCurrentCulture();
            var asyncVariable0 = await UserValidator.ValidateAsync(user).WithCurrentCulture();
            if (!asyncVariable0.Succeeded)
            {
                return asyncVariable0;
            }
            if (UserLockoutEnabledByDefault && SupportsUserLockout)
            {
                await GetUserLockoutStore().SetLockoutEnabledAsync(user, true).WithCurrentCulture();
            }
            await Store.CreateAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        public async virtual Task<IdentityResult> CreateAsync(TUser user, string password)
        {
            IdentityResult result;
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            var asyncVariable0 = await UpdatePassword(passwordStore, user, password).WithCurrentCulture();
            if (!asyncVariable0.Succeeded)
            {
                result = asyncVariable0;
            }
            else
            {
                result = await CreateAsync(user).WithCurrentCulture();
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
            return ClaimsIdentityFactory.CreateAsync(this, user, authenticationType);
        }

        internal async Task<SecurityToken> CreateSecurityTokenAsync(TKey userId)
        {
            var encoding2 = Encoding.GetEncoding("GB2312");
            var s = await GetSecurityStampAsync(userId).WithCurrentCulture();
            return new SecurityToken(encoding2.GetBytes(s));
        }

        public async virtual Task<IdentityResult> DeleteAsync(TUser user)
        {
            ThrowIfDisposed();
            await Store.DeleteAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed) return;
            Store.Dispose();
            _disposed = true;
        }

        public virtual Task<TUser> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            return GetLoginStore().FindAsync(login);
        }

        public async virtual Task<TUser> FindAsync(string userName, string password)
        {
            TUser local;
            ThrowIfDisposed();
            var user = await FindByNameAsync(userName).WithCurrentCulture();
            if (user == null)
            {
                local = default(TUser);
            }
            else
            {
                var introduced14 = await CheckPasswordAsync(user, password).WithCurrentCulture();
                local = introduced14 ? user : default(TUser);
            }
            return local;
        }

        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            var emailStore = GetEmailStore();
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
            ThrowIfDisposed();
            var securityToken = await CreateSecurityTokenAsync(userId).WithCurrentCulture();
            var num2 = Rfc6238AuthenticationService.GenerateCode(securityToken, phoneNumber);
            var provider = CultureInfo.InvariantCulture;
            var format = "D6";
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
            ThrowIfDisposed();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (!_factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, twoFactorProvider));
            }
            return await _factors[twoFactorProvider].GenerateAsync(twoFactorProvider, this, user).WithCurrentCulture();
        }

        public async virtual Task<string> GenerateUserTokenAsync(string purpose, TKey userId)
        {
            ThrowIfDisposed();
            if (UserTokenProvider == null)
            {
                throw new NotSupportedException(Resources.NoTokenProvider);
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await UserTokenProvider.GenerateAsync(purpose, this, user).WithCurrentCulture();
        }

        public async virtual Task<int> GetAccessFailedCountAsync(TKey userId)
        {
            ThrowIfDisposed();
            var userLockoutStore = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await userLockoutStore.GetAccessFailedCountAsync(user).WithCurrentCulture();
        }

        public async virtual Task<IList<Claim>> GetClaimsAsync(TKey userId)
        {
            ThrowIfDisposed();
            var claimStore = GetClaimStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await claimStore.GetClaimsAsync(user).WithCurrentCulture();
        }

        private IUserClaimStore<TUser, TKey> GetClaimStore()
        {
            var store = Store as IUserClaimStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserClaimStore);
            }
            return store;
        }

        public async virtual Task<string> GetEmailAsync(TKey userId)
        {
            ThrowIfDisposed();
            var emailStore = GetEmailStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await emailStore.GetEmailAsync(user).WithCurrentCulture();
        }

        internal IUserEmailStore<TUser, TKey> GetEmailStore()
        {
            var store = Store as IUserEmailStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserEmailStore);
            }
            return store;
        }

        public async virtual Task<bool> GetLockoutEnabledAsync(TKey userId)
        {
            ThrowIfDisposed();
            var userLockoutStore = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await userLockoutStore.GetLockoutEnabledAsync(user).WithCurrentCulture();
        }

        public async virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TKey userId)
        {
            ThrowIfDisposed();
            var userLockoutStore = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await userLockoutStore.GetLockoutEndDateAsync(user).WithCurrentCulture();
        }

        public async virtual Task<IList<UserLoginInfo>> GetLoginsAsync(TKey userId)
        {
            ThrowIfDisposed();
            var loginStore = GetLoginStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await loginStore.GetLoginsAsync(user).WithCurrentCulture();
        }

        private IUserLoginStore<TUser, TKey> GetLoginStore()
        {
            var store = Store as IUserLoginStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserLoginStore);
            }
            return store;
        }

        private IUserPasswordStore<TUser, TKey> GetPasswordStore()
        {
            var store = Store as IUserPasswordStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserPasswordStore);
            }
            return store;
        }

        public async virtual Task<string> GetPhoneNumberAsync(TKey userId)
        {
            ThrowIfDisposed();
            var phoneNumberStore = GetPhoneNumberStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await phoneNumberStore.GetPhoneNumberAsync(user).WithCurrentCulture();
        }

        internal IUserPhoneNumberStore<TUser, TKey> GetPhoneNumberStore()
        {
            var store = Store as IUserPhoneNumberStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserPhoneNumberStore);
            }
            return store;
        }

        public async virtual Task<IList<string>> GetRolesAsync(TKey userId)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
        }

        public async virtual Task<string> GetSecurityStampAsync(TKey userId)
        {
            ThrowIfDisposed();
            var securityStore = GetSecurityStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await securityStore.GetSecurityStampAsync(user).WithCurrentCulture();
        }

        private IUserSecurityStampStore<TUser, TKey> GetSecurityStore()
        {
            var store = Store as IUserSecurityStampStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserSecurityStampStore);
            }
            return store;
        }

        public async virtual Task<bool> GetTwoFactorEnabledAsync(TKey userId)
        {
            ThrowIfDisposed();
            var userTwoFactorStore = GetUserTwoFactorStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await userTwoFactorStore.GetTwoFactorEnabledAsync(user).WithCurrentCulture();
        }

        internal IUserLockoutStore<TUser, TKey> GetUserLockoutStore()
        {
            var store = Store as IUserLockoutStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserLockoutStore);
            }
            return store;
        }

        private IUserRoleStore<TUser, TKey> GetUserRoleStore()
        {
            var store = Store as IUserRoleStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserRoleStore);
            }
            return store;
        }

        internal IUserTwoFactorStore<TUser, TKey> GetUserTwoFactorStore()
        {
            var store = Store as IUserTwoFactorStore<TUser, TKey>;
            if (store == null)
            {
                throw new NotSupportedException(Resources.StoreNotIUserTwoFactorStore);
            }
            return store;
        }

        /// <summary>
        /// Returns a list of valid two factor providers for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async virtual Task<IList<string>> GetValidTwoFactorProvidersAsync(TKey userId)
        {
            ThrowIfDisposed();
            var twoFactorStore = GetUserTwoFactorStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return _factors.Select(x => x.Key).ToList();
        }

        public async virtual Task<bool> HasPasswordAsync(TKey userId)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await passwordStore.HasPasswordAsync(user).WithCurrentCulture();
        }

        public async virtual Task<bool> IsEmailConfirmedAsync(TKey userId)
        {
            ThrowIfDisposed();
            var emailStore = GetEmailStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await emailStore.GetEmailConfirmedAsync(user).WithCurrentCulture();
        }

        public async virtual Task<bool> IsInRoleAsync(TKey userId, string role)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture();
        }

        public async virtual Task<bool> IsLockedOutAsync(TKey userId)
        {
            bool flag2;
            ThrowIfDisposed();
            var userLockoutStore = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var introduced17 = await userLockoutStore.GetLockoutEnabledAsync(user).WithCurrentCulture();
            if (!introduced17)
            {
                flag2 = false;
            }
            else
            {
                var lockoutTime = await userLockoutStore.GetLockoutEndDateAsync(user).WithCurrentCulture();
                flag2 = lockoutTime >= DateTimeOffset.UtcNow;
            }
            return flag2;
        }

        public async virtual Task<bool> IsPhoneNumberConfirmedAsync(TKey userId)
        {
            ThrowIfDisposed();
            var phoneNumberStore = GetPhoneNumberStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await phoneNumberStore.GetPhoneNumberConfirmedAsync(user).WithCurrentCulture();
        }

        private static string NewSecurityStamp()
        {
            return Guid.NewGuid().ToString();
        }

        public async virtual Task<IdentityResult> NotifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token)
        {
            ThrowIfDisposed();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (!_factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, twoFactorProvider));
            }
            await _factors[twoFactorProvider].NotifyAsync(token, this, user).WithCurrentCulture();
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
            ThrowIfDisposed();
            var claimStore = GetClaimStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await claimStore.RemoveClaimAsync(user, claim).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        public async virtual Task<IdentityResult> RemoveFromRoleAsync(TKey userId, string role)
        {
            IdentityResult result;
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var introduced20 = await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture();
            if (!introduced20)
            {
                result = new IdentityResult(Resources.UserNotInRole);
            }
            else
            {
                await userRoleStore.RemoveFromRoleAsync(user, role).WithCurrentCulture();
                result = await UpdateAsync(user).WithCurrentCulture();
            }
            return result;
        }
        
        public async virtual Task<IdentityResult> RemoveLoginAsync(TKey userId, UserLoginInfo login)
        {
            ThrowIfDisposed();
            var loginStore = GetLoginStore();
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await loginStore.RemoveLoginAsync(user, login).WithCurrentCulture();
            await UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        public async virtual Task<IdentityResult> RemovePasswordAsync(TKey userId)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await passwordStore.SetPasswordHashAsync(user, null).WithCurrentCulture();
            await UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        public async virtual Task<IdentityResult> ResetAccessFailedCountAsync(TKey userId)
        {
            IdentityResult success;
            ThrowIfDisposed();
            var userLockoutStore = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var introduced19 = await GetAccessFailedCountAsync(user.Id).WithCurrentCulture();
            if (introduced19 == 0)
            {
                success = IdentityResult.Success;
            }
            else
            {
                await userLockoutStore.ResetAccessFailedCountAsync(user).WithCurrentCulture();
                success = await UpdateAsync(user).WithCurrentCulture();
            }
            return success;
        }

        public async virtual Task<IdentityResult> ResetPasswordAsync(TKey userId, string token, string newPassword)
        {
            IdentityResult result;
            ThrowIfDisposed();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var introduced21 = await VerifyUserTokenAsync(userId, "ResetPassword", token).WithCurrentCulture();
            if (!introduced21)
            {
                result = IdentityResult.Failed(Resources.InvalidToken);
            }
            else
            {
                var passwordStore = GetPasswordStore();
                var asyncVariable0 = await UpdatePassword(passwordStore, user, newPassword).WithCurrentCulture();
                if (!asyncVariable0.Succeeded)
                {
                    result = asyncVariable0;
                }
                else
                {
                    result = await UpdateAsync(user).WithCurrentCulture();
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
                var introduced12 = await ((UserManager<TUser, TKey>)this).GetEmailAsync(userId).WithCurrentCulture();
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
                var introduced12 = await ((UserManager<TUser, TKey>)this).GetPhoneNumberAsync(userId).WithCurrentCulture();
                message2.Destination = introduced12;
                asyncVariable0.Body = message;
                IdentityMessage msg = asyncVariable0;
                await ((UserManager<TUser, TKey>)this).SmsService.SendAsync(msg).WithCurrentCulture();
            }
        }

        public async virtual Task<IdentityResult> SetEmailAsync(TKey userId, string email)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            var emailStore = ((UserManager<TUser, TKey>)this).GetEmailStore();
            var user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await emailStore.SetEmailAsync(user, email).WithCurrentCulture();
            await emailStore.SetEmailConfirmedAsync(user, false).WithCurrentCulture();
            await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture();
        }

        public async virtual Task<IdentityResult> SetLockoutEnabledAsync(TKey userId, bool enabled)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            var userLockoutStore = ((UserManager<TUser, TKey>)this).GetUserLockoutStore();
            var user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await userLockoutStore.SetLockoutEnabledAsync(user, enabled).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture();
        }

        public async virtual Task<IdentityResult> SetLockoutEndDateAsync(TKey userId, DateTimeOffset lockoutEnd)
        {
            IdentityResult result;
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            var userLockoutStore = ((UserManager<TUser, TKey>)this).GetUserLockoutStore();
            var user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var introduced20 = await userLockoutStore.GetLockoutEnabledAsync(user).WithCurrentCulture();
            if (!introduced20)
            {
                result = IdentityResult.Failed(Resources.LockoutNotEnabled);
            }
            else
            {
                await userLockoutStore.SetLockoutEndDateAsync(user, lockoutEnd).WithCurrentCulture();
                result = await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture();
            }
            return result;
        }

        public async virtual Task<IdentityResult> SetPhoneNumberAsync(TKey userId, string phoneNumber)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            var phoneNumberStore = ((UserManager<TUser, TKey>)this).GetPhoneNumberStore();
            var user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await phoneNumberStore.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
            await phoneNumberStore.SetPhoneNumberConfirmedAsync(user, false).WithCurrentCulture();
            await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture();
        }

        public async virtual Task<IdentityResult> SetTwoFactorEnabledAsync(TKey userId, bool enabled)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            var userTwoFactorStore = ((UserManager<TUser, TKey>)this).GetUserTwoFactorStore();
            var user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await userTwoFactorStore.SetTwoFactorEnabledAsync(user, enabled).WithCurrentCulture();
            await ((UserManager<TUser, TKey>)this).UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture();
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
            var asyncVariable0 = await ((UserManager<TUser, TKey>)this).UserValidator.ValidateAsync(user).WithCurrentCulture();
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
            var asyncVariable0 = await ((UserManager<TUser, TKey>)this).PasswordValidator.ValidateAsync(newPassword).WithCurrentCulture();
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
            var securityStore = ((UserManager<TUser, TKey>)this).GetSecurityStore();
            var user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await securityStore.SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
            return await ((UserManager<TUser, TKey>)this).UpdateAsync(user).WithCurrentCulture();
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
            var securityToken = await ((UserManager<TUser, TKey>)this).CreateSecurityTokenAsync(userId).WithCurrentCulture();
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
            var hashedPassword = await store.GetPasswordHashAsync(user).WithCurrentCulture();
            return (((UserManager<TUser, TKey>)this).PasswordHasher.VerifyHashedPassword(hashedPassword, password) != PasswordVerificationResult.Failed);
        }

        public async virtual Task<bool> VerifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            var user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (!((UserManager<TUser, TKey>)this)._factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, twoFactorProvider));
            }
            var provider = ((UserManager<TUser, TKey>)this)._factors[twoFactorProvider];
            return await provider.ValidateAsync(twoFactorProvider, token, (UserManager<TUser, TKey>)this, user).WithCurrentCulture();
        }

        public async virtual Task<bool> VerifyUserTokenAsync(TKey userId, string purpose, string token)
        {
            ((UserManager<TUser, TKey>)this).ThrowIfDisposed();
            if (((UserManager<TUser, TKey>)this).UserTokenProvider == null)
            {
                throw new NotSupportedException(Resources.NoTokenProvider);
            }
            var user = await ((UserManager<TUser, TKey>)this).FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await ((UserManager<TUser, TKey>)this).UserTokenProvider.ValidateAsync(purpose, token, (UserManager<TUser, TKey>)this, user).WithCurrentCulture();
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
