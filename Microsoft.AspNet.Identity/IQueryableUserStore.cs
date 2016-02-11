using System;
using System.Linq;

namespace Microsoft.AspNet.Identity
{
    public interface IQueryableUserStore<TUser> : IQueryableUserStore<TUser, string>, IUserStore<TUser, string>, IDisposable where TUser : class, IUser<string>
    {
    }

    public interface IQueryableUserStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
    {
        IQueryable<TUser> Users { get; }
    }
}
