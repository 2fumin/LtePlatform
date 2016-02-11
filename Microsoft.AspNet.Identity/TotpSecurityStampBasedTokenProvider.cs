using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public class TotpSecurityStampBasedTokenProvider<TUser, TKey> : IUserTokenProvider<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
    {
        public async virtual Task<string> GenerateAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
        {
            var securityToken = await manager.CreateSecurityTokenAsync(user.Id).WithCurrentCulture();
            var modifier = await GetUserModifierAsync(purpose, manager, user).WithCurrentCulture();
            return Rfc6238AuthenticationService.GenerateCode(securityToken, modifier).ToString("D6", CultureInfo.InvariantCulture);
        }

        public virtual Task<string> GetUserModifierAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult(string.Concat("Totp:", purpose, ":", user.Id));
        }

        public virtual Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return Task.FromResult(manager.SupportsUserSecurityStamp);
        }

        public virtual Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult(0);
        }

        public async virtual Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser, TKey> manager, TUser user)
        {
            bool flag2;
            int code;
            if (!int.TryParse(token, out code))
            {
                flag2 = false;
            }
            else
            {
                var securityToken = await manager.CreateSecurityTokenAsync(user.Id).WithCurrentCulture();
                var modifier = await GetUserModifierAsync(purpose, manager, user).WithCurrentCulture();
                flag2 = (securityToken != null) && Rfc6238AuthenticationService.ValidateCode(securityToken, code, modifier);
            }
            return flag2;
        }
        
    }
}
