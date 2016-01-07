using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.MicroKernel;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.MicroKernel.SubSystems.Conversion;
using Castle.Services.Logging.NLogIntegration;
using Castle.Windsor;
using NUnit.Framework;

namespace Castle.Facilities.Logging.Tests.Classes
{
    [TestFixture]
    public class MyFacilityTests : BaseTest
    {
        [Test]
        public void Test_CreateNLogImplementation()
        {
            var container = CreateConfiguredContainer(LoggerImplementation.NLog);
        }

        [Test]
        public void Test_CreateConfiguredContainer()
        {
            IWindsorContainer container = new WindsorContainer(new DefaultConfigurationStore());
            var configFile = GetConfigFile(LoggerImplementation.NLog);
            Assert.AreEqual(configFile, "LoggingFacility\\NLog.facilities.test.config");

            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.NLog).WithConfig(configFile));
        }

        [Test]
        public void Test_AddFacility()
        {
            var container = new WindsorContainer(new DefaultConfigurationStore());
            container.AddFacility<LoggingFacility>(
                f => f.LogUsing(LoggerImplementation.NLog).WithConfig("LoggingFacility\\NLog.facilities.test.config"));
        }

        [Test]
        public void Test_Kernel_AddFacility()
        {
            var kernel = new DefaultKernel();
            kernel.AddFacility<LoggingFacility>(
                f => f.LogUsing(LoggerImplementation.NLog).WithConfig("LoggingFacility\\NLog.facilities.test.config"));
        }

        [Test]
        public void Test_DefaultKernel_AddFacility()
        {
            var facility = new LoggingFacility();
            facility.LogUsing(LoggerImplementation.NLog).WithConfig("LoggingFacility\\NLog.facilities.test.config");
            Assert.AreEqual(facility.GetType().FullName, "Castle.Facilities.Logging.LoggingFacility");
            var kernel = new DefaultKernel();
            kernel.AddFacility(facility);
        }

        [Test]
        public void Test_DefaultKernel_AddFacility2()
        {
            var facility = new LoggingFacility();
            facility.LogUsing(LoggerImplementation.NLog).WithConfig("LoggingFacility\\NLog.facilities.test.config");
            var kernel = new DefaultKernel();
#pragma warning disable 612, 618
            kernel.AddFacility("Castle.Facilities.Logging.LoggingFacility", facility);
#pragma warning restore 612, 618
        }

        [Test]
        public void Test_DefaultKernel_AddFacility3()
        {
            var facility = new LoggingFacility();
            facility.LogUsing(LoggerImplementation.NLog).WithConfig("LoggingFacility\\NLog.facilities.test.config");
            var kernel = new DefaultKernel();

            var configuration =
                kernel.ConfigurationStore.GetFacilityConfiguration("LoggingFacility\\NLog.facilities.test.config");
            facility.SetKerenlAndConfig(kernel, configuration);
            facility.SetUpTypeConverter();
            var loggerFactory = facility.ReadConfigurationAndCreateLoggerFactory();
        }

        [Test]
        public void Test_DefaultKernel_AddFacility4()
        {
            var facility = new LoggingFacility();
            facility.LogUsing(LoggerImplementation.NLog).WithConfig("LoggingFacility\\NLog.facilities.test.config");
            var kernel = new DefaultKernel();

            var configuration =
                kernel.ConfigurationStore.GetFacilityConfiguration("LoggingFacility\\NLog.facilities.test.config");
            facility.SetKerenlAndConfig(kernel, configuration);
            facility.SetUpTypeConverter();
            var logApi = facility.ReadLoggingApi();
            var factory = facility.CreateProperLoggerFactory(logApi);
        }

        [Test]
        public void Test_DefaultKernel_AddFacility5()
        {
            var facility = new LoggingFacility();
            facility.LogUsing(LoggerImplementation.NLog).WithConfig("LoggingFacility\\NLog.facilities.test.config");
            var kernel = new DefaultKernel();

            var configuration =
                kernel.ConfigurationStore.GetFacilityConfiguration("LoggingFacility\\NLog.facilities.test.config");
            facility.SetKerenlAndConfig(kernel, configuration);
            facility.SetUpTypeConverter();
            var logApi = facility.ReadLoggingApi();
            Assert.AreEqual(logApi, LoggerImplementation.NLog);
            var converter = kernel.GetSubSystem(SubSystemConstants.ConversionManagerKey);
            var loggerFactoryType =
                ((ITypeConverter) converter).PerformConversion<Type>(
                    "Castle.Services.Logging.NLogIntegration.NLogFactory," +
                    "Castle.Core,Version=3.3.0.0, Culture=neutral," +
                    "PublicKeyToken=407dd0808d44fbdc");
            Assert.AreEqual(loggerFactoryType, typeof(NLogFactory));
            var ctorArgs = facility.GetLoggingFactoryArguments(loggerFactoryType);
            Assert.AreEqual(ctorArgs.Length, 1);
            var factory = loggerFactoryType.CreateInstance<ILoggerFactory>(ctorArgs);
        }


        [Test]
        public void Test_DefaultKernel_AddFacility6()
        {
            var facility = new LoggingFacility();
            facility.LogUsing(LoggerImplementation.NLog).WithConfig("LoggingFacility\\NLog.facilities.test.config");
            var kernel = new DefaultKernel();

            var configuration =
                kernel.ConfigurationStore.GetFacilityConfiguration("LoggingFacility\\NLog.facilities.test.config");
            facility.SetKerenlAndConfig(kernel, configuration);
            facility.SetUpTypeConverter();
            Assert.IsFalse(facility.IsConfiguredExternally());
            var configFile = facility.GetConfigFile();
            Assert.AreEqual(configFile, "LoggingFacility\\NLog.facilities.test.config");
            var ctorArgs = facility.GetLoggingFactoryArguments(typeof(NLogFactory));
            Assert.AreEqual(ctorArgs.Length, 1);
            Assert.AreEqual(ctorArgs[0], "LoggingFacility\\NLog.facilities.test.config");
            ReflectionUtil.EnsureIsAssignable<ILoggerFactory>(typeof(NLogFactory));
            var factory = ReflectionUtil.Instantiate<ILoggerFactory>(typeof(NLogFactory), ctorArgs ?? new object[0]);
        }

        [Test]
        public void Test_DefaultKernel_AddFacility7()
        {
            var facility = new LoggingFacility();
            facility.LogUsing(LoggerImplementation.NLog).WithConfig("LoggingFacility\\NLog.facilities.test.config");
            var kernel = new DefaultKernel();

            var configuration =
                kernel.ConfigurationStore.GetFacilityConfiguration("LoggingFacility\\NLog.facilities.test.config");
            facility.SetKerenlAndConfig(kernel, configuration);
            facility.SetUpTypeConverter();
            Assert.IsFalse(facility.IsConfiguredExternally());
            var configFile = facility.GetConfigFile();
            Assert.AreEqual(configFile, "LoggingFacility\\NLog.facilities.test.config");
            var ctorArgs = facility.GetLoggingFactoryArguments(typeof(NLogFactory));
            Assert.AreEqual(ctorArgs.Length, 1);
            Assert.AreEqual(ctorArgs[0], "LoggingFacility\\NLog.facilities.test.config");
            ReflectionUtil.EnsureIsAssignable<ILoggerFactory>(typeof(NLogFactory));
            //var factory = ReflectionUtil.Instantiate<ILoggerFactory>(typeof(NLogFactory), ctorArgs ?? new object[0]);
            //var types = ctorArgs.ConvertAll(a => a?.GetType() ?? typeof(object));
            //Assert.AreEqual(types.Length, 1);
            //Assert.AreEqual(types[0], typeof(string));
            var constructor = (typeof(NLogFactory)).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, 
                new [] {typeof(string)}, null);
            Assert.IsNotNull(constructor);
            var factory = (ILoggerFactory)constructor.Instantiate(ctorArgs);
        }

        [Test]
        public void Test_DefaultKernel_AddFacility8()
        {
            var facility = new LoggingFacility();
            facility.LogUsing(LoggerImplementation.NLog).WithConfig("LoggingFacility\\NLog.facilities.test.config");
            var kernel = new DefaultKernel();

            var configuration =
                kernel.ConfigurationStore.GetFacilityConfiguration("LoggingFacility\\NLog.facilities.test.config");
            facility.SetKerenlAndConfig(kernel, configuration);
            facility.SetUpTypeConverter();
            Assert.IsFalse(facility.IsConfiguredExternally());
            var configFile = facility.GetConfigFile();
            Assert.AreEqual(configFile, "LoggingFacility\\NLog.facilities.test.config");
            var ctorArgs = facility.GetLoggingFactoryArguments(typeof(NLogFactory));
            Assert.AreEqual(ctorArgs.Length, 1);
            Assert.AreEqual(ctorArgs[0], "LoggingFacility\\NLog.facilities.test.config");
            ReflectionUtil.EnsureIsAssignable<ILoggerFactory>(typeof(NLogFactory));
            var constructor = (typeof(NLogFactory)).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null,
                new[] { typeof(string) }, null);
            Assert.IsNotNull(constructor);
            //var factory = (ILoggerFactory)constructor.Instantiate(ctorArgs);
            var factories = new ConcurrentDictionary<ConstructorInfo, Func<object[], object>>();
            var factory = BuildFactory(constructor);
            var result = factory.Invoke(ctorArgs);
        }

        static Func<object[], object> BuildFactory(ConstructorInfo ctor)
        {
            var parameterInfos = ctor.GetParameters();
            var parameterExpressions = new Expression[parameterInfos.Length];
            var argument = Expression.Parameter(typeof(object[]), "parameters");
            for (var i = 0; i < parameterExpressions.Length; i++)
            {
                parameterExpressions[i] = Expression.Convert(
                    Expression.ArrayIndex(argument, Expression.Constant(i, typeof(int))),
                    parameterInfos[i].ParameterType.IsByRef ? parameterInfos[i].ParameterType.GetElementType() : parameterInfos[i].ParameterType);
            }
            return Expression.Lambda<Func<object[], object>>(
                Expression.New(ctor, parameterExpressions),
                new[] { argument }).Compile();
        }
    }
}
