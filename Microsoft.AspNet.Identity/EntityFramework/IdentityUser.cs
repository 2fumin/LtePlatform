using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class IdentityUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUser
    {
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        public IdentityUser(string userName) : this()
        {
            UserName = userName;
        }
    }

    public class IdentityUser<TKey, TLogin, TRole, TClaim> : IUser<TKey> 
        where TLogin : IdentityUserLogin<TKey> 
        where TRole : IdentityUserRole<TKey> 
        where TClaim : IdentityUserClaim<TKey>
    {
        public IdentityUser()
        {
            Claims = new List<TClaim>();
            Roles = new List<TRole>();
            Logins = new List<TLogin>();
        }

        public virtual int AccessFailedCount { get; set; }

        public virtual ICollection<TClaim> Claims { get; private set; }

        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual TKey Id { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual DateTime? LockoutEndDateUtc { get; set; }

        public virtual ICollection<TLogin> Logins { get; private set; }

        public virtual string PasswordHash { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual ICollection<TRole> Roles { get; private set; }

        public virtual string SecurityStamp { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual string UserName { get; set; }
    }
}
