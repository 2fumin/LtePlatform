using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.EntityFramework.Dependency;
using Abp.Tests;
using Castle.MicroKernel.Registration;
using NUnit.Framework;

namespace Abp.EntityFramework.Tests.Repositories
{
    [TestFixture]
    public class SimpleRegister_Test : TestBaseWithLocalIocManager
    {
        [Test]
        public void Test_Assembly()
        {
            var assembly = Assembly.GetExecutingAssembly();
            Assert.IsNotNull(assembly);
            Assert.AreEqual(assembly.FullName, "Abp.EntityFramework.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        }

        [Test]
        public void Test_ConventionalRegistrationContext()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            Assert.IsNotNull(context);
        }

        [Test]
        public void Test_FromAssembly()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = Classes.FromAssembly(context.Assembly);
            Assert.IsNotNull(descriptor);
        }

        [Test]
        public void Test_BasedOn()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = Classes.FromAssembly(context.Assembly).IncludeNonPublicTypes().BasedOn<AbpDbContext>();
            Assert.IsNotNull(descriptor);
            Assert.AreEqual(context.IocManager, LocalIocManager);
            Assert.IsNotNull(context.IocManager.IocContainer);
            Assert.IsNotNull(context.IocManager.IocContainer.Kernel);
        }

        [Test]
        public void Test_LifestyleTransient()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor =
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<AbpDbContext>()
                    .WithServiceSelf()
                    .LifestyleTransient();
            Assert.IsNotNull(descriptor);
            Assert.IsNotNull(descriptor.WithService);
        }

        [Test]
        public void Test_Configure()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor =
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<AbpDbContext>()
                    .WithServiceSelf()
                    .LifestyleTransient();
            var result = descriptor.Configure(c => c.DynamicParameters(
                (kernel, dynamicParams) =>
                {
                    var connectionString = context.IocManager.GetNameOrConnectionStringOrNull();
                    if (!string.IsNullOrWhiteSpace(connectionString))
                    {
                        dynamicParams["nameOrConnectionString"] = connectionString;
                    }
                }));
            Assert.IsNotNull(result);
        }

        [Test]
        public void Test_Register()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var result = BasedOnDescriptor(context);
            context.IocManager.IocContainer.Register(result);
        }

        [Test]
        public void Test_KernelRegister()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var result = BasedOnDescriptor(context);
            var kernel = context.IocManager.IocContainer.Kernel;
            kernel.Register(result);
        }

        private static BasedOnDescriptor BasedOnDescriptor(ConventionalRegistrationContext context)
        {
            var descriptor =
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<AbpDbContext>()
                    .WithServiceSelf()
                    .LifestyleTransient();
            var result = descriptor.Configure(c => c.DynamicParameters(
                (kernel, dynamicParams) =>
                {
                    var connectionString = context.IocManager.GetNameOrConnectionStringOrNull();
                    if (!string.IsNullOrWhiteSpace(connectionString))
                    {
                        dynamicParams["nameOrConnectionString"] = connectionString;
                    }
                }));
            return result;
        }

        [Test]
        public void Test_GetNameOrConnectionStringOrNull()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var connectionString = context.IocManager.GetNameOrConnectionStringOrNull();
            Assert.IsNull(connectionString);
        }

        [Test]
        public void Test_LocalIocManager()
        {
            Assert.IsFalse(LocalIocManager.IsRegistered<IAbpStartupConfiguration>());
        }
    }
}
