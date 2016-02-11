using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class RoleStore<TRole> : RoleStore<TRole, string, IdentityUserRole>, IQueryableRoleStore<TRole> 
        where TRole : IdentityRole, new()
    {
        public RoleStore() : base(new IdentityDbContext())
        {
            DisposeContext = true;
        }

        public RoleStore(DbContext context) : base(context)
        {
        }
    }

    public class RoleStore<TRole, TKey, TUserRole> : IQueryableRoleStore<TRole, TKey> 
        where TRole : IdentityRole<TKey, TUserRole>, new() 
        where TUserRole : IdentityUserRole<TKey>, new()
    {
        private bool _disposed;
        private EntityStore<TRole> _roleStore;

        public RoleStore(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            Context = context;
            _roleStore = new EntityStore<TRole>(context);
        }

        public async virtual Task CreateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _roleStore.Create(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public async virtual Task DeleteAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _roleStore.Delete(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
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
            _roleStore = null;
        }

        public Task<TRole> FindByIdAsync(TKey roleId)
        {
            ThrowIfDisposed();
            return _roleStore.GetByIdAsync(roleId);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            return _roleStore.EntitySet.FirstOrDefaultAsync(u => 
            (string.Equals(u.Name, roleName, StringComparison.CurrentCultureIgnoreCase)));
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public async virtual Task UpdateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _roleStore.Update(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public DbContext Context { get; private set; }

        public bool DisposeContext { get; set; }

        public IQueryable<TRole> Roles => _roleStore.EntitySet;
    }
}
