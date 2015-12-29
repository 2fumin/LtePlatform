using System;
using Castle.Core.Configuration;
using Castle.Core.Logging;
using Castle.MicroKernel;

namespace Castle.Facilities.Logging.Tests
{
    class MyLoggingFacility : LoggingFacility
    {
        public MyLoggingFacility(IKernel kernel, IConfiguration facilityConfig)
        {
            this.kernel = kernel;
            this.facilityConfig = facilityConfig;
        }

        public IKernel MyKernel => kernel;

        public static string ExtendedLog4NetLoggerFactoryTypeName =>
            "Castle.Services.Logging.Log4netIntegration.ExtendedLog4netFactory," +
            "Castle.Core,Version=3.3.0.0, Culture=neutral," +
            "PublicKeyToken=407dd0808d44fbdc";

        public void MyInit()
        {
            Init();
        }

        public ILoggerFactory CreateLoggerFactory()
        {
            return ReadConfigurationAndCreateLoggerFactory();
        }

        public void SetupConverter()
        {
            SetUpTypeConverter();
        }

        public LoggerImplementation ReadApi()
        {
            return ReadLoggingApi();
        }

        public ILoggerFactory CreateLoggerFactory(LoggerImplementation loggerApi)
        {
            return CreateProperLoggerFactory(loggerApi);
        }

        public Type GetFactoryType(LoggerImplementation loggerApi)
        {
            return GetLoggingFactoryType(loggerApi);
        }
    }
}