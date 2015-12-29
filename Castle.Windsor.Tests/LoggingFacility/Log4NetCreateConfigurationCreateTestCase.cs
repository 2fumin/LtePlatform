using System;
using Castle.MicroKernel;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.MicroKernel.SubSystems.Conversion;
using Castle.Windsor;
using NUnit.Framework;

namespace Castle.Facilities.Logging.Tests
{
    [TestFixture]
    public class Log4NetCreateConfigurationCreateTestCase : BaseTest
    {
        [Test]
        public void Test()
        {
            var container = CreateConfiguredContainer(LoggerImplementation.ExtendedLog4net);
            Assert.IsNotNull(container);
        }

        [Test]
        public void Test_CreateContainer()
        {
            var container = CreateConfiguredContainer(LoggerImplementation.ExtendedLog4net, string.Empty);
            Assert.IsNotNull(container);
        }

        [Test]
        public void Test_GetConfigFile()
        {
            IWindsorContainer container = new WindsorContainer(new DefaultConfigurationStore());
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Assert.IsNotNull(configFile);
        }

        [Test]
        public void Test_GetConfigFile_AddFacility()
        {
            IWindsorContainer container = new WindsorContainer(new DefaultConfigurationStore());
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile));

            Assert.IsNotNull(container);
        }

        [Test]
        public void Test_Kernal()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            kernel.AddFacility<LoggingFacility>(
                (f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile)));
            Assert.IsNotNull(kernel);
        }

        [Test]
        public void Test_Facility()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new LoggingFacility();
            onCreate.Invoke(facility);
            Assert.IsNotNull(facility);
        }

        [Test]
        public void Test_Kernerl_AddFacility()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new LoggingFacility();
            onCreate.Invoke(facility);
            kernel.AddFacility(facility);
            Assert.IsNotNull(facility);
        }

        [Test]
        public void Test_Facility_FullName()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Assert.AreEqual(configFile, "LoggingFacility\\log4net.facilities.test.config");
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new LoggingFacility();
            onCreate.Invoke(facility);
            Assert.AreEqual(facility.GetType().FullName, "Castle.Facilities.Logging.LoggingFacility");
        }

        [Test]
        public void Test_Kernel_AddFacility_WithFullName()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new LoggingFacility();
            onCreate.Invoke(facility);
#pragma warning disable 612, 618
            kernel.AddFacility("Castle.Facilities.Logging.LoggingFacility", facility);
#pragma warning restore 612, 618
            Assert.IsNotNull(facility);
        }
        
        [Test]
        public void Test_Kernel_AddFacility_ToFacilities()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new LoggingFacility();
            onCreate.Invoke(facility);
            ((IFacility) facility).Init(kernel, kernel.ConfigurationStore.GetFacilityConfiguration("Castle.Facilities.Logging.LoggingFacility"));
            Assert.IsNotNull(facility);
        }

        [Test]
        public void Test_Kenel_GetFacilityConfiguration()
        {
            var kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new LoggingFacility();
            onCreate.Invoke(facility);
#pragma warning disable 612, 618
            kernel.InsertOneFacility(facility);
#pragma warning restore 612, 618
            var configuration =
                kernel.ConfigurationStore.GetFacilityConfiguration("Castle.Facilities.Logging.LoggingFacility");
            Assert.IsNull(configuration);
        }

        [Test]
        public void Test_SchemeDelimiter()
        {
            Assert.AreEqual(("Castle.Facilities.Logging.LoggingFacility").IndexOf(Uri.SchemeDelimiter, StringComparison.Ordinal), -1);
        }

        [Test]
        public void Test_Kernel_AddFacility_Null()
        {
            IKernel kernel = new DefaultKernel();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new LoggingFacility();
            onCreate.Invoke(facility);
            ((IFacility)facility).Init(kernel, null);
            Assert.IsNotNull(facility);
        }

        [Test]
        public void Test_MyFacility_Init()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new MyLoggingFacility(kernel, null);
            onCreate.Invoke(facility);
            facility.MyInit();
            Assert.IsNotNull(facility);
        }

        [Test]
        public void Test_MyFacility_CreateLoggerFactory()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new MyLoggingFacility(kernel, null);
            onCreate.Invoke(facility);
            facility.SetupConverter();
            var factory = facility.CreateLoggerFactory();
            Assert.IsNotNull(factory);
        }

        [Test]
        public void Test_MyFacility_ReadLoggingApi()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new MyLoggingFacility(kernel, null);
            onCreate.Invoke(facility);
            facility.SetupConverter();
            var api = facility.ReadApi();
            Assert.IsNotNull(api);
            Assert.AreEqual(api, LoggerImplementation.ExtendedLog4net);
        }

        [Test]
        public void Test_MyFacility_CreateLoggerFactory_FromApi()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new MyLoggingFacility(kernel, null);
            onCreate.Invoke(facility);
            facility.SetupConverter();
            var factory = facility.CreateLoggerFactory(LoggerImplementation.ExtendedLog4net);
            Assert.IsNotNull(factory);
        }

        [Test]
        public void Test_MyFacility_GetLoggerFactoryType()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new MyLoggingFacility(kernel, null);
            onCreate.Invoke(facility);
            facility.SetupConverter();
            var type = facility.GetFactoryType(LoggerImplementation.ExtendedLog4net);
            Assert.IsNotNull(type);
        }

        [Test]
        public void Test_MyFacility_SetupConverter()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new MyLoggingFacility(kernel, null);
            onCreate.Invoke(facility);
            var converter =
                facility.MyKernel.GetSubSystem(SubSystemConstants.ConversionManagerKey) as IConversionManager;
            Assert.IsNotNull(converter);
        }

        [Test]
        public void Test_MyFacility_SetupConverter_PerformConversion()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new MyLoggingFacility(kernel, null);
            onCreate.Invoke(facility);
            var converter =
                facility.MyKernel.GetSubSystem(SubSystemConstants.ConversionManagerKey) as IConversionManager;
            Assert.IsNotNull(converter);
            var type= converter.PerformConversion<Type>(MyLoggingFacility.ExtendedLog4NetLoggerFactoryTypeName);
            Assert.IsNotNull(type);
        }

        [Test]
        public void Test_MyFacility_Converter_PerformConversion()
        {
            IKernel kernel = new DefaultKernel();
            kernel.ConfigurationStore = new DefaultConfigurationStore();
            var configFile = GetConfigFile(LoggerImplementation.ExtendedLog4net);
            Action<LoggingFacility> onCreate = f => f.LogUsing(LoggerImplementation.ExtendedLog4net).WithConfig(configFile);
            var facility = new MyLoggingFacility(kernel, null);
            onCreate.Invoke(facility);
            var converter =
                facility.MyKernel.GetSubSystem(SubSystemConstants.ConversionManagerKey) as IConversionManager;
            Assert.IsNotNull(converter);
            var type =
                (Type)
                    converter.PerformConversion(MyLoggingFacility.ExtendedLog4NetLoggerFactoryTypeName, typeof (Type));
            Assert.IsNotNull(type);
        }
    }
}