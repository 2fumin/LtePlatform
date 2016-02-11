using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Properties;

namespace Microsoft.AspNet.Identity
{
    public class RoleManager<TRole> : RoleManager<TRole, string> where TRole : class, IRole<string>
    {
        public RoleManager(IRoleStore<TRole, string> store) : base(store)
        {
        }
    }

    public class RoleManager<TRole, TKey> : IDisposable where TRole : class, IRole<TKey> where TKey : IEquatable<TKey>
    {
        private bool _disposed;
        private IIdentityValidator<TRole> _roleValidator;

        public RoleManager(IRoleStore<TRole, TKey> store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            Store = store;
            RoleValidator = new RoleValidator<TRole, TKey>(this);
        }

        public async virtual Task<IdentityResult> CreateAsync(TRole role)
        {
            IdentityResult success;
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var asyncVariable0 = await RoleValidator.ValidateAsync(role).WithCurrentCulture();
            if (!asyncVariable0.Succeeded)
            {
                success = asyncVariable0;
            }
            else
            {
                await Store.CreateAsync(role).WithCurrentCulture();
                success = IdentityResult.Success;
            }
            return success;
        }

        public async virtual Task<IdentityResult> DeleteAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            await Store.DeleteAsync(role).WithCurrentCulture();
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
            }
            _disposed = true;
        }

        public async virtual Task<TRole> FindByIdAsync(TKey roleId)
        {
            ThrowIfDisposed();
            return await Store.FindByIdAsync(roleId).WithCurrentCulture();
        }

        public async virtual Task<TRole> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            return await Store.FindByNameAsync(roleName).WithCurrentCulture();
        }

        public async virtual Task<bool> RoleExistsAsync(string roleName)
        {
            ThrowIfDisposed();
            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            var result = await FindByNameAsync(roleName).WithCurrentCulture();
            return (result != null);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public async virtual Task<IdentityResult> UpdateAsync(TRole role)
        {
            IdentityResult success;
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var asyncVariable0 = await RoleValidator.ValidateAsync(role).WithCurrentCulture();
            if (!asyncVariable0.Succeeded)
            {
                success = asyncVariable0;
            }
            else
            {
                await Store.UpdateAsync(role).WithCurrentCulture();
                success = IdentityResult.Success;
            }
            return success;
        }

        public virtual IQueryable<TRole> Roles
        {
            get
            {
                var store = Store as IQueryableRoleStore<TRole, TKey>;
                if (store == null)
                {
                    throw new NotSupportedException(Resources.StoreNotIQueryableRoleStore);
                }
                return store.Roles;
            }
        }

        public IIdentityValidator<TRole> RoleValidator
        {
            get
            {
                return _roleValidator;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _roleValidator = value;
            }
        }

        protected IRoleStore<TRole, TKey> Store { get; }
        
    }
}
