using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Properties;

namespace Microsoft.AspNet.Identity
{
    public class RoleValidator<TRole> : RoleValidator<TRole, string> where TRole : class, IRole<string>
    {
        public RoleValidator(RoleManager<TRole, string> manager) : base(manager)
        {
        }
    }

    public class RoleValidator<TRole, TKey> : IIdentityValidator<TRole> where TRole : class, IRole<TKey> where TKey : IEquatable<TKey>
    {
        public RoleValidator(RoleManager<TRole, TKey> manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            Manager = manager;
        }

        public async virtual Task<IdentityResult> ValidateAsync(TRole item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var errors = new List<string>();
            await ValidateRoleName(item, errors).WithCurrentCulture();
            var success = errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
            return success;
        }

        private async Task ValidateRoleName(TRole role, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
            {
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, "Name"));
                return;
            }
            var owner = await Manager.FindByNameAsync(role.Name).WithCurrentCulture<TRole>();
            if ((owner != null) && !EqualityComparer<TKey>.Default.Equals(owner.Id, role.Id))
            {
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.DuplicateName, role.Name));
            }
        }

        private RoleManager<TRole, TKey> Manager { get; }
        
    }
}
