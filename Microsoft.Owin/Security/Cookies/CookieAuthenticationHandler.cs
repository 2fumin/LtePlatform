using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;

namespace Microsoft.Owin.Security.Cookies
{
    internal class CookieAuthenticationHandler : AuthenticationHandler<CookieAuthenticationOptions>
    {
        private readonly ILogger _logger;
        private DateTimeOffset _renewExpiresUtc;
        private DateTimeOffset _renewIssuedUtc;
        private string _sessionKey;
        private bool _shouldRenew;
        private const string HeaderNameCacheControl = "Cache-Control";
        private const string HeaderNameExpires = "Expires";
        private const string HeaderNamePragma = "Pragma";
        private const string HeaderValueMinusOne = "-1";
        private const string HeaderValueNoCache = "no-cache";
        private const string SessionIdClaim = "Microsoft.Owin.Security.Cookies-SessionId";

        public CookieAuthenticationHandler(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            _logger = logger;
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if ((Response.StatusCode != 0x191) || !Options.LoginPath.HasValue)
            {
                return Task.FromResult(0);
            }
            var challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);
            try
            {
                if (challenge != null)
                {
                    var redirectUri = challenge.Properties.RedirectUri;
                    if (string.IsNullOrWhiteSpace(redirectUri))
                    {
                        var str2 = (Request.PathBase + Request.Path) + Request.QueryString;
                        redirectUri = string.Concat(Request.Scheme, Uri.SchemeDelimiter, Request.Host, 
                            Request.PathBase, Options.LoginPath, new QueryString(Options.ReturnUrlParameter, str2));
                    }
                    var context = new CookieApplyRedirectContext(Context, Options, redirectUri);
                    Options.Provider.ApplyRedirect(context);
                }
            }
            catch (Exception exception)
            {
                var context2 = new CookieExceptionContext(Context, Options, 
                    CookieExceptionContext.ExceptionLocation.ApplyResponseChallenge, exception, null);
                Options.Provider.Exception(context2);
                if (context2.Rethrow)
                {
                    throw;
                }
            }
            return Task.FromResult<object>(null);
        }

        protected async override Task ApplyResponseGrantAsync()
        {
            var signIn = Helper.LookupSignIn(Options.AuthenticationType);
            var shouldSignin = signIn != null;
            var signOut = Helper.LookupSignOut(Options.AuthenticationType, Options.AuthenticationMode);
            var shouldSignout = signOut != null;
            if ((shouldSignin || shouldSignout) || _shouldRenew)
            {
                var ticket = await AuthenticateAsync();
                try
                {
                    var cookieOptions = new CookieOptions
                    {
                        Domain = Options.CookieDomain,
                        HttpOnly = Options.CookieHttpOnly,
                        Path = Options.CookiePath ?? "/"
                    };
                    if (Options.CookieSecure == CookieSecureOption.SameAsRequest)
                    {
                        cookieOptions.Secure = Request.IsSecure;
                    }
                    else
                    {
                        cookieOptions.Secure = Options.CookieSecure == CookieSecureOption.Always;
                    }
                    if (shouldSignin)
                    {
                        DateTimeOffset utcNow;
                        var signInContext = new CookieResponseSignInContext(Context, Options, 
                            Options.AuthenticationType, signIn.Identity, signIn.Properties, cookieOptions);
                        if (signInContext.Properties.IssuedUtc.HasValue)
                        {
                            utcNow = signInContext.Properties.IssuedUtc.Value;
                        }
                        else
                        {
                            utcNow = Options.SystemClock.UtcNow;
                            signInContext.Properties.IssuedUtc = utcNow;
                        }
                        if (!signInContext.Properties.ExpiresUtc.HasValue)
                        {
                            signInContext.Properties.ExpiresUtc = utcNow.Add(Options.ExpireTimeSpan);
                        }
                        Options.Provider.ResponseSignIn(signInContext);
                        if (signInContext.Properties.IsPersistent)
                        {
                            var expiresUtc = signInContext.Properties.ExpiresUtc;
                            var offset = expiresUtc.GetValueOrDefault();
                            signInContext.CookieOptions.Expires = offset.ToUniversalTime().DateTime;
                        }
                        ticket = new AuthenticationTicket(signInContext.Identity, signInContext.Properties);
                        if (Options.SessionStore != null)
                        {
                            if (_sessionKey != null)
                            {
                                await Options.SessionStore.RemoveAsync(_sessionKey);
                            }
                            var handler2 = this;
                            var introduced36 = await Options.SessionStore.StoreAsync(ticket);
                            handler2._sessionKey = introduced36;
                            var identity = new ClaimsIdentity(new[]
                            {
                                new Claim(SessionIdClaim, _sessionKey)
                            }, Options.AuthenticationType);
                            ticket = new AuthenticationTicket(identity, null);
                        }
                        var cookieValue = Options.TicketDataFormat.Protect(ticket);
                        Options.CookieManager.AppendResponseCookie(Context, Options.CookieName, cookieValue, 
                            signInContext.CookieOptions);
                        var signedInContext = new CookieResponseSignedInContext(Context, Options, 
                            Options.AuthenticationType, signInContext.Identity, signInContext.Properties);
                        Options.Provider.ResponseSignedIn(signedInContext);
                    }
                    else if (shouldSignout)
                    {
                        if ((Options.SessionStore != null) && (_sessionKey != null))
                        {
                            await Options.SessionStore.RemoveAsync(_sessionKey);
                        }
                        var asyncVariable0 = new CookieResponseSignOutContext(Context, Options, cookieOptions);
                        Options.Provider.ResponseSignOut(asyncVariable0);
                        Options.CookieManager.DeleteCookie(Context, Options.CookieName, 
                            asyncVariable0.CookieOptions);
                    }
                    else if (_shouldRenew)
                    {
                        ticket.Properties.IssuedUtc = _renewIssuedUtc;
                        ticket.Properties.ExpiresUtc = _renewExpiresUtc;
                        if ((Options.SessionStore != null) && (_sessionKey != null))
                        {
                            await Options.SessionStore.RenewAsync(_sessionKey, ticket);
                            var identity = new ClaimsIdentity(new[]
                            {
                                new Claim(SessionIdClaim, _sessionKey)
                            }, Options.AuthenticationType);
                            ticket = new AuthenticationTicket(identity, null);
                        }
                        var cookieValue = Options.TicketDataFormat.Protect(ticket);
                        if (ticket.Properties.IsPersistent)
                        {
                            cookieOptions.Expires = _renewExpiresUtc.ToUniversalTime().DateTime;
                        }
                        Options.CookieManager.AppendResponseCookie(Context, Options.CookieName, cookieValue, cookieOptions);
                    }
                    Response.Headers.Set(HeaderNameCacheControl, HeaderValueNoCache);
                    Response.Headers.Set(HeaderNamePragma, HeaderValueNoCache);
                    Response.Headers.Set(HeaderNameExpires, HeaderValueMinusOne);
                    var shouldLoginRedirect = (shouldSignin && Options.LoginPath.HasValue) && (Request.Path == Options.LoginPath);
                    var shouldLogoutRedirect = (shouldSignout && Options.LogoutPath.HasValue) && (Request.Path == Options.LogoutPath);
                    if ((shouldLoginRedirect || shouldLogoutRedirect) && (Response.StatusCode == 200))
                    {
                        var str = Request.Query.Get(Options.ReturnUrlParameter);
                        if (!string.IsNullOrWhiteSpace(str) && IsHostRelative(str))
                        {
                            var context = new CookieApplyRedirectContext(Context, Options, str);
                            Options.Provider.ApplyRedirect(context);
                        }
                    }
                }
                catch (Exception exception)
                {
                    var context2 
                        = new CookieExceptionContext(Context, Options, 
                        CookieExceptionContext.ExceptionLocation.ApplyResponseGrant, exception, ticket);
                    Options.Provider.Exception(context2);
                    if (context2.Rethrow)
                    {
                        throw;
                    }
                }
            }
        }

