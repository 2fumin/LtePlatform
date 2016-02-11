using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IIdentityMessageService
    {
        Task SendAsync(IdentityMessage message);
    }
}
