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
using Abp.Tests.Dependency;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.ModelBuilder.Inspectors;
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

        [Test]
        public void Test_KernelRegister2()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var result = BasedOnDescriptor(context);
            var kernel = new DefaultKernel();
            kernel.Register(result);
        }

        [Test]
        public void Test_KernelRegister3()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var result = BasedOnDescriptor(context);
            var kernel = new DefaultKernel();
            ((IRegistration) result).Register(kernel);
        }

        [Test]
        public void Test_KernelRegister4()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = Classes.FromAssembly(context.Assembly);
            var result = descriptor.BasedOn(new List<Type> {typeof(AbpDbContext)});
            var kernel = new DefaultKernel();
            ((IRegistration)result).Register(kernel);
        }

        [Test]
        public void Test_KernelRegister5()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            var criterias = new List<BasedOnDescriptor> {result};
            var kernel = new DefaultKernel();
            ((IRegistration)result).Register(kernel);
        }

        [Test]
        public void Test_KernelRegister6()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = descriptor.BasedOn(new List<Type> { typeof(AbpDbContext) });
            var kernel = new DefaultKernel();
            Assert.AreEqual(result.FromDescriptor, descriptor);
            ((IRegistration)result).Register(kernel);
        }

        [Test]
        public void Test_KernelRegister9()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = descriptor.BasedOn(new List<Type> { typeof(AbpDbContext) });
            var kernel = new DefaultKernel();
            ((IRegistration)descriptor).Register(kernel);
        }

        [Test]
        public void Test_KernelRegister7()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            var kernel = new DefaultKernel();
            Assert.AreEqual(result.FromDescriptor, descriptor);
            ((IRegistration)result).Register(kernel);
        }

        [Test]
        public void Test_KernelRegister8()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            descriptor.Criterias.Add(result);
            var kernel = new DefaultKernel();
            ((IRegistration)descriptor).Register(kernel);
        }

        [Test]
        public void Test_KernelRegister10()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            descriptor.Criterias.Add(result);
            var kernel = new DefaultKernel();
            result.TryRegister(typeof (AbpDbContext), kernel);
        }

        [Test]
        public void Test_KernelRegister11()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            descriptor.Criterias.Add(result);
            var kernel = new DefaultKernel();
            foreach (var type in descriptor.GetTypes(kernel))
            {
                foreach (var criteria in descriptor.Criterias)
                {
                    if (criteria.TryRegister(type, kernel))
                    {
                        break;
                    }
                }
            }
        }

        [Test]
        public void Test_KernelRegister12()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            descriptor.Criterias.Add(result);
            var kernel = new DefaultKernel();
            var types = descriptor.GetTypes(kernel).ToList();
            foreach (var type in types)
            {
                foreach (var criteria in descriptor.Criterias)
                {
                    try
                    {
                        if (criteria.TryRegister(type, kernel))
                        {
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine(type.Assembly.FullName + @"---" + type.Name);
                        throw new NullReferenceException("aaa");
                    }
                }
            }
        }

        [Test]
        public void Test_KernelRegister13()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            descriptor.Criterias.Add(result);
            var kernel = new DefaultKernel();
            result.TryRegister(typeof(MyDbContext), kernel);
        }

        [Test]
        public void Test_KernelRegister14()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            descriptor.Criterias.Add(result);
            var kernel = new DefaultKernel();
            Type[] baseTypes;
            var type = typeof (MyDbContext);
            Assert.IsTrue(result.Accepts(type, out baseTypes));
            Assert.AreEqual(baseTypes.Length, 1);
            var defaults = CastleComponentAttribute.GetDefaultsFor(type);
            Assert.IsNotNull(defaults);
            var serviceTypes = result.WithService.GetServices(type, baseTypes);
            Assert.AreEqual(serviceTypes.Count, 0);
            Assert.AreEqual(defaults.Services.Length, 1);
            serviceTypes = defaults.Services;
            Assert.AreEqual(serviceTypes.ElementAt(0), typeof(MyDbContext));
            var registration = Component.For(serviceTypes);
            registration.ImplementedBy(type);
            result.Configuration?.Invoke(registration);
            Assert.IsNull(registration.Name);
            Assert.IsNull(defaults.Name);
            registration.RegisterOptionally();
            kernel.Register(registration);
        }

        [Test]
        public void Test_KernelRegister15()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            descriptor.Criterias.Add(result);
            var kernel = new DefaultKernel();
            Type[] baseTypes;
            var type = typeof(MyDbContext);
            Assert.IsTrue(result.Accepts(type, out baseTypes));
            Assert.AreEqual(baseTypes.Length, 1);
            var defaults = CastleComponentAttribute.GetDefaultsFor(type);
            Assert.IsNotNull(defaults);
            var serviceTypes = result.WithService.GetServices(type, baseTypes);
            Assert.AreEqual(serviceTypes.Count, 0);
            Assert.AreEqual(defaults.Services.Length, 1);
            serviceTypes = defaults.Services;
            Assert.AreEqual(serviceTypes.ElementAt(0), typeof(MyDbContext));
            var registration = Component.For(serviceTypes);
            registration.ImplementedBy(type);
            result.Configuration?.Invoke(registration);
            Assert.IsNull(registration.Name);
            Assert.IsNull(defaults.Name);
            registration.RegisterOptionally();

            var token = kernel.OptimizeDependencyResolution();
            ((IRegistration)registration).Register(kernel);
            token?.Dispose();
        }

        [Test]
        public void Test_KernelRegister16()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            descriptor.Criterias.Add(result);
            var kernel = new DefaultKernel();
            Type[] baseTypes;
            var type = typeof(MyDbContext);
            Assert.IsTrue(result.Accepts(type, out baseTypes));
            Assert.AreEqual(baseTypes.Length, 1);
            var defaults = CastleComponentAttribute.GetDefaultsFor(type);
            Assert.IsNotNull(defaults);
            var serviceTypes = result.WithService.GetServices(type, baseTypes);
            Assert.AreEqual(serviceTypes.Count, 0);
            Assert.AreEqual(defaults.Services.Length, 1);
            serviceTypes = defaults.Services;
            Assert.AreEqual(serviceTypes.ElementAt(0), typeof(MyDbContext));
            var registration = Component.For(serviceTypes);
            registration.ImplementedBy(type);
            result.Configuration?.Invoke(registration);
            Assert.IsNull(registration.Name);
            Assert.IsNull(defaults.Name);
            registration.RegisterOptionally();

            var token = kernel.OptimizeDependencyResolution();
            var services = registration.FilterServices(kernel);
            Assert.AreEqual(services.Length, 1);
            Assert.AreEqual(services[0], typeof(MyDbContext));
            var builder = kernel.ComponentModelBuilder;
            var customContributors = registration.GetContributors(services);
            var componentModel = builder.BuildModel(customContributors);
            token?.Dispose();
        }

        [Test]
        public void Test_MyDbContext()
        {
            var targetType = typeof(MyDbContext);
            
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            var properties = targetType.GetProperties(bindingFlags);
            Assert.AreEqual(properties.Length, 7);
            foreach (var property in properties)
            {
                Console.WriteLine(property.Name);
                try
                {
                    var parameters = property.GetIndexParameters();
                }
                catch
                {
                    throw new NullReferenceException(@"GetIndexParameters Fail:" + property.Name);
                }
            }
        }

        [Test]
        public void Test_KernelRegister17()
        {
            var context = new ConventionalRegistrationContext(
                Assembly.GetExecutingAssembly(),
                LocalIocManager,
                new ConventionalRegistrationConfig());
            var descriptor = new FromAssemblyDescriptor(context.Assembly, Classes.MyFilter);
            var result = new BasedOnDescriptor(new List<Type> { typeof(AbpDbContext) }, descriptor, Classes.MyFilter);
            descriptor.Criterias.Add(result);
            var kernel = new DefaultKernel();
            Type[] baseTypes;
            var type = typeof(MyDbContext);
            Assert.IsTrue(result.Accepts(type, out baseTypes));
            Assert.AreEqual(baseTypes.Length, 1);
            Assert.AreEqual(baseTypes[0].Name, "AbpDbContext");
            var defaults = CastleComponentAttribute.GetDefaultsFor(type);
            Assert.IsNotNull(defaults);
            var serviceTypes = result.WithService.GetServices(type, baseTypes);
            Assert.AreEqual(serviceTypes.Count, 0);
            Assert.AreEqual(defaults.Services.Length, 1);
            serviceTypes = defaults.Services;
            Assert.AreEqual(serviceTypes.ElementAt(0), typeof(MyDbContext));
            var registration = Component.For(serviceTypes);
            registration.ImplementedBy(type);
            result.Configuration?.Invoke(registration);
            Assert.IsNull(registration.Name);
            Assert.IsNull(defaults.Name);
            registration.RegisterOptionally();

            var token = kernel.OptimizeDependencyResolution();
            var services = registration.FilterServices(kernel);
            Assert.AreEqual(services.Length, 1);
            Assert.AreEqual(services[0], typeof(MyDbContext));
            var builder = kernel.ComponentModelBuilder;
            var customContributors = registration.GetContributors(services);
            var model = new ComponentModel();

            Array.ForEach(customContributors, c => c.BuildComponentModel(kernel, model));
            Assert.AreEqual(builder.Contributors.Length, 11);
            Assert.AreEqual(customContributors.Length, 2);
            foreach (var construction in builder.Contributors)
            {
                if (construction is PropertiesDependenciesModelInspector)
                {
                    var targetType = model.Implementation;
                    Assert.AreEqual(model.InspectionBehavior, PropertiesInspectionBehavior.Undefined);
                    model.InspectionBehavior =
                            (construction as PropertiesDependenciesModelInspector)
                                .GetInspectionBehaviorFromTheConfiguration(model.Configuration);
                    Assert.AreEqual(model.InspectionBehavior, PropertiesInspectionBehavior.All);
                    var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                    var properties = targetType.GetProperties(bindingFlags);
                    Assert.AreEqual(properties.Length, 7);

                    foreach (var property in properties)
                    {
                        Console.WriteLine(property.Name);
                        try
                        {
                            var canWrite = property.CanWrite;
                        }
                        catch
                        {
                            Console.WriteLine(@"CanWrite Fail, Construction:{0}, Property{1}", construction, property);
                        }
                        try
                        {
                            var parameters = property.GetIndexParameters();
                        }
                        catch
                        {
                            throw new NullReferenceException(@"GetIndexParameters Fail:"+property.Name);
                        }
                        try
                        {
                            var hasAttribute = property.HasAttribute<DoNotWireAttribute>();
                        }
                        catch
                        {
                            Console.WriteLine(@"HasAttribute Fail, Construction:{0}, Property{1}", construction, property);
                        }
                    }
                    break;
                }
            }
            token?.Dispose();
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
