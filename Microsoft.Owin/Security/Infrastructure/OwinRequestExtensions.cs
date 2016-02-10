using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Security.Infrastructure
{
    internal static class OwinRequestExtensions
    {
        public static object RegisterAuthenticationHandler(this IOwinRequest request, AuthenticationHandler handler)
        {
            var chained =
                request
                    .Get<Func<string[],
                            Action
                                <IIdentity, IDictionary<string, string>, IDictionary<string, object>, object
                                    >, object, Task>>(
                                        Constants.SecurityAuthenticate);
            var hook = new Hook(handler, chained);
            request
                .Set(Constants.SecurityAuthenticate,
                    new Func
                        <string[],
                            Action
                                <IIdentity, IDictionary<string, string>, IDictionary<string, object>, object
                                    >, object, Task>(hook.AuthenticateAsync));
            return hook;
        }

        public static void UnregisterAuthenticationHandler(this IOwinRequest request, object registration)
        {
            var hook = registration as Hook;
            if (hook == null)
            {
                throw new InvalidOperationException(
                    Resources.Exception_UnhookAuthenticationStateType);
            }
            request
                .Set(Constants.SecurityAuthenticate,
                                hook.Chained);
        }

        private class Hook
        {
            private readonly AuthenticationHandler _handler;

            public Hook(AuthenticationHandler handler,
                Func
                    <string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>,
                        object, Task> chained)
            {
                _handler = handler;
                Chained = chained;
            }

            public async Task AuthenticateAsync(string[] authenticationTypes,
                Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object> callback,
                object state)
            {
                if (authenticationTypes == null)
                {
                    callback(null, null, _handler.BaseOptions.Description.Properties, state);
                }
                else if (authenticationTypes.Contains(_handler.BaseOptions.AuthenticationType,
                    StringComparer.Ordinal))
                {
                    var asyncVariable0 = await _handler.AuthenticateAsync();
                    if (asyncVariable0?.Identity != null)
                    {
                        callback(asyncVariable0.Identity, asyncVariable0.Properties.Dictionary,
                            _handler.BaseOptions.Description.Properties, state);
                    }
                }
                if (Chained != null)
                {
                    await Chained(authenticationTypes, callback, state);
                }
            }

            public
                Func
                    <string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>,
                        object, Task> Chained { get; }

        }
    }
}
