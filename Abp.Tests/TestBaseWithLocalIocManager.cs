using System;
using Abp.Dependency;

namespace Abp.Tests
{
    public abstract class TestBaseWithLocalIocManager : IDisposable
    {
        protected readonly IIocManager LocalIocManager;

        protected TestBaseWithLocalIocManager()
        {
            LocalIocManager = new IocManager();
        }

        public virtual void Dispose()
        {
            LocalIocManager.Dispose();
        }
    }
}