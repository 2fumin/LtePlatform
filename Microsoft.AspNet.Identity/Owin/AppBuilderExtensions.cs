using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;

namespace Microsoft.AspNet.Identity.Owin
{
    public static class AppBuilderExtensions
    {
        private const string CookiePrefix = ".AspNet.";

        public static IAppBuilder CreatePerOwinContext<T>(this IAppBuilder app, Func<T> createCallback) where T : class, IDisposable
        {
            return app.CreatePerOwinContext<T>(((Func<IdentityFactoryOptions<T>, IOwinContext, T>)((options, context) => createCallback())));
        }

        public static IAppBuilder CreatePerOwinContext<T>(this IAppBuilder app, Func<IdentityFactoryOptions<T>, IOwinContext, T> createCallback) where T : class, IDisposable
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.CreatePerOwinContext<T>(createCallback, delegate (IdentityFactoryOptions<T> options, T instance) {
                instance.Dispose();
            });
        }

        public static IAppBuilder CreatePerOwinContext<T>(this IAppBuilder app, Func<IdentityFactoryOptions<T>, IOwinContext, T> createCallback, Action<IdentityFactoryOptions<T>, T> disposeCallback) where T : class, IDisposable
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (createCallback == null)
            {
                throw new ArgumentNullException(nameof(createCallback));
            }
            if (disposeCallback == null)
            {
                throw new ArgumentNullException(nameof(disposeCallback));
            }
            object[] args = new object[1];
            IdentityFactoryOptions<T> options = new IdentityFactoryOptions<T>
            {
                DataProtectionProvider = app.GetDataProtectionProvider()
            };
            IdentityFactoryProvider<T> provider = new IdentityFactoryProvider<T>
            {
                OnCreate = createCallback,
                OnDispose = disposeCallback
            };
            options.Provider = provider;
            args[0] = options;
            app.Use(typeof(IdentityFactoryMiddleware<T, IdentityFactoryOptions<T>>), args);
            return app;
        }

        public static void UseExternalSignInCookie(this IAppBuilder app)
        {
            app.UseExternalSignInCookie("ExternalCookie");
        }

        public static void UseExternalSignInCookie(this IAppBuilder app, string externalAuthenticationType)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.SetDefaultSignInAsAuthenticationType(externalAuthenticationType);
            CookieAuthenticationOptions options = new CookieAuthenticationOptions
            {
                AuthenticationType = externalAuthenticationType,
                AuthenticationMode = AuthenticationMode.Passive,
                CookieName = CookiePrefix + externalAuthenticationType,
                ExpireTimeSpan = TimeSpan.FromMinutes(5.0)
            };
            app.UseCookieAuthentication(options);
        }

        public static void UseOAuthBearerTokens(this IAppBuilder app, OAuthAuthorizationServerOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            app.UseOAuthAuthorizationServer(options);
            OAuthBearerAuthenticationOptions options2 = new OAuthBearerAuthenticationOptions
            {
                AccessTokenFormat = options.AccessTokenFormat,
                AccessTokenProvider = options.AccessTokenProvider,
                AuthenticationMode = options.AuthenticationMode,
                AuthenticationType = options.AuthenticationType,
                Description = options.Description,
                Provider = new ApplicationOAuthBearerProvider(),
                SystemClock = options.SystemClock
            };
            app.UseOAuthBearerAuthentication(options2);
            OAuthBearerAuthenticationOptions options3 = new OAuthBearerAuthenticationOptions
            {
                AccessTokenFormat = options.AccessTokenFormat,
                AccessTokenProvider = options.AccessTokenProvider,
                AuthenticationMode = AuthenticationMode.Passive,
                AuthenticationType = "ExternalBearer",
                Description = options.Description,
                Provider = new ExternalOAuthBearerProvider(),
                SystemClock = options.SystemClock
            };
            app.UseOAuthBearerAuthentication(options3);
        }

        public static void UseTwoFactorRememberBrowserCookie(this IAppBuilder app, string authenticationType)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            CookieAuthenticationOptions options = new CookieAuthenticationOptions
            {
                AuthenticationType = authenticationType,
                AuthenticationMode = AuthenticationMode.Passive,
                CookieName = ".AspNet." + authenticationType
            };
            app.UseCookieAuthentication(options);
        }

        public static void UseTwoFactorSignInCookie(this IAppBuilder app, string authenticationType, TimeSpan expires)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            CookieAuthenticationOptions options = new CookieAuthenticationOptions
            {
                AuthenticationType = authenticationType,
                AuthenticationMode = AuthenticationMode.Passive,
                CookieName = CookiePrefix + authenticationType,
                ExpireTimeSpan = expires
            };
            app.UseCookieAuthentication(options);
        }

        private class ApplicationOAuthBearerProvider : OAuthBearerAuthenticationProvider
        {
            public override Task ValidateIdentity(OAuthValidateIdentityContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }
                if (context.Ticket.Identity.Claims.Any<Claim>(c => c.Issuer != "LOCAL AUTHORITY"))
                {
                    context.Rejected();
                }
                return Task.FromResult<object>(null);
            }
        }

        private class ExternalOAuthBearerProvider : OAuthBearerAuthenticationProvider
        {
            public override Task ValidateIdentity(OAuthValidateIdentityContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }
                if (context.Ticket.Identity.Claims.Count<Claim>() == 0)
                {
                    context.Rejected();
                }
                else if (context.Ticket.Identity.Claims.All<Claim>(c => c.Issuer == "LOCAL AUTHORITY"))
                {
                    context.Rejected();
                }
                return Task.FromResult<object>(null);
            }
        }
    }
}
