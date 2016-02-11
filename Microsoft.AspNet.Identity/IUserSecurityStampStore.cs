using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserSecurityStampStore<TUser> : IUserSecurityStampStore<TUser, string>, IDisposable where TUser : class, IUser<string>
    {
    }

    public interface IUserSecurityStampStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
    {
        Task<string> GetSecurityStampAsync(TUser user);

        Task SetSecurityStampAsync(TUser user, string stamp);
    }
}
