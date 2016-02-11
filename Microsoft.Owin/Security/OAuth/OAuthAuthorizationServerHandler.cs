using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth.Messages;
using Newtonsoft.Json;

namespace Microsoft.Owin.Security.OAuth
{
    internal class OAuthAuthorizationServerHandler : AuthenticationHandler<OAuthAuthorizationServerOptions>
    {
        private AuthorizeEndpointRequest _authorizeEndpointRequest;
        private OAuthValidateClientRedirectUriContext _clientContext;
        private readonly ILogger _logger;

        public OAuthAuthorizationServerHandler(ILogger logger)
        {
            _logger = logger;
        }

        protected override async Task ApplyResponseGrantAsync()
        {
            if (((_clientContext != null) && (_authorizeEndpointRequest != null)) && (Response.StatusCode == 200))
            {
                var signIn = Helper.LookupSignIn(Options.AuthenticationType);
                if (signIn != null)
                {
                    var returnParameter = new Dictionary<string, string>();
                    if (_authorizeEndpointRequest.IsAuthorizationCodeGrantType)
                    {
                        var utcNow = Options.SystemClock.UtcNow;
                        signIn.Properties.IssuedUtc = utcNow;
                        signIn.Properties.ExpiresUtc = utcNow.Add(Options.AuthorizationCodeExpireTimeSpan);
                        signIn.Properties.Dictionary["client_id"] = _authorizeEndpointRequest.ClientId;
                        if (!string.IsNullOrEmpty(_authorizeEndpointRequest.RedirectUri))
                        {
                            signIn.Properties.Dictionary["redirect_uri"] = _authorizeEndpointRequest.RedirectUri;
                        }
                        var context = new AuthenticationTokenCreateContext(Context, Options.AuthorizationCodeFormat,
                            new AuthenticationTicket(signIn.Identity, signIn.Properties));
                        await Options.AuthorizationCodeProvider.CreateAsync(context);
                        var token = context.Token;
                        if (string.IsNullOrEmpty(token))
                        {
                            _logger.WriteError(
                                "response_type code requires an Options.AuthorizationCodeProvider implementing a single-use token.");
                            var validatingContext = new OAuthValidateAuthorizeRequestContext(Context, Options,
                                _authorizeEndpointRequest, _clientContext);
                            validatingContext.SetError("unsupported_response_type");
                            await SendErrorRedirectAsync(_clientContext, validatingContext);
                        }
                        else
                        {
                            var authResponseContext = new OAuthAuthorizationEndpointResponseContext(Context, Options,
                                new AuthenticationTicket(signIn.Identity, signIn.Properties), _authorizeEndpointRequest,
                                null, token);
                            await Options.Provider.AuthorizationEndpointResponse(authResponseContext);
                            using (var enumerator = authResponseContext.AdditionalResponseParameters.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    var current = enumerator.Current;
                                    returnParameter[current.Key] = current.Value.ToString();
                                }
                            }
                            returnParameter["code"] = token;
                            if (!string.IsNullOrEmpty(_authorizeEndpointRequest.State))
                            {
                                returnParameter["state"] = _authorizeEndpointRequest.State;
                            }
                            string uri;
                            if (_authorizeEndpointRequest.IsFormPostResponseMode)
                            {
                                uri = Options.FormPostEndpoint.ToString();
                                returnParameter["redirect_uri"] = _clientContext.RedirectUri;
                            }
                            else
                            {
                                uri = _clientContext.RedirectUri;
                            }
                            var enumerator2 = returnParameter.Keys.GetEnumerator();
                            try
                            {
                                while (enumerator2.MoveNext())
                                {
                                    var name = enumerator2.Current;
                                    uri = WebUtilities.AddQueryString(uri, name, returnParameter[name]);
                                }
                            }
                            finally
                            {
                                enumerator2.Dispose();
                            }
                            Response.Redirect(uri);
                        }
                    }
                    else if (_authorizeEndpointRequest.IsImplicitGrantType)
                    {
                        var redirectUri = _clientContext.RedirectUri;
                        var currentUtc = Options.SystemClock.UtcNow;
                        signIn.Properties.IssuedUtc = currentUtc;
                        signIn.Properties.ExpiresUtc = currentUtc.Add(Options.AccessTokenExpireTimeSpan);
                        signIn.Properties.Dictionary["client_id"] = _authorizeEndpointRequest.ClientId;
                        var accessTokenContext = new AuthenticationTokenCreateContext(Context, Options.AccessTokenFormat,
                            new AuthenticationTicket(signIn.Identity, signIn.Properties));
                        await Options.AccessTokenProvider.CreateAsync(accessTokenContext);
                        var accessToken = accessTokenContext.Token;
                        if (string.IsNullOrEmpty(accessToken))
                        {
                            accessToken = accessTokenContext.SerializeTicket();
                        }
                        var expiresUtc = accessTokenContext.Ticket.Properties.ExpiresUtc;
                        Appender appender = new Appender(redirectUri, '#');
                        appender.Append("access_token", accessToken).Append("token_type", "bearer");
                        if (expiresUtc.HasValue)
                        {
                            var nullable2 = expiresUtc;
                            var offset = currentUtc;
                            var nullable = new TimeSpan?(nullable2.GetValueOrDefault() - offset);
                            appender.Append("expires_in",
                                ((long) (nullable.Value.TotalSeconds + 0.5)).ToString(CultureInfo.InvariantCulture));
                        }
                        if (!string.IsNullOrEmpty(_authorizeEndpointRequest.State))
                        {
                            appender.Append("state", _authorizeEndpointRequest.State);
                        }
                        var authResponseContext = new OAuthAuthorizationEndpointResponseContext(Context, Options,
                            new AuthenticationTicket(signIn.Identity, signIn.Properties), _authorizeEndpointRequest,
                            accessToken, null);
                        await Options.Provider.AuthorizationEndpointResponse(authResponseContext);
                        using (var enumerator3 = authResponseContext.AdditionalResponseParameters.GetEnumerator())
                        {
                            while (enumerator3.MoveNext())
                            {
                                var pair2 = enumerator3.Current;
                                appender.Append(pair2.Key, pair2.Value.ToString());
                            }
                        }
                        Response.Redirect(appender.ToString());
                    }
                }
            }
        }

        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            return Task.FromResult<AuthenticationTicket>(null);
        }

        public override async Task<bool> InvokeAsync()
        {
            var context = new OAuthMatchEndpointContext(Context, Options);
            if (Options.AuthorizeEndpointPath.HasValue && (Options.AuthorizeEndpointPath == Request.Path))
            {
                context.MatchesAuthorizeEndpoint();
            }
            else if (Options.TokenEndpointPath.HasValue && (Options.TokenEndpointPath == Request.Path))
            {
                context.MatchesTokenEndpoint();
            }
            await Options.Provider.MatchEndpoint(context);
            if (context.IsRequestCompleted)
            {
                return true;
            }
            if (context.IsAuthorizeEndpoint || context.IsTokenEndpoint)
            {
                if (!Options.AllowInsecureHttp &&
                    string.Equals(Request.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.WriteWarning(
                        "Authorization server ignoring http request because AllowInsecureHttp is false.", new string[0]);
                    return false;
                }
                if (context.IsAuthorizeEndpoint)
                {
                    return await InvokeAuthorizeEndpointAsync();
                }
                if (context.IsTokenEndpoint)
                {
                    await InvokeTokenEndpointAsync();
                    return true;
                }
            }
            return false;
        }

        private async Task<bool> InvokeAuthorizeEndpointAsync()
        {
            var authorizeRequest = new AuthorizeEndpointRequest(Request.Query);
            var clientContext = new OAuthValidateClientRedirectUriContext(Context, Options, authorizeRequest.ClientId,
                authorizeRequest.RedirectUri);
            if (!string.IsNullOrEmpty(authorizeRequest.RedirectUri))
            {
                Uri validatingUri;
                var acceptableUri = true;
                if (!Uri.TryCreate(authorizeRequest.RedirectUri, UriKind.Absolute, out validatingUri))
                {
                    acceptableUri = false;
                }
                else if (!string.IsNullOrEmpty(validatingUri.Fragment))
                {
                    acceptableUri = false;
                }
                else if (!Options.AllowInsecureHttp &&
                         string.Equals(validatingUri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
                {
                    acceptableUri = false;
                }
                if (!acceptableUri)
                {
                    clientContext.SetError("invalid_request");
                    return await SendErrorRedirectAsync(clientContext, clientContext);
                }
            }
            await Options.Provider.ValidateClientRedirectUri(clientContext);
            if (!clientContext.IsValidated)
            {
                _logger.WriteVerbose("Unable to validate client information");
                return await SendErrorRedirectAsync(clientContext, clientContext);
            }
            var context = new OAuthValidateAuthorizeRequestContext(Context, Options, authorizeRequest, clientContext);
            if (string.IsNullOrEmpty(authorizeRequest.ResponseType))
            {
                _logger.WriteVerbose("Authorize endpoint request missing required response_type parameter");
                context.SetError("invalid_request");
            }
            else if (!authorizeRequest.IsAuthorizationCodeGrantType && !authorizeRequest.IsImplicitGrantType)
            {
                _logger.WriteVerbose("Authorize endpoint request contains unsupported response_type parameter");
                context.SetError("unsupported_response_type");
            }
            else
            {
                await Options.Provider.ValidateAuthorizeRequest(context);
            }
            if (!context.IsValidated)
            {
                return await SendErrorRedirectAsync(clientContext, context);
            }
            _clientContext = clientContext;
            _authorizeEndpointRequest = authorizeRequest;
            var authorizeEndpointContext = new OAuthAuthorizeEndpointContext(Context, Options, authorizeRequest);
            await Options.Provider.AuthorizeEndpoint(authorizeEndpointContext);
            return authorizeEndpointContext.IsRequestCompleted;
        }

        private async Task InvokeTokenEndpointAsync()
        {
            var utcNow = Options.SystemClock.UtcNow;
            utcNow = utcNow.Subtract(TimeSpan.FromMilliseconds(utcNow.Millisecond));
            var parameters = await Request.ReadFormAsync();
            var context = new OAuthValidateClientAuthenticationContext(Context, Options, parameters);
            await Options.Provider.ValidateClientAuthentication(context);
            if (!context.IsValidated)
            {
                _logger.WriteError("clientID is not valid.");
                if (!context.HasError)
                {
                    context.SetError("invalid_client");
                }
                await SendErrorAsJsonAsync(context);
            }
            else
            {
                var tokenRequest = new TokenEndpointRequest(parameters);
                var validatingContext = new OAuthValidateTokenRequestContext(Context, Options, tokenRequest, context);
                AuthenticationTicket ticket = null;
                if (tokenRequest.IsAuthorizationCodeGrantType)
                {
                    ticket = await InvokeTokenEndpointAuthorizationCodeGrantAsync(validatingContext, utcNow);
                }
                else if (tokenRequest.IsResourceOwnerPasswordCredentialsGrantType)
                {
                    ticket =
                        await InvokeTokenEndpointResourceOwnerPasswordCredentialsGrantAsync(validatingContext, utcNow);
                }
                else if (tokenRequest.IsClientCredentialsGrantType)
                {
                    ticket = await InvokeTokenEndpointClientCredentialsGrantAsync(validatingContext, utcNow);
                }
                else if (tokenRequest.IsRefreshTokenGrantType)
                {
                    ticket = await InvokeTokenEndpointRefreshTokenGrantAsync(validatingContext, utcNow);
                }
                else if (tokenRequest.IsCustomExtensionGrantType)
                {
                    ticket = await InvokeTokenEndpointCustomGrantAsync(validatingContext, utcNow);
                }
                else
                {
                    _logger.WriteError("grant type is not recognized");
                    validatingContext.SetError("unsupported_grant_type");
                }
                if (ticket == null)
                {
                    await SendErrorAsJsonAsync(validatingContext);
                }
                else
                {
                    byte[] body;
                    ticket.Properties.IssuedUtc = utcNow;
                    ticket.Properties.ExpiresUtc = utcNow.Add(Options.AccessTokenExpireTimeSpan);
                    var tokenEndpointContext = new OAuthTokenEndpointContext(Context, Options, ticket, tokenRequest);
                    await Options.Provider.TokenEndpoint(tokenEndpointContext);
                    if (tokenEndpointContext.TokenIssued)
                    {
                        ticket = new AuthenticationTicket(tokenEndpointContext.Identity, tokenEndpointContext.Properties);
                    }
                    else
                    {
                        _logger.WriteError("Token was not issued to tokenEndpointContext");
                        validatingContext.SetError("invalid_grant");
                        await SendErrorAsJsonAsync(validatingContext);
                        return;
                    }
                    var accessTokenContext = new AuthenticationTokenCreateContext(Context, Options.AccessTokenFormat,
                        ticket);
                    await Options.AccessTokenProvider.CreateAsync(accessTokenContext);
                    var token = accessTokenContext.Token;
                    if (string.IsNullOrEmpty(token))
                    {
                        token = accessTokenContext.SerializeTicket();
                    }
                    var expiresUtc = ticket.Properties.ExpiresUtc;
                    var refreshTokenCreateContext = new AuthenticationTokenCreateContext(Context,
                        Options.RefreshTokenFormat, accessTokenContext.Ticket);
                    await Options.RefreshTokenProvider.CreateAsync(refreshTokenCreateContext);
                    var refreshToken = refreshTokenCreateContext.Token;
                    var tokenEndpointResponseContext = new OAuthTokenEndpointResponseContext(Context, Options, ticket,
                        tokenRequest, token, tokenEndpointContext.AdditionalResponseParameters);
                    await Options.Provider.TokenEndpointResponse(tokenEndpointResponseContext);
                    var stream = new MemoryStream();
                    using (var writer = new JsonTextWriter(new StreamWriter(stream)))
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("access_token");
                        writer.WriteValue(token);
                        writer.WritePropertyName("token_type");
                        writer.WriteValue("bearer");
                        if (expiresUtc.HasValue)
                        {
                            var nullable2 = expiresUtc;
                            var offset = utcNow;
                            var nullable = new TimeSpan?(nullable2.GetValueOrDefault() - offset);
                            var totalSeconds = (long) nullable.Value.TotalSeconds;
                            if (totalSeconds > 0L)
                            {
                                writer.WritePropertyName("expires_in");
                                writer.WriteValue(totalSeconds);
                            }
                        }
                        if (!string.IsNullOrEmpty(refreshToken))
                        {
                            writer.WritePropertyName("refresh_token");
                            writer.WriteValue(refreshToken);
                        }
                        using (
                            var enumerator = tokenEndpointResponseContext.AdditionalResponseParameters.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                var current = enumerator.Current;
                                writer.WritePropertyName(current.Key);
                                writer.WriteValue(current.Value);
                            }
                        }
                        writer.WriteEndObject();
                        writer.Flush();
                        body = stream.ToArray();
                    }
                    Response.ContentType = "application/json;charset=UTF-8";
                    Response.Headers.Set("Cache-Control", "no-cache");
                    Response.Headers.Set("Pragma", "no-cache");
                    Response.Headers.Set("Expires", "-1");
                    Response.ContentLength = stream.ToArray().Length;
                    await Response.WriteAsync(body, Request.CallCancelled);
                }
            }
        }

        private async Task<AuthenticationTicket> InvokeTokenEndpointAuthorizationCodeGrantAsync(
            OAuthValidateTokenRequestContext validatingContext, DateTimeOffset currentUtc)
        {
            var tokenRequest = validatingContext.TokenRequest;
            var context = new AuthenticationTokenReceiveContext(Context, Options.AuthorizationCodeFormat,
                tokenRequest.AuthorizationCodeGrant.Code);
            await Options.AuthorizationCodeProvider.ReceiveAsync(context);
            var asyncVariable0 = context.Ticket;
            if (asyncVariable0 == null)
            {
                _logger.WriteError("invalid authorization code");
                validatingContext.SetError("invalid_grant");
            }
            else
            {
                if (asyncVariable0.Properties.ExpiresUtc.HasValue)
                {
                    var expiresUtc = asyncVariable0.Properties.ExpiresUtc;
                    if (!((expiresUtc.GetValueOrDefault() < currentUtc)))
                    {
                        string clientId;
                        string redirectUri;
                        if (!asyncVariable0.Properties.Dictionary.TryGetValue("client_id", out clientId) ||
                            !string.Equals(clientId, validatingContext.ClientContext.ClientId, StringComparison.Ordinal))
                        {
                            _logger.WriteError("authorization code does not contain matching client_id");
                            validatingContext.SetError("invalid_grant");
                            return null;
                        }
                        if (asyncVariable0.Properties.Dictionary.TryGetValue("redirect_uri", out redirectUri))
                        {
                            asyncVariable0.Properties.Dictionary.Remove("redirect_uri");
                            if (
                                !string.Equals(redirectUri, tokenRequest.AuthorizationCodeGrant.RedirectUri,
                                    StringComparison.Ordinal))
                            {
                                _logger.WriteError("authorization code does not contain matching redirect_uri");
                                validatingContext.SetError("invalid_grant");
                                return null;
                            }
                        }
                        await Options.Provider.ValidateTokenRequest(validatingContext);
                        var grantContext = new OAuthGrantAuthorizationCodeContext(Context, Options, asyncVariable0);
                        if (validatingContext.IsValidated)
                        {
                            await Options.Provider.GrantAuthorizationCode(grantContext);
                        }
                        return ReturnOutcome(validatingContext, grantContext, grantContext.Ticket, "invalid_grant");
                    }
                }
                _logger.WriteError("expired authorization code");
                validatingContext.SetError("invalid_grant");
            }
            return null;
        }

        private async Task<AuthenticationTicket> InvokeTokenEndpointClientCredentialsGrantAsync(
            OAuthValidateTokenRequestContext validatingContext, DateTimeOffset currentUtc)
        {
            AuthenticationTicket ticket;
            var tokenRequest = validatingContext.TokenRequest;
            await Options.Provider.ValidateTokenRequest(validatingContext);
            if (!validatingContext.IsValidated)
            {
                ticket = null;
            }
            else
            {
                var context = new OAuthGrantClientCredentialsContext(Context, Options,
                    validatingContext.ClientContext.ClientId, tokenRequest.ClientCredentialsGrant.Scope);
                await Options.Provider.GrantClientCredentials(context);
                ticket = ReturnOutcome(validatingContext, context, context.Ticket, "unauthorized_client");
            }
            return ticket;
        }

        private async Task<AuthenticationTicket> InvokeTokenEndpointCustomGrantAsync(
            OAuthValidateTokenRequestContext validatingContext, DateTimeOffset currentUtc)
        {
            var tokenRequest = validatingContext.TokenRequest;
            await Options.Provider.ValidateTokenRequest(validatingContext);
            var context = new OAuthGrantCustomExtensionContext(Context, Options,
                validatingContext.ClientContext.ClientId, tokenRequest.GrantType,
                tokenRequest.CustomExtensionGrant.Parameters);
            if (validatingContext.IsValidated)
            {
                await Options.Provider.GrantCustomExtension(context);
            }
            return ReturnOutcome(validatingContext, context, context.Ticket, "unsupported_grant_type");
        }

        private async Task<AuthenticationTicket> InvokeTokenEndpointRefreshTokenGrantAsync(
            OAuthValidateTokenRequestContext validatingContext, DateTimeOffset currentUtc)
        {
            var tokenRequest = validatingContext.TokenRequest;
            var context = new AuthenticationTokenReceiveContext(Context, Options.RefreshTokenFormat,
                tokenRequest.RefreshTokenGrant.RefreshToken);
            await Options.RefreshTokenProvider.ReceiveAsync(context);
            var asyncVariable0 = context.Ticket;
            if (asyncVariable0 == null)
            {
                _logger.WriteError("invalid refresh token");
                validatingContext.SetError("invalid_grant");
            }
            else
            {
                if (asyncVariable0.Properties.ExpiresUtc.HasValue)
                {
                    var expiresUtc = asyncVariable0.Properties.ExpiresUtc;
                    if (!((expiresUtc.GetValueOrDefault() < currentUtc)))
                    {
                        await Options.Provider.ValidateTokenRequest(validatingContext);
                        var grantContext = new OAuthGrantRefreshTokenContext(Context, Options, asyncVariable0,
                            validatingContext.ClientContext.ClientId);
                        if (validatingContext.IsValidated)
                        {
                            await Options.Provider.GrantRefreshToken(grantContext);
                        }
                        return ReturnOutcome(validatingContext, grantContext, grantContext.Ticket, "invalid_grant");
                    }
                }
                _logger.WriteError("expired refresh token");
                validatingContext.SetError("invalid_grant");
            }
            return null;
        }

        private async Task<AuthenticationTicket> InvokeTokenEndpointResourceOwnerPasswordCredentialsGrantAsync(
            OAuthValidateTokenRequestContext validatingContext, DateTimeOffset currentUtc)
        {
            var tokenRequest = validatingContext.TokenRequest;
            await Options.Provider.ValidateTokenRequest(validatingContext);
            var context = new OAuthGrantResourceOwnerCredentialsContext(Context, Options,
                validatingContext.ClientContext.ClientId, tokenRequest.ResourceOwnerPasswordCredentialsGrant.UserName,
                tokenRequest.ResourceOwnerPasswordCredentialsGrant.Password,
                tokenRequest.ResourceOwnerPasswordCredentialsGrant.Scope);
            if (validatingContext.IsValidated)
            {
                await Options.Provider.GrantResourceOwnerCredentials(context);
            }
            return ReturnOutcome(validatingContext, context, context.Ticket, "invalid_grant");
        }

        private static AuthenticationTicket ReturnOutcome(OAuthValidateTokenRequestContext validatingContext,
            BaseValidatingContext<OAuthAuthorizationServerOptions> grantContext, AuthenticationTicket ticket,
            string defaultError)
        {
            if (!validatingContext.IsValidated)
            {
                return null;
            }
            if (!grantContext.IsValidated)
            {
                if (grantContext.HasError)
                {
                    validatingContext.SetError(grantContext.Error, grantContext.ErrorDescription, grantContext.ErrorUri);
                }
                else
                {
                    validatingContext.SetError(defaultError);
                }
                return null;
            }
            if (ticket == null)
            {
                validatingContext.SetError(defaultError);
                return null;
            }
            return ticket;
        }

        private Task SendErrorAsJsonAsync(BaseValidatingContext<OAuthAuthorizationServerOptions> validatingContext)
        {
            byte[] buffer;
            var str = validatingContext.HasError ? validatingContext.Error : "invalid_request";
            var str2 = validatingContext.HasError ? validatingContext.ErrorDescription : null;
            var str3 = validatingContext.HasError ? validatingContext.ErrorUri : null;
            var stream = new MemoryStream();
            using (var writer = new JsonTextWriter(new StreamWriter(stream)))
            {
                writer.WriteStartObject();
                writer.WritePropertyName("error");
                writer.WriteValue(str);
                if (!string.IsNullOrEmpty(str2))
                {
                    writer.WritePropertyName("error_description");
                    writer.WriteValue(str2);
                }
                if (!string.IsNullOrEmpty(str3))
                {
                    writer.WritePropertyName("error_uri");
                    writer.WriteValue(str3);
                }
                writer.WriteEndObject();
                writer.Flush();
                buffer = stream.ToArray();
            }
            Response.StatusCode = 400;
            Response.ContentType = "application/json;charset=UTF-8";
            Response.Headers.Set("Cache-Control", "no-cache");
            Response.Headers.Set("Pragma", "no-cache");
            Response.Headers.Set("Expires", "-1");
            var length = buffer.Length;
            Response.Headers.Set("Content-Length", length.ToString(CultureInfo.InvariantCulture));
            return Response.WriteAsync(buffer, Request.CallCancelled);
        }

        private async Task<bool> SendErrorPageAsync(string error, string errorDescription, string errorUri)
        {
            byte[] body;
            Response.StatusCode = 400;
            Response.Headers.Set("Cache-Control", "no-cache");
            Response.Headers.Set("Pragma", "no-cache");
            Response.Headers.Set("Expires", "-1");
            if (!Options.ApplicationCanDisplayErrors)
            {
                var stream = new MemoryStream();
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("error: {0}", error);
                    if (!string.IsNullOrEmpty(errorDescription))
                    {
                        writer.WriteLine("error_description: {0}", errorDescription);
                    }
                    if (!string.IsNullOrEmpty(errorUri))
                    {
                        writer.WriteLine("error_uri: {0}", errorUri);
                    }
                    writer.Flush();
                    body = stream.ToArray();
                }
                Response.ContentType = "text/plain;charset=UTF-8";
                var length = body.Length;
                Response.Headers.Set("Content-Length", length.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                Context.Set("oauth.Error", error);
                Context.Set("oauth.ErrorDescription", errorDescription);
                Context.Set("oauth.ErrorUri", errorUri);
                return false;
            }
            await Response.WriteAsync(body, Request.CallCancelled);
            return true;
        }

        private Task<bool> SendErrorRedirectAsync(OAuthValidateClientRedirectUriContext clientContext,
            BaseValidatingContext<OAuthAuthorizationServerOptions> validatingContext)
        {
            if (clientContext == null)
            {
                throw new ArgumentNullException("clientContext");
            }
            var error = validatingContext.HasError ? validatingContext.Error : "invalid_request";
            var errorDescription = validatingContext.HasError ? validatingContext.ErrorDescription : null;
            var errorUri = validatingContext.HasError ? validatingContext.ErrorUri : null;
            if (!clientContext.IsValidated)
            {
                return SendErrorPageAsync(error, errorDescription, errorUri);
            }
            var uri = WebUtilities.AddQueryString(clientContext.RedirectUri, "error", error);
            if (!string.IsNullOrEmpty(errorDescription))
            {
                uri = WebUtilities.AddQueryString(uri, "error_description", errorDescription);
            }
            if (!string.IsNullOrEmpty(errorUri))
            {
                uri = WebUtilities.AddQueryString(uri, "error_uri", errorUri);
            }
            Response.Redirect(uri);
            return Task.FromResult(true);
        }

        private class Appender
        {
            private readonly char _delimiter;
            private bool _hasDelimiter;
            private readonly StringBuilder _sb;

            public Appender(string value, char delimiter)
            {
                _sb = new StringBuilder(value);
                _delimiter = delimiter;
                _hasDelimiter = value.IndexOf(delimiter) != -1;
            }

            public Appender Append(string name, string value)
            {
                _sb.Append(_hasDelimiter ? '&' : _delimiter)
                    .Append(Uri.EscapeDataString(name))
                    .Append('=')
                    .Append(Uri.EscapeDataString(value));
                _hasDelimiter = true;
                return this;
            }

            public override string ToString()
            {
                return _sb.ToString();
            }
        }

    }
}
