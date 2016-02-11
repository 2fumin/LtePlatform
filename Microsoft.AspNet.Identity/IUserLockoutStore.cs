using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserLockoutStore<TUser, in TKey> : IUserStore<TUser, TKey> where TUser : class, IUser<TKey>
    {
        Task<int> GetAccessFailedCountAsync(TUser user);

        Task<bool> GetLockoutEnabledAsync(TUser user);

        Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user);

        Task<int> IncrementAccessFailedCountAsync(TUser user);

        Task ResetAccessFailedCountAsync(TUser user);

        Task SetLockoutEnabledAsync(TUser user, bool enabled);

        Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd);
    }
}
