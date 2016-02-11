﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserRoleStore<TUser> : IUserRoleStore<TUser, string> where TUser : class, IUser<string>
    {
    }

    public interface IUserRoleStore<TUser, in TKey> : IUserStore<TUser, TKey> where TUser : class, IUser<TKey>
    {
        Task AddToRoleAsync(TUser user, string roleName);

        Task<IList<string>> GetRolesAsync(TUser user);

        Task<bool> IsInRoleAsync(TUser user, string roleName);

        Task RemoveFromRoleAsync(TUser user, string roleName);
    }
}
