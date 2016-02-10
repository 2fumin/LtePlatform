using System;
using System.Threading.Tasks;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Security.Infrastructure
{
    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        public virtual void Create(AuthenticationTokenCreateContext context)
        {
            if ((OnCreateAsync != null) && (OnCreate == null))
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            OnCreate?.Invoke(context);
        }

        public async virtual Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            if ((OnCreateAsync != null) && (OnCreate == null))
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            if (OnCreateAsync != null)
            {
                await OnCreateAsync(context);
            }
            else
            {
                Create(context);
            }
        }

        public virtual void Receive(AuthenticationTokenReceiveContext context)
        {
            if ((OnReceiveAsync != null) && (OnReceive == null))
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            OnReceive?.Invoke(context);
        }

        public async virtual Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            if ((OnReceiveAsync != null) && (OnReceive == null))
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            if (OnReceiveAsync != null)
            {
                await OnReceiveAsync(context);
            }
            else
            {
                Receive(context);
            }
        }

        public Action<AuthenticationTokenCreateContext> OnCreate { get; set; }

        public Func<AuthenticationTokenCreateContext, Task> OnCreateAsync { get; set; }

        public Action<AuthenticationTokenReceiveContext> OnReceive { get; set; }

        public Func<AuthenticationTokenReceiveContext, Task> OnReceiveAsync { get; set; }
        
    }
}