        protected async override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationTicket ticket = null;
            try
            {
                var requestCookie = Options.CookieManager.GetRequestCookie(Context, Options.CookieName);
                if (string.IsNullOrWhiteSpace(requestCookie))
                {
                    return null;
                }
                ticket = Options.TicketDataFormat.Unprotect(requestCookie);
                if (ticket == null)
                {
                    _logger.WriteWarning("Unprotect ticket failed");
                    return null;
                }
                if (Options.SessionStore != null)
                {
                    var claim = ticket.Identity.Claims.FirstOrDefault(c => c.Type.Equals(SessionIdClaim));
                    if (claim == null)
                    {
                        _logger.WriteWarning("SessoinId missing");
                        return null;
                    }
                    _sessionKey = claim.Value;
                    ticket = await Options.SessionStore.RetrieveAsync(_sessionKey);
                    if (ticket == null)
                    {
                        _logger.WriteWarning("Identity missing in session store");
                        return null;
                    }
                }
                var utcNow = Options.SystemClock.UtcNow;
                var issuedUtc = ticket.Properties.IssuedUtc;
                var expiresUtc = ticket.Properties.ExpiresUtc;
                if (expiresUtc.HasValue && (expiresUtc.Value < utcNow))
                {
                    if (Options.SessionStore != null)
                    {
                        await Options.SessionStore.RemoveAsync(_sessionKey);
                    }
                    return null;
                }
                var allowRefresh = ticket.Properties.AllowRefresh;
                if (((issuedUtc.HasValue && expiresUtc.HasValue) && Options.SlidingExpiration) 
                    && (!allowRefresh.HasValue || allowRefresh.Value))
                {
                    var span = utcNow.Subtract(issuedUtc.Value);
                    if (expiresUtc.Value.Subtract(utcNow) < span)
                    {
                        _shouldRenew = true;
                        _renewIssuedUtc = utcNow;
                        var timeSpan = expiresUtc.Value.Subtract(issuedUtc.Value);
                        _renewExpiresUtc = utcNow.Add(timeSpan);
                    }
                }
                var asyncVariable1 = new CookieValidateIdentityContext(Context, ticket, Options);
                await Options.Provider.ValidateIdentity(asyncVariable1);
                return new AuthenticationTicket(asyncVariable1.Identity, asyncVariable1.Properties);
            }
            catch (Exception exception)
            {
                var context = new CookieExceptionContext(Context, Options, 
                    CookieExceptionContext.ExceptionLocation.AuthenticateAsync, exception, ticket);
                Options.Provider.Exception(context);
                if (context.Rethrow)
                {
                    throw;
                }
                return context.Ticket;
            }
        }

        private static bool IsHostRelative(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            if (path.Length == 1)
            {
                return (path[0] == '/');
            }
            return (((path[0] == '/') && (path[1] != '/')) && (path[1] != '\\'));
        }
        
    }
}
