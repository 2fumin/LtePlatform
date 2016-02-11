using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Properties;

namespace Microsoft.AspNet.Identity
{
    public class UserValidator<TUser> : UserValidator<TUser, string> where TUser : class, IUser<string>
    {
        public UserValidator(UserManager<TUser, string> manager) : base(manager)
        {
        }
    }

    public class UserValidator<TUser, TKey> : IIdentityValidator<TUser> where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        public UserValidator(UserManager<TUser, TKey> manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            AllowOnlyAlphanumericUserNames = true;
            Manager = manager;
        }

        public virtual async Task<IdentityResult> ValidateAsync(TUser item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var errors = new List<string>();
            await ValidateUserName(item, errors).WithCurrentCulture();
            if (RequireUniqueEmail)
            {
                await ValidateEmailAsync(item, errors).WithCurrentCulture();
            }
            var success = errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
            return success;
        }

        private async Task ValidateEmailAsync(TUser user, ICollection<string> errors)
        {
            var email =
                await Manager.GetEmailStore().GetEmailAsync(user).WithCurrentCulture();
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, "Email"));
            }
            else
            {
                try
                {
                    new MailAddress(email);
                }
                catch (FormatException)
                {
                    errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.InvalidEmail, email));
                    return;
                }
                var owner =
                    await
                        Manager.FindByEmailAsync(email).WithCurrentCulture();
                if ((owner != null) && !EqualityComparer<TKey>.Default.Equals(owner.Id, user.Id))
                {
                    errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.DuplicateEmail, email));
                }
            }
        }

        private async Task ValidateUserName(TUser user, List<string> errors)
        {
            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                if (!AllowOnlyAlphanumericUserNames ||
                    Regex.IsMatch(user.UserName, @"^[A-Za-z0-9@_\.]+$"))
                {
                    var owner = await
                        Manager.FindByNameAsync(user.UserName)
                            .WithCurrentCulture();
                    if ((owner != null) && !EqualityComparer<TKey>.Default.Equals(owner.Id, user.Id))
                    {
                        errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.DuplicateName, user.UserName));
                    }
                }
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.InvalidUserName, user.UserName));
            }
            else
            {
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, "Name"));
            }
            
        }

        public bool AllowOnlyAlphanumericUserNames { get; set; }

        private UserManager<TUser, TKey> Manager { get; }

        public bool RequireUniqueEmail { get; set; }
    }
}