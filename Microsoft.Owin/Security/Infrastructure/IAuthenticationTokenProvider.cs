using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
    public interface IAuthenticationTokenProvider
    {
        void Create(AuthenticationTokenCreateContext context);

        Task CreateAsync(AuthenticationTokenCreateContext context);

        void Receive(AuthenticationTokenReceiveContext context);

        Task ReceiveAsync(AuthenticationTokenReceiveContext context);
    }
}
