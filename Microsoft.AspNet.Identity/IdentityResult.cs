using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.Properties;

namespace Microsoft.AspNet.Identity
{
    public class IdentityResult
    {
        public IdentityResult(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                errors = new[] { Resources.DefaultError };
            }
            Succeeded = false;
            Errors = errors;
        }

        public IdentityResult(params string[] errors) : this((IEnumerable<string>)errors)
        {
        }

        protected IdentityResult(bool success)
        {
            Succeeded = success;
            Errors = new string[0];
        }

        public IdentityResult()
        {
            Succeeded = false;
            Errors = new string[0];
        }

        public static IdentityResult Failed(params string[] errors)
        {
            return new IdentityResult(errors);
        }

        public IEnumerable<string> Errors { get; private set; }

        public bool Succeeded { get; private set; }

        public static IdentityResult Success { get; } = new IdentityResult(true);

        public void Merge(IdentityResult other)
        {
            Succeeded = Succeeded || other.Succeeded;
            Errors = Errors.Concat(other.Errors);
        }
    }
}
