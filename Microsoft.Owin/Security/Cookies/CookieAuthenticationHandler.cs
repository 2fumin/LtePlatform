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
                throw new ArgumentNullException("logger");
            }
            this._logger = logger;
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if ((base.Response.StatusCode != 0x191) || !base.Options.LoginPath.HasValue)
            {
                return Task.FromResult<int>(0);
            }
            AuthenticationResponseChallenge challenge = base.Helper.LookupChallenge(base.Options.AuthenticationType, base.Options.AuthenticationMode);
            try
            {
                if (challenge != null)
                {
                    string redirectUri = challenge.Properties.RedirectUri;
                    if (string.IsNullOrWhiteSpace(redirectUri))
                    {
                        string str2 = (string)((base.Request.PathBase + base.Request.Path) + base.Request.QueryString);
                        redirectUri = string.Concat(new object[] { base.Request.Scheme, Uri.SchemeDelimiter, base.Request.Host, base.Request.PathBase, base.Options.LoginPath, new QueryString(base.Options.ReturnUrlParameter, str2) });
                    }
                    CookieApplyRedirectContext context = new CookieApplyRedirectContext(base.Context, base.Options, redirectUri);
                    base.Options.Provider.ApplyRedirect(context);
                }
            }
            catch (Exception exception)
            {
                CookieExceptionContext context2 = new CookieExceptionContext(base.Context, base.Options, CookieExceptionContext.ExceptionLocation.ApplyResponseChallenge, exception, null);
                base.Options.Provider.Exception(context2);
                if (context2.Rethrow)
                {
                    throw;
                }
            }
            return Task.FromResult<object>(null);
        }

        protected async override Task ApplyResponseGrantAsync()
        {
            AuthenticationResponseGrant signIn = this.Helper.LookupSignIn(this.Options.AuthenticationType);
            bool shouldSignin = signIn != null;
            AuthenticationResponseRevoke signOut = this.Helper.LookupSignOut(this.Options.AuthenticationType, this.Options.AuthenticationMode);
            bool shouldSignout = signOut != null;
            if ((shouldSignin || shouldSignout) || this._shouldRenew)
            {
                AuthenticationTicket ticket = await this.AuthenticateAsync();
                try
                {
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Domain = this.Options.CookieDomain,
                        HttpOnly = this.Options.CookieHttpOnly,
                        Path = this.Options.CookiePath ?? "/"
                    };
                    if (this.Options.CookieSecure == CookieSecureOption.SameAsRequest)
                    {
                        cookieOptions.Secure = this.Request.IsSecure;
                    }
                    else
                    {
                        cookieOptions.Secure = this.Options.CookieSecure == CookieSecureOption.Always;
                    }
                    if (shouldSignin)
                    {
                        DateTimeOffset utcNow;
                        CookieResponseSignInContext signInContext = new CookieResponseSignInContext(this.Context, this.Options, this.Options.AuthenticationType, signIn.Identity, signIn.Properties, cookieOptions);
                        if (signInContext.Properties.IssuedUtc.HasValue)
                        {
                            utcNow = signInContext.Properties.IssuedUtc.Value;
                        }
                        else
                        {
                            utcNow = this.Options.SystemClock.UtcNow;
                            signInContext.Properties.IssuedUtc = new DateTimeOffset?(utcNow);
                        }
                        if (!signInContext.Properties.ExpiresUtc.HasValue)
                        {
                            signInContext.Properties.ExpiresUtc = new DateTimeOffset?(utcNow.Add(this.Options.ExpireTimeSpan));
                        }
                        this.Options.Provider.ResponseSignIn(signInContext);
                        if (signInContext.Properties.IsPersistent)
                        {
                            DateTimeOffset? expiresUtc = signInContext.Properties.ExpiresUtc;
                            DateTimeOffset offset = expiresUtc.HasValue ? expiresUtc.GetValueOrDefault() : utcNow.Add(this.Options.ExpireTimeSpan);
                            signInContext.CookieOptions.Expires = new DateTime?(offset.ToUniversalTime().DateTime);
                        }
                        ticket = new AuthenticationTicket(signInContext.Identity, signInContext.Properties);
                        if (this.Options.SessionStore != null)
                        {
                            if (this._sessionKey != null)
                            {
                                await this.Options.SessionStore.RemoveAsync(this._sessionKey);
                            }
                            CookieAuthenticationHandler handler2 = this;
                            string introduced36 = await this.Options.SessionStore.StoreAsync(ticket);
                            handler2._sessionKey = introduced36;
                            ClaimsIdentity identity = new ClaimsIdentity(new Claim[] { new Claim("Microsoft.Owin.Security.Cookies-SessionId", this._sessionKey) }, this.Options.AuthenticationType);
                            ticket = new AuthenticationTicket(identity, null);
                        }
                        string cookieValue = this.Options.TicketDataFormat.Protect(ticket);
                        this.Options.CookieManager.AppendResponseCookie(this.Context, this.Options.CookieName, cookieValue, signInContext.CookieOptions);
                        CookieResponseSignedInContext signedInContext = new CookieResponseSignedInContext(this.Context, this.Options, this.Options.AuthenticationType, signInContext.Identity, signInContext.Properties);
                        this.Options.Provider.ResponseSignedIn(signedInContext);
                    }
                    else if (shouldSignout)
                    {
                        if ((this.Options.SessionStore != null) && (this._sessionKey != null))
                        {
                            await this.Options.SessionStore.RemoveAsync(this._sessionKey);
                        }
                        CookieResponseSignOutContext asyncVariable0 = new CookieResponseSignOutContext(this.Context, this.Options, cookieOptions);
                        this.Options.Provider.ResponseSignOut(asyncVariable0);
                        this.Options.CookieManager.DeleteCookie(this.Context, this.Options.CookieName, asyncVariable0.CookieOptions);
                    }
                    else if (this._shouldRenew)
                    {
                        ticket.Properties.IssuedUtc = new DateTimeOffset?(this._renewIssuedUtc);
                        ticket.Properties.ExpiresUtc = new DateTimeOffset?(this._renewExpiresUtc);
                        if ((this.Options.SessionStore != null) && (this._sessionKey != null))
                        {
                            await this.Options.SessionStore.RenewAsync(this._sessionKey, ticket);
                            ClaimsIdentity identity = new ClaimsIdentity(new Claim[] { new Claim("Microsoft.Owin.Security.Cookies-SessionId", this._sessionKey) }, this.Options.AuthenticationType);
                            ticket = new AuthenticationTicket(identity, null);
                        }
                        string cookieValue = this.Options.TicketDataFormat.Protect(ticket);
                        if (ticket.Properties.IsPersistent)
                        {
                            cookieOptions.Expires = new DateTime?(this._renewExpiresUtc.ToUniversalTime().DateTime);
                        }
                        this.Options.CookieManager.AppendResponseCookie(this.Context, this.Options.CookieName, cookieValue, cookieOptions);
                    }
                    this.Response.Headers.Set("Cache-Control", "no-cache");
                    this.Response.Headers.Set("Pragma", "no-cache");
                    this.Response.Headers.Set("Expires", "-1");
                    bool shouldLoginRedirect = (shouldSignin && this.Options.LoginPath.HasValue) && (this.Request.Path == this.Options.LoginPath);
                    bool shouldLogoutRedirect = (shouldSignout && this.Options.LogoutPath.HasValue) && (this.Request.Path == this.Options.LogoutPath);
                    if ((shouldLoginRedirect || shouldLogoutRedirect) && (this.Response.StatusCode == 200))
                    {
                        string str = this.Request.Query.Get(this.Options.ReturnUrlParameter);
                        if (!string.IsNullOrWhiteSpace(str) && IsHostRelative(str))
                        {
                            CookieApplyRedirectContext context = new CookieApplyRedirectContext(this.Context, this.Options, str);
                            this.Options.Provider.ApplyRedirect(context);
                        }
                    }
                }
                catch (Exception exception)
                {
                    CookieExceptionContext context2 = new CookieExceptionContext(this.Context, this.Options, CookieExceptionContext.ExceptionLocation.ApplyResponseGrant, exception, ticket);
                    this.Options.Provider.Exception(context2);
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
                string requestCookie = this.Options.CookieManager.GetRequestCookie(this.Context, this.Options.CookieName);
                if (string.IsNullOrWhiteSpace(requestCookie))
                {
                    return null;
                }
                ticket = this.Options.TicketDataFormat.Unprotect(requestCookie);
                if (ticket == null)
                {
                    this._logger.WriteWarning("Unprotect ticket failed", new string[0]);
                    return null;
                }
                if (this.Options.SessionStore != null)
                {
                    Claim claim = ticket.Identity.Claims.FirstOrDefault<Claim>(c => c.Type.Equals("Microsoft.Owin.Security.Cookies-SessionId"));
                    if (claim == null)
                    {
                        this._logger.WriteWarning("SessoinId missing", new string[0]);
                        return null;
                    }
                    this._sessionKey = claim.Value;
                    ticket = await this.Options.SessionStore.RetrieveAsync(this._sessionKey);
                    if (ticket == null)
                    {
                        this._logger.WriteWarning("Identity missing in session store", new string[0]);
                        return null;
                    }
                }
                DateTimeOffset utcNow = this.Options.SystemClock.UtcNow;
                DateTimeOffset? issuedUtc = ticket.Properties.IssuedUtc;
                DateTimeOffset? expiresUtc = ticket.Properties.ExpiresUtc;
                if (expiresUtc.HasValue && (expiresUtc.Value < utcNow))
                {
                    if (this.Options.SessionStore != null)
                    {
                        await this.Options.SessionStore.RemoveAsync(this._sessionKey);
                    }
                    return null;
                }
                bool? allowRefresh = ticket.Properties.AllowRefresh;
                if (((issuedUtc.HasValue && expiresUtc.HasValue) && this.Options.SlidingExpiration) && (!allowRefresh.HasValue || allowRefresh.Value))
                {
                    TimeSpan span = utcNow.Subtract(issuedUtc.Value);
                    if (expiresUtc.Value.Subtract(utcNow) < span)
                    {
                        this._shouldRenew = true;
                        this._renewIssuedUtc = utcNow;
                        TimeSpan timeSpan = expiresUtc.Value.Subtract(issuedUtc.Value);
                        this._renewExpiresUtc = utcNow.Add(timeSpan);
                    }
                }
                CookieValidateIdentityContext asyncVariable1 = new CookieValidateIdentityContext(this.Context, ticket, this.Options);
                await this.Options.Provider.ValidateIdentity(asyncVariable1);
                return new AuthenticationTicket(asyncVariable1.Identity, asyncVariable1.Properties);
            }
            catch (Exception exception)
            {
                CookieExceptionContext context = new CookieExceptionContext(this.Context, this.Options, CookieExceptionContext.ExceptionLocation.AuthenticateAsync, exception, ticket);
                this.Options.Provider.Exception(context);
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
