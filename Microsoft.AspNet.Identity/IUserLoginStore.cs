using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserLoginStore<TUser> : IUserLoginStore<TUser, string> where TUser : class, IUser<string>
    {
    }

    public interface IUserLoginStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable where TUser : class, IUser<TKey>
    {
        Task AddLoginAsync(TUser user, UserLoginInfo login);

        Task<TUser> FindAsync(UserLoginInfo login);

        Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user);

        Task RemoveLoginAsync(TUser user, UserLoginInfo login);
    }
}
