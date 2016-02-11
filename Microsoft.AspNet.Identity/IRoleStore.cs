using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IRoleStore<TRole> : IRoleStore<TRole, string> where TRole : IRole<string>
    {
    }

    public interface IRoleStore<TRole, in TKey> : IDisposable where TRole : IRole<TKey>
    {
        Task CreateAsync(TRole role);

        Task DeleteAsync(TRole role);

        Task<TRole> FindByIdAsync(TKey roleId);

        Task<TRole> FindByNameAsync(string roleName);

        Task UpdateAsync(TRole role);
    }
}
