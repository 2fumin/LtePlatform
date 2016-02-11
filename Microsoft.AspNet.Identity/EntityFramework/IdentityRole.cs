using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class IdentityRole : IdentityRole<string, IdentityUserRole>
    {
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public IdentityRole(string roleName) : this()
        {
            Name = roleName;
        }
    }

    public class IdentityRole<TKey, TUserRole> : IRole<TKey> where TUserRole : IdentityUserRole<TKey>
    {
        public IdentityRole()
        {
            Users = new List<TUserRole>();
        }

        public TKey Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<TUserRole> Users { get; private set; }
    }
}
