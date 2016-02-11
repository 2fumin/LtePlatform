using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public class ClaimsIdentityFactory<TUser> : ClaimsIdentityFactory<TUser, string> where TUser : class, IUser<string>
    {
    }

    public class ClaimsIdentityFactory<TUser, TKey> : IClaimsIdentityFactory<TUser, TKey>
        where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
    {
        internal const string DefaultIdentityProviderClaimValue = "ASP.NET Identity";

        internal const string IdentityProviderClaimType =
            "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

        public ClaimsIdentityFactory()
        {
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            UserIdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            UserNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
            SecurityStampClaimType = Constants.DefaultSecurityStampClaimType;
        }

        public virtual string ConvertIdToString(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return key.ToString();
        }

        public virtual async Task<ClaimsIdentity> CreateAsync(UserManager<TUser, TKey> manager, TUser user,
            string authenticationType)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var id = new ClaimsIdentity(authenticationType, UserNameClaimType, RoleClaimType);
            id.AddClaim(new Claim(UserIdClaimType, ConvertIdToString(user.Id),
                "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(new Claim(UserNameClaimType, user.UserName,
                "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(new Claim(IdentityProviderClaimType,
                DefaultIdentityProviderClaimValue, "http://www.w3.org/2001/XMLSchema#string"));
            if (manager.SupportsUserSecurityStamp)
            {
                var tuple = new Tuple<ClaimsIdentity, string>(id,
                    SecurityStampClaimType);
                var introduced20 = await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture<string>();
                tuple.Item1.AddClaim(new Claim(tuple.Item2, introduced20));
            }
            if (manager.SupportsUserRole)
            {
                var roles = await manager.GetRolesAsync(user.Id).WithCurrentCulture<IList<string>>();
                using (var enumerator = roles.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        id.AddClaim(new Claim(RoleClaimType, current,
                            "http://www.w3.org/2001/XMLSchema#string"));
                    }
                }
            }
            if (!manager.SupportsUserClaim) return id;
            var identity2 = id;
            var claims = await manager.GetClaimsAsync(user.Id).WithCurrentCulture<IList<Claim>>();
            identity2.AddClaims(claims);
            return id;
        }

        public string RoleClaimType { get; set; }

        public string SecurityStampClaimType { get; set; }

        public string UserIdClaimType { get; set; }

        public string UserNameClaimType { get; set; }
    }
}