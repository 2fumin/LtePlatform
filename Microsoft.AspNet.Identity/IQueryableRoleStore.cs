using System;
using System.Linq;

namespace Microsoft.AspNet.Identity
{
    public interface IQueryableRoleStore<TRole> : IQueryableRoleStore<TRole, string> where TRole : IRole<string>
    {
    }

    public interface IQueryableRoleStore<TRole, in TKey> : IRoleStore<TRole, TKey> where TRole : IRole<TKey>
    {
        IQueryable<TRole> Roles { get; }
    }
}
