using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MongoDb.Configuration;
using MongoDB.Driver;

namespace Abp.MongoDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MongoDB.
    /// </summary>
    public class MongoDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Gets a reference to MongoDB Database.
        /// </summary>
        public MongoDatabase Database { get; private set; }

        private readonly IAbpMongoDbModuleConfiguration _configuration;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MongoDbUnitOfWork(IAbpMongoDbModuleConfiguration configuration, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            _configuration = configuration;
        }

        protected override void BeginUow()
        {
            Database = new MongoClient(_configuration.ConnectionString)
                .GetServer()
                .GetDatabase(_configuration.DatatabaseName);
        }

        public override void SaveChanges()
        {

        }

        public override async Task SaveChangesAsync()
        {
            await Task.Run(() => { SaveChanges(); });
        }

        protected override void CompleteUow()
        {

        }

        protected override async Task CompleteUowAsync()
        {
            await Task.Run(() => { CompleteUow(); });
        }

        protected override void DisposeUow()
        {

        }
    }
}