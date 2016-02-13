using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace System.Web.Http
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class HostAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public HostAuthenticationAttribute(string authenticationType) : this(new HostAuthenticationFilter(authenticationType))
        {
            AuthenticationType = authenticationType;
        }

        internal HostAuthenticationAttribute(IAuthenticationFilter innerFilter)
        {
            if (innerFilter == null)
            {
                throw new ArgumentNullException(nameof(innerFilter));
            }
            InnerFilter = innerFilter;
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            return InnerFilter.AuthenticateAsync(context, cancellationToken);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return InnerFilter.ChallengeAsync(context, cancellationToken);
        }

        public bool AllowMultiple => true;

        public string AuthenticationType { get; }

        internal IAuthenticationFilter InnerFilter { get; }
    }
}
