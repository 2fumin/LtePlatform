using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserPasswordStore<TUser> : IUserPasswordStore<TUser, string> where TUser : class, IUser<string>
    {
    }

    public interface IUserPasswordStore<TUser, in TKey> : IUserStore<TUser, TKey> where TUser : class, IUser<TKey>
    {
        Task<string> GetPasswordHashAsync(TUser user);

        Task<bool> HasPasswordAsync(TUser user);

        Task SetPasswordHashAsync(TUser user, string passwordHash);
    }
}
