using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;

namespace LtePlatform
{
    public class NinjectDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly List<IDisposable> disposableService = new List<IDisposable>();

        public NinjectDependencyResolver()
        {
            NinjectKernel = new StandardKernel();
            NinjectKernel.AddBindings();
        }

        public NinjectDependencyResolver(NinjectDependencyResolver parent)
        {
            NinjectKernel = parent.NinjectKernel;
        }

        public IKernel NinjectKernel { get; }

        public object GetService(Type serviceType)
        {
            return NinjectKernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            foreach (object service in NinjectKernel.GetAll(serviceType))
            {
                AddDisposableService(service);
                yield return service;
            }
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyResolver(this);
        }

        private void AddDisposableService(object service)
        {
            IDisposable disposable = service as IDisposable;
            if (disposable != null && !disposableService.Contains(disposable))
            {
                disposableService.Add(disposable);
            }
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in disposableService)
            {
                disposable.Dispose();
            }
        }
    }
}
