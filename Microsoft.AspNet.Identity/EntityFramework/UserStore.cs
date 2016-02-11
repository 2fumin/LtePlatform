using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Properties;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class UserStore<TUser> 
        : UserStore<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUserStore<TUser> 
        where TUser : IdentityUser
    {
        public UserStore() : this(new IdentityDbContext())
        {
            DisposeContext = true;
        }

        public UserStore(DbContext context) : base(context)
        {
        }
    }

    public class UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> 
        : IUserLoginStore<TUser, TKey>, IUserClaimStore<TUser, TKey>, IUserRoleStore<TUser, TKey>, 
        IUserPasswordStore<TUser, TKey>, IUserSecurityStampStore<TUser, TKey>, 
        IQueryableUserStore<TUser, TKey>, IUserEmailStore<TUser, TKey>, 
        IUserPhoneNumberStore<TUser, TKey>, IUserTwoFactorStore<TUser, TKey>, IUserLockoutStore<TUser, TKey> 
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim> 
        where TRole : IdentityRole<TKey, TUserRole> 
        where TKey : IEquatable<TKey> 
        where TUserLogin : IdentityUserLogin<TKey>, new() 
        where TUserRole : IdentityUserRole<TKey>, new() 
        where TUserClaim : IdentityUserClaim<TKey>, new()
    {
        private bool _disposed;
        private readonly IDbSet<TUserLogin> _logins;
        private readonly EntityStore<TRole> _roleStore;
        private readonly IDbSet<TUserClaim> _userClaims;
        private readonly IDbSet<TUserRole> _userRoles;
        private EntityStore<TUser> _userStore;

        public UserStore(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            Context = context;
            AutoSaveChanges = true;
            _userStore = new EntityStore<TUser>(context);
            _roleStore = new EntityStore<TRole>(context);
            _logins = Context.Set<TUserLogin>();
            _userClaims = Context.Set<TUserClaim>();
            _userRoles = Context.Set<TUserRole>();
        }

        public virtual Task AddClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            var entity = Activator.CreateInstance<TUserClaim>();
            entity.UserId = user.Id;
            entity.ClaimType = claim.Type;
            entity.ClaimValue = claim.Value;
            _userClaims.Add(entity);
            return Task.FromResult(0);
        }

        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var entity = Activator.CreateInstance<TUserLogin>();
            entity.UserId = user.Id;
            entity.ProviderKey = login.ProviderKey;
            entity.LoginProvider = login.LoginProvider;
            _logins.Add(entity);
            return Task.FromResult(0);
        }

        public async virtual Task AddToRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(Resources.ValueCannotBeNullOrEmpty, nameof(roleName));
            }
            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => (r.Name.ToUpper() == roleName.ToUpper())).WithCurrentCulture();
            if (roleEntity == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.RoleNotFound, new object[] { roleName }));
            }
            var asyncVariable0 = Activator.CreateInstance<TUserRole>();
            asyncVariable0.UserId = user.Id;
            asyncVariable0.RoleId = roleEntity.Id;
            var entity = asyncVariable0;
            _userRoles.Add(entity);
        }

        private bool AreClaimsLoaded(TUser user)
        {
            return Context.Entry(user).Collection(u => u.Claims).IsLoaded;
        }

        private bool AreLoginsLoaded(TUser user)
        {
            return Context.Entry(user).Collection(u => u.Logins).IsLoaded;
        }

        public async virtual Task CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userStore.Create(user);
            await SaveChanges().WithCurrentCulture();
        }

        public async virtual Task DeleteAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userStore.Delete(user);
            await SaveChanges().WithCurrentCulture();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext && disposing)
            {
                Context?.Dispose();
            }
            _disposed = true;
            Context = null;
            _userStore = null;
        }

        private async Task EnsureClaimsLoaded(TUser user)
        {
            TKey userId;
            if (!AreClaimsLoaded(user))
            {
                userId = user.Id;
            }
            else
            {
                return;
            }
            await (from uc in _userClaims
                   where uc.UserId.Equals(userId)
                   select uc).LoadAsync().WithCurrentCulture();
            Context.Entry(user).Collection(u => u.Claims).IsLoaded = true;
        }

        private async Task EnsureLoginsLoaded(TUser user)
        {
            TKey userId;
            if (!AreLoginsLoaded(user))
            {
                userId = user.Id;
            }
            else
            {
                return;
            }
            await (from uc in _logins
                   where uc.UserId.Equals(userId)
                   select uc).LoadAsync().WithCurrentCulture();
            Context.Entry(user).Collection(u => u.Logins).IsLoaded = true;
        }

        private async Task EnsureRolesLoaded(TUser user)
        {
            TKey userId;
            if (!Context.Entry(user).Collection(u => u.Roles).IsLoaded)
            {
                userId = user.Id;
            }
            else
            {
                return;
            }
            await (from uc in _userRoles
                   where uc.UserId.Equals(userId)
                   select uc).LoadAsync().WithCurrentCulture();
            Context.Entry(user).Collection(u => u.Roles).IsLoaded = true;
        }

        public async virtual Task<TUser> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var provider = login.LoginProvider;
            var key = login.ProviderKey;
            var userLogin 
                = await _logins.FirstOrDefaultAsync(l => ((l.LoginProvider == provider) && (l.ProviderKey == key))).WithCurrentCulture();
            if (userLogin == null) return default(TUser);
            var userId = userLogin.UserId;
            return await GetUserAggregateAsync(u => u.Id.Equals(userId)).WithCurrentCulture();
        }

        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.Email.ToUpper() == email.ToUpper());
        }

        public virtual Task<TUser> FindByIdAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.Id.Equals(userId));
        }

        public virtual Task<TUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => string.Equals(u.UserName, userName, StringComparison.CurrentCultureIgnoreCase));
        }

        public virtual Task<int> GetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.AccessFailedCount);
        }

        public async virtual Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await EnsureClaimsLoaded(user).WithCurrentCulture();
            return (from c in user.Claims select new Claim(c.ClaimType, c.ClaimValue)).ToList<Claim>();
        }

        public virtual Task<string> GetEmailAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email);
        }

        public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.EmailConfirmed);
        }

        public virtual Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.LockoutEnabled);
        }

        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.LockoutEndDateUtc.HasValue 
                ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) 
                : new DateTimeOffset());
        }

        public async virtual Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await EnsureLoginsLoaded(user).WithCurrentCulture();
            return (from l in user.Logins select new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList<UserLoginInfo>();
        }

        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<string> GetPhoneNumberAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PhoneNumber);
        }

        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public async virtual Task<IList<string>> GetRolesAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;
            var source = from userRole in _userRoles
                                        where userRole.UserId.Equals(userId)
                                        select userRole into userRole
                                        join role in _roleStore.DbEntitySet on userRole.RoleId equals role.Id
                                        select role.Name;
            return await source.ToListAsync().WithCurrentCulture();
        }

        public virtual Task<string> GetSecurityStampAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.TwoFactorEnabled);
        }

        protected async virtual Task<TUser> GetUserAggregateAsync(Expression<Func<TUser, bool>> filter)
        {
            TKey id;
            TUser user;
            if (FindByIdFilterParser.TryMatchAndGetId(filter, out id))
            {
                user = await _userStore.GetByIdAsync(id).WithCurrentCulture();
            }
            else
            {
                user = await Users.FirstOrDefaultAsync(filter).WithCurrentCulture();
            }
            if (user != null)
            {
                await EnsureClaimsLoaded(user).WithCurrentCulture();
                await EnsureLoginsLoaded(user).WithCurrentCulture();
                await EnsureRolesLoaded(user).WithCurrentCulture();
            }
            return user;
        }

        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public async virtual Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(Resources.ValueCannotBeNullOrEmpty, nameof(roleName));
            }
            var role = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => 
                (string.Equals(r.Name, roleName, StringComparison.CurrentCultureIgnoreCase))).WithCurrentCulture();
            if (role != null)
            {
                var userId = user.Id;
                var roleId = role.Id;
                return await _userRoles.AnyAsync(ur => (ur.RoleId.Equals(roleId) && ur.UserId.Equals(userId))).WithCurrentCulture();
            }
            return false;
        }

        public async virtual Task RemoveClaimAsync(TUser user, Claim claim)
        {
            IEnumerable<TUserClaim> claims;
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            var claimValue = claim.Value;
            var claimType = claim.Type;
            var userId = default(TKey);
            if (!AreClaimsLoaded(user))
            {
                userId = user.Id;
            }
            else
            {
                Func<TUserClaim, bool> predicate = uc => (uc.ClaimValue == claimValue) && (uc.ClaimType == claimType);
                claims = user.Claims.Where(predicate).ToList();
                RemoveFromTheClaims(claims);
            }
            var list = await (from uc in _userClaims
                where ((uc.ClaimValue == claimValue) && (uc.ClaimType == claimType)) && uc.UserId.Equals(userId)
                select uc).ToListAsync<TUserClaim>().WithCurrentCulture<List<TUserClaim>>();
            claims = list;
            RemoveFromTheClaims(claims);
        }

        private void RemoveFromTheClaims(IEnumerable<TUserClaim> claims)
        {
            using (var enumerator = claims.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var entity = enumerator.Current;
                    _userClaims.Remove(entity);
                }
            }
        }

        public async virtual Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(Resources.ValueCannotBeNullOrEmpty, nameof(roleName));
            }
            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => 
            (string.Equals(r.Name, roleName, StringComparison.CurrentCultureIgnoreCase))).WithCurrentCulture();
            if (roleEntity != null)
            {
                var roleId = roleEntity.Id;
                var userId = user.Id;
                var entity = await _userRoles.FirstOrDefaultAsync(r => 
                    (roleId.Equals(r.RoleId) && r.UserId.Equals(userId))).WithCurrentCulture();
                if (entity != null)
                {
                    _userRoles.Remove(entity);
                }
            }
        }

        public async virtual Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            TUserLogin entry;
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var provider = login.LoginProvider;
            var key = login.ProviderKey;
            var userId = default(TKey);
            if (!AreLoginsLoaded(user))
            {
                userId = user.Id;
            }
            else
            {
                Func<TUserLogin, bool> predicate = ul => (ul.LoginProvider == provider) && (ul.ProviderKey == key);
                entry = user.Logins.SingleOrDefault(predicate);
                if (entry != null)
                {
                    _logins.Remove(entry);
                }
            }
            entry = await _logins.SingleOrDefaultAsync(ul => 
                (((ul.LoginProvider == provider) && (ul.ProviderKey == key)) && ul.UserId.Equals(userId))).WithCurrentCulture();
            if (entry != null)
            {
                _logins.Remove(entry);
            }
        }

        public virtual Task ResetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        private async Task SaveChanges()
        {
            if (!AutoSaveChanges)
            {
                return;
            }
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public virtual Task SetEmailAsync(TUser user, string email)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Email = email;
            return Task.FromResult(0);
        }

        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEndDateUtc = (lockoutEnd == DateTimeOffset.MinValue) ? null : new DateTime?(lockoutEnd.UtcDateTime);
            return Task.FromResult(0);
        }

        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public virtual Task SetSecurityStampAsync(TUser user, string stamp)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public async virtual Task UpdateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userStore.Update(user);
            await SaveChanges().WithCurrentCulture();
        }

        public bool AutoSaveChanges { get; set; }

        public DbContext Context { get; private set; }

        public bool DisposeContext { get; set; }

        public IQueryable<TUser> Users
        {
            get
            {
                return _userStore.EntitySet;
            }
        }
        

        private static class FindByIdFilterParser
        {
            private static readonly MethodInfo EqualsMethodInfo;
            private static readonly Expression<Func<TUser, bool>> Predicate;
            private static readonly MemberInfo UserIdMemberInfo;

            static FindByIdFilterParser()
            {
                Predicate = u => u.Id.Equals(default(TKey));
                EqualsMethodInfo = ((MethodCallExpression) Predicate.Body).Method;
                UserIdMemberInfo = ((MemberExpression) ((MethodCallExpression) Predicate.Body).Object).Member;
            }

            internal static bool TryMatchAndGetId(Expression<Func<TUser, bool>> filter, out TKey id)
            {
                MemberExpression operand;
                id = default(TKey);
                if (filter.Body.NodeType != ExpressionType.Call)
                {
                    return false;
                }
                var body = (MethodCallExpression) filter.Body;
                if (body.Method != EqualsMethodInfo)
                {
                    return false;
                }
                if (((body.Object == null) || 
                    (body.Object.NodeType != ExpressionType.MemberAccess)) || 
                    (((MemberExpression) body.Object).Member != UserIdMemberInfo))
                {
                    return false;
                }
                if (body.Arguments.Count != 1)
                {
                    return false;
                }
                switch (body.Arguments[0].NodeType)
                {
                    case ExpressionType.Convert:
                        var expression3 = (UnaryExpression) body.Arguments[0];
                        if (expression3.Operand.NodeType != ExpressionType.MemberAccess)
                        {
                            return false;
                        }
                        operand = (MemberExpression) expression3.Operand;
                        break;
                    case ExpressionType.MemberAccess:
                        operand = (MemberExpression) body.Arguments[0];
                        break;
                    default:
                        return false;
                }
                if ((operand.Member.MemberType != MemberTypes.Field) || (operand.Expression.NodeType != ExpressionType.Constant))
                {
                    return false;
                }
                var member = (FieldInfo) operand.Member;
                var obj2 = ((ConstantExpression) operand.Expression).Value;
                id = (TKey) member.GetValue(obj2);
                return true;
            }
        }
    }
}
