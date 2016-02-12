using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Http.Owin
{
    using Microsoft.Owin.Security;
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Owin.Properties;

    public class PassiveAuthenticationMessageHandler : DelegatingHandler
    {
        private static readonly Lazy<IPrincipal> _anonymousPrincipal =
            new Lazy<IPrincipal>(() => new ClaimsPrincipal(new ClaimsIdentity()), true);
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            SetCurrentPrincipalToAnonymous(request);
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            SuppressDefaultAuthenticationChallenges(request);
            return response;
        }

        private static void SetCurrentPrincipalToAnonymous(HttpRequestMessage request)
        {
            HttpRequestContext requestContext = request.GetRequestContext();
            if (requestContext == null)
            {
                throw new ArgumentException(Resources.Request_RequestContextMustNotBeNull, "request");
            }
            requestContext.Principal = _anonymousPrincipal.Value;
        }

        private static void SuppressDefaultAuthenticationChallenges(HttpRequestMessage request)
        {
            IAuthenticationManager authenticationManager = request.GetAuthenticationManager();
            if (authenticationManager == null)
            {
                throw new InvalidOperationException(Resources.IAuthenticationManagerNotAvailable);
            }
            AuthenticationResponseChallenge authenticationResponseChallenge =
                authenticationManager.AuthenticationResponseChallenge;
            string[] authenticationTypes = new string[1];
            if (authenticationResponseChallenge == null)
            {
                authenticationManager.AuthenticationResponseChallenge =
                    new AuthenticationResponseChallenge(authenticationTypes, new AuthenticationProperties());
            }
            else if ((authenticationResponseChallenge.AuthenticationTypes == null) ||
                     (authenticationResponseChallenge.AuthenticationTypes.Length == 0))
            {
                authenticationManager.AuthenticationResponseChallenge =
                    new AuthenticationResponseChallenge(authenticationTypes,
                        authenticationResponseChallenge.Properties);
            }
        }

    }
}
