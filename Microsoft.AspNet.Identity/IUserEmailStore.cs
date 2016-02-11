using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserEmailStore<TUser> : IUserEmailStore<TUser, string> where TUser : class, IUser<string>
    {
    }

    public interface IUserEmailStore<TUser, in TKey> : IUserStore<TUser, TKey> where TUser : class, IUser<TKey>
    {
        Task<TUser> FindByEmailAsync(string email);

        Task<string> GetEmailAsync(TUser user);

        Task<bool> GetEmailConfirmedAsync(TUser user);

        Task SetEmailAsync(TUser user, string email);

        Task SetEmailConfirmedAsync(TUser user, bool confirmed);
    }
}
