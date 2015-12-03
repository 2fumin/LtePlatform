using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Moq;

namespace Lte.Parameters.MockOperations
{
    public static class MockItemsAudited
    {
        public static void SynchronizeAuditedValues<T, TRepository>(this Mock<TRepository> repository)
            where T : AuditedEntity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Count()).Returns(
                repository.Object.GetAll().Count());
            repository.Setup(x => x.GetAllList()).Returns(
                repository.Object.GetAll().ToList());
        }

        public static void MockAuditedItems<T, TRepository>(this Mock<TRepository> repository,
            IQueryable<T> items)
            where T : AuditedEntity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.GetAll()).Returns(items);
            repository.SynchronizeAuditedValues<T, TRepository>();
        }

        public static void MockAuditedSaveItems<T, TRepository>(
            this Mock<TRepository> repository, IEnumerable<T> items)
            where T : AuditedEntity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Insert(It.IsAny<T>())).Callback<T>(
                e =>
                {
                    repository.Setup(x => x.GetAll()).Returns(
                        items.Concat(new List<T> { e }).AsQueryable());
                    SynchronizeAuditedValues<T, TRepository>(repository);
                });
        }

        public static void MockAuditedSaveItems<T, TRepository>(
            this Mock<TRepository> repository)
            where T : AuditedEntity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Insert(It.IsAny<T>())).Callback<T>(
                e =>
                {
                    IEnumerable<T> btss = repository.Object.GetAll();
                    repository.Setup(x => x.GetAll()).Returns(
                        btss.Concat(new List<T> { e }).AsQueryable());
                    SynchronizeAuditedValues<T, TRepository>(repository);
                });
        }

        public static void MockAuditedDeleteItems<T, TRepository>(
            this Mock<TRepository> repository, IEnumerable<T> items)
            where T : AuditedEntity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Delete(It.Is<T>(e => e != null
                && items.FirstOrDefault(y => y == e) != null))
                ).Callback<T>(
                e =>
                {
                    repository.Setup(x => x.GetAll()).Returns(
                        items.Except(new List<T> { e }).AsQueryable());
                    SynchronizeAuditedValues<T, TRepository>(repository);
                });
        }

        public static void MockAuditedDeleteItems<T, TRepository>(
            this Mock<TRepository> repository)
            where T : AuditedEntity
            where TRepository : class, IRepository<T>
        {
            if (repository.Object != null)
            {
                IEnumerable<T> items = repository.Object.GetAll();
                repository.Setup(x => x.Delete(It.Is<T>(e => e != null
                    && items.FirstOrDefault(y => y == e) != null))
                    ).Callback<T>(
                    e =>
                    {
                        repository.Setup(x => x.GetAll()).Returns(
                            items.Except(new List<T> { e }).AsQueryable());
                        SynchronizeAuditedValues<T, TRepository>(repository);
                    });
            }
        }
    }
}
