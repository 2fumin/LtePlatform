using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Cookies
{
    public interface IAuthenticationSessionStore
    {
        Task RemoveAsync(string key);

        Task RenewAsync(string key, AuthenticationTicket ticket);

        Task<AuthenticationTicket> RetrieveAsync(string key);

        Task<string> StoreAsync(AuthenticationTicket ticket);
    }
}
