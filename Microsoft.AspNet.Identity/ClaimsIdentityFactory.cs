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
            this.RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            this.UserIdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            this.UserNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
            this.SecurityStampClaimType = "AspNet.Identity.SecurityStamp";
        }

        public virtual string ConvertIdToString(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return key.ToString();
        }

        public virtual async Task<ClaimsIdentity> CreateAsync(UserManager<TUser, TKey> manager, TUser user,
            string authenticationType)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            ClaimsIdentity id = new ClaimsIdentity(authenticationType,
                ((ClaimsIdentityFactory<TUser, TKey>) this).UserNameClaimType,
                ((ClaimsIdentityFactory<TUser, TKey>) this).RoleClaimType);
            id.AddClaim(new Claim(((ClaimsIdentityFactory<TUser, TKey>) this).UserIdClaimType,
                ((ClaimsIdentityFactory<TUser, TKey>) this).ConvertIdToString(user.Id),
                "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(new Claim(((ClaimsIdentityFactory<TUser, TKey>) this).UserNameClaimType, user.UserName,
                "http://www.w3.org/2001/XMLSchema#string"));
            id.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));
            if (manager.SupportsUserSecurityStamp)
            {
                Tuple<ClaimsIdentity, string> tuple = new Tuple<ClaimsIdentity, string>(id,
                    ((ClaimsIdentityFactory<TUser, TKey>) this).SecurityStampClaimType);
                string introduced20 = await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture<string>();
                tuple2.Item1.AddClaim(new Claim(tuple2.Item2, introduced20));
            }
            if (manager.SupportsUserRole)
            {
                IList<string> roles = await manager.GetRolesAsync(user.Id).WithCurrentCulture<IList<string>>();
                using (IEnumerator<string> enumerator = roles.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        string current = enumerator.Current;
                        id.AddClaim(new Claim(((ClaimsIdentityFactory<TUser, TKey>) this).RoleClaimType, current,
                            "http://www.w3.org/2001/XMLSchema#string"));
                    }
                }
            }
            if (manager.SupportsUserClaim)
            {
                ClaimsIdentity identity3;
                ClaimsIdentity identity2 = id;
                IList<Claim> claims = await manager.GetClaimsAsync(user.Id).WithCurrentCulture<IList<Claim>>();
                identity3.AddClaims(claims);
            }
            return id;
        }

        public string RoleClaimType { get; set; }

        public string SecurityStampClaimType { get; set; }

        public string UserIdClaimType { get; set; }

        public string UserNameClaimType { get; set; }
    }
}