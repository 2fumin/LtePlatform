using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Properties;

namespace Microsoft.AspNet.Identity
{
    public class MinimumLengthValidator : IIdentityValidator<string>
    {
        public MinimumLengthValidator(int requiredLength)
        {
            RequiredLength = requiredLength;
        }

        public virtual Task<IdentityResult> ValidateAsync(string item)
        {
            if (!string.IsNullOrWhiteSpace(item) && (item.Length >= RequiredLength))
            {
                return Task.FromResult(IdentityResult.Success);
            }
            return
                Task.FromResult(
                    IdentityResult.Failed(string.Format(CultureInfo.CurrentCulture, Resources.PasswordTooShort,
                        RequiredLength)));
        }

        public int RequiredLength { get; set; }
    }
}
