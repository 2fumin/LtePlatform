using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Microsoft.Owin.Security.Infrastructure
{
    public abstract class AuthenticationHandler
    {
        private Task _applyResponse;
        private bool _applyResponseInitialized;
        private object _applyResponseSyncLock;
        private Task<AuthenticationTicket> _authenticate;
        private bool _authenticateInitialized;
        private object _authenticateSyncLock;
        private AuthenticationOptions _baseOptions;
        private object _registration;
        private static readonly RNGCryptoServiceProvider Random = new RNGCryptoServiceProvider();

        protected AuthenticationHandler()
        {
        }

        private async Task ApplyResponseAsync()
        {
            try
            {
                if (!Faulted)
                {
                    await
                        LazyInitializer.EnsureInitialized(ref _applyResponse, ref _applyResponseInitialized,
                            ref _applyResponseSyncLock, ApplyResponseCoreAsync);
                }
            }
            catch (Exception)
            {
                Faulted = true;
                throw;
            }
        }

        protected virtual Task ApplyResponseChallengeAsync()
        {
            return Task.FromResult<object>(null);
        }

        protected async virtual Task ApplyResponseCoreAsync()
        {
            await ApplyResponseGrantAsync();
            await ApplyResponseChallengeAsync();
        }

        protected virtual Task ApplyResponseGrantAsync()
        {
            return Task.FromResult<object>(null);
        }

        public Task<AuthenticationTicket> AuthenticateAsync()
        {
            return LazyInitializer.EnsureInitialized(ref _authenticate, ref _authenticateInitialized,
                ref _authenticateSyncLock, AuthenticateCoreAsync);
        }

        protected abstract Task<AuthenticationTicket> AuthenticateCoreAsync();
        protected async Task BaseInitializeAsync(AuthenticationOptions options, IOwinContext context)
        {
            _baseOptions = options;
            Context = context;
            Helper = new SecurityHelper(context);
            RequestPathBase = Request.PathBase;
            _registration = Request.RegisterAuthenticationHandler(this);
            Response.OnSendingHeaders(OnSendingHeaderCallback, this);
            await InitializeCoreAsync();
            if (BaseOptions.AuthenticationMode == AuthenticationMode.Active)
            {
                AuthenticationTicket asyncVariable0 = await AuthenticateAsync();
                if ((asyncVariable0 != null) && (asyncVariable0.Identity != null))
                {
                    Helper.AddUserIdentity(asyncVariable0.Identity);
                }
            }
        }

        protected void GenerateCorrelationId(AuthenticationProperties properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }
            string key = ".AspNet.Correlation." + BaseOptions.AuthenticationType;
            byte[] data = new byte[0x20];
            Random.GetBytes(data);
            string str2 = TextEncodings.Base64Url.Encode(data);
            CookieOptions options = new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsSecure
            };
            properties.Dictionary[key] = str2;
            Response.Cookies.Append(key, str2, options);
        }

        protected virtual Task InitializeCoreAsync()
        {
            return Task.FromResult<object>(null);
        }

        public virtual Task<bool> InvokeAsync()
        {
            return Task.FromResult(false);
        }

        private static void OnSendingHeaderCallback(object state)
        {
            AuthenticationHandler handler = (AuthenticationHandler)state;
            handler.ApplyResponseAsync().Wait();
        }

        internal async Task TeardownAsync()
        {
            await ApplyResponseAsync();
            await TeardownCoreAsync();
            Request.UnregisterAuthenticationHandler(_registration);
        }

        protected virtual Task TeardownCoreAsync()
        {
            return Task.FromResult<object>(null);
        }

        protected bool ValidateCorrelationId(AuthenticationProperties properties, ILogger logger)
        {
            string str3;
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }
            string key = ".AspNet.Correlation." + BaseOptions.AuthenticationType;
            string str2 = Request.Cookies[key];
            if (string.IsNullOrWhiteSpace(str2))
            {
                logger.WriteWarning("{0} cookie not found.", new string[] { key });
                return false;
            }
            CookieOptions options = new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsSecure
            };
            Response.Cookies.Delete(key, options);
            if (!properties.Dictionary.TryGetValue(key, out str3))
            {
                logger.WriteWarning("{0} state property not found.", new string[] { key });
                return false;
            }
            properties.Dictionary.Remove(key);
            if (!string.Equals(str2, str3, StringComparison.Ordinal))
            {
                logger.WriteWarning("{0} correlation cookie and state property mismatch.", new string[] { key });
                return false;
            }
            return true;
        }

        internal AuthenticationOptions BaseOptions
        {
            get
            {
                return _baseOptions;
            }
        }

        protected IOwinContext Context { get; private set; }

        protected bool Faulted { get; set; }

        protected SecurityHelper Helper { get; private set; }

        protected IOwinRequest Request
        {
            get
            {
                return Context.Request;
            }
        }

        protected PathString RequestPathBase { get; private set; }

        protected IOwinResponse Response
        {
            get
            {
                return Context.Response;
            }
        }
    }
}
