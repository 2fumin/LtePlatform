using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Microsoft.Owin.Security;

namespace System.Web.Http
{
    public class HostAuthenticationFilter : IAuthenticationFilter
    {
        public HostAuthenticationFilter(string authenticationType)
        {
            if (authenticationType == null)
            {
                throw new ArgumentNullException(nameof(authenticationType));
            }
            AuthenticationType = authenticationType;
        }

        private static AuthenticationResponseChallenge AddChallengeAuthenticationType(
            AuthenticationResponseChallenge challenge, string authenticationType)
        {
            AuthenticationProperties properties;
            var list = new List<string>();
            if (challenge != null)
            {
                var authenticationTypes = challenge.AuthenticationTypes;
                if (authenticationTypes != null)
                {
                    list.AddRange(authenticationTypes);
                }
                properties = challenge.Properties;
            }
            else
            {
                properties = new AuthenticationProperties();
            }
            list.Add(authenticationType);
            return new AuthenticationResponseChallenge(list.ToArray(), properties);
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var request = context.Request;
            if (request == null)
            {
                throw new InvalidOperationException(Owin.Properties.Resources.HttpAuthenticationContext_RequestMustNotBeNull);
            }
            var authenticationManagerOrThrow = GetAuthenticationManagerOrThrow(request);
            cancellationToken.ThrowIfCancellationRequested();
            var asyncVariable0 =
                await authenticationManagerOrThrow.AuthenticateAsync(AuthenticationType);
            IIdentity identity = asyncVariable0?.Identity;
            if (identity != null)
            {
                context.Principal = new ClaimsPrincipal(identity);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var request = context.Request;
            if (request == null)
            {
                throw new InvalidOperationException(
                    Owin.Properties.Resources.HttpAuthenticationChallengeContext_RequestMustNotBeNull);
            }
            var authenticationManagerOrThrow = GetAuthenticationManagerOrThrow(request);
            authenticationManagerOrThrow.AuthenticationResponseChallenge =
                AddChallengeAuthenticationType(authenticationManagerOrThrow.AuthenticationResponseChallenge,
                    AuthenticationType);
            return TaskHelpers.Completed();
        }

        private static IAuthenticationManager GetAuthenticationManagerOrThrow(HttpRequestMessage request)
        {
            IAuthenticationManager authenticationManager = request.GetAuthenticationManager();
            if (authenticationManager == null)
            {
                throw new InvalidOperationException(Owin.Properties.Resources.IAuthenticationManagerNotAvailable);
            }
            return authenticationManager;
        }

        public bool AllowMultiple => true;

        public string AuthenticationType { get; }
    }
}
