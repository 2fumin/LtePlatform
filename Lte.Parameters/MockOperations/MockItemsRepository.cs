using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Moq;

namespace Lte.Parameters.MockOperations
{
    public static class MockItemsRepository
    {
        public static void SynchronizeValues<T, TRepository>(this Mock<TRepository> repository)
            where T : Entity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Count()).Returns(
                repository.Object.GetAll().Count());
            repository.Setup(x => x.GetAllList()).Returns(
                repository.Object.GetAll().ToList());
        }

        public static void MockQueryItems<T, TRepository>(this Mock<TRepository> repository,
            IQueryable<T> items)
            where T : Entity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.GetAll()).Returns(items);
            repository.SynchronizeValues<T, TRepository>();
        }

        public static void MockRepositorySaveItems<T, TRepository>(
            this Mock<TRepository> repository, IEnumerable<T> items)
            where T : Entity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Insert(It.IsAny<T>())).Callback<T>(
                e =>
                {
                    repository.Setup(x => x.GetAll()).Returns(
                        items.Concat(new List<T> { e }).AsQueryable());
                    SynchronizeValues<T, TRepository>(repository);
                });
        }

        public static void MockRepositorySaveItems<T, TRepository>(
            this Mock<TRepository> repository)
            where T : Entity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Insert(It.IsAny<T>())).Callback<T>(
                e =>
                {
                    IEnumerable<T> btss = repository.Object.GetAll();
                    repository.Setup(x => x.GetAll()).Returns(
                        btss.Concat(new List<T> { e }).AsQueryable());
                    SynchronizeValues<T, TRepository>(repository);
                });
            repository.Setup(x => x.InsertAsync(It.IsAny<T>())).Callback<T>(e => repository.Object.Insert(e));
        }

        public static void MockRepositoryDeleteItems<T, TRepository>(
            this Mock<TRepository> repository, IEnumerable<T> items)
            where T : Entity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Delete(It.Is<T>(e => e != null
                && items.FirstOrDefault(y => y == e) != null))
                ).Callback<T>(
                e =>
                {
                    repository.Setup(x => x.GetAll()).Returns(
                        items.Except(new List<T> { e }).AsQueryable());
                    SynchronizeValues<T, TRepository>(repository);
                });
        }

        public static void MockRepositoryDeleteItems<T, TRepository>(
            this Mock<TRepository> repository)
            where T : Entity
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
                        SynchronizeValues<T, TRepository>(repository);
                    });
            }
        }
    }
}
