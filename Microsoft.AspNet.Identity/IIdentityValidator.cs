using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IIdentityValidator<in T>
    {
        Task<IdentityResult> ValidateAsync(T item);
    }
}
