using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserClaimStore<TUser> : IUserClaimStore<TUser, string> where TUser : class, IUser<string>
    {
    }

    public interface IUserClaimStore<TUser, in TKey> : IUserStore<TUser, TKey> where TUser : class, IUser<TKey>
    {
        Task AddClaimAsync(TUser user, Claim claim);

        Task<IList<Claim>> GetClaimsAsync(TUser user);

        Task RemoveClaimAsync(TUser user, Claim claim);
    }
}
