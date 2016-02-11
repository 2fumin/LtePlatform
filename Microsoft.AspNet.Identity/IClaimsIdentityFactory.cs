using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Threading.Tasks;

    public interface IClaimsIdentityFactory<TUser> where TUser : class, IUser
    {
        Task<ClaimsIdentity> CreateAsync(UserManager<TUser> manager, TUser user, string authenticationType);
    }

    public interface IClaimsIdentityFactory<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
    {
        Task<ClaimsIdentity> CreateAsync(UserManager<TUser, TKey> manager, TUser user, string authenticationType);
    }
}
