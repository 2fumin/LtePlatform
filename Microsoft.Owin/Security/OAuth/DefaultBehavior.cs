using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
    internal static class DefaultBehavior
    {
        internal static readonly Func<OAuthGrantAuthorizationCodeContext, Task> GrantAuthorizationCode;
        internal static readonly Func<OAuthGrantRefreshTokenContext, Task> GrantRefreshToken;
        internal static readonly Func<OAuthValidateAuthorizeRequestContext, Task> ValidateAuthorizeRequest;
        internal static readonly Func<OAuthValidateTokenRequestContext, Task> ValidateTokenRequest;

        static DefaultBehavior()
        {
            ValidateAuthorizeRequest = delegate (OAuthValidateAuthorizeRequestContext context) {
                context.Validated();
                return Task.FromResult<object>(null);
            };
            ValidateTokenRequest = delegate (OAuthValidateTokenRequestContext context) {
                context.Validated();
                return Task.FromResult<object>(null);
            };
            GrantAuthorizationCode = delegate (OAuthGrantAuthorizationCodeContext context) {
                if (context.Ticket?.Identity != null && context.Ticket.Identity.IsAuthenticated)
                {
                    context.Validated();
                }
                return Task.FromResult<object>(null);
            };
            GrantRefreshToken = delegate (OAuthGrantRefreshTokenContext context) {
                if (context.Ticket?.Identity != null && context.Ticket.Identity.IsAuthenticated)
                {
                    context.Validated();
                }
                return Task.FromResult<object>(null);
            };
        }
    }
}
