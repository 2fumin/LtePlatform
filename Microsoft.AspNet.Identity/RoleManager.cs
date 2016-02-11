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
                throw new ArgumentNullException("store");
            }
            this.Store = store;
            this.RoleValidator = new RoleValidator<TRole, TKey>((RoleManager<TRole, TKey>)this);
        }

        public async virtual Task<IdentityResult> CreateAsync(TRole role)
        {
            IdentityResult success;
            ((RoleManager<TRole, TKey>)this).ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            IdentityResult asyncVariable0 = await ((RoleManager<TRole, TKey>)this).RoleValidator.ValidateAsync(role).WithCurrentCulture<IdentityResult>();
            if (!asyncVariable0.Succeeded)
            {
                success = asyncVariable0;
            }
            else
            {
                await ((RoleManager<TRole, TKey>)this).Store.CreateAsync(role).WithCurrentCulture();
                success = IdentityResult.Success;
            }
            return success;
        }

        public async virtual Task<IdentityResult> DeleteAsync(TRole role)
        {
            ((RoleManager<TRole, TKey>)this).ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            await ((RoleManager<TRole, TKey>)this).Store.DeleteAsync(role).WithCurrentCulture();
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this._disposed)
            {
                this.Store.Dispose();
            }
            this._disposed = true;
        }

        public async virtual Task<TRole> FindByIdAsync(TKey roleId)
        {
            ((RoleManager<TRole, TKey>)this).ThrowIfDisposed();
            return await ((RoleManager<TRole, TKey>)this).Store.FindByIdAsync(roleId).WithCurrentCulture<TRole>();
        }

        public async virtual Task<TRole> FindByNameAsync(string roleName)
        {
            ((RoleManager<TRole, TKey>)this).ThrowIfDisposed();
            if (roleName == null)
            {
                throw new ArgumentNullException("roleName");
            }
            return await ((RoleManager<TRole, TKey>)this).Store.FindByNameAsync(roleName).WithCurrentCulture<TRole>();
        }

        public async virtual Task<bool> RoleExistsAsync(string roleName)
        {
            ((RoleManager<TRole, TKey>)this).ThrowIfDisposed();
            if (roleName == null)
            {
                throw new ArgumentNullException("roleName");
            }
            TRole result = await ((RoleManager<TRole, TKey>)this).FindByNameAsync(roleName).WithCurrentCulture<TRole>();
            return (result != null);
        }

        private void ThrowIfDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(base.GetType().Name);
            }
        }

        public async virtual Task<IdentityResult> UpdateAsync(TRole role)
        {
            IdentityResult success;
            ((RoleManager<TRole, TKey>)this).ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            IdentityResult asyncVariable0 = await ((RoleManager<TRole, TKey>)this).RoleValidator.ValidateAsync(role).WithCurrentCulture<IdentityResult>();
            if (!asyncVariable0.Succeeded)
            {
                success = asyncVariable0;
            }
            else
            {
                await ((RoleManager<TRole, TKey>)this).Store.UpdateAsync(role).WithCurrentCulture();
                success = IdentityResult.Success;
            }
            return success;
        }

        public virtual IQueryable<TRole> Roles
        {
            get
            {
                IQueryableRoleStore<TRole, TKey> store = this.Store as IQueryableRoleStore<TRole, TKey>;
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
                return this._roleValidator;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this._roleValidator = value;
            }
        }

        protected IRoleStore<TRole, TKey> Store { get; private set; }
        
    }
}
