using System.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Castle.MicroKernel.Registration;

namespace Abp.EntityFramework.Dependency
{
    /// <summary>
    /// Registers classes derived from AbpDbContext with configurations.
    /// </summary>
    public class EntityFrameworkConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<AbpDbContext>()
                    .WithServiceSelf()
                    .LifestyleTransient()
                    .Configure(c => c.DynamicParameters(
                        (kernel, dynamicParams) =>
                        {
                            var connectionString = context.IocManager.GetNameOrConnectionStringOrNull();
                            if (!string.IsNullOrWhiteSpace(connectionString))
                            {
                                dynamicParams["nameOrConnectionString"] = connectionString;
                            }
                        })));
        }

    }
}