using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserPhoneNumberStore<TUser> : IUserPhoneNumberStore<TUser, string> where TUser : class, IUser<string>
    {
    }

    public interface IUserPhoneNumberStore<TUser, in TKey> : IUserStore<TUser, TKey> where TUser : class, IUser<TKey>
    {
        Task<string> GetPhoneNumberAsync(TUser user);

        Task<bool> GetPhoneNumberConfirmedAsync(TUser user);

        Task SetPhoneNumberAsync(TUser user, string phoneNumber);

        Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed);
    }
}
