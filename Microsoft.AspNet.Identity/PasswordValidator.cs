using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Properties;

namespace Microsoft.AspNet.Identity
{
    public class PasswordValidator : IIdentityValidator<string>
    {
        public virtual bool IsDigit(char c)
        {
            return ((c >= '0') && (c <= '9'));
        }

        public virtual bool IsLetterOrDigit(char c)
        {
            if (!IsUpper(c) && !IsLower(c))
            {
                return IsDigit(c);
            }
            return true;
        }

        public virtual bool IsLower(char c)
        {
            return ((c >= 'a') && (c <= 'z'));
        }

        public virtual bool IsUpper(char c)
        {
            return ((c >= 'A') && (c <= 'Z'));
        }

        public virtual Task<IdentityResult> ValidateAsync(string item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var values = new List<string>();
            if (string.IsNullOrWhiteSpace(item) || (item.Length < RequiredLength))
            {
                values.Add(string.Format(CultureInfo.CurrentCulture, Resources.PasswordTooShort, RequiredLength));
            }
            if (RequireNonLetterOrDigit && item.All(IsLetterOrDigit))
            {
                values.Add(Resources.PasswordRequireNonLetterOrDigit);
            }
            if (RequireDigit && item.All(c => !IsDigit(c)))
            {
                values.Add(Resources.PasswordRequireDigit);
            }
            if (RequireLowercase && item.All(c => !IsLower(c)))
            {
                values.Add(Resources.PasswordRequireLower);
            }
            if (RequireUppercase && item.All(c => !IsUpper(c)))
            {
                values.Add(Resources.PasswordRequireUpper);
            }
            return Task.FromResult(values.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(string.Join(" ", values)));
        }

        public bool RequireDigit { get; set; }

        public int RequiredLength { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireNonLetterOrDigit { get; set; }

        public bool RequireUppercase { get; set; }
    }
}
