using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.Owin.Security.Infrastructure
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SecurityHelper
    {
        private readonly IOwinContext _context;
        public SecurityHelper(IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public void AddUserIdentity(IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var principal = new ClaimsPrincipal(identity);
            var user = _context.Request.User;
            if (user != null)
            {
                var principal3 = user as ClaimsPrincipal;
                if (principal3 == null)
                {
                    var identity2 = user.Identity;
                    if (identity2.IsAuthenticated)
                    {
                        principal.AddIdentity((identity2 as ClaimsIdentity) ?? new ClaimsIdentity(identity2));
                    }
                }
                else
                {
                    foreach (var identity3 in principal3.Identities)
                    {
                        if (identity3.IsAuthenticated)
                        {
                            principal.AddIdentity(identity3);
                        }
                    }
                }
            }
            _context.Request.User = principal;
        }

        public AuthenticationResponseChallenge LookupChallenge(string authenticationType, AuthenticationMode authenticationMode)
        {
            if (authenticationType == null)
            {
                throw new ArgumentNullException(nameof(authenticationType));
            }
            var authenticationResponseChallenge = _context.Authentication.AuthenticationResponseChallenge;
            if (authenticationResponseChallenge?.AuthenticationTypes != null &&
                (authenticationResponseChallenge.AuthenticationTypes.Length != 0))
                return
                    authenticationResponseChallenge.AuthenticationTypes.Any(
                        str => string.Equals(str, authenticationType, StringComparison.Ordinal))
                        ? authenticationResponseChallenge
                        : null;
            if (authenticationMode != AuthenticationMode.Active)
            {
                return null;
            }
            return (authenticationResponseChallenge ?? new AuthenticationResponseChallenge(null, null));
        }

        public AuthenticationResponseGrant LookupSignIn(string authenticationType)
        {
            if (authenticationType == null)
            {
                throw new ArgumentNullException(nameof(authenticationType));
            }
            var authenticationResponseGrant = _context.Authentication.AuthenticationResponseGrant;
            return authenticationResponseGrant != null
                ? (from identity in authenticationResponseGrant.Principal.Identities
                    where string.Equals(authenticationType, identity.AuthenticationType, StringComparison.Ordinal)
                    select
                        new AuthenticationResponseGrant(identity,
                            authenticationResponseGrant.Properties ?? new AuthenticationProperties())).FirstOrDefault()
                : null;
        }

        public AuthenticationResponseRevoke LookupSignOut(string authenticationType, AuthenticationMode authenticationMode)
        {
            if (authenticationType == null)
            {
                throw new ArgumentNullException(nameof(authenticationType));
            }
            var authenticationResponseRevoke = _context.Authentication.AuthenticationResponseRevoke;
            if (authenticationResponseRevoke == null) return null;
            if ((authenticationResponseRevoke.AuthenticationTypes == null) || (authenticationResponseRevoke.AuthenticationTypes.Length == 0))
            {
                return authenticationMode != AuthenticationMode.Active ? null : authenticationResponseRevoke;
            }
            for (var i = 0; i != authenticationResponseRevoke.AuthenticationTypes.Length; i++)
            {
                if (string.Equals(authenticationType, authenticationResponseRevoke.AuthenticationTypes[i], StringComparison.Ordinal))
                {
                    return authenticationResponseRevoke;
                }
            }
            return null;
        }

        public bool Equals(SecurityHelper other)
        {
            return Equals(_context, other._context);
        }

        public override bool Equals(object obj)
        {
            return ((obj is SecurityHelper) && Equals((SecurityHelper)obj));
        }

        public override int GetHashCode()
        {
            return _context?.GetHashCode() ?? 0;
        }

        public static bool operator ==(SecurityHelper left, SecurityHelper right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SecurityHelper left, SecurityHelper right)
        {
            return !left.Equals(right);
        }
    }
}
